namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on disagree to a category on preferences popup
    /// </summary>
    public class PreferencesClickCategoryDisagreeEvent : Event
    {
        private string categoryId;

        public PreferencesClickCategoryDisagreeEvent(
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
