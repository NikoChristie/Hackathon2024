using System;
using System.Collections.Generic;
namespace F1_visualizer;

public class Track
{
	private List<Location> filteredLoc;
	public float maxX {get; private set;}
	public float maxY {get; private set;}
	public float minX {get; private set;}
	public float minY {get; private set;}
	public int centerX {get; private set;}
	public int centerY {get; private set;}

	public Track(List<Location> location, int screenHeight, int screenWidth)
	{
		this.centerY = screenHeight / 2;
		this.centerX = screenWidth / 2;
		maxX = 0; 
		maxY = 0; 
		minX = 0; 
		minY = 0;
		filteredLoc = new List<Location>();
		filterTrack(location);
        convertCoords();
	}

	public void filterTrack(List<Location> location)
	{
		foreach (var loc in location)
		{
			if (loc.x != 0 || loc.y != 0)
			{
				maxX = findMax(maxX, loc.x);
				maxY = findMax(maxY, loc.y);
				minX = findMin(minX, loc.x);
				minY = findMin(minY, loc.y);	

				filteredLoc.Add(loc);
			}
		}
	}

	public List<Location> getFilteredLoc() 
	{
		return filteredLoc;
	}

	private float findMax(float curr, float newVal)
	{
		return curr > newVal ? curr : newVal;
	}

    private float findMin(float curr, float newVal)
    {
        return curr < newVal ? curr : newVal;
    }

	public (float, float) convertPoint(float x, float y) {
		// x = centerX / 2 + (float)((x - minX) / (maxX - minX)) * centerX;
		// y = centerY / 2 + (float)((y - minY) / (maxY - minY)) * centerY;
		// return (x + centerX, y + centerY);
		return (x, y);
	}

	private void convertCoords() 
	{
		foreach(var loc in filteredLoc)
		{
			loc.x = centerX / 2 + (float)((loc.x - minX) / (maxX - minX)) * centerX;
			loc.y = centerY / 2 + (float)((loc.y - minY) / (maxY - minY)) * centerY;
		}
	}
}
