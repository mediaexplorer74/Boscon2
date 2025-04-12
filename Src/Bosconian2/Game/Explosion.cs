
// Type: Starkiller.Explosion
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class Explosion : Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    public int width;
    public int height;
    public int size;
    public int frame;
    public int frameRate;
    public int frameRateCounter;

    public Explosion(
      ContentManager Content,
      GraphicsDeviceManager Graphics,
      Vector2 c,
      Texture2D t)
      : base(Content, Graphics)
    {
      this.LoadContent("explosion");
      this.Content = Content;
      this.Graphics = Graphics;
      this.width = t.Width;
      this.height = t.Height;
      this.size = this.width < this.height ? this.height : this.width;
      this.Position = new Vector2(c.X - (float) (this.size / 2), c.Y - (float) (this.size / 2));
      this.frame = 0;
      this.frameRate = 1;
      this.frameRateCounter = 0;
    }

    public override void Update()
    {
      if (this.frameRateCounter == this.frameRate)
      {
        ++this.frame;
        this.frameRateCounter = 0;
      }
      else
        ++this.frameRateCounter;
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Texture, new Rectangle((int) this.Position.X, (int) this.Position.Y, this.size, this.size), new Rectangle?(new Rectangle(this.frame * 100, 0, 100, 100)), Color.White);
    }
  }
}
