
// Type: Starkiller.Brute
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
  internal class Brute : Enemy
  {
    private new ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Ship ship;
    private float angle;

    public Brute(ContentManager Content, GraphicsDeviceManager Graphics, Ship ship, int level)
      : base(Content, Graphics, level)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.ship = ship;
      this.sprite = "brute";
      this.hp = (float) (10.0 + (double) level * 0.10000000149011612);
      this.points = 250 + level * 5;
      this.speed = (float) (1.0 + (double) level * 0.05000000074505806);
    }

    public new void LoadContent() => base.LoadContent();

    public override void Update()
    {
      this.angle = (float) Math.Atan2((double) this.Position.Y - (double) this.ship.Position.Y, (double) this.Position.X - (double) this.ship.Position.X);
      this.Position = new Vector2(this.Position.X - this.speed * (float) Math.Cos((double) this.angle), this.Position.Y - (float) (1.0 * Math.Sin((double) this.angle)));
      base.Update();
    }

    public override void Draw(SpriteBatch spriteBatch) => base.Draw(spriteBatch);
  }
}
