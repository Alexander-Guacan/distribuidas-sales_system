using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace NWindProxyService;

public class Proxy
{
    private readonly string _baseAdress = "http://localhost:5030";

    public async Task<T> SendPost<T, PostData>(string requestURI, PostData data)
    {
        T? result = default;
        using (var client = new HttpClient())
        {
            try
            {
                requestURI = _baseAdress + requestURI;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonData = JsonConvert.SerializeObject(data);
                HttpResponseMessage response = await client.PostAsync(requestURI, new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json"));

                var resultWebAPI = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(resultWebAPI);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
        }

        return result;
    }

    public async Task<T> SendGet<T>(string requestURI)
    {
        T? result = default;

        using (var client = new HttpClient())
        {
            try
            {
                requestURI = _baseAdress + requestURI;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var resultJson = await client.GetStringAsync(requestURI);
                result = JsonConvert.DeserializeObject<T>(resultJson);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
        }

        return result;
    }

    public async Task<T> SendPut<T, PutData>(string requestURI, PutData data)
    {
        T? result = default;
        using (var client = new HttpClient())
        {
            try
            {
                requestURI = _baseAdress + requestURI;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonData = JsonConvert.SerializeObject(data);
                HttpResponseMessage response = await client.PutAsync(requestURI, new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json"));

                var resultWebAPI = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(resultWebAPI);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
        }

        return result;
    }

    public async Task<T> SendDelete<T>(string requestURI)
    {
        T? result = default;

        using (var client = new HttpClient())
        {
            try
            {
                requestURI = _baseAdress + requestURI;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync(requestURI);
                var resultJson = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(resultJson);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
        }

        return result;
    }
}