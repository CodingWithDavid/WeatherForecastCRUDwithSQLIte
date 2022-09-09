using Microsoft.EntityFrameworkCore;

public class WeatherForecastService
{
    private WeatherDbContext dbContext;

    public WeatherForecastService(WeatherDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<WeatherForecast>> GetAsync()
    {
        List<WeatherForecast> result = await dbContext.WeatherForecasts.ToListAsync();
        if(result.Count == 0)
        {
            result = await PopulateDataBase();
        }
        return result;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public async Task<List<WeatherForecast>> PopulateDataBase()
    {
        List<WeatherForecast> result = new();

        DateTime startDate = DateTime.Now;

        for (int i = 1; i < 11; i++)
        {
            WeatherForecast wf = new()
            {
                Id = i,
                Date = startDate.AddDays(i),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            //add to database
            dbContext.WeatherForecasts.Add(wf);
            result.Add(wf);
        }
        await dbContext.SaveChangesAsync();

        return result;
    }

    public async Task<bool> AddForeCast(WeatherForecast inForecast)
    {
        bool result = true;

        if(inForecast != null && inForecast.Id == 0)
        {
            //validate
            if (!inForecast.Validate())
                return false;

            //Save
            try
            {
                dbContext.WeatherForecasts.Add(inForecast);
                if (await dbContext.SaveChangesAsync() > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
        }
        return result;
    }

    public async Task<bool> UpdateForeCast(WeatherForecast inForecast)
    {
        bool result = false;

        if (inForecast.Id > 0)
        {
            //validate
            if (!inForecast.Validate())
                return false;

            //Save
            try
            {
                dbContext.WeatherForecasts.Update(inForecast);
                if( await dbContext.SaveChangesAsync() > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
        }
        return result;
    }

    public async Task<bool> DeleteForecast(WeatherForecast inForecast)
    {
        bool result = false;

        //validate
        if (inForecast != null)
        {
            try
            {
                //delete it
                dbContext.WeatherForecasts.Remove(inForecast);
                if (await dbContext.SaveChangesAsync() > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
        }
        return result;
    }
}
