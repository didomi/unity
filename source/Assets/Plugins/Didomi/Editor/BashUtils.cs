public class BashUtils {
    /// <summary>
    /// Execute one or more bash commands contained in a string using a new process.
    /// </summary>
    /// <param name="commands">String with commands to be executed in the bash shell</param>
    public static void Execute(string commands) {
        // Create a new process object for executing the command
        System.Diagnostics.Process process = new System.Diagnostics.Process();
    
        // Configure the process start info to use the bash shell, set the arguments to be the command string to execute and redirect output to file 
        process.StartInfo.FileName = "/bin/bash";
        process.StartInfo.Arguments = $"-c \"{commands}\"";
        
        // Configure the process redirection and encoding
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.EnvironmentVariables["LANG"] = "en_US.UTF-8"; // set LANG to en_US.UTF-8
        
        // Start the process execution
        process.Start();
        
        // Wait for the process to finish and get the error output
        string output = process.StandardError.ReadToEnd();
        
        // Wait for the process to exit
        process.WaitForExit();
        
        // Log the output of the process
        UnityEngine.Debug.Log($"Didomi - Execute bash commands in a Process. Output: {output}");
    }
}
