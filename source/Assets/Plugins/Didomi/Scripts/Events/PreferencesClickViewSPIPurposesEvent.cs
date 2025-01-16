using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on "Limit the use of my Sensitive Personal Information" on preferences screen
    /// </summary>
    [ObsoleteAttribute("SPI purposes are now displayed in preferences screen.")]
    public class PreferencesClickViewSPIPurposesEvent : Event
    {
    }
}
