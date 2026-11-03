namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// The user navigated to another layer of a widget (for instance with a "Learn more" or "View vendors" button).
    /// Not triggered when the widget is closed or when consent is given or refused.
    /// </summary>
    public class WidgetSwitchLayerEvent : Event
    {
        private string widgetId;
        private string layerName;
        private string targetLayerName;

        public WidgetSwitchLayerEvent(
            string widgetId,
            string layerName,
            string targetLayerName
        )
        {
            this.widgetId = widgetId;
            this.layerName = layerName;
            this.targetLayerName = targetLayerName;
        }

        /// <summary>
        /// Identifier of the widget in which the navigation happened.
        /// Null if unknown.
        /// </summary>
        /// <returns></returns>
        public string getWidgetId()
        {
            return widgetId;
        }

        /// <summary>
        /// Name of the layer the user navigated from.
        /// Null if unknown.
        /// </summary>
        /// <returns></returns>
        public string getLayerName()
        {
            return layerName;
        }

        /// <summary>
        /// Name of the layer the user navigated to.
        /// Null if unknown.
        /// </summary>
        /// <returns></returns>
        public string getTargetLayerName()
        {
            return targetLayerName;
        }
    }
}
