namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on disagree to a category on preferences popup
    /// </summary>
    public class PreferencesClickCategoryDisagreeEvent : Event
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
        public PreferencesClickCategoryDisagreeEvent(
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
