using System.Collections.Generic;

namespace IO.Didomi.SDK
{
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
      
        public CurrentUserStatusTransaction EnableVendor(string vendorId)
        {
            vendorsStatus.Add(vendorId, new CurrentUserStatus.VendorStatus(vendorId, true));
            return this;
        }

        public CurrentUserStatusTransaction EnableVendors(params string[] vendorIds)
        {
            foreach (string vendorId in vendorIds)
            {
                EnableVendor(vendorId);
            }
            return this;
        }

        public CurrentUserStatusTransaction DisableVendor(string vendorId)
        {
            vendorsStatus.Add(vendorId, new CurrentUserStatus.VendorStatus(vendorId, false));
            return this;
        }

        public CurrentUserStatusTransaction DisableVendors(params string[] vendorIds)
        {
            foreach (string vendorId in vendorIds)
            {
                DisableVendor(vendorId);
            }
            return this;
        }

        public CurrentUserStatusTransaction EnablePurpose(string purposeId)
        {
            purposesStatus.Add(purposeId, new CurrentUserStatus.PurposeStatus(purposeId, true));
            return this;
        }

        public CurrentUserStatusTransaction EnablePurposes(params string[] purposeIds)
        {
            foreach (string purposeId in purposeIds)
            {
                EnablePurpose(purposeId);
            }
            return this;
        }

        public CurrentUserStatusTransaction DisablePurpose(string purposeId)
        {
            purposesStatus.Add(purposeId, new CurrentUserStatus.PurposeStatus(purposeId, false));
            return this;
        }

        public CurrentUserStatusTransaction DisablePurposes(params string[] purposeIds)
        {
            foreach (string purposeId in purposeIds)
            {
                DisablePurpose(purposeId);
            }
            return this;
        }

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
