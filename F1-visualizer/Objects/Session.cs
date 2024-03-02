using System;
namespace F1_visualizer;

public class Session
{
    public string location { get; set; }
    public int country_key { get; set; }
    public string country_code { get; set; }
    public string country_name { get; set; }
    public int circuit_key { get; set; }
    public string circuit_short_name { get; set; }
    public string date_start { get; set; }
    public string date_end { get; set; }
    public string gmt_offset { get; set; }
    public int session_key { get; set; }
    public int meeting_key { get; set; }
    public int year { get; set; }



    public string toString()
    {
        return string.Format("%d - %s", session_key, country_name);
    }
}
