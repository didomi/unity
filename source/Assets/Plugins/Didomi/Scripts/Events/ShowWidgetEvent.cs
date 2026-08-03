namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// A widget has been displayed
    /// </summary>
    public class ShowWidgetEvent : Event
    {
        private string widgetId;
        private string layerName;

        public ShowWidgetEvent(
            string widgetId,
            string layerName
        )
        {
            this.widgetId = widgetId;
            this.layerName = layerName;
        }

        /// <summary>
        /// Identifier of the widget that was displayed, as resolved by the Rules Engine.
        /// Null if unknown.
        /// </summary>
        /// <returns></returns>
        public string getWidgetId()
        {
            return widgetId;
        }

        /// <summary>
        /// Name of the layer at which the widget was displayed.
        /// Null if unknown or default.
        /// </summary>
        /// <returns></returns>
        public string getLayerName()
        {
            return layerName;
        }
    }
}
