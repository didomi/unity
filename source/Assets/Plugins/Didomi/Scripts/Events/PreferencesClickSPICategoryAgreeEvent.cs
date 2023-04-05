namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on agree to a category on sensitive personal information screen
    /// </summary>
    public class PreferencesClickSPICategoryAgreeEvent : Event
    {

        /**
         * ID of the category
         */
        private string categoryId;

        /**
         * Constructor
         *
         * @param categoryId ID of the category
         */
        public PreferencesClickSPICategoryAgreeEvent(
            string categoryId
        )
        {
            this.categoryId = categoryId;
        }

        public string getCategoryId()
        {
            return categoryId;
        }
    }
}
