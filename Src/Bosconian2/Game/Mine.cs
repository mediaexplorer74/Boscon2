
// Type: Starkiller.Mine
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class Mine : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private int x;
    private int y;
    private bool explosion = false;
    private new int timer = 400;
    private Ship ship;
    private bool collision = false;

    public Mine(ContentManager Content, GraphicsDeviceManager Graphics, Ship ship, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.x = this.RNG(0, 1);
      this.y = this.RNG(0, 1);
      if (this.x == 0)
        this.x = -1;
      if (this.y == 0)
        this.y = -1;
      this.sprite = "mine";
      this.hp = (float) (1.0 + (double) level * 0.10000000149011612);
      this.points = 50 + level * 5;
      this.speed = (float) (1.5 + (double) level * 0.05000000074505806);
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      if (this.CollidesWith((Object) this.ship) && !this.collision && this.state == State.Alive)
      {
        //if (!Game1.GodMode)
        //{
            this.state = State.Dead;

            if (!Game1.GodMode)
                this.ship.State = State.Dead;

            this.collision = true;
        //}
        
      }
      if (this.timer == 400 && this.state == State.Dead)
        this.explosion = true;
      if (!this.explosion)
      {
        if ((double) this.Position.X <= 0.0 || (double) this.Position.X + (double) this.Texture.Width >= (double) this.Graphics.PreferredBackBufferWidth)
          this.x *= -1;
        if ((double) this.Position.Y <= 50.0 || (double) this.Position.Y + (double) this.Texture.Height >= (double) this.Graphics.PreferredBackBufferHeight)
          this.y *= -1;
        this.Position = new Vector2(this.Position.X + this.speed * (float) this.x, this.Position.Y + this.speed * (float) this.y);
      }
      else
      {
        if (this.bullets.Count == 0 && this.timer == 400)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 0.0f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 0.785f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 1.57f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 2.355f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 3.14f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 3.925f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 4.71f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, 5.495f, 0, 2));
          this.bullets[this.bullets.Count - 1].LoadContent("debris");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
        }
        else
          --this.timer;
        if (this.timer == 0)
          this.explosion = false;
      }
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      if (!this.explosion)
        return;
      foreach (Object bullet in this.bullets)
        bullet.Draw(spriteBatch);
    }
  }
}
