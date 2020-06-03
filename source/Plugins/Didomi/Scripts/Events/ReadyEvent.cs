namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// The SDK is initialized and ready
    /// Warning: This only gets fired once.Use the `Didomi.onReady` function to make sure your callback always gets executed.
    /// </summary>
    public class ReadyEvent : Event
    {
    }
}
