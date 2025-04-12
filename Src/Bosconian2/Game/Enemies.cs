
// Type: Starkiller.Enemies
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class Enemies : Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    public Enemy[] list;
    private int r;
    private Ship ship;

    public Enemies(ContentManager Content, GraphicsDeviceManager Graphics, Ship ship)
      : base(Content, Graphics)
    {
      this.Graphics = Graphics;
      this.Content = Content;
      this.list = new Enemy[15];
      this.ship = ship;
    }

    public new void LoadContent()
    {
      for (int index = 0; index < this.list.Length; ++index)
      {
        if (this.list[index] != null)
          this.list[index].LoadContent();
      }
    }

    public new void Update()
    {
      for (int index1 = 0; index1 < this.list.Length; ++index1)
      {
        if (this.list[index1] != null)
        {
          if (this.list[index1].state == State.Alive || this.list[index1] is Mine)
            this.list[index1].Update();
          if (this.list[index1].bullets.Count > 0)
          {
            for (int index2 = 0; index2 < this.list[index1].bullets.Count; ++index2)
            {
              this.list[index1].bullets[index2].Update();
              if (this.list[index1].bullets[index2].Offscreen((Object) this.list[index1].bullets[index2]))
                this.list[index1].bullets.RemoveAt(index2);
            }
          }
        }
      }
    }

    public new void Draw(SpriteBatch spriteBatch)
    {
      for (int index1 = 0; index1 < this.list.Length; ++index1)
      {
        if (this.list[index1] != null && this.list[index1].bullets.Count > 0)
        {
          for (int index2 = 0; index2 < this.list[index1].bullets.Count; ++index2)
            this.list[index1].bullets[index2].Draw(spriteBatch);
        }
        if (this.list[index1] != null && this.list[index1].state == State.Alive)
          this.list[index1].Draw(spriteBatch);
      }
    }

    public Enemy GenerateEnemy(int level)
    {
      this.r = this.RNG(1, 100);
      if (this.r == 1)
        return (Enemy) new Invader(this.Content, this.Graphics, level);
      if (this.r <= 50)
        return (Enemy) new Bug(this.Content, this.Graphics, level);
      if (this.r <= 60)
        return (Enemy) new Hunter(this.Content, this.Graphics, this.ship, level);
      if (this.r <= 70)
        return (Enemy) new Mine(this.Content, this.Graphics, this.ship, level);
      if (this.r <= 80)
        return (Enemy) new Viper(this.Content, this.Graphics, level);
      return this.r <= 90 ? (Enemy) new Sniper(this.Content, this.Graphics, this.ship, level) : (Enemy) new Brute(this.Content, this.Graphics, this.ship, level);
    }

    public void GenerateBoss(int level)
    {
      this.r = this.RNG(0, 2);
      if (this.r == 0)
      {
        this.list[0] = (Enemy) new Death(this.Content, this.Graphics, level);
        this.list[0].LoadContent();
        this.list[0].Position = new Vector2((float) (this.Graphics.PreferredBackBufferWidth / 2 - this.list[0].Texture.Width / 2), (float) (this.Graphics.PreferredBackBufferHeight / 2 - this.list[0].Texture.Height / 2));
      }
      else if (this.r == 1)
      {
        this.list[0] = (Enemy) new Evil(this.Content, this.Graphics, this.ship, level);
        this.list[0].LoadContent();
        this.list[0].Barrier = this.Content.Load<Texture2D>("ebarrier");
        this.list[0].Position = new Vector2((float) (this.Graphics.PreferredBackBufferWidth / 2 - this.list[0].Texture.Width / 2), (float) (this.list[0].Texture.Height + 55));
      }
      else
      {
        this.list[0] = (Enemy) new Giant(this.Content, this.Graphics, this.ship, level);
        this.list[0].LoadContent();
        this.list[0].Position = new Vector2(0.0f, -100f);
      }
    }

    public void GenerateSwarm(int level)
    {
      for (int index = 0; index < 15; ++index)
      {
        this.r = this.RNG(1, 100);
        if (index == 2)
        {
          this.list[index] = this.GenerateEnemy(level);
          this.list[index].LoadContent();
          this.list[index].Position = new Vector2((float) (this.Graphics.PreferredBackBufferWidth / 2) - this.list[index].Center.X, (float) (this.Graphics.PreferredBackBufferHeight / 2) - this.list[index].Center.Y);
        }
        else if (this.r <= 50)
        {
          this.list[index] = this.GenerateEnemy(level);
          this.list[index].LoadContent();
        }
      }
      for (int index = 0; index < 15; ++index)
      {
        if (this.list[index] != null)
        {
          if (index < 5)
            this.list[index].Position = new Vector2((float) (100 + index * 250) + (50f - this.list[index].Center.X), (float) (50.0 + (50.0 - (double) this.list[index].Center.Y)));
          else if (index < 10)
            this.list[index].Position = new Vector2((float) (100 + (index - 5) * 250) + (50f - this.list[index].Center.X), (float) (150.0 + (50.0 - (double) this.list[index].Center.Y)));
          else if (index < 15)
            this.list[index].Position = new Vector2((float) (100 + (index - 10) * 250) + (50f - this.list[index].Center.X), (float) (250.0 + (50.0 - (double) this.list[index].Center.Y)));
        }
      }
    }

    public bool CheckEmpty()
    {
      for (int index = 0; index < this.list.Length; ++index)
      {
        if (this.list[index] != null && this.list[index].state == State.Alive)
          return false;
      }
      return true;
    }
  }
}
