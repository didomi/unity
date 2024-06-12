namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on agree to a category on preferences popup
    /// </summary>
    public class PreferencesClickCategoryAgreeEvent : Event
    {
        private string categoryId;

        public PreferencesClickCategoryAgreeEvent(
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
