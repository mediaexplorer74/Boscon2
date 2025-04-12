
// Type: Starkiller.PowerUp
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class PowerUp : Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    public State state;
    public PU powerUp;
    private int x;
    private int y;
    public int timer;
    private string sprite;

    public PowerUp(ContentManager Content, GraphicsDeviceManager Graphics, int powerUpType)
      : base(Content, Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.x = this.RNG(0, 1);
      this.y = this.RNG(0, 1);
      if (this.x == 0)
        this.x = -1;
      if (this.y == 0)
        this.y = -1;
      switch (powerUpType)
      {
        case 1:
          this.powerUp = PU.MG;
          this.sprite = "MG";
          break;
        case 2:
          this.powerUp = PU.M;
          this.sprite = "M";
          break;
        case 3:
          this.powerUp = PU.S;
          this.sprite = "S";
          break;
        case 4:
          this.powerUp = PU.B;
          this.sprite = "B";
          break;
        case 5:
          this.powerUp = PU.F;
          this.sprite = "F";
          break;
      }
      this.LoadContent(this.sprite);
      this.timer = 0;
      this.state = State.Alive;
    }

    public new void Update()
    {
      if (this.timer >= 300)
      {
       //if (!Game1.GodMode)
         this.state = State.Dead;
      }
      if ((double) this.Position.X <= 0.0 || (double) this.Position.X + (double) this.Texture.Width >= (double) this.Graphics.PreferredBackBufferWidth)
        this.x *= -1;
      if ((double) this.Position.Y <= 50.0 || (double) this.Position.Y + (double) this.Texture.Height >= (double) this.Graphics.PreferredBackBufferHeight)
        this.y *= -1;
      this.Position = new Vector2(this.Position.X + (float) (3 * this.x), this.Position.Y + (float) (3 * this.y));
      ++this.timer;
      base.Update();
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
      if (this.state != State.Alive)
        return;
      base.Draw(spriteBatch);
    }
  }
}
