namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on agree to a category on preferences popup
    /// </summary>
    public class PreferencesClickCategoryAgreeEvent : Event
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
        public PreferencesClickCategoryAgreeEvent(
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
