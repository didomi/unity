using System.Collections.Generic;

namespace IO.Didomi.SDK
{
    /// <summary>
    /// Asynchronously enable and disable purposes and vendors through different methods.
    /// These changes will not be reflected on the user status until the `commit` method is called.
    /// </summary>
    public class CurrentUserStatusTransaction
    {
        private Dictionary<string, CurrentUserStatus.VendorStatus> vendorsStatus = new Dictionary<string, CurrentUserStatus.VendorStatus>();
        private Dictionary<string, CurrentUserStatus.PurposeStatus> purposesStatus = new Dictionary<string, CurrentUserStatus.PurposeStatus>();

        public delegate bool InternalCommit(
             ISet<string> enabledVendors,
             ISet<string> disabledVendors,
             ISet<string> enabledPurposes,
             ISet<string> disabledPurposes
        );
        private InternalCommit CommitAction;

        public CurrentUserStatusTransaction(InternalCommit commitAction)
        {
            this.CommitAction = commitAction;
        }

        /// <summary>
        /// Enable the provided purpose
        /// </summary>
        /// <param name="vendorId">The vendor id to enable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction EnableVendor(string vendorId)
        {
            vendorsStatus.Add(vendorId, new CurrentUserStatus.VendorStatus(vendorId, true));
            return this;
        }

        /// <summary>
        /// Enable the provided vendors
        /// </summary>
        /// <param name="vendorIds">The list of vendor ids to enable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction EnableVendors(params string[] vendorIds)
        {
            foreach (string vendorId in vendorIds)
            {
                EnableVendor(vendorId);
            }
            return this;
        }

        /// <summary>
        /// Disable the provided vendor
        /// </summary>
        /// <param name="vendorId">The vendor id to disable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction DisableVendor(string vendorId)
        {
            vendorsStatus.Add(vendorId, new CurrentUserStatus.VendorStatus(vendorId, false));
            return this;
        }

        /// <summary>
        /// Disable the provided vendors
        /// </summary>
        /// <param name="vendorIds">The list of vendor ids to disable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction DisableVendors(params string[] vendorIds)
        {
            foreach (string vendorId in vendorIds)
            {
                DisableVendor(vendorId);
            }
            return this;
        }

        /// <summary>
        /// Enable the provided purpose
        /// </summary>
        /// <param name="purposeId">The purpose id to enable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction EnablePurpose(string purposeId)
        {
            purposesStatus.Add(purposeId, new CurrentUserStatus.PurposeStatus(purposeId, true));
            return this;
        }

        /// <summary>
        /// Enable the provided purposes
        /// </summary>
        /// <param name="purposeIds">The list of purpose ids to enable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction EnablePurposes(params string[] purposeIds)
        {
            foreach (string purposeId in purposeIds)
            {
                EnablePurpose(purposeId);
            }
            return this;
        }

        /// <summary>
        /// Disable the provided purpose
        /// </summary>
        /// <param name="purposeId">The purpose id to disable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction DisablePurpose(string purposeId)
        {
            purposesStatus.Add(purposeId, new CurrentUserStatus.PurposeStatus(purposeId, false));
            return this;
        }

        /// <summary>
        /// Disable the provided purposes
        /// </summary>
        /// <param name="purposeIds">The list of purpose ids to disable</param>
        /// <returns>Self for chain calls</returns>
        public CurrentUserStatusTransaction DisablePurposes(params string[] purposeIds)
        {
            foreach (string purposeId in purposeIds)
            {
                DisablePurpose(purposeId);
            }
            return this;
        }

        /// <summary>
        /// Update the user status with the registered changes. If a value was not modified in the transaction, its value will still the same.
        /// </summary>
        /// <returns>True if user status was updated, false otherwise</returns>
        public bool Commit()
        {
            ISet<string> enabledVendors = new HashSet<string>();
            ISet<string> disabledVendors = new HashSet<string>();
            foreach (CurrentUserStatus.VendorStatus status in vendorsStatus.Values)
            {
                if (status.Enabled)
                {
                    enabledVendors.Add(status.Id);
                } else
                {
                    disabledVendors.Add(status.Id);
                }
            }
            ISet<string> enabledPurposes = new HashSet<string>();
            ISet<string> disabledPurposes = new HashSet<string>();
            foreach (CurrentUserStatus.PurposeStatus status in purposesStatus.Values)
            {
                if (status.Enabled)
                {
                    enabledPurposes.Add(status.Id);
                }
                else
                {
                    disabledPurposes.Add(status.Id);
                }
            }

            return CommitAction.Invoke(enabledVendors, disabledVendors, enabledPurposes, disabledPurposes);
        }
    }
}
