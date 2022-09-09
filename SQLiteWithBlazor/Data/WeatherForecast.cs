    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public bool Validate()
        {
            if (this == null)
                return false;

            if (string.IsNullOrEmpty(this.TemperatureC.ToString()) || string.IsNullOrEmpty(this.Summary) || string.IsNullOrEmpty(this.Date.ToString()))
                return false;

            return true;
        }
    }
