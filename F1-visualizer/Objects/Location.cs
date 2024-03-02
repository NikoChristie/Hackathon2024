using System;
namespace F1_visualizer;

public class Location
{
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }
	public int driver_number { get; set; }
	public DateTime date{ get; set; }
	public int session_key { get; set; }
	public int meeting_key { get; set; }
}
