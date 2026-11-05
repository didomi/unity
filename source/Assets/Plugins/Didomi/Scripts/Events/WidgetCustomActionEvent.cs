namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// The user clicked a custom action button in a widget.
    /// Does not change the widget visibility or the user consent.
    /// </summary>
    public class WidgetCustomActionEvent : Event
    {
        private string widgetId;
        private string layerName;
        private string customActionId;

        public WidgetCustomActionEvent(
            string widgetId,
            string layerName,
            string customActionId
        )
        {
            this.widgetId = widgetId;
            this.layerName = layerName;
            this.customActionId = customActionId;
        }

        /// <summary>
        /// Identifier of the widget, as resolved by the Rules Engine.
        /// </summary>
        /// <returns></returns>
        public string getWidgetId()
        {
            return widgetId;
        }

        /// <summary>
        /// Name of the layer where the custom action button was clicked.
        /// </summary>
        /// <returns></returns>
        public string getLayerName()
        {
            return layerName;
        }

        /// <summary>
        /// Client-defined action identifier, passed through unchanged.
        /// </summary>
        /// <returns></returns>
        public string getCustomActionId()
        {
            return customActionId;
        }
    }
}
