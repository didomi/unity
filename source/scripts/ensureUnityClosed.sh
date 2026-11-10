#!/bin/bash

#--------------------------------------------------------------------
# Make sure no Unity process is using the project before running tests.
# Source this file, then call: ensure_unity_closed "<projectDir>"
#
# - Fails if the Unity Editor (interactive) has the project open:
#   it is never killed automatically, close it yourself.
# - Kills leftover batchmode Unity processes and their helper processes
#   (asset import workers, package manager...) still using the project.
# - Waits for the project lock file (Temp/UnityLockfile) to be released.
#--------------------------------------------------------------------

# PIDs of Unity processes whose command line references the project
_unity_project_pids() {
    local projectDir="$1"
    ps -axo pid=,command= | grep -F "/Unity.app/Contents/" | grep -iF -- "$projectDir" \
        | grep -v grep | awk '{print $1}'
}

# PIDs holding the project lock file
_unity_lock_pids() {
    local lockFile="$1/Temp/UnityLockfile"
    [ -e "$lockFile" ] && lsof -t "$lockFile" 2>/dev/null
}

ensure_unity_closed() {
    local projectDir="$1"
    local pids pid cmd interactive=""

    pids=$( { _unity_project_pids "$projectDir"; _unity_lock_pids "$projectDir"; } | sort -u)
    for pid in $pids; do
        cmd=$(ps -o command= -p "$pid" 2>/dev/null)
        # Main editor without -batchmode = interactive editor opened by the user
        if echo "$cmd" | grep -q "/Unity.app/Contents/MacOS/Unity" \
            && ! echo "$cmd" | grep -qiE -- "-batchmode|AssetImportWorker"; then
            interactive="$interactive $pid"
        fi
    done

    if [ -n "$interactive" ]; then
        echo "The Unity Editor has this project open (pid:$interactive). Close it and run the tests again."
        return 1
    fi

    if [ -n "$pids" ]; then
        echo "Stopping leftover Unity processes using the project (pid: $(echo $pids))..."
        kill $pids 2>/dev/null
        local waited=0
        while [ $waited -lt 20 ]; do
            pids=$( { _unity_project_pids "$projectDir"; _unity_lock_pids "$projectDir"; } | sort -u)
            [ -z "$pids" ] && break
            sleep 1
            waited=$((waited + 1))
        done
        if [ -n "$pids" ]; then
            echo "Force killing Unity processes (pid: $(echo $pids))..."
            kill -9 $pids 2>/dev/null
            sleep 2
        fi
    fi

    pids=$( { _unity_project_pids "$projectDir"; _unity_lock_pids "$projectDir"; } | sort -u)
    if [ -n "$pids" ]; then
        echo "Unity processes are still using the project (pid: $(echo $pids)), aborting."
        return 1
    fi

    return 0
}
