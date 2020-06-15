using System;

namespace IO.Didomi.SDK
{

    /**
     * Purpose category
     */
    [Serializable]
    public class PurposeCategory
    {
        private string purposeId;

        private string icon;

        private string type = "purpose";

        public PurposeCategory(string purposeId, string icon)
        {
            this.purposeId = purposeId;
            this.icon = icon;
        }

        /// <summary>
        /// Getter method for purpose ID.
        /// </summary>
        /// <returns></returns>
        public string GetPurposeId()
        {
            if (purposeId == null)
            {
                purposeId = "";
            }
            return purposeId;
        }
        
        /// <summary>
        /// Getter method for the purpose icon.
        /// </summary>
        /// <returns></returns>
        public string GetIcon()
        {
            if (icon == null)
            {
                icon = "";
            }
            return icon;
        }
        
        /// <summary>
        /// Getter method for the purpose type.
        /// </summary>
        /// <returns></returns>
        public string GetPurposeType()
        {
            return type;
        }
    }
}
