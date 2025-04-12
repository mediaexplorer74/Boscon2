
// Type: Starkiller.EMissile
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
  internal class EMissile : Bullet
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Ship ship;
    private Vector2 targetPosition;
    private Vector2 enemy;
    private int cooldown = 0;

    public EMissile(
      ContentManager Content,
      GraphicsDeviceManager Graphics,
      Ship ship,
      Vector2 enemy)
      : base(Content, Graphics, ship.Position, 0.0f, 0, 0)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.enemy = enemy;
      this.damage = 2;
      this.targetPosition = new Vector2(this.Position.X, this.Position.Y + 1000f);
      this.Rotation = -1.57079637f;
    }

    public override void Update()
    {
      if (this.cooldown == 20)
      {
        this.targetPosition = this.ship.Center;
        this.cooldown = 0;
      }
      else
        ++this.cooldown;
      this.Rotation = EMissile.WrapAngle(this.Rotation + MathHelper.Clamp(EMissile.WrapAngle((float) Math.Atan2((double) this.Position.Y - (double) this.targetPosition.Y, (double) this.Position.X - (double) this.targetPosition.X) - this.Rotation), -0.025f, 0.025f));
      this.Position = new Vector2(this.Position.X - (float) (5.0 * Math.Cos((double) this.Rotation)), this.Position.Y - (float) (5.0 * Math.Sin((double) this.Rotation)));
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Texture, this.Position, new Rectangle?(), Color.White, this.Rotation + 1.57079637f, Vector2.Zero, Vector2.One, SpriteEffects.None, 1f);
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
