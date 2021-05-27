namespace SimpleCalculator.Models
{
    class Settings
    {
        public bool RestoreResultsAtStartup { get; set; }

        public WindowStatus MainWindowStatus { get; set; }

        public Settings()
        {
            RestoreResultsAtStartup = true;
            MainWindowStatus = new WindowStatus()
            {
                Left = 200,
                Top = 200,
                Width = 600,
                Height = 450
            };
        }
    }
}
