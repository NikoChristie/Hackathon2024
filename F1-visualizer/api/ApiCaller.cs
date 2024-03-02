using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

namespace F1_visualizer;

enum endpointNames
{
    Driver,
}


/*
[
  {
    "driver_number": 1,
    "broadcast_name": "M VERSTAPPEN",
    "full_name": "Max VERSTAPPEN",
    "name_acronym": "VER",
    "team_name": "Red Bull Racing",
    "team_colour": "3671C6",
    "first_name": "Max",
    "last_name": "Verstappen",
    "headshot_url": "https://www.formula1.com/content/dam/fom-website/drivers/M/MAXVER01_Max_Verstappen/maxver01.png.transform/1col/image.png",
    "country_code": "NED",
    "session_key": 9158,
    "meeting_key": 1219
  }
]

*/

public static class ApiCaller {
    static Dictionary<string, string> endpointPaths;
    static readonly String Ham = "drivers?driver_number=44&session_key=9158";
    //Create singleton HttpClient
    static readonly HttpClient client;

    static ApiCaller(){
        endpointPaths  = new Dictionary<string, string>
        {
            { "drivers", "drivers?driver_number={0}&session_key={1}" },
            { "driversSessionKeyOnly", "drivers?session_key={0}" },
            { "location", "location?session_key={0}&driver_number={1}" },
            { "meetings", "meetings?year={0}&country_name={1}" },
            { "sessions", "sessions?country_name={0}&session_name={1}&year={2}}" },
            { "team_radio", "team_radio?session_key={0}&driver_number={1}" }
        };
        client = new HttpClient();
        client.BaseAddress = new Uri("https://api.openf1.org/v1/");
    }

    static async Task<string> GetRequest(String endpoint){
        try{
            using HttpResponseMessage GetResponse = await client.GetAsync(endpoint);

            GetResponse.EnsureSuccessStatusCode();
            string GetBody = await GetResponse.Content.ReadAsStringAsync();

            return GetBody;
        }catch (HttpRequestException e){

            Console.WriteLine("Request failed {0}",e.Message);
            return "something went wrong.";
        }

    }

    public static string GenerateRequest(string endpoint, string[] values){
        string request = endpointPaths[endpoint];
        request = string.Format(request,values);
        
        return request;
    }

    public static string CallApi(string formattedPath)
    {
        var apiCall  = Task.Run(() => ApiCaller.GetRequest(formattedPath));
        apiCall.Wait();
        return apiCall.Result;
    }

    public static string GetDriverUrl(int driverNumber, int sessionKey) {
	string driver = CallApi(GenerateRequest("drivers", new string[] { driverNumber.ToString(),
								   sessionKey.ToString()}));

	Driver xs = JsonSerializer.Deserialize<List<Driver>>(driver)[0];

	return xs.headshot_url;
    }

    public static MemoryStream GetDriverPicture(int driverNumber, int sessionKey) {

	string url = GetDriverUrl(driverNumber, sessionKey);
	byte[] imageData;

	using (var wc = new System.Net.WebClient())
	    imageData = wc.DownloadData(url);

	return new MemoryStream(imageData);
    }

}
