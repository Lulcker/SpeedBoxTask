using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restub;
using System.Net;
using System.Xml;
using TaskApiCdek.Data;
using TaskApiCdek.ViewModels;

namespace TaskApiCdek.Repositories
{
    public class CdekClient : ICdekClient
    {

        public List<City> GetCity()
        {
            List<City> cityList = new List<City>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("http://integration.cdek.ru/v1/location/cities");
            XmlElement? xRoot = xDoc.DocumentElement;


            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    City city = new City();

                    XmlNode? cityName = xnode.Attributes.GetNamedItem("cityName");
                    city.CityName = cityName?.Value;

                    XmlNode? cityCode = xnode.Attributes.GetNamedItem("cityCode");
                    city.CityCode = Convert.ToInt32(cityCode?.Value);

                    XmlNode? cityUuid = xnode.Attributes.GetNamedItem("cityUuid");
                    city.CityUuid = cityUuid?.Value;

                    XmlNode? country = xnode.Attributes.GetNamedItem("country");
                    city.Country = country?.Value;

                    XmlNode? countryCode = xnode.Attributes.GetNamedItem("countryCode");
                    city.CountryCode = countryCode?.Value;

                    XmlNode? region = xnode.Attributes.GetNamedItem("region");
                    city.Region = region?.Value;

                    XmlNode? regionCode = xnode.Attributes.GetNamedItem("regionCode");
                    city.RegionCode = Convert.ToInt32(regionCode?.Value);

                    XmlNode? fiasGuid = xnode.Attributes.GetNamedItem("fiasGuid");
                    city.FiasGuid = fiasGuid?.Value;

                    cityList.Add(city);
                }
            }
            return cityList;
        }

        public double CalculateTariff(IndexViewModel model)
        {
            var FromLocation = GetCity().FirstOrDefault(x => x.FiasGuid == model.FiasFrom);
            if (FromLocation == null) 
            { 
                throw new Exception("ФИАС номера отправителя нет в базе данных"); 
            }
            var FromCode = FromLocation.CityCode;

            var ToLocation = GetCity().FirstOrDefault(x => x.FiasGuid == model.FiasTo);
            if (ToLocation == null)
            {
                throw new Exception("ФИАС номера получателя нет в базе данных");
            }
            var ToCode = ToLocation.CityCode;
            var t = new TariffRequest
            {
                Version = "1.0",
                DateExecute = DateTime.Now.ToString("yyyy-MM-dd"),
                SenderCityId = FromCode,
                ReceiverCityId = ToCode,
                TariffId = "121",
                Packages =
                {
                    new PackageSize
                    {
                        Weight = Math.Round(model.Weight / 1000.0 , 1),
                        Length = (model.Length / 10),
                        Width = (model.Width / 10),
                        Height = (model.Height / 10)
                    }
                }
            };
           
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.cdek.ru/calculator/calculate_price_by_json.php");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(t);
                Console.WriteLine(json);
                streamWriter.Write(json);
            }
            double price;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JObject json = JObject.Parse(result);
                var ans = json["result"]["price"];
                price = (double)ans;
            }

            return price;
        }
    }
}
