
// Type: Starkiller.Bullet
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
  internal class Bullet : Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Vector2 ship;
    private int speed;
    public bool laser = false;

    public Bullet(
      ContentManager Content,
      GraphicsDeviceManager Graphics,
      Vector2 ship,
      float angle,
      int side,
      int speed)
      : base(Content, Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.Rotation = angle;
      this.speed = speed;
      switch (side)
      {
        case -2:
          this.Position = new Vector2(ship.X + 25f, ship.Y + 20f);
          break;
        case -1:
          this.Position = new Vector2(ship.X + 25f, ship.Y + 30f);
          break;
        case 1:
          this.Position = new Vector2(ship.X + 15f, ship.Y + 30f);
          break;
        case 2:
          this.Position = new Vector2(ship.X + 15f, ship.Y + 20f);
          break;
        default:
          this.Position = new Vector2(ship.X + 20f, ship.Y);
          break;
      }
      this.damage = 1;
    }

    public override void Update()
    {
      this.Position = new Vector2(this.Position.X + (float) this.speed * (float) Math.Sin((double) this.Rotation), this.Position.Y - (float) this.speed * (float) Math.Cos((double) this.Rotation));
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Texture, this.Position, new Rectangle?(), Color.White, this.Rotation, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
    }
  }
}
