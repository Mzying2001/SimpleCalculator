namespace SimpleCalculator.Models
{
    class Settings
    {
        public bool RestoreResultsAtStartup { get; set; }

        public Settings()
        {
            RestoreResultsAtStartup = true;
        }
    }
}
