using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Text.Json;
using System;
using System.IO;
using System;
using System.Collections.Generic;
using System;
namespace F1_visualizer;

public class Game1 : Game
{
    Texture2D ballTexture;
    Texture2D car1;
    Texture2D buttonTexture;
    private SpriteFont titleFont;
    private SpriteFont bodyFont;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    List<Location> filteredLoc;
    List<Car> cars = new List<Car>();

    private string[] driverAndSession;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public Game1(string[] driverAndSession)
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        this.driverAndSession = driverAndSession;
    }

    protected override void Initialize()
    {
        //LoadDriver("9161");
        //LoadDriver("9157");
        //LoadDriver("9173");
	LoadDriver(Console.ReadLine().Trim());

        base.Initialize();
    }


    public void LoadDriver(string sessionKey) {
	for(int i = 1; i < 40; i++) {
	    //cars.Add(new Car(0,0,0));
	    DataController da = new DataController();
	    string[] arr = {sessionKey, i.ToString()};
	    string jsonString = da.ProcessCall("location", arr);




	    List<Location> location = JsonSerializer.Deserialize<List<Location>>(jsonString);

	    if(location.Count > 0) {

		Track track = new Track(
		    location,
		    _graphics.PreferredBackBufferHeight,
		    _graphics.PreferredBackBufferWidth
		);

		var driverStream = ApiCaller.GetDriverPicture(i, 9161);
		Texture2D driverTexture = Texture2D.FromStream(GraphicsDevice, driverStream);

		Car car = new Car(0, 0, 0, driverTexture);

		// Set the track of the car
		car.setLocation(location);
		car.track = track;

		cars.Add(car);

		if(filteredLoc == null) {
		    filteredLoc = track.getFilteredLoc();
		}
	    }
	}
    }

    protected override void LoadContent()
    {
        //titleFont = Content.Load<SpriteFont>("font");
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        buttonTexture = Content.Load<Texture2D>("button");
        // TODO: use this.Content to load your game content here
        ballTexture = Content.Load<Texture2D>("1x1");
        car1 = Content.Load<Texture2D>("car1");
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        // current_time += gameTime.ElapsedGameTime.TotalMilliseconds;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGreen);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        foreach (var loc in filteredLoc)
        {
            _spriteBatch.Draw(
                ballTexture,
                new Vector2(loc.x, loc.y),
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                new Vector2(5, 5),
                SpriteEffects.None,
                0f
            );
        }

        foreach(Car car in cars) {
            double total_ms = gameTime.ElapsedGameTime.TotalMilliseconds;
            car.drivingTime = car.drivingTime + total_ms * 5;
            car.Draw(
                _spriteBatch, 
                car1
            );

        }
	
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    protected void DrawTrack(){
        foreach (var loc in filteredLoc)
        {
            _spriteBatch.Draw(
                ballTexture,
                new Vector2(loc.x, loc.y),
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                new Vector2(5, 5),
                SpriteEffects.None,
                0f
            );
        }
    }
    protected void DrawSplashPage()
    {
        Vector2 fontSize;

        int buttonStartX = 50;
        int buttonStartY = 50;
        int buttonBuffer = 20;
        int buttonWidth = 300;
        int buttonHeight = 50;
        //Draw session
        int buttonIndex = 0;
        int textOffsetY = buttonHeight/2;
        int textOffsetX = buttonWidth/2;
        //fontSize =  titleFont.MeasureString("Session Selection");

        _spriteBatch.Draw(buttonTexture, new Rectangle(buttonStartX+(buttonIndex*(buttonBuffer+buttonWidth)), buttonStartY,buttonWidth , buttonHeight), Color.White);
        //_spriteBatch.DrawString(titleFont, "Session Selection", new Vector2(buttonStartX+(buttonIndex*(buttonBuffer+buttonWidth)+((buttonWidth-fontSize.X)/2)), buttonStartY+((buttonHeight-fontSize.Y)/2)), Color.Red);
        buttonIndex-=-1;
        fontSize =  titleFont.MeasureString("Driver Selection");

        _spriteBatch.Draw(buttonTexture, new Rectangle(buttonStartX+(buttonIndex*(buttonBuffer+buttonWidth)), buttonStartY,buttonWidth , buttonHeight), Color.White);
        //_spriteBatch.DrawString(titleFont, "Driver Selection", new Vector2(buttonStartX+(buttonIndex*(buttonBuffer+buttonWidth)+((buttonWidth-fontSize.X)/2)), buttonStartY+((buttonHeight-fontSize.Y)/2)), Color.Red);


        //Draw Driver

    }
}
