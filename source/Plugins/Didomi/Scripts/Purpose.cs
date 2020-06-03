using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IO.Didomi.SDK
{
    [Serializable]
    public class Purpose
    {
        private string id;

        private string iabId;

        private string name;

        private string description;

        [IgnoreDataMemberAttribute]
        private  bool custom = false;

        [IgnoreDataMemberAttribute]
        private  bool essential = false;

        [IgnoreDataMemberAttribute]
        private  PurposeCategory category;

        public Purpose(string id, string iabId, string name, string description) : this(id, iabId, name, description, false)
        {
        }

        public Purpose(string id, string iabId, string name, string description, bool custom)
        {
            this.id = id;
            this.iabId = iabId;
            this.name = name;
            this.description = description;
            this.custom = custom;
        }

        public string GetId()
        {
            return id;
        }

        public string GetIabId()
        {
            return iabId;
        }

        public string GetName()
        {
            return name;
        }

        public string GetDescription()
        {
            return description;
        }

        public bool IsCustom()
        {
            return custom;
        }

        public bool IsEssential()
        {
            return essential;
        }

        public void SetEssential(bool essential)
        {
            this.essential = essential;
        }

        /// <summary>
        /// Getter method for category.
        /// </summary>
        /// <returns></returns>
        public PurposeCategory GetCategory()
        {
            return category;
        }
  
        /// <summary>
        /// Setter method for category.
        /// </summary>
        /// <param name="category"></param>
        public void SetCategory(PurposeCategory category)
        {
            this.category = category;
        }

        /// <summary>
        /// Get IDs from a list of purposes
        /// </summary>
        /// <param name="purposes"></param>
        /// <returns></returns>
        public static HashSet<string> getIds(ICollection<Purpose> purposes)
        {
            HashSet<string> purposeIds = new HashSet<string>();

            foreach (Purpose purpose in purposes)
            {
                purposeIds.Add(purpose.GetId());
            }

            return purposeIds;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            if (!(obj is Purpose)) {
                return false;
            }

            return ((Purpose)obj).GetId().Equals(this.GetId());
        }

        public override int GetHashCode()
        {
            if (id == null)
            {
                return base.GetHashCode();
            }
            else
            {
                return id.GetHashCode();
            }
        }
    }
}