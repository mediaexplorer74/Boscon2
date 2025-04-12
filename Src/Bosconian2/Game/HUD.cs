
// Type: Starkiller.HUD
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace Starkiller
{
  internal class HUD
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    private Texture2D bar;
    public SpriteFont font;
    public int level = 0;
    public int score = 0;
    public Enemy boss;
    public float maxhp = 0.0f;

    public HUD(ContentManager Content, GraphicsDeviceManager Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
    }

    public void LoadContent()
    {
      this.bar = this.Content.Load<Texture2D>("bar");
      this.font = this.Content.Load<SpriteFont>("font");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.bar, Vector2.Zero, Color.Black);
      if (this.level <= 0)
        return;
      spriteBatch.DrawString(this.font, "LEVEL " + (object) this.level, new Vector2(10f, 10f), Color.White);
      spriteBatch.DrawString(this.font, this.score.ToString(), new Vector2((float) (this.Graphics.PreferredBackBufferWidth - 10) - this.font.MeasureString(this.score.ToString()).X, 10f), Color.White);
      if (this.boss != null)
        spriteBatch.Draw(this.bar, new Rectangle(this.Graphics.PreferredBackBufferWidth / 2 - 180, 10, (int) ((double) this.boss.hp * 360.0 / (double) this.maxhp), (int) this.font.MeasureString("LEVEL").Y), Color.Red);
    }
  }
}
