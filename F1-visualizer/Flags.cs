using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Text.Json;
using System;
using System.IO;
using System.Collections.Generic;

namespace F1_visualizer;

class Flags {
    private const string flagDir = "Flags";

    private static Dictionary<string, Texture2D> flagTextures;

    public static void Initialize(GraphicsDevice graphicsDevice) {
	flagTextures = GetAllFlags(graphicsDevice);
    }

    private static Dictionary<string, Texture2D> GetAllFlags(GraphicsDevice graphicsDevice) {
	Dictionary<string, Texture2D> flags = new ();
	string[] flagFiles = Directory.GetFiles(flagDir);

	foreach(string file in flagFiles) {
	    string countryCode = Path.GetFileNameWithoutExtension(file);
	    Console.WriteLine($"Loaded {countryCode}");
	    flags.Add(countryCode, Texture2D.FromFile(graphicsDevice, file));	    
	}

	return flags;
    }

    public static Texture2D getCountryFlag(string countryCode) {
	if(flagTextures == null) {
	    Console.WriteLine("Country Textures Not Initialized");
	    return null;
	}

	if(flagTextures.ContainsKey(countryCode)) {
	    return flagTextures[countryCode];
	}

	Console.WriteLine($"Country code '{countryCode}' doesn't exist");
	return null;
    }


}
