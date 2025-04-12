
// Type: Starkiller.Hunter
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class Hunter : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Ship ship;
    private int direction;
    private int cooldown;
    private int side;

    public Hunter(ContentManager Content, GraphicsDeviceManager Graphics, Ship ship, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.sprite = "hunter";
      this.hp = (float) (5.0 + (double) level * 0.10000000149011612);
      this.points = 200 + level * 5;
      this.speed = (float) (1.5 + (double) level * 0.05000000074505806);
      this.cooldown = this.RNG(0, 150);
      this.side = this.RNG(0, 1);
      this.direction = this.RNG(0, 1);
      if (this.direction != 0)
        return;
      this.direction = -1;
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      if (this.cooldown == 0)
      {
        this.bullets.Add((Bullet) new EMissile(this.Content, this.Graphics, this.ship, this.Position));
        this.bullets[this.bullets.Count - 1].LoadContent("emissile");
        if (this.side == 0)
        {
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 10f, this.Position.Y + 40f);
          this.side = 1;
        }
        else
        {
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 60f, this.Position.Y + 40f);
          this.side = 0;
        }
        this.missileSound.Play();
        //this.missileSound.Dispose();
        this.cooldown = 150;
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
