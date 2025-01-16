using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on disagree to a category on sensitive personal information screen
    /// </summary>
    [ObsoleteAttribute("SPI purposes are now treated as other purposes.Use PreferencesClickCategoryDisagreeEvent instead.")]
    public class PreferencesClickSPICategoryDisagreeEvent : Event
    {
        private string categoryId;

        public PreferencesClickSPICategoryDisagreeEvent(
            string categoryId
        )
        {
            this.categoryId = categoryId;
        }

        /// <summary>
        /// ID of the category
        /// </summary>
        /// <returns></returns>
        public string getCategoryId()
        {
            return categoryId;
        }
    }
}
