namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on agree to a category on sensitive personal information screen
    /// </summary>
    public class PreferencesClickSPICategoryAgreeEvent : Event
    {
        private string categoryId;

        public PreferencesClickSPICategoryAgreeEvent(
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
