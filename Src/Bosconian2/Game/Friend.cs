
// Type: Starkiller.Friend
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
  internal class Friend : Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    public Ship ship;
    private Enemy target = (Enemy) null;
    private Vector2 targetPosition;
    private int cooldown;

    public Friend(ContentManager Content, GraphicsDeviceManager Graphics, Ship ship)
      : base(Content, Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.LoadContent("friend");
    }

    public new void Update()
    {
      if (this.ship.enemies != null)
      {
        for (int index = 0; index < this.ship.enemies.list.Length; ++index)
        {
          if (this.target == null && this.ship.enemies.list[index] != null && this.ship.enemies.list[index].state == State.Alive)
            this.target = this.ship.enemies.list[index];
          else if (this.ship.enemies.list[index] != null && this.ship.enemies.list[index].state == State.Alive && this.Distance((Object) this, (Object) this.ship.enemies.list[index]) < this.Distance((Object) this, (Object) this.target))
            this.target = this.ship.enemies.list[index];
        }
      }
      if (this.target != null)
        this.targetPosition = new Vector2(this.target.Center.X, this.target.Center.Y);
      if (this.target != null && this.target.state == State.Dead)
        this.target = (Enemy) null;
      if (this.cooldown == 0)
      {
        Bullet bullet = this.target == null
                    ? new Bullet(this.Content, this.Graphics, this.Center - new Vector2(25f, 10f), 0.0f, 0, 50) 
                    : new Bullet(this.Content, this.Graphics, this.Center - new Vector2(25f, 10f), 
                       (float) Math.Atan2((double) this.targetPosition.X - (double) this.Center.X, 
                        (double) this.Center.Y - (double) this.targetPosition.Y), 0, 50);
        bullet.LoadContent("fbullet");
        this.ship.bullets.Add((Object) bullet);
                
        this.laserSound.Play(0.5f, 0.0f, 0.0f);
        //this.laserSound.Dispose();
        this.cooldown = 10;
      }
      else
        --this.cooldown;
      this.Rotation += 0.1f;
      this.Position.X = (float) (50.0 * Math.Cos((double) this.Rotation)) + this.ship.Center.X - (float) (this.Texture.Width / 2);
      this.Position.Y = (float) (50.0 * Math.Sin((double) this.Rotation)) + this.ship.Center.Y - (float) (this.Texture.Height / 2);
      base.Update();
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Texture, this.Position, Color.White);
    }
  }
}
