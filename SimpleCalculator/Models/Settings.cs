namespace SimpleCalculator.Models
{
    class Settings
    {
        public bool RestoreResultsAtStartup { get; set; }

        public static Settings DefaultSettings
        {
            get => new Settings()
            {
                RestoreResultsAtStartup = true,
            };
        }
    }
}
