
// Type: Starkiller.Missile
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
  internal class Missile : Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Vector2 ship;
    private Enemies enemies;
    public Enemy target = (Enemy) null;
    private Vector2 targetPosition;
    private int cooldown = 0;

    public Missile(
      ContentManager Content,
      GraphicsDeviceManager Graphics,
      Vector2 ship,
      int side,
      Enemies enemies)
      : base(Content, Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.enemies = enemies;
      if (side == 1)
        this.Position = new Vector2(ship.X + 30f, ship.Y + 25f);
      else
        this.Position = new Vector2(ship.X + 5f, ship.Y + 25f);
      this.damage = 2;
      this.Rotation = 1.57079637f;
    }

    public override void Update()
    {
      if (this.cooldown == 20 && this.enemies != null)
      {
        this.target = (Enemy) null;
        for (int index = 0; index < this.enemies.list.Length; ++index)
        {
          if (this.target == null && this.enemies.list[index] != null && this.enemies.list[index].state == State.Alive)
            this.target = this.enemies.list[index];
          else if (this.enemies.list[index] != null && this.enemies.list[index].state == State.Alive && this.Distance((Object) this, (Object) this.enemies.list[index]) < this.Distance((Object) this, (Object) this.target))
            this.target = this.enemies.list[index];
        }
        if (this.target != null)
          this.targetPosition = new Vector2(this.target.Center.X, this.target.Center.Y);
        this.cooldown = 0;
      }
      else
        ++this.cooldown;
      if (this.target != null)
        this.Rotation = Missile.WrapAngle(this.Rotation + MathHelper.Clamp(Missile.WrapAngle((float) Math.Atan2((double) this.Position.Y - (double) this.targetPosition.Y, (double) this.Position.X - (double) this.targetPosition.X) - this.Rotation), -0.05f, 0.05f));
      this.Position = new Vector2(this.Position.X - (float) (10.0 * Math.Cos((double) this.Rotation)), this.Position.Y - (float) (10.0 * Math.Sin((double) this.Rotation)));
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Texture, this.Position, new Rectangle?(), Color.White, this.Rotation - 1.57079637f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
    }

    private static float WrapAngle(float radians)
    {
      while ((double) radians < -3.1415927410125732)
        radians += 6.28318548f;
      while ((double) radians > 3.1415927410125732)
        radians -= 6.28318548f;
      return radians;
    }
  }
}
