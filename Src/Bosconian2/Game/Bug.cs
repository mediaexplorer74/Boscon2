
// Type: Starkiller.Bug
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class Bug : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private int direction;
    private int cooldown;

    public Bug(ContentManager Content, GraphicsDeviceManager Graphics, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.sprite = "bug";
      this.hp = (float) (2.0 + (double) level * 0.10000000149011612);
      this.points = 100 + level * 5;
      this.speed = (float) (3.0 + (double) level * 0.05000000074505806);
      this.cooldown = this.RNG(0, 100);
      this.direction = this.RNG(1, 100);
      if (this.direction <= 50)
        this.direction = -1;
      else
        this.direction = 1;
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      if (this.cooldown == 0)
      {
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 3.14f, 0, 3));
        this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
        this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 25f, this.Position.Y + 60f);
        this.laserSound.Play(0.5f, 0.0f, 0.0f);
        //this.laserSound.Dispose();
        this.cooldown = 100;
      }
      else if (this.cooldown != 0)
        --this.cooldown;
      this.Position = new Vector2(this.Position.X + this.speed * (float) this.direction, this.Position.Y);
      if ((double) this.Position.X <= 0.0 || (double) this.Position.X + (double) this.Texture.Width >= (double) this.Graphics.PreferredBackBufferWidth)
        this.direction *= -1;
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
