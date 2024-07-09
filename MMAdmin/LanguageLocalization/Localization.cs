namespace MMAdmin.LanguageLocalization
{
    public static class Localization
    {
        public static ILocalizationService Current { get; private set; } = new LocalizationService();

        public static void Init(ILocalizationService service)
        {
            Current = service;
        }
    }
}
