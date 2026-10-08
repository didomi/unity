namespace IO.Didomi.SDK
{
    /// <summary>
    /// Parameters used to display a widget with <c>Didomi.ShowWidget</c>
    /// </summary>
    public class DidomiWidgetParameters
    {
        /// <summary>
        /// ID of the widget to show. When null, the widget is selected automatically by the Rules Engine.
        /// </summary>
        public string WidgetId { get; }

        /// <summary>
        /// Name of the layer at which the widget should open (e.g. <c>notice</c>, <c>purposes</c> or <c>vendors</c>).
        /// When null, the widget opens at its default layer.
        /// </summary>
        public string LayerName { get; }

        public DidomiWidgetParameters(string widgetId = null, string layerName = null)
        {
            this.WidgetId = widgetId;
            this.LayerName = layerName;
        }
    }
}
