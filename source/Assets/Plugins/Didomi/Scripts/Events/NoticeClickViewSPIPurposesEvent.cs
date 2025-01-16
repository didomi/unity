using System;

namespace IO.Didomi.SDK.Events
{
    /// <summary>
    /// Click on "Limit the use of my Sensitive Personal Information" on notice
    /// </summary>
    [ObsoleteAttribute("SPI purposes are now displayed in preferences screen. Use NoticeClickMoreInfo instead.")]
    public class NoticeClickViewSPIPurposesEvent : Event
    {
    }
}
