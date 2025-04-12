
// Type: Starkiller.Enemy
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace Starkiller
{
  internal class Enemy : Object
  {
    public ContentManager Content;
    private GraphicsDeviceManager Graphics;
    public string sprite;
    public State state;
    public float hp;
    public float speed;
    public int timer = 0;
    public int level;
    public bool exploded = false;
    public bool barrier = false;
    public Texture2D Barrier;
    public List<Bullet> bullets;

    public Enemy(ContentManager Content, GraphicsDeviceManager Graphics, int lvl)
      : base(Content, Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.state = State.Alive;
      this.bullets = new List<Bullet>();
      this.level = this.RNG(lvl - 1, lvl + 3);
      if (this.level >= 0)
        return;
      this.level = 0;
    }

    public new void LoadContent()
    {
      this.LoadContent(this.sprite);
      this.Flash = this.Content.Load<Texture2D>(this.sprite + "_f");
    }

    public new virtual void Update()
    {
      if ((double) this.hp <= 0.0 && this.state == State.Alive)
      {       
         this.explosionSound.Play(1f, 0.0f, 0.0f);
         //this.explosionSound.Dispose();
            this.state = State.Dead;        
      }

      if (this.hit && this.state == State.Alive)
      {
        this.timer = 7;
        this.hit = false;
      }
      if (this.timer > 0)
        --this.timer;

      base.Update();
    }

    public new virtual void Draw(SpriteBatch spriteBatch)
    {
      if (this.state != State.Alive)
        return;
      if (this.timer == 0)
        base.Draw(spriteBatch);
      else
        spriteBatch.Draw(this.Flash, this.Position, Color.White);
    }
  }
}
