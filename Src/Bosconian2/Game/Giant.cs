
// Type: Starkiller.Giant
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Starkiller
{
  internal class Giant : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Ship ship;
    private int spreadCooldown;
    private int gunCooldown;
    private int gunDirection;
    private int missileCooldown;
    private int blastCooldown;
    private int cannonCooldown;
    private float leftGunAngle;
    private float middleGunAngle;
    private float rightGunAngle;

    public Giant(ContentManager Content, GraphicsDeviceManager Graphics, Ship ship, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.sprite = "giant";
      this.hp = (float) (450 + level * 5);
      this.points = 10000 + level * 100;
      this.gunDirection = this.RNG(0, 1);
      if (this.gunDirection != 0)
        return;
      this.gunDirection = -1;
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      if ((double) this.ship.Center.X <= 70.0)
      {
        if (this.blastCooldown == 10)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 5));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X - 570f, this.Center.Y + 65f);
          this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 5));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X - 590f, this.Center.Y + 75f);
          this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 5));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X - 610f, this.Center.Y + 85f);
          this.bullets[this.bullets.Count - 1].LoadContent("fbullet");

          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.blastCooldown = 0;
        }
        else
          ++this.blastCooldown;
      }
      else if ((double) this.ship.Center.X > 70.0 && (double) this.ship.Center.X <= 475.0)
      {
        this.middleGunAngle = 0.0f;
        this.rightGunAngle = 0.0f;
        if (this.gunCooldown == 15)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f + this.leftGunAngle), 0, 4));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X - 2.5 - 315.0), this.Center.Y + 100f);
          this.bullets[this.bullets.Count - 1].LoadContent("pbullet");
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.gunCooldown = 0;
        }
        else
          ++this.gunCooldown;
        if (this.missileCooldown == 50)
        {
          this.bullets.Add((Bullet) new EMissile(this.Content, this.Graphics, this.ship, this.Position));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X - 7.5 - 200.0), this.Center.Y + 50f);
          this.bullets[this.bullets.Count - 1].LoadContent("emissile");
          this.missileSound.Play();
          //this.missileSound.Dispose();
          ++this.missileCooldown;
        }
        else if (this.missileCooldown == 100)
        {
          this.bullets.Add((Bullet) new EMissile(this.Content, this.Graphics, this.ship, this.Position));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X - 7.5 - 440.0), this.Center.Y + 80f);
          this.bullets[this.bullets.Count - 1].LoadContent("emissile");
          this.missileSound.Play();
          //this.missileSound.Dispose();
          this.missileCooldown = 0;
        }
        else
          ++this.missileCooldown;
        if ((double) this.leftGunAngle >= 60.0 || (double) this.leftGunAngle <= -60.0)
          this.gunDirection *= -1;
        this.leftGunAngle += (float) this.gunDirection * 0.5f;
      }
      else if ((double) this.ship.Center.X > 475.0 && (double) this.ship.Center.X <= 805.0)
      {
        this.leftGunAngle = 0.0f;
        this.rightGunAngle = 0.0f;
        if (this.spreadCooldown == 100)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(200f), 0, 2));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X + 2.5 + 120.0), this.Center.Y + 85f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(-200f), 0, 2));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X + 2.5 + 120.0), this.Center.Y + 85f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 2));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X + 2.5 + 120.0), this.Center.Y + 85f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(200f), 0, 2));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X + 2.5 - 120.0), this.Center.Y + 85f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(-200f), 0, 2));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X + 2.5 - 120.0), this.Center.Y + 85f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 2));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X + 2.5 - 120.0), this.Center.Y + 85f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.spreadCooldown = 0;
        }
        else
          ++this.spreadCooldown;
        if (this.gunCooldown == 15)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f + this.middleGunAngle), 0, 4));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X - 2.5f, this.Center.Y + 140f);
          this.bullets[this.bullets.Count - 1].LoadContent("pbullet");
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.gunCooldown = 0;
        }
        else
          ++this.gunCooldown;
        if ((double) this.middleGunAngle >= 60.0 || (double) this.middleGunAngle <= -60.0)
          this.gunDirection *= -1;
        this.middleGunAngle += (float) this.gunDirection * 0.5f;
      }
      else if ((double) this.ship.Center.X > 805.0 && (double) this.ship.Center.X <= 1210.0)
      {
        this.middleGunAngle = 0.0f;
        this.leftGunAngle = 0.0f;
        if (this.gunCooldown == 15)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f + this.rightGunAngle), 0, 4));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X - 2.5 + 325.0), this.Center.Y + 100f);
          this.bullets[this.bullets.Count - 1].LoadContent("pbullet");
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.gunCooldown = 0;
        }
        else
          ++this.gunCooldown;
        if (this.missileCooldown == 50)
        {
          this.bullets.Add((Bullet) new EMissile(this.Content, this.Graphics, this.ship, this.Position));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X - 7.5 + 200.0), this.Center.Y + 50f);
          this.bullets[this.bullets.Count - 1].LoadContent("emissile");
          this.missileSound.Play();
          //this.missileSound.Dispose();
          ++this.missileCooldown;
        }
        else if (this.missileCooldown == 100)
        {
          this.bullets.Add((Bullet) new EMissile(this.Content, this.Graphics, this.ship, this.Position));
          this.bullets[this.bullets.Count - 1].Position = new Vector2((float) ((double) this.Center.X - 7.5 + 440.0), this.Center.Y + 80f);
          this.bullets[this.bullets.Count - 1].LoadContent("emissile");
          this.missileSound.Play();
          //this.missileSound.Dispose();
          this.missileCooldown = 0;
        }
        else
          ++this.missileCooldown;
        if ((double) this.rightGunAngle >= 60.0 || (double) this.rightGunAngle <= -60.0)
          this.gunDirection *= -1;
        this.rightGunAngle += (float) this.gunDirection * 0.5f;
      }
      else if (this.blastCooldown == 10)
      {
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 5));
        this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X + 580f, this.Center.Y + 65f);
        this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 5));
        this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X + 600f, this.Center.Y + 75f);
        this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
        this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, 5));
        this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X + 620f, this.Center.Y + 85f);
        this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
        this.laserSound.Play(0.5f, 0.0f, 0.0f);
        //this.laserSound.Dispose();
        this.blastCooldown = 0;
      }
      else
        ++this.blastCooldown;
      if ((double) this.hp < 150.0)
      {
        if ((double) this.Position.Y < 50.0)
          this.Position = new Vector2(this.Position.X, this.Position.Y + 1f);
        if (this.cannonCooldown == 100)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, (float) Math.Atan2((double) this.ship.Center.X - (double) this.Center.X + 5.0, (double) this.Center.Y - 60.0 - (double) this.ship.Center.Y), 0, 5));
          this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X + 5f, this.Center.Y - 60f);
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          ++this.cannonCooldown;
        }
        else if (this.cannonCooldown == 200)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, (float) Math.Atan2((double) this.ship.Center.X - (double) this.Center.X + 15.0, (double) this.Center.Y - 45.0 - (double) this.ship.Center.Y), 0, 5));
          this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X + 15f, this.Center.Y - 45f);
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          ++this.cannonCooldown;
        }
        else if (this.cannonCooldown == 300)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, (float) Math.Atan2((double) this.ship.Center.X - (double) this.Center.X - 10.0, (double) this.Center.Y - 45.0 - (double) this.ship.Center.Y), 0, 5));
          this.bullets[this.bullets.Count - 1].LoadContent("fbullet");
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Center.X - 5f, this.Center.Y - 45f);
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.cannonCooldown = 0;
        }
        else
          ++this.cannonCooldown;
      }
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      if (this.state == State.Alive)
      {
        if (this.timer == 0)
          spriteBatch.Draw(this.Texture, this.Position, Color.White);
        else
          spriteBatch.Draw(this.Flash, this.Position, Color.White);
      }
      foreach (Object bullet in this.bullets)
        bullet.Draw(spriteBatch);
    }
  }
}
