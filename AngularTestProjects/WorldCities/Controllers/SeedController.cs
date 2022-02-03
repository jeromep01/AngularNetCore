using CaseStudies.Core.Geography;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using WorldCities.Data;

namespace WorldCities.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;
        public SeedController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            var path = Path.Combine(env.ContentRootPath, string.Format("Data/Source/worldcities.xslx"));

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var ep = new ExcelPackage(stream))
                {
                    var ws = ep.Workbook.Worksheets[0];

                    var nbCountries = 0;
                    var nbCities = 0;

                    var lstCountries = context.Countries.ToList();
                    for (int nrow = 2; nrow <= ws.Dimension.End.Row; nrow++)
                    {
                        var row = ws.Cells[nrow, 1, nrow, ws.Dimension.End.Column];

                        var name = row[nrow, 5].GetValue<string>();

                        if (!lstCountries.Any(x => x.Name == name))
                        {
                            var country = new Country();

                            country.Name = name;
                            country.ISO2 = row[nrow, 6].GetValue<string>();
                            country.ISO3 = row[nrow, 7].GetValue<string>();

                            context.Countries.Add(country);
                            await context.SaveChangesAsync();

                            lstCountries.Add(country);

                            nbCountries++;
                        }
                    }

                    for (int nrow = 2; nrow <= ws.Dimension.End.Row; nrow++)
                    {
                        var row = ws.Cells[nrow, 1, nrow, ws.Dimension.End.Column];

                        var name = row[nrow, 1].GetValue<string>();

                        if (!lstCountries.Any(x => x.Name == name))
                        {
                            var city = new City();

                            city.Name = name;
                            city.Name_ASCII = row[nrow, 2].GetValue<string>();
                            city.Latitude = row[nrow, 3].GetValue<decimal>();
                            city.Longitude = row[nrow, 4].GetValue<decimal>();

                            var countryName = row[nrow, 5].GetValue<string>();
                            var country = lstCountries.SingleOrDefault(x => x.Name == countryName);
                            city.CountryId = country.Id;

                            context.Cities.Add(city);
                            await context.SaveChangesAsync();

                            nbCities++;
                        }
                    }

                    return new JsonResult(new
                    {
                        Cities = nbCities,
                        Countries = nbCountries
                    });
                }
            }
        }
    }
}
