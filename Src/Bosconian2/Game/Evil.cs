
// Type: Starkiller.Evil
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
  internal class Evil : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Ship ship;
    private int direction;
    private int side;
    private int spreadCooldown;
    private int spreadSpeed;
    private int missileCooldown;
    private int machineGunCooldown;
    private int behaviorTimer;
    private int behaviorRandom;
    private Behavior behavior;

    public Evil(ContentManager Content, GraphicsDeviceManager Graphics, Ship ship, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.sprite = "evil";
      this.hp = (float) (90 + level);
      this.points = 10000 + level * 100;
      this.spreadSpeed = 5;
      this.direction = this.RNG(0, 1);
      if (this.direction == 0)
        this.direction = -1;
      this.behaviorRandom = this.RNG(0, 2);
      this.behavior = this.behaviorRandom != 0 ? (this.behaviorRandom != 1 ? Behavior.MG : Behavior.M) : Behavior.S;
      this.behaviorTimer = 0;
      this.side = 2;
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      if (this.behavior == Behavior.S)
      {
        if (this.spreadCooldown == 0)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(195f), 0, this.spreadSpeed));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 25f, this.Position.Y + 45f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(-195f), 0, this.spreadSpeed));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 25f, this.Position.Y + 45f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(210f), 0, this.spreadSpeed));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 25f, this.Position.Y + 45f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(-210f), 0, this.spreadSpeed));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 25f, this.Position.Y + 45f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, MathHelper.ToRadians(180f), 0, this.spreadSpeed));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 25f, this.Position.Y + 45f);
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");

          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.spreadCooldown = 30;
        }
        else
          --this.spreadCooldown;
        this.Position = new Vector2(this.Position.X + (float) (4 * this.direction), this.Position.Y);
        if ((double) this.Position.X <= 0.0 || (double) this.Position.X + (double) this.Texture.Width >= (double) this.Graphics.PreferredBackBufferWidth)
          this.direction *= -1;
      }
      else if (this.behavior == Behavior.M)
      {
        if (this.missileCooldown == 0)
        {
          this.bullets.Add((Bullet) new EMissile(this.Content, this.Graphics, this.ship, this.Position));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 5f, this.Position.Y + 25f);
          this.bullets[this.bullets.Count - 1].LoadContent("emissile");
          this.bullets.Add((Bullet) new EMissile(this.Content, this.Graphics, this.ship, this.Position));
          this.bullets[this.bullets.Count - 1].Position = new Vector2(this.Position.X + 30f, this.Position.Y + 25f);
          this.bullets[this.bullets.Count - 1].LoadContent("emissile");
          this.missileSound.Play();
          //this.missileSound.Dispose();
          this.missileCooldown = 70;
        }
        else
          --this.missileCooldown;
        this.Position = new Vector2(this.Position.X + (float) (4 * this.direction), this.Position.Y);
        if ((double) this.Position.X <= 0.0 || (double) this.Position.X + (double) this.Texture.Width >= (double) this.Graphics.PreferredBackBufferWidth)
          this.direction *= -1;
      }
      else
      {
        this.barrier = true;
        this.Rotation = (float) Math.Atan2((double) this.ship.Center.X - (double) this.Center.X, (double) this.Center.Y - (double) this.ship.Center.Y);
        if (this.machineGunCooldown == 0)
        {
          this.bullets.Add(new Bullet(this.Content, this.Graphics, this.Position, this.Rotation, this.side, 10));
          this.bullets[this.bullets.Count - 1].LoadContent("ebullet");
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.machineGunCooldown = 20;
          this.side *= -1;
        }
        else
          --this.machineGunCooldown;
      }
      if (this.behaviorTimer >= 300)
      {
        this.Rotation = 0.0f;
        this.barrier = false;
        this.behaviorRandom = this.RNG(0, 2);
        this.behavior = this.behaviorRandom != 0 ? (this.behaviorRandom != 1 ? Behavior.MG : Behavior.M) : Behavior.S;
        this.behaviorTimer = 0;
      }
      else
        ++this.behaviorTimer;
      base.Update();
      this.Hitbox.Height = this.Texture.Height;
      this.Hitbox.Width = this.Texture.Width;
      this.Hitbox.Location = new Point((int) this.Position.X, (int) this.Position.Y);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      foreach (Object bullet in this.bullets)
        bullet.Draw(spriteBatch);
      if (this.state != State.Alive)
        return;
      if (this.barrier)
      {
        if (this.timer == 0)
          spriteBatch.Draw(this.Texture, this.Center, new Rectangle?(), Color.White, this.Rotation + 3.14159274f, new Vector2((float) (this.Texture.Width / 2), (float) (this.Texture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
        else
          spriteBatch.Draw(this.Flash, this.Center, new Rectangle?(), Color.White, this.Rotation + 3.14159274f, new Vector2((float) (this.Texture.Width / 2), (float) (this.Texture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
        spriteBatch.Draw(this.Barrier, this.Position - 5f * Vector2.One, Color.White);
      }
      else if (this.timer == 0)
        spriteBatch.Draw(this.Texture, this.Center, new Rectangle?(), Color.White, this.Rotation, new Vector2((float) (this.Texture.Width / 2), (float) (this.Texture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
      else
        spriteBatch.Draw(this.Flash, this.Center, new Rectangle?(), Color.White, this.Rotation, new Vector2((float) (this.Texture.Width / 2), (float) (this.Texture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
    }
  }
}
