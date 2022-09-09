namespace SQLiteWithBlazor.Services
{
    public class AppDataService
    {
        public WeatherForecast? WeatherForecast { get; set; }
        public string  ActionResult { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

    }
}
