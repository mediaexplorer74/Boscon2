
// Type: Starkiller.Invader
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class Invader : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private int direction;

    public Invader(ContentManager Content, GraphicsDeviceManager Graphics, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.sprite = "invader";
      this.hp = (float) (10.0 + (double) level * 0.10000000149011612);
      this.points = 1000 + level * 5;
      this.speed = (float) (5.0 + (double) level * 0.05000000074505806);
      this.direction = this.RNG(0, 1);
      if (this.direction != 0)
        return;
      this.direction = -1;
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      this.Position = new Vector2(this.Position.X + this.speed * (float) this.direction, this.Position.Y);
      if ((double) this.Position.X <= 0.0 || (double) this.Position.X + (double) this.Texture.Width >= (double) this.Graphics.PreferredBackBufferWidth)
        this.direction *= -1;
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
