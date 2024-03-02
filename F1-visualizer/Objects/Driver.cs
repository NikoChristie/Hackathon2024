using System;
namespace F1_visualizer;

public class Driver
{
	public int driver_number { get; set; }
	public int session_key { get; set; }
	public int meeting_key { get; set; }
	public string full_name { get; set; }
	public string headshot_url {get;set;}


	public string toString() {
		return string.Format("%d - %s", driver_number, full_name);
	}
}
