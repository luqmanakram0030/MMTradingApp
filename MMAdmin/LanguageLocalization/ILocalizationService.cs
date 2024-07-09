using System.Globalization;

namespace MMAdmin.LanguageLocalization
{
    public delegate void CultureChanged(CultureInfo cultureInfo);

    public interface ILocalizationService
    {
        CultureInfo[] CultureInfoList { get; }
        CultureInfo CurrentCultureInfo { get; set; }
        CultureInfo CurrentNeutralCultureInfo { get; }
        CultureInfo DeviceCultureInfo { get; }
        bool IsRightToLeft { get; }
        FlowDirection FlowDirection { get; }
        CultureInfo[] NeutralCultureInfoList { get; }
        CultureInfo GetCultureInfo(string name);
        void EnsureDeviceOrDefaultCulture(string defaultCultureName, params string[] availableCultures);

        event CultureChanged OnCultureChanged;
    }
}
