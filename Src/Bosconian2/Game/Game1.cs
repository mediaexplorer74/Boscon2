
// Type: Starkiller.Game1
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
//using Microsoft.Xna.Framework.Storage;
using System;
using System.IO;
using System.Xml.Serialization;

#nullable disable
namespace Starkiller
{
  public class Game1 : Microsoft.Xna.Framework.Game
  {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Gameplay gameplay;
    private Stars stars;
    private Stars smallStars;
    private Stars bigStars;
    private Song music;
    private Song music2;
    private Song music3;
    private Song music4;
    private Song musicM;
    private bool menu = true;
    private bool pause = false;
    private bool credits = false;
    private bool saved = false;
    private Texture2D logo;
    private Texture2D cursor;
    private Texture2D background;
    private Texture2D creditsText;
    private KeyboardState lastKey;
    private GamePadState lastButton;
    private SpriteFont font;
    private int selection = 1;
    private int menuItems = 3;
    private int highScore = 0;
    private StorageDevice device;
    private string containerName = "Starkiller";
    private string filename = "highscore.sav";
    public static bool GodMode = true; // for brave International April,12 Day! =)

    public Game1()
    {
      this.graphics = new GraphicsDeviceManager((Microsoft.Xna.Framework.Game) this);
      this.Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      this.graphics.PreferredBackBufferHeight = 720;
      this.graphics.PreferredBackBufferWidth = 1280;

      //this.graphics.ToggleFullScreen();
      this.graphics.IsFullScreen = true;//false;

      this.graphics.ApplyChanges();

      this.gameplay = new Gameplay(this.Content, this.graphics);
      this.stars = new Stars(this.Content, this.graphics, 4, 12, 3, Color.White);
      this.smallStars = new Stars(this.Content, this.graphics, 2, 15, 2, Color.White);
      this.bigStars = new Stars(this.Content, this.graphics, 6, 10, 4, Color.Cyan);
      MediaPlayer.IsRepeating = true;
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      this.InitiateLoad();
      this.gameplay.LoadContent();
      this.stars.LoadContent();
      this.smallStars.LoadContent();
      this.bigStars.LoadContent();
      this.music = this.Content.Load<Song>("Music/starkiller-level-music");
      this.music2 = this.Content.Load<Song>("Music/Starkiller-Boss-Theme");
      this.music3 = this.Content.Load<Song>("Music/Starkiller-Boss-Theme");
      this.music4 = this.Content.Load<Song>("Music/Starkiller-Boss-Theme");
      this.musicM = this.Content.Load<Song>("Music/Starkiller Menu Theme");
      this.logo = this.Content.Load<Texture2D>("logo");
      this.cursor = this.Content.Load<Texture2D>("cursor");
      this.background = this.Content.Load<Texture2D>("background");
      this.creditsText = this.Content.Load<Texture2D>("credits");
      this.font = this.Content.Load<SpriteFont>("font");
    }

    protected override void UnloadContent()
    {
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


        protected override void Update(GameTime gameTime)
        {
            TouchCollection touchState = TouchPanel.GetState();
            
            KeyboardState kbdState;
            int num1;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) /*|| IsTouchFiring(touchState)*/)
            {
                kbdState = Keyboard.GetState();
                num1 = kbdState.IsKeyDown(Keys.LeftAlt) ? 1 : 0;
            }
            else
                num1 = 0;
            if (num1 != 0)
                this.graphics.ToggleFullScreen();

            if (!this.menu)
            {
                if (!this.pause)
                {
                    kbdState = Keyboard.GetState();
                    int num2;
                    if ((!kbdState.IsKeyDown(Keys.Enter) || !this.lastKey.IsKeyUp(Keys.Enter))/* && !IsTouchFiring(touchState)*/)
                    {
                        if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && this.lastButton.IsButtonUp(Buttons.Start))
                        {
                            kbdState = Keyboard.GetState();
                            num2 = kbdState.IsKeyUp(Keys.LeftAlt) ? 1 : 0;
                        }
                        else
                            num2 = 0;
                    }
                    else
                        num2 = 1;
                    if (num2 != 0)
                        this.pause = true;

                    if (this.gameplay.ship.State == State.Dead)
                    {
                        if (this.gameplay.hud.score > this.highScore)
                            this.highScore = this.gameplay.hud.score;
                        if (!this.saved)
                        {
                            this.InitiateSave();
                            this.saved = true;
                        }
                        kbdState = Keyboard.GetState();
                        int num3;
                        if ((!kbdState.IsKeyDown(Keys.Enter) || !this.lastKey.IsKeyUp(Keys.Enter))/* && !IsTouchFiring(touchState)*/)
                        {
                            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && this.lastButton.IsButtonUp(Buttons.Start))
                            {
                                kbdState = Keyboard.GetState();
                                num3 = kbdState.IsKeyUp(Keys.LeftAlt) ? 1 : 0;
                            }
                            else
                                num3 = 0;
                        }
                        else
                            num3 = 1;
                        if (num3 != 0)
                        {
                            this.gameplay.Reset();
                            this.saved = false;
                            this.pause = false;
                        }
                        else
                        {
                            kbdState = Keyboard.GetState();
                            if ((kbdState.IsKeyDown(Keys.Escape) && this.lastKey.IsKeyUp(Keys.Escape)) ||
                                (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back) && this.lastButton.IsButtonUp(Buttons.Back)))
                            {
                                this.menu = true;
                                this.gameplay.changeSong = true;
                            }
                        }
                    }
                    this.gameplay.Update();
                }
                else
                {
                    kbdState = Keyboard.GetState();
                    int num4;
                    if ((!kbdState.IsKeyDown(Keys.Enter) || !this.lastKey.IsKeyUp(Keys.Enter)) /*&& !IsTouchFiring(touchState)*/)
                    {
                        if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) && this.lastButton.IsButtonUp(Buttons.Start))
                        {
                            kbdState = Keyboard.GetState();
                            num4 = kbdState.IsKeyUp(Keys.LeftAlt) ? 1 : 0;
                        }
                        else
                            num4 = 0;
                    }
                    else
                        num4 = 1;
                    if (num4 != 0)
                    {
                        this.pause = false;
                    }
                    else
                    {
                        kbdState = Keyboard.GetState();
                        if ((kbdState.IsKeyDown(Keys.Escape) && this.lastKey.IsKeyUp(Keys.Escape)) ||
                            (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back) && this.lastButton.IsButtonUp(Buttons.Back)))
                        {
                            if (this.gameplay.hud.score > this.highScore)
                                this.highScore = this.gameplay.hud.score;
                            if (!this.saved)
                            {
                                this.InitiateSave();
                                this.saved = true;
                            }
                            this.menu = true;
                            this.gameplay.changeSong = true;
                            this.pause = false;
                            this.gameplay.Reset();
                            this.saved = false;
                        }
                    }
                }
                if (this.gameplay.changeSong)
                {
                    MediaPlayer.Stop();
                    if (this.menu)
                        MediaPlayer.Play(this.musicM);
                    else if (this.gameplay.enemies.list[0] is Death && this.gameplay.enemies.list[0].state == State.Alive)
                        MediaPlayer.Play(this.music2);
                    else if (this.gameplay.enemies.list[0] is Evil && this.gameplay.enemies.list[0].state == State.Alive)
                        MediaPlayer.Play(this.music3);
                    else if (this.gameplay.enemies.list[0] is Giant && this.gameplay.enemies.list[0].state == State.Alive)
                        MediaPlayer.Play(this.music4);
                    else
                        MediaPlayer.Play(this.music);
                    this.gameplay.changeSong = false;
                }
            }
            else if (!this.credits)
            {
                if (this.gameplay.changeSong)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(this.musicM);
                    this.gameplay.changeSong = false;
                }
                kbdState = Keyboard.GetState();
                int num5;
                if (((kbdState.IsKeyDown(Keys.Enter) && this.lastKey.IsKeyUp(Keys.Enter)) || IsTouchFiring(touchState) ||
                     (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && this.lastButton.IsButtonUp(Buttons.A))) &&
                    this.selection == 1)
                {
                    kbdState = Keyboard.GetState();
                    num5 = kbdState.IsKeyUp(Keys.LeftAlt) ? 1 : 0;
                }
                else
                    num5 = 0;
                if (num5 != 0)
                {
                    this.menu = false;
                    this.gameplay.Reset();
                    this.saved = false;
                    this.gameplay.changeSong = true;
                }
                else
                {
                    kbdState = Keyboard.GetState();
                    int num6;
                    if (((kbdState.IsKeyDown(Keys.Enter) && this.lastKey.IsKeyUp(Keys.Enter)) || IsTouchFiring(touchState) ||
                         (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && this.lastButton.IsButtonUp(Buttons.A))) &&
                        this.selection == 2)
                    {
                        kbdState = Keyboard.GetState();
                        num6 = kbdState.IsKeyUp(Keys.LeftAlt) ? 1 : 0;
                    }
                    else
                        num6 = 0;
                    if (num6 != 0)
                    {
                        this.credits = true;
                    }
                    else
                    {
                        kbdState = Keyboard.GetState();
                        int num7;
                        if (((kbdState.IsKeyDown(Keys.Enter) && this.lastKey.IsKeyUp(Keys.Enter)) || IsTouchFiring(touchState) ||
                             (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && this.lastButton.IsButtonUp(Buttons.A))) &&
                            this.selection == 3)
                        {
                            kbdState = Keyboard.GetState();
                            num7 = kbdState.IsKeyUp(Keys.LeftAlt) ? 1 : 0;
                        }
                        else
                            num7 = 0;
                        if (num7 != 0)
                            this.Exit();
                    }
                }
                kbdState = Keyboard.GetState();
                if (kbdState.IsKeyDown(Keys.Down) && this.lastKey.IsKeyUp(Keys.Down) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown) && this.lastButton.IsButtonUp(Buttons.DPadDown) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickDown) && this.lastButton.IsButtonUp(Buttons.LeftThumbstickDown))
                {
                    if (this.selection < this.menuItems)
                        ++this.selection;
                    else
                        this.selection = 1;
                }
                else
                {
                    kbdState = Keyboard.GetState();
                    if (kbdState.IsKeyDown(Keys.Up) && this.lastKey.IsKeyUp(Keys.Up) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp) && this.lastButton.IsButtonUp(Buttons.DPadUp) ||
                        GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftThumbstickUp) && this.lastButton.IsButtonUp(Buttons.LeftThumbstickUp))
                    {
                        if (this.selection > 1)
                            --this.selection;
                        else
                            this.selection = this.menuItems;
                    }
                }
            }
            else
            {
                kbdState = Keyboard.GetState();
                if (kbdState.IsKeyDown(Keys.Escape) && this.lastKey.IsKeyUp(Keys.Escape) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back) && this.lastButton.IsButtonUp(Buttons.Back))
                    this.credits = false;
            }
            if (!this.pause)
            {
                this.stars.Update();
                this.smallStars.Update();
                this.bigStars.Update();
            }
            this.lastKey = Keyboard.GetState();
            this.lastButton = GamePad.GetState(PlayerIndex.One);
            base.Update(gameTime);
        }

  
    protected override void Draw(GameTime gameTime)
    {
      this.spriteBatch.Begin();
      this.spriteBatch.GraphicsDevice.Clear(Color.Black);
      this.stars.Draw(this.spriteBatch);
      this.smallStars.Draw(this.spriteBatch);
      this.bigStars.Draw(this.spriteBatch);
      if (!this.menu)
      {
        this.gameplay.Draw(this.spriteBatch);
        if (this.pause)
        {
          this.spriteBatch.Draw(this.background, Vector2.Zero, new Color(0, 0, 0, 200));

          this.spriteBatch.DrawString(this.font, 
              "PAUSED", new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) 
              - this.font.MeasureString("PAUSED").X / 2f, (float) (this.graphics.PreferredBackBufferHeight / 2)
              - this.font.MeasureString("PAUSED").Y / 2f), Color.White);

          this.spriteBatch.DrawString(this.font, 
              "Press Enter or Start to continue and Esc or Back to quit", 
              new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2)
              - this.font.MeasureString("Press Enter or Start to continue and Esc or Back to quit").X / 4f,
              (float) ((double) (this.graphics.PreferredBackBufferHeight / 2)
              + (double) this.font.MeasureString("PAUSED").Y / 2.0 + 10.0)), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
        }
        else if (this.gameplay.ship.State == State.Dead)
        {
          this.spriteBatch.Draw(this.background, Vector2.Zero, new Color(0, 0, 0, 200));
          this.spriteBatch.DrawString(this.font, "GAME OVER", new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("GAME OVER").X / 2f, (float) (this.graphics.PreferredBackBufferHeight / 2) - this.font.MeasureString("GAME OVER").Y / 2f), Color.White);
          this.spriteBatch.DrawString(this.font, "Press Enter or Start to try again and Esc or Back to quit", new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("Press Enter or Start to try again and Esc or Back to quit").X / 4f, (float) ((double) (this.graphics.PreferredBackBufferHeight / 2) + (double) this.font.MeasureString("GAME OVER").Y / 2.0 + 10.0)), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
        }
      }
      else if (!this.credits)
      {
        this.spriteBatch.Draw(this.logo, new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2 - this.logo.Width / 2), 100f), Color.White);
        this.spriteBatch.DrawString(this.font, "START", new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("START").X / 2f, (float) ((double) (this.graphics.PreferredBackBufferHeight / 2) - (double) this.font.MeasureString("START").Y / 2.0 + 50.0)), Color.White);
        this.spriteBatch.DrawString(this.font, "CREDITS", new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("CREDITS").X / 2f, (float) ((double) (this.graphics.PreferredBackBufferHeight / 2) - (double) this.font.MeasureString("CREDITS").Y / 2.0 + (double) this.font.MeasureString("START").Y + 20.0 + 50.0)), Color.White);
        this.spriteBatch.DrawString(this.font, "EXIT", new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("EXIT").X / 2f, (float) ((double) (this.graphics.PreferredBackBufferHeight / 2) - (double) this.font.MeasureString("EXIT").Y / 2.0 + (double) this.font.MeasureString("START").Y + (double) this.font.MeasureString("CREDITS").Y + 40.0 + 50.0)), Color.White);
       
                if (this.selection == 1)
        {
          this.spriteBatch.Draw(this.cursor, new Vector2((float) ((double) (this.graphics.PreferredBackBufferWidth / 2) - (double) this.font.MeasureString("START").X / 2.0 - (double) this.cursor.Width - 25.0), (float) (this.graphics.PreferredBackBufferHeight / 2 - this.cursor.Height / 2 + 50)), Color.White);
          this.spriteBatch.Draw(this.cursor, new Rectangle((int) ((double) (this.graphics.PreferredBackBufferWidth / 2) + (double) this.font.MeasureString("START").X / 2.0 + 25.0), this.graphics.PreferredBackBufferHeight / 2 - this.cursor.Height / 2 + 50, this.cursor.Width, this.cursor.Height), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
        }
        else if (this.selection == 2)
        {
          this.spriteBatch.Draw(this.cursor, new Vector2((float) 
              ((double) (this.graphics.PreferredBackBufferWidth / 2) 
              - (double) this.font.MeasureString("CREDITS").X / 2.0 - (double) this.cursor.Width - 25.0), 
              (float) ((double) (this.graphics.PreferredBackBufferHeight / 2 - this.cursor.Height / 2)
              + (double) this.font.MeasureString("START").Y + 20.0 + 50.0)), Color.White);

          this.spriteBatch.Draw(this.cursor, new Rectangle((int) 
              ((double) (this.graphics.PreferredBackBufferWidth / 2)
              + (double) this.font.MeasureString("CREDITS").X / 2.0 + 25.0), 
              (int) ((double) (this.graphics.PreferredBackBufferHeight / 2 - this.cursor.Height / 2) 
              + (double) this.font.MeasureString("START").Y + 20.0 + 50.0), this.cursor.Width, 
              this.cursor.Height), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 
              SpriteEffects.FlipHorizontally, 0.0f);
        }
        else
        {
          this.spriteBatch.Draw(this.cursor, new Vector2((float) 
              ((double) (this.graphics.PreferredBackBufferWidth / 2) 
              - (double) this.font.MeasureString("EXIT").X / 2.0 - (double) this.cursor.Width - 25.0),
              (float) ((double) (this.graphics.PreferredBackBufferHeight / 2 - this.cursor.Height / 2) 
              + (double) this.font.MeasureString("CREDITS").Y
              + (double) this.font.MeasureString("START").Y + 40.0 + 50.0)), Color.White);

          this.spriteBatch.Draw(this.cursor, new Rectangle((int)
              ((double) (this.graphics.PreferredBackBufferWidth / 2) 
              + (double) this.font.MeasureString("EXIT").X / 2.0 + 25.0), 
              (int) ((double) (this.graphics.PreferredBackBufferHeight / 2 - this.cursor.Height / 2)
              + (double) this.font.MeasureString("CREDITS").Y + (double) this.font.MeasureString("START").Y 
              + 40.0 + 50.0), this.cursor.Width, this.cursor.Height), new Rectangle?(), 
              Color.White, 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
        }
        this.spriteBatch.DrawString(this.font, "HIGH SCORE " + (object) this.highScore, 
            new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("HIGH SCORE " + (object) this.highScore).X / 2f, (float) ((double) (this.graphics.PreferredBackBufferHeight / 2) - (double) this.font.MeasureString("HIGH SCORE " + (object) this.highScore).Y / 2.0 + (double) this.font.MeasureString("START").Y + (double) this.font.MeasureString("CREDITS").Y + 175.0)), Color.White);
        this.spriteBatch.DrawString(this.font, "Use Arrows or DPad to select and Enter or A to confirm",
            new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("Use Arrows or DPad to select and Enter or A to confirm").X / 4f, (float) ((double) this.graphics.PreferredBackBufferHeight - (double) this.font.MeasureString("Use Arrows or DPad to select and Enter or A to confirm").Y / 2.0 - 10.0)), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
      }
      else
      {
        this.spriteBatch.Draw(this.creditsText, new Rectangle(0, 0, this.creditsText.Width, this.creditsText.Height),
            Color.White);
        this.spriteBatch.DrawString(this.font, "Press Esc or Back to return to the main menu", 
            new Vector2((float) (this.graphics.PreferredBackBufferWidth / 2) - this.font.MeasureString("Press Esc or Back to return to the main menu").X / 4f, (float) ((double) this.graphics.PreferredBackBufferHeight - (double) this.font.MeasureString("Press Esc or Back to return to the main menu").Y / 2.0 - 10.0)), Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
      }
      this.spriteBatch.End();
      base.Draw(gameTime);
    }

    public object UserDetails { get; private set; }

    private void InitiateSave()
    {
      //RnD
      this.device = (StorageDevice) null;
      StorageDevice.BeginShowSelector(PlayerIndex.One, new AsyncCallback(this.SaveToDevice), (object) null);
    }

        private void SaveToDevice(IAsyncResult result)
        {
            this.device = StorageDevice.EndShowSelector(result);
            if (this.device == null || !this.device.IsConnected)
                return;
            Game1.SaveGame o = new Game1.SaveGame()
            {
                hs = this.highScore
            };
            IAsyncResult result1 = this.device.BeginOpenContainer(this.containerName, 
                (AsyncCallback)null, (object)null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer storageContainer = this.device.EndOpenContainer(result1);
            if (storageContainer.FileExists(this.filename))
                storageContainer.DeleteFile(this.filename);

            Stream file = storageContainer.CreateFile(this.filename);
            new XmlSerializer(typeof(Game1.SaveGame)).Serialize(file, (object)o);
            file.Dispose();
            storageContainer.Dispose();
            result.AsyncWaitHandle.Dispose(); 
        }

    private void InitiateLoad()
    {
      this.device = (StorageDevice) null;
      StorageDevice.BeginShowSelector(PlayerIndex.One, new AsyncCallback(this.LoadFromDevice), (object) null);
    }

        private void LoadFromDevice(IAsyncResult result)
        {
            this.device = StorageDevice.EndShowSelector(result);
            IAsyncResult result1 = this.device.BeginOpenContainer(this.containerName, (AsyncCallback)null, (object)null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer storageContainer = this.device.EndOpenContainer(result1);
            result.AsyncWaitHandle.Dispose(); 
            
            if (!storageContainer.FileExists(this.filename))
                return;
            Stream stream = storageContainer.OpenFile(this.filename, FileMode.Open);
            Game1.SaveGame saveGame = (Game1.SaveGame)new XmlSerializer(typeof(Game1.SaveGame)).Deserialize(stream);
            stream.Dispose();   
            storageContainer.Dispose();
            this.highScore = saveGame.hs;
        }

  
    [Serializable]
    public struct SaveGame
    {
      public int hs;
    }
  }
}
