namespace MMAdmin
{
    public static class BadgeCounterService
    {
        private static int _count;
        private static int _secondCount;

        public static int Count
        {
            get => _count;
            private set
            {
                _count = value;
                CountChanged?.Invoke(null, _count);
            }
        }

        public static int SecondCount
        {
            get => _secondCount;
            private set
            {
                _secondCount = value;
                SecondCountChanged?.Invoke(null, _secondCount);
            }
        }

        public static void SetCount(int count) => Count = count;
        public static void SetSecondCount(int count) => SecondCount = count;

        public static event EventHandler<int> CountChanged;
        public static event EventHandler<int> SecondCountChanged;
    }
}
