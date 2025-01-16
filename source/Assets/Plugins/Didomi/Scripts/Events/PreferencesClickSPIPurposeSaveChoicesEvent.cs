using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on save on the sensitive personal information screen
    /// </summary>
    [ObsoleteAttribute("SPI purposes are now displayed in preferences screen. Use PreferencesClickSaveChoicesEvent instead.")]
    public class PreferencesClickSPIPurposeSaveChoicesEvent : Event
    {
    }
}
