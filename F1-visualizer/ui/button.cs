using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class Button{
    public Vector2 position;
    public Texture2D texture;

    public Button(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch){
        spriteBatch.Draw(texture,position,Color.White);
    }
}