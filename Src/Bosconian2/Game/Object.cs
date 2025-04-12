
// Type: Starkiller.Object
// Assembly: Starkiller, Version=3.4.0.456, Culture=neutral, PublicKeyToken=null
// MVID: 9485544D-417F-4E34-8497-6D92CF738EBF
// Modded by [M]edia[E]xplorer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace Starkiller
{
  internal class Object
  {
    private ContentManager Content;
    private GraphicsDeviceManager Graphics;
    public Texture2D Texture;
    public Texture2D Flash;
    public Vector2 Position;
    public Vector2 Center;
    public Rectangle Hitbox;
    public Matrix Transform;
    public float Rotation;
    private static readonly Random random = new Random();
    private static readonly object syncLock = new object();
    public int damage;
    public int points;
    public bool hit = false;
    public SoundEffect laserSound;
    public SoundEffect missileSound;
    public SoundEffect explosionSound;
    public SoundEffect powerupSound;

    public Object(ContentManager Content, GraphicsDeviceManager Graphics)
    {
      this.Content = Content;
      this.Graphics = Graphics;
      this.Position = Vector2.Zero;
    }

    public void LoadContent(string textureName)
    {
      this.Texture = this.Content.Load<Texture2D>(textureName);
      this.Hitbox = new Rectangle((int) this.Position.X, (int) this.Position.Y, this.Texture.Width, this.Texture.Height);
      this.Center = new Vector2(this.Position.X + (float) (this.Texture.Width / 2), this.Position.Y + (float) (this.Texture.Height / 2));
      this.Transform = Matrix.CreateTranslation(new Vector3(this.Position, 0.0f)) * Matrix.CreateRotationZ(this.Rotation);
      this.laserSound = this.Content.Load<SoundEffect>("laser");
      this.missileSound = this.Content.Load<SoundEffect>("pew");
      this.explosionSound = this.Content.Load<SoundEffect>("boom");
      this.powerupSound = this.Content.Load<SoundEffect>("powerup");
    }

    public virtual void Update()
    {
      this.Transform = Matrix.CreateRotationZ(this.Rotation) * Matrix.CreateTranslation(new Vector3(this.Position, 0.0f));
      this.Hitbox = Object.CalculateBoundingRectangle(new Rectangle(0, 0, this.Texture.Width, this.Texture.Height), this.Transform);
      this.Center = new Vector2(this.Position.X + (float) (this.Texture.Width / 2), this.Position.Y + (float) (this.Texture.Height / 2));
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.Texture, this.Position, Color.White);
    }

    public double Distance(Object obj1, Object obj2)
    {
      return Math.Sqrt(Math.Pow((double) (obj1.Hitbox.Center.X - obj2.Hitbox.Center.X), 2.0) + Math.Pow((double) (obj1.Hitbox.Center.Y - obj2.Hitbox.Center.Y), 2.0));
    }

    public bool Offscreen(Object obj)
    {
      Rectangle rectangle = new Rectangle(0, 0, this.Graphics.PreferredBackBufferWidth, this.Graphics.PreferredBackBufferHeight);
      return !obj.Hitbox.Intersects(rectangle);
    }

    public int RNG(int min, int max)
    {
      lock (Object.syncLock)
        return Object.random.Next(min, max + 1);
    }

    public static bool IntersectPixels(Object objectA, Object objectB)
    {
      Matrix transform1 = objectA.Transform;
      Matrix transform2 = objectB.Transform;
      int width1 = objectA.Texture.Width;
      int width2 = objectB.Texture.Width;
      int height1 = objectA.Texture.Height;
      int height2 = objectB.Texture.Height;
      Color[] data1 = new Color[objectA.Texture.Width * objectA.Texture.Height];
      Color[] data2 = new Color[objectB.Texture.Width * objectB.Texture.Height];
      objectA.Texture.GetData<Color>(data1);
      objectB.Texture.GetData<Color>(data2);
      Matrix matrix = transform1 * Matrix.Invert(transform2);
      Vector2 vector2_1 = Vector2.TransformNormal(Vector2.UnitX, matrix);
      Vector2 vector2_2 = Vector2.TransformNormal(Vector2.UnitY, matrix);
      Vector2 vector2_3 = Vector2.Transform(Vector2.Zero, matrix);
      for (int index1 = 0; index1 < height1; ++index1)
      {
        Vector2 vector2_4 = vector2_3;
        for (int index2 = 0; index2 < width1; ++index2)
        {
          int num1 = (int) Math.Round((double) vector2_4.X);
          int num2 = (int) Math.Round((double) vector2_4.Y);
          if (0 <= num1 && num1 < width2 && 0 <= num2 && num2 < height2)
          {
            Color color1 = data1[index2 + index1 * width1];
            Color color2 = data2[num1 + num2 * width2];
            if (color1.A != (byte) 0 && color2.A > (byte) 0)
              return true;
          }
          vector2_4 += vector2_1;
        }
        vector2_3 += vector2_2;
      }
      return false;
    }

    public static Rectangle CalculateBoundingRectangle(Rectangle rectangle, Matrix transform)
    {
      Vector2 result1 = new Vector2((float) rectangle.Left, (float) rectangle.Top);
      Vector2 result2 = new Vector2((float) rectangle.Right, (float) rectangle.Top);
      Vector2 result3 = new Vector2((float) rectangle.Left, (float) rectangle.Bottom);
      Vector2 result4 = new Vector2((float) rectangle.Right, (float) rectangle.Bottom);
      Vector2.Transform(ref result1, ref transform, out result1);
      Vector2.Transform(ref result2, ref transform, out result2);
      Vector2.Transform(ref result3, ref transform, out result3);
      Vector2.Transform(ref result4, ref transform, out result4);
      Vector2 vector2_1 = Vector2.Min(Vector2.Min(result1, result2), Vector2.Min(result3, result4));
      Vector2 vector2_2 = Vector2.Max(Vector2.Max(result1, result2), Vector2.Max(result3, result4));
      return new Rectangle((int) vector2_1.X, (int) vector2_1.Y, (int) ((double) vector2_2.X - (double) vector2_1.X), (int) ((double) vector2_2.Y - (double) vector2_1.Y));
    }

    public bool CollidesWith(Object obj)
    {
      return this.Hitbox.Intersects(obj.Hitbox) && Object.IntersectPixels(this, obj);
    }

    internal void LoadContent() => throw new NotImplementedException();
  }
}
