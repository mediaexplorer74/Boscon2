
// Type: Starkiller.Gameplay
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
  internal class Gameplay
  {
    private ContentManager Content;
    private GraphicsDeviceManager graphics;
    public Ship ship;
    public Enemies enemies;
    public HUD hud;
    private List<Explosion> explosions;
    private List<PowerUp> powerUps;
    private int timer = 0;
    public bool changeSong = true;
    private Texture2D background;

    public Gameplay(ContentManager Content, GraphicsDeviceManager graphics)
    {
      this.Content = Content;
      this.graphics = graphics;
      this.ship = new Ship(Content, graphics, this.enemies);
      this.enemies = new Enemies(Content, graphics, this.ship);
      this.hud = new HUD(Content, graphics);
      this.explosions = new List<Explosion>();
      this.powerUps = new List<PowerUp>();
    }

    public void LoadContent()
    {
      this.ship.LoadContent();
      this.enemies.LoadContent();
      this.hud.LoadContent();
      this.background = this.Content.Load<Texture2D>("background");
    }

    public void Update()
    {
      this.ship.Update();
      this.enemies.Update();
      if (this.enemies.CheckEmpty())
      {
        if (this.timer == 150)
        {
          if ((this.hud.level + 1) % 10 == 0)
          {
            this.enemies.GenerateBoss(this.hud.level);
            this.hud.maxhp = this.enemies.list[0].hp;
            this.hud.boss = this.enemies.list[0];
            this.changeSong = true;
          }
          else
          {
            this.enemies.GenerateSwarm(this.hud.level);
            if (this.hud.level % 10 == 0 && this.hud.level > 0)
              this.changeSong = true;
            this.hud.boss = (Enemy) null;
          }
          this.ship.enemies = this.enemies;
          ++this.hud.level;
          this.timer = 0;
        }
        else
          ++this.timer;
      }
      for (int index1 = 0; index1 < this.enemies.list.Length; ++index1)
      {
        if (this.enemies.list[index1] != null && this.enemies.list[index1].state == State.Dead && !this.enemies.list[index1].exploded)
        {
          this.hud.score += this.enemies.list[index1].points;
          for (int index2 = 0; index2 < this.enemies.list[index1].bullets.Count; ++index2)
          {
            if (index2 < this.enemies.list[index1].bullets.Count && this.enemies.list[index1].bullets[index2].laser)
              this.enemies.list[index1].bullets.RemoveAt(index2);
          }
          for (int index3 = 0; index3 < this.enemies.list[index1].bullets.Count; ++index3)
          {
            if (index3 < this.enemies.list[index1].bullets.Count && this.enemies.list[index1].bullets[index3].laser)
              this.enemies.list[index1].bullets.RemoveAt(index3);
          }
          for (int index4 = 0; index4 < this.enemies.list[index1].bullets.Count; ++index4)
          {
            if (index4 < this.enemies.list[index1].bullets.Count && this.enemies.list[index1].bullets[index4].laser)
              this.enemies.list[index1].bullets.RemoveAt(index4);
          }
          if (this.ship.RNG(0, 3) == 0)
          {
            this.powerUps.Add(new PowerUp(this.Content, this.graphics, this.ship.RNG(1, 5)));
            this.powerUps[this.powerUps.Count - 1].Position = this.enemies.list[index1].Center - new Vector2((float) (this.powerUps[this.powerUps.Count - 1].Texture.Width / 2), (float) (this.powerUps[this.powerUps.Count - 1].Texture.Height / 2));
          }
          this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index1].Center, this.enemies.list[index1].Texture));
          this.enemies.list[index1].exploded = true;
        }
      }
      if (this.ship.bullets.Count > 0)
      {
        for (int index5 = 0; index5 < this.ship.bullets.Count; ++index5)
        {
          if (index5 < this.ship.bullets.Count)
            this.ship.bullets[index5].Update();
          if (this.enemies != null)
          {
            for (int index6 = 0; index6 < this.enemies.list.Length; ++index6)
            {
              if (this.ship.bullets.Count > 0 && index5 < this.ship.bullets.Count && this.enemies.list[index6] != null && this.enemies.list[index6].state == State.Alive && ((this.enemies.list[index6] is Death || this.enemies.list[index6] is Evil) && this.ship.bullets[index5].Hitbox.Intersects(this.enemies.list[index6].Hitbox) || this.ship.bullets[index5].CollidesWith((Object) this.enemies.list[index6])))
              {
                if (!this.enemies.list[index6].barrier)
                {
                  this.enemies.list[index6].hp -= (float) this.ship.bullets[index5].damage;
                  this.enemies.list[index6].hit = true;
                }
                if (this.ship.bullets[index5] is Missile)
                {
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.ship.bullets[index5].Center, this.ship.Texture));
                  this.ship.explosionSound.Play();
                  //this.ship.explosionSound.Dispose();
                }
                else
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.ship.bullets[index5].Center, this.ship.bullets[index5].Texture));
                this.ship.bullets.RemoveAt(index5);
              }
            }
          }
          if (index5 < this.ship.bullets.Count && this.ship.Offscreen(this.ship.bullets[index5]))
            this.ship.bullets.RemoveAt(index5);
          if (this.enemies.list.Length != 0)
          {
            for (int index7 = 0; index7 < this.enemies.list.Length; ++index7)
            {
              if (index7 < this.enemies.list.Length && this.enemies.list[index7] != null && this.enemies.list[index7].bullets.Count > 0)
              {
                for (int index8 = 0; index8 < this.enemies.list[index7].bullets.Count; ++index8)
                {
                  if (index5 < this.ship.bullets.Count && index8 < this.enemies.list[index7].bullets.Count && this.ship.bullets[index5].CollidesWith((Object) this.enemies.list[index7].bullets[index8]) && this.enemies.list[index7].bullets[index8] is EMissile)
                  {
                    this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index7].bullets[index8].Center, this.ship.Texture));
                    this.ship.explosionSound.Play(1f, 0.0f, 0.0f);
                    //this.ship.explosionSound.Dispose();

                    if (this.ship.bullets[index5] is Missile)
                    {
                      this.explosions.Add(new Explosion(this.Content, this.graphics, this.ship.bullets[index5].Center, this.ship.Texture));
                      this.ship.explosionSound.Play();
                      //this.ship.explosionSound.Dispose();
                    }
                    else
                      this.explosions.Add(new Explosion(this.Content, this.graphics, this.ship.bullets[index5].Center, this.ship.bullets[index5].Texture));
                    this.enemies.list[index7].bullets.RemoveAt(index8);
                    this.ship.bullets.RemoveAt(index5);
                  }
                }
              }
            }
          }
        }
      }
      for (int index = 0; index < this.enemies.list.Length; ++index)
      {
        if (this.enemies.list[index] != null && this.enemies.list[index].state == State.Alive 
                    && this.ship.CollidesWith((Object) this.enemies.list[index]) && this.ship.State == State.Alive)
        {
            if (this.ship.barrier)
                this.ship.barrier = false;
            else
            {
              if (!Game1.GodMode)
                 this.ship.State = State.Dead;
            }
        }
      }
      if (this.ship.State == State.Alive)
      {
        for (int index9 = 0; index9 < this.enemies.list.Length; ++index9)
        {
          if (this.enemies.list[index9] != null && this.enemies.list[index9].bullets.Count > 0)
          {
            for (int index10 = 0; index10 < this.enemies.list[index9].bullets.Count; ++index10)
            {
              if (this.ship.friend.CollidesWith((Object) this.enemies.list[index9].bullets[index10]) && this.ship.friendActive)
              {
                if (this.enemies.list[index9].bullets[index10] is EMissile)
                {
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index9].bullets[index10].Center, this.ship.Texture));
                  this.ship.explosionSound.Play(1f, 0.0f, 0.0f);
                  //this.ship.explosionSound.Dispose();
                }
                else if (this.enemies.list[index9].bullets[index10].laser)
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index9].bullets[index10].Center, this.ship.Texture));
                else
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index9].bullets[index10].Center, this.enemies.list[index9].bullets[index10].Texture));
                if (!this.enemies.list[index9].bullets[index10].laser)
                  this.enemies.list[index9].bullets.RemoveAt(index10);
              }
              else if (this.ship.CollidesWith((Object) this.enemies.list[index9].bullets[index10]))
              {
                if (this.ship.barrier)
                    this.ship.barrier = false;
                else
                {
                    if (!Game1.GodMode)
                      this.ship.State = State.Dead;
                }

                if (this.enemies.list[index9].bullets[index10] is EMissile)
                {
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index9].bullets[index10].Center, this.ship.Texture));
                  this.ship.explosionSound.Play(1f, 0.0f, 0.0f);
                  //this.ship.explosionSound.Dispose();
                }
                else if (this.enemies.list[index9].bullets[index10].laser)
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index9].bullets[index10].Center, this.ship.Texture));
                else
                  this.explosions.Add(new Explosion(this.Content, this.graphics, this.enemies.list[index9].bullets[index10].Center, this.enemies.list[index9].bullets[index10].Texture));
                if (!this.enemies.list[index9].bullets[index10].laser)
                  this.enemies.list[index9].bullets.RemoveAt(index10);
              }
            }
          }
        }
      }
      if (this.ship.State == State.Dead && !this.ship.exploded)
      {
        this.explosions.Add(new Explosion(this.Content, this.graphics, this.ship.Center, this.ship.Texture));
        this.ship.explosionSound.Play(1f, 0.0f, 0.0f);
        //this.ship.explosionSound.Dispose();
        this.ship.exploded = true;
      }
      if (this.ship.friendActive && this.ship.friendTimer == 299)
      {
        this.explosions.Add(new Explosion(this.Content, this.graphics, this.ship.friend.Center, this.ship.friend.Texture));
        this.ship.explosionSound.Play(1f, 0.0f, 0.0f);
        //this.ship.explosionSound.Dispose();
      }
      if (this.explosions.Count > 0)
      {
        for (int index = 0; index < this.explosions.Count; ++index)
        {
          this.explosions[index].Update();
          if (this.explosions[index].frame > 9)
            this.explosions.RemoveAt(index);
        }
      }
      if (this.powerUps.Count <= 0)
        return;
      for (int index = 0; index < this.powerUps.Count; ++index)
      {
        if (index < this.powerUps.Count)
        {
          this.powerUps[index].Update();
          if (this.ship.CollidesWith((Object) this.powerUps[index]) && this.powerUps[index].state == State.Alive && this.ship.State == State.Alive)
          {
            if (this.powerUps[index].powerUp == PU.B)
              this.ship.barrier = true;
            else if (this.powerUps[index].powerUp == PU.F)
            {
              this.ship.friendActive = true;
              this.ship.friendTimer = 0;
            }
            else if (this.powerUps[index].powerUp == PU.MG)
              this.ship.Weapon = Weapon.MachineGun;
            else if (this.powerUps[index].powerUp == PU.M)
              this.ship.Weapon = Weapon.Missile;
            else if (this.powerUps[index].powerUp == PU.S)
              this.ship.Weapon = Weapon.Spread;

            this.ship.powerupSound.Play();                        
            //this.ship.powerupSound.Dispose();
            
            this.powerUps.RemoveAt(index);
          }
          else if (this.powerUps[index].state == State.Dead)
            this.powerUps.RemoveAt(index);
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this.ship.Draw(spriteBatch);
      this.enemies.Draw(spriteBatch);
      this.hud.Draw(spriteBatch);
      if (this.timer > 0)
        spriteBatch.DrawString(this.hud.font, "LEVEL " + (object) (this.hud.level + 1), new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.hud.font.MeasureString("LEVEL " + (object) (this.hud.level + 1)).X / 2f, (float) (this.graphics.PreferredBackBufferHeight / 2) - this.hud.font.MeasureString("LEVEL " + (object) (this.hud.level + 1)).Y / 2f), Color.White);
      if (this.explosions.Count > 0)
      {
        for (int index = 0; index < this.explosions.Count; ++index)
          this.explosions[index].Draw(spriteBatch);
      }
      if (this.powerUps.Count <= 0)
        return;
      for (int index = 0; index < this.powerUps.Count; ++index)
        this.powerUps[index].Draw(spriteBatch);
    }

    public void Reset()
    {
      this.hud.level = 0;
      this.hud.score = 0;
      this.hud.boss = (Enemy) null;
      this.changeSong = true;
      for (int index = 0; index < this.enemies.list.Length; ++index)
        this.enemies.list[index] = (Enemy) null;
      this.ship.bullets.Clear();
      this.powerUps.Clear();
      this.ship.State = State.Alive;
      this.ship.Weapon = Weapon.Gun;
      this.ship.friendActive = false;
      this.ship.friendTimer = 0;
      this.ship.exploded = false;
      this.ship.Position = new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2 - this.ship.Texture.Width / 2), (float) (this.graphics.PreferredBackBufferHeight - this.ship.Texture.Height - 5));
    }
  }
}
