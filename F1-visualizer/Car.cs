using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace F1_visualizer;

public class Car {

    public double X {get; private set;} 
    public double Y {get; private set;} 
    public double Z {get; private set;} 

    public DateTime startTime {get; private set;}
    public double drivingTime {get; set;} = 1000000;

    public const float Width = 25;
    public const float Height = 25;
    public double PrevX {get; private set;} 
    public double PrevY {get; private set;} 
	public double PrevRot {get; private set;} 
	private List<Location> locations;
	public Track track {get; set;} // DO NOT MODIFY


    public Texture2D driverTexture;

    // public List
    public List<(double, double, double)> positions = Car.GetLoop(1000);

    public Car(double x, double y, double z, Texture2D driverTexture) {
		this.X = x;
		this.Y = y;
		this.Z = z;
		PrevRot = 0.0f;
		this.driverTexture = driverTexture;
    }

    private static List<(double, double, double)> GetLoop(int step) {
		List<(double, double, double)> loop = new List<(double, double, double)>();

		const double LOOP_SIZE = 50;

		for(double theta = 0; theta < 2 * Math.PI; theta += (2 * Math.PI) / step) {
			loop.Add((LOOP_SIZE * Math.Cos(theta), LOOP_SIZE * Math.Sin(theta), 0));
		}

		return loop;
    }

	public bool setLocation(List<Location> location) {
		if (location.Count == 0) { return false; }

		startTime = location[0].date;
		locations = location;

		return true;
	}

    public void UpdatePosition() {

		DateTime currentTime = startTime.AddMilliseconds(drivingTime);

		// Gets the current location
		Location loc = null;
		for (int i = 0; i < locations.Count; i++) {
			if (locations[i].date> currentTime) {
				loc = locations[i];
				i = locations.Count;
			}
		}

		if (loc == null) { return; }
		if (!PrevX.Equals(loc.x) && !PrevY.Equals(loc.y)){
			(PrevX, PrevY) = (this.X, this.Y);
		}
		(this.X, this.Y) = track.convertPoint(loc.x, loc.y);
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture) {
		UpdatePosition();
		spriteBatch.Draw(
			texture,
			new Vector2((float)this.X, (float)this.Y),
			null,
			Color.Black,
			(float) getRotation(), // rotation
			new Vector2(texture.Width/2, texture.Height/2),
			new Vector2(0.05f, 0.05f),
			SpriteEffects.None,
			0f
		);
		spriteBatch.Draw(
		    driverTexture,
		    new Vector2((float)this.X, (float)this.Y),
		    null,
		    Color.White,
		     3.14f + (float)getRotation(),
		    new Vector2(0, 0),
		    new Vector2(0.5f, 0.5f),
		    SpriteEffects.None,
		    0f
		);
    }

    public double MinX {
	get {
	    return this.positions.Select(p => p.Item1).Min();
        }
    }

    public double MaxX {
	get {
	    return this.positions.Select(p => p.Item1).Max();
        }
    }

    public double MinY {
	get {
	    return this.positions.Select(p => p.Item2).Min();
        }
    }

    public double MaxY {
	get {
	    return this.positions.Select(p => p.Item2).Max();
        }
    }

	public double getRotation() {
		double rot = PrevRot;
		if (!PrevX.Equals(X) && !PrevY.Equals(Y))
		{
			rot = Math.Atan2(Y - PrevY, X - PrevX);
			PrevRot = rot;
		}
		return rot;

	}
}
