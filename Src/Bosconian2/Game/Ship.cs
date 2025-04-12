
// Type: Starkiller.Ship
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SharpDX.XInput;
using System.Collections.Generic;

#nullable disable
namespace Starkiller
{
  internal class Ship : Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    public State State;
    public Weapon Weapon;
    public List<Object> bullets;
    private int cooldown = 0;
    private int side = 1;
    public bool barrier = false;
    private Texture2D Barrier;
    public bool friendActive = false;
    public Friend friend;
    public int friendTimer = 0;
    public Enemies enemies;
    public bool exploded = false;

    public Ship(ContentManager Content, GraphicsDeviceManager Graphics, Enemies enemies)
      : base(Content, Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.bullets = new List<Object>();
      this.enemies = enemies;
      this.Weapon = Weapon.Gun;
      this.State = State.Alive;
      this.friend = new Friend(Content, Graphics, this);
    }

    public new void LoadContent()
    {
      this.LoadContent("ship");
      this.Barrier = this.Content.Load<Texture2D>("barrier");
      this.Position = new Vector2((float) (this.Graphics.PreferredBackBufferWidth / 2 - this.Texture.Width / 2), (float) (this.Graphics.PreferredBackBufferHeight - this.Texture.Height - 5));
    }

        private bool IsTouchFiring(TouchCollection touchState)
        {
            foreach (var touch in touchState)
            {
                if (touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved)
                {
                    return true;
                }
            }
            return false;
        }

        private Vector2 GetTouchMovement(TouchCollection touchState)
        {
            foreach (var touch in touchState)
            {
                if (touch.State == TouchLocationState.Moved)
                {
                    return new Vector2(touch.Position.X - this.Position.X, touch.Position.Y - this.Position.Y) * 0.1f;
                }
            }
            return Vector2.Zero;
        }


        public new void Update()
    {
      if (this.friendActive)
      {
        this.friend.ship.enemies = this.enemies;
        this.friend.Update();
        ++this.friendTimer;
      }
      if (this.friendTimer >= 300)
      {
        this.friendActive = false;
        this.friendTimer = 0;
      }
      KeyboardState keyboardState = Keyboard.GetState();
      GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
      TouchCollection touchState = TouchPanel.GetState();
            
      if (this.State == State.Alive)
      {
        if (this.Weapon == Weapon.Gun)
        {
          if ((keyboardState.IsKeyDown(Keys.Space)
                || gamepadState.IsButtonDown(Buttons.X) 
                || IsTouchFiring(touchState)) && this.cooldown == 0)
          {
            this.bullets.Add((Object) new Bullet(this.Content, this.Graphics, this.Position, 0.0f, 0, 25));
            this.bullets[this.bullets.Count - 1].LoadContent("bullet");
            this.laserSound.Play(0.5f, 0.0f, 0.0f);
            //this.laserSound.Dispose();
            this.cooldown = 15;
          }
        }
        else if (this.Weapon == Weapon.MachineGun)
        {
          if ((keyboardState.IsKeyDown(Keys.Space) 
                        || gamepadState.IsButtonDown(Buttons.X)
                        || IsTouchFiring(touchState)
                        ) && this.cooldown == 0)
          {
            this.bullets.Add((Object) new Bullet(this.Content, this.Graphics, this.Position, 0.0f, this.side, 50));
            this.bullets[this.bullets.Count - 1].LoadContent("bullet");
            this.laserSound.Play(0.5f, 0.0f, 0.0f);
            //this.laserSound.Dispose();
            this.cooldown = 8;
            this.side *= -1;
          }
        }
        else if (this.Weapon == Weapon.Missile)
        {
          if ((keyboardState.IsKeyDown(Keys.Space) 
                        || gamepadState.IsButtonDown(Buttons.X)
                        || IsTouchFiring(touchState)
                        ) && this.cooldown == 0)
          {
            this.bullets.Add((Object) new Missile(this.Content, this.Graphics, this.Position, this.side, this.enemies));
            this.bullets[this.bullets.Count - 1].LoadContent("missile");
            this.missileSound.Play();
            //this.missileSound.Dispose();
            this.cooldown = 20;
            this.side *= -1;
          }
        }
        else if (this.Weapon == Weapon.Spread 
                    && 
                    (keyboardState.IsKeyDown(Keys.Space)
                    || gamepadState.IsButtonDown(Buttons.X)
                    || IsTouchFiring(touchState)) && this.cooldown == 0)
        {
          this.bullets.Add((Object) new Bullet(this.Content, this.Graphics, this.Position,
              MathHelper.ToRadians(15f), 0, 25));
          this.bullets[this.bullets.Count - 1].LoadContent("bullet");
          this.bullets.Add((Object) new Bullet(this.Content, this.Graphics, this.Position,
              MathHelper.ToRadians(-15f), 0, 25));
          this.bullets[this.bullets.Count - 1].LoadContent("bullet");
          this.bullets.Add((Object) new Bullet(this.Content, this.Graphics, 
              this.Position, 0.0f, 0, 25));
          this.bullets[this.bullets.Count - 1].LoadContent("bullet");
          this.laserSound.Play(0.5f, 0.0f, 0.0f);
          //this.laserSound.Dispose();
          this.cooldown = 15;
        }

        if (this.cooldown != 0)
          --this.cooldown;


            /*if (keyboardState.IsKeyDown(Keys.Left) || gamepadState.IsButtonDown(Buttons.LeftThumbstickLeft) || gamepadState.IsButtonDown(Buttons.DPadLeft))
                this.Position = new Vector2(this.Position.X - 6f, this.Position.Y);
            else if (keyboardState.IsKeyDown(Keys.Right) || gamepadState.IsButtonDown(Buttons.LeftThumbstickRight) || gamepadState.IsButtonDown(Buttons.DPadRight))
                this.Position = new Vector2(this.Position.X + 6f, this.Position.Y);
            if (keyboardState.IsKeyDown(Keys.Up) || gamepadState.IsButtonDown(Buttons.LeftThumbstickUp) || gamepadState.IsButtonDown(Buttons.DPadUp))
                this.Position = new Vector2(this.Position.X, this.Position.Y - 6f);
            else if (keyboardState.IsKeyDown(Keys.Down) || gamepadState.IsButtonDown(Buttons.LeftThumbstickDown) || gamepadState.IsButtonDown(Buttons.DPadDown))
                this.Position = new Vector2(this.Position.X, this.Position.Y + 6f);
            */

            Vector2 touchMovement = GetTouchMovement(touchState);
            if (touchMovement != Vector2.Zero)
            {
                this.Position += touchMovement;
            }
            else
            {
                    if 
                    (  keyboardState.IsKeyDown(Keys.Left)
                            || gamepadState.IsButtonDown(Buttons.LeftThumbstickLeft)
                            || gamepadState.IsButtonDown(Buttons.DPadLeft)
                    )
                    {
                        this.Position = new Vector2(this.Position.X - 6f, this.Position.Y);
                    }
                    else if 
                    (  keyboardState.IsKeyDown(Keys.Right)
                            || gamepadState.IsButtonDown(Buttons.LeftThumbstickRight)
                            || gamepadState.IsButtonDown(Buttons.DPadRight)
                    )
                    {
                        this.Position = new Vector2(this.Position.X + 6f, this.Position.Y);
                    }

                    if 
                    ( keyboardState.IsKeyDown(Keys.Up)
                            || gamepadState.IsButtonDown(Buttons.LeftThumbstickUp)
                            || gamepadState.IsButtonDown(Buttons.DPadUp)
                    )
                    {
                        this.Position = new Vector2(this.Position.X, this.Position.Y - 6f);
                    }
                    else if 
                    ( keyboardState.IsKeyDown(Keys.Down)
                            || gamepadState.IsButtonDown(Buttons.LeftThumbstickDown)
                            || gamepadState.IsButtonDown(Buttons.DPadDown)
                    )
                    {
                        this.Position = new Vector2(this.Position.X, this.Position.Y + 6f);
                    }
            }

            this.Position.X = MathHelper.Clamp(this.Position.X, 0.0f, (float) (this.Graphics.PreferredBackBufferWidth - this.Texture.Width));
            this.Position.Y = MathHelper.Clamp(this.Position.Y, 50f, (float) (this.Graphics.PreferredBackBufferHeight - this.Texture.Height));
        
      }
      if (keyboardState.IsKeyUp(Keys.Space) 
                && gamepadState.IsButtonUp(Buttons.X) 
                && !IsTouchFiring(touchState))
        this.cooldown = 3;
      base.Update();
    }



    public new void Draw(SpriteBatch spriteBatch)
    {
      foreach (Object bullet in this.bullets)
        bullet.Draw(spriteBatch);
      if (this.State == State.Alive)
        base.Draw(spriteBatch);
      if (this.barrier)
        spriteBatch.Draw(this.Barrier, this.Position - 5f * Vector2.One, Color.White);
      if (!this.friendActive)
        return;
      this.friend.Draw(spriteBatch);
    }
  }
}
