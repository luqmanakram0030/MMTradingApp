using MMAdmin.Utility;
using System.Globalization;

namespace MMAdmin.LanguageLocalization
{
    public class LocalizationService : ILocalizationService
    {
        CultureInfo _currentCultureInfo = CultureInfo.InstalledUICulture;
        public CultureInfo CurrentCultureInfo
        {
            get
            {
                return _currentCultureInfo;
            }
            set
            {
                _currentCultureInfo = value;
                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;
                OnCultureChanged?.Invoke(value);
            }
        }

        public CultureInfo CurrentNeutralCultureInfo
        {
            get
            {
                return CurrentCultureInfo.IsNeutralCulture ? CurrentCultureInfo :
                    CurrentCultureInfo.Parent;
            }
        }

        public bool IsRightToLeft => CurrentCultureInfo.TextInfo.IsRightToLeft;

        public CultureInfo DeviceCultureInfo => CultureInfo.InstalledUICulture;

        public CultureInfo[] CultureInfoList => CultureInfo.GetCultures(CultureTypes.AllCultures);

        public CultureInfo[] NeutralCultureInfoList => CultureInfo.GetCultures(CultureTypes.NeutralCultures);

        public FlowDirection FlowDirection => IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        public event CultureChanged OnCultureChanged;

        public void EnsureDeviceOrDefaultCulture(string defaultCultureName, params string[] availableCultures)
        {
            try
            {
                string code = CurrentCultureInfo.Name;
                if (availableCultures.Any(x => x == code))
                {
                    return;
                }

                if (CurrentCultureInfo.IsNeutralCulture)
                {
                    string codeToSearch = $"{code}-";
                    string[] cultures = availableCultures.Where(x => x.StartsWith(codeToSearch)).ToArray();
                    if (cultures.Length > 0)
                    {
                        CurrentCultureInfo = GetCultureInfo(cultures[0]);
                        return;
                    }
                }

                string neutralCode = CurrentCultureInfo.Parent.Name;

                string[] culturesRelatedToParent = availableCultures.Where(x => x.StartsWith(neutralCode)).ToArray();
                if (culturesRelatedToParent.Length > 0)
                {
                    CurrentCultureInfo = GetCultureInfo(culturesRelatedToParent[0]);
                    return;
                }

                CurrentCultureInfo = GetCultureInfo(defaultCultureName);
            }
            catch (Exception ex)
            {
                Common.DisplayErrorMessage($"System Error: {ex.Message}");
            }
        }

        public CultureInfo GetCultureInfo(string name) { return CultureInfo.GetCultureInfo(name); }
    }
}
