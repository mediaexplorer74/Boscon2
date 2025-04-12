
// Type: Starkiller.Death
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class Death : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private int x;
    private int y;
    private int cooldown = 0;
    private int cooldown2 = 0;
    private int cooldown3 = 0;
    private float angle = 0.785f;
    private float angle2 = 0.0f;

    public Death(ContentManager Content, GraphicsDeviceManager Graphics, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.x = this.RNG(0, 1);
      this.y = this.RNG(0, 1);
      if (this.x == 0)
        this.x = -1;
      if (this.y == 0)
        this.y = -1;
      this.sprite = "death";
      this.hp = (float) (150 + level * 5);
      this.points = 10000 + level * 100;
      this.timer = 0;
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      if ((double) this.hp <= 50.0)
      {
        if ((double) this.Position.X <= 0.0 || (double) this.Position.X + (double) this.Texture.Width >= (double) this.Graphics.PreferredBackBufferWidth)
          this.x *= -1;
        if ((double) this.Position.Y <= 50.0 || (double) this.Position.Y + (double) this.Texture.Height >= (double) this.Graphics.PreferredBackBufferHeight)
          this.y *= -1;
        this.Position = new Vector2(this.Position.X + 0.5f * (float) this.x, this.Position.Y + 0.5f * (float) this.y);
      }
      if (this.cooldown == 35)
      {
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle + 0.785f, 0, 3));
        this.bullets[this.bullets.Count - 1].LoadContent("blueball");
        this.bullets[this.bullets.Count - 1].Position = this.Center;

        this.laserSound.Play();
        //this.laserSound.Dispose();
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle + 2.355f, 0, 3));
        this.bullets[this.bullets.Count - 1].LoadContent("blueball");
        this.bullets[this.bullets.Count - 1].Position = this.Center;
        this.laserSound.Play();
     
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle + 3.92500019f, 0, 3));
        this.bullets[this.bullets.Count - 1].LoadContent("blueball");
        this.bullets[this.bullets.Count - 1].Position = this.Center;
        
        this.laserSound.Play();
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle + 5.49500036f, 0, 3));
        this.bullets[this.bullets.Count - 1].LoadContent("blueball");
        this.bullets[this.bullets.Count - 1].Position = this.Center;
        
        this.laserSound.Play(0.5f, 0.0f, 0.0f);
        
        this.cooldown = 0;
      }
      else
        ++this.cooldown;
      if (this.cooldown2 == 100)
      {
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle2, 0, 10));
        this.bullets[this.bullets.Count - 1].LoadContent("yellowball");
        this.bullets[this.bullets.Count - 1].Position = this.Center;
        this.laserSound.Play(0.5f, 0.0f, 0.0f);
        //this.laserSound.Dispose();
        this.cooldown2 = 0;
      }
      else
        ++this.cooldown2;
      if ((double) this.hp <= 100.0)
      {
        if (this.cooldown3 == 180)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle, 0, 0));
          this.bullets[this.bullets.Count - 1].LoadContent("redlaser");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle + 1.57f, 0, 0));
          this.bullets[this.bullets.Count - 1].LoadContent("redlaser");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle + 3.14f, 0, 0));
          this.bullets[this.bullets.Count - 1].LoadContent("redlaser");
          this.bullets[this.bullets.Count - 1].Position = this.Center;
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.angle + 4.71f, 0, 0));
          this.bullets[this.bullets.Count - 1].LoadContent("redlaser");
          this.bullets[this.bullets.Count - 1].Position = this.Center + new Vector2(-2.5f, 5f);
          foreach (Bullet bullet in this.bullets)
          {
            if (bullet.Texture.Height > 500)
              bullet.laser = true;
          }
          --this.cooldown3;
        }
        else if (this.cooldown3 < 180 && this.cooldown3 > 0)
        {
          foreach (Bullet bullet in this.bullets)
          {
            if (bullet.laser)
            {
              bullet.Rotation += 0.005f;
              bullet.Position = this.Center;
            }
          }
          this.powerupSound.Play();
          //this.powerupSound.Dispose();
          --this.cooldown3;
        }
        else if (this.cooldown3 > 180)
        {
          for (int index = 0; index < this.bullets.Count; ++index)
          {
            if (this.bullets[index].laser)
              this.bullets.RemoveAt(index);
          }
          --this.cooldown3;
        }
        else if (this.cooldown3 == 0)
          this.cooldown3 = 360;
      }
      this.angle += 0.005f;
      this.angle2 -= 2f;
      this.Rotation = this.angle;
      base.Update();
      this.Hitbox.Height = this.Texture.Height;
      this.Hitbox.Width = this.Texture.Width;
      this.Hitbox.Location = new Point((int) this.Position.X, (int) this.Position.Y);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (this.state == State.Alive)
      {
        if (this.timer == 0)
          spriteBatch.Draw(this.Texture, this.Center, new Rectangle?(), Color.White, this.Rotation, new Vector2((float) (this.Texture.Width / 2), (float) (this.Texture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
        else
          spriteBatch.Draw(this.Flash, this.Center, new Rectangle?(), Color.White, this.Rotation, new Vector2((float) (this.Texture.Width / 2), (float) (this.Texture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
      }
      foreach (Object bullet in this.bullets)
        bullet.Draw(spriteBatch);
    }
  }
}
