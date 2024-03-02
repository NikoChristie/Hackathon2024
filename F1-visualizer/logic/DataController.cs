
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
namespace F1_visualizer;

public class DataController {
    public string ProcessCall(string endpoint, string[] values) {
        string formattedRequest = ApiCaller.GenerateRequest(endpoint, values);
        string result = ApiCaller.CallApi(formattedRequest);
        return result;
    }

    public List<Session> GetSessionsList() {
        string fileName = string.Format("..{0}F1-visualizer{0}logic{0}2023race.json", Path.DirectorySeparatorChar);
        string jsonString = File.ReadAllText(fileName);

        List<Session> sessions = JsonSerializer.Deserialize<List<Session>>(jsonString);

        return sessions;
    }

    public List<Driver> GetSessionDrivers(string sessionId) {
        // DataController da = new DataController();
        string[] arr = { sessionId };
        string res = ProcessCall("driversSessionKeyOnly", arr);

        List<Driver> drivers = JsonSerializer.Deserialize<List<Driver>>(res);

        return drivers;
    }


}