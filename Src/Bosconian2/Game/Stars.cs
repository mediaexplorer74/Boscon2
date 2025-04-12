
// Type: Starkiller.Stars
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace Starkiller
{
  internal class Stars
  {
    private Texture2D star;
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private List<Vector2> stars;
    private int cooldown;
    private int maxCooldown;
    private int speed;
    private int size;
    private Object random;
    private Color color;

    public Stars(
      ContentManager Content,
      GraphicsDeviceManager Graphics,
      int maxCooldown,
      int speed,
      int size,
      Color color)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.cooldown = 0;
      this.maxCooldown = maxCooldown;
      this.speed = speed;
      this.size = size;
      this.color = color;
      this.stars = new List<Vector2>();
      this.random = new Object(Content, Graphics);
    }

    public void LoadContent() => this.star = this.Content.Load<Texture2D>("star");

    public void Update()
    {
      if (this.cooldown == 0)
      {
        this.stars.Add(new Vector2((float) this.random.RNG(0, this.Graphics.PreferredBackBufferWidth), 0.0f));
        this.cooldown = this.maxCooldown;
      }
      for (int index = 0; index < this.stars.Count; ++index)
      {
        if ((double) this.stars[index].Y >= (double) this.Graphics.PreferredBackBufferHeight)
        {
          this.stars.RemoveAt(index);
        }
        else
        {
          Vector2 vector2 = new Vector2(this.stars[index].X, this.stars[index].Y + (float) this.speed);
          this.stars[index] = vector2;
        }
      }
      if (this.cooldown == 0)
        return;
      --this.cooldown;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (Vector2 star in this.stars)
        spriteBatch.Draw(this.star, star, new Rectangle?(new Rectangle(0, 0, this.size, this.size)), this.color);
    }
  }
}
