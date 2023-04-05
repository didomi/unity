namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on disagree to a category on sensitive personal information screen
    /// </summary>
    public class PreferencesClickSPICategoryDisagreeEvent : Event
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
        public PreferencesClickSPICategoryDisagreeEvent(
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
