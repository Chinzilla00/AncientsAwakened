using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace AAMod.Backgrounds
{
    public class ShenASky : CustomSky
    {
        
        public static Texture2D PlanetTexture;
        public static Texture2D BGTexture;
        public static Texture2D SkyTex;
        public static Texture2D MeteorTexture;
        public bool Active;
        public float Intensity;
        private UnifiedRandom _random = new UnifiedRandom();
        private struct Meteor
        {
            public Vector2 Position;

            public float Depth;

            public int FrameCounter;

            public float Scale;

            public float StartX;
        }
        private Meteor[] Meteors;

        public override void OnLoad()
        {
            MeteorTexture = TextureManager.Load("Backgrounds/ShenMeteor");
            SkyTex = TextureManager.Load("Backgrounds/Stars");
            PlanetTexture = TextureManager.Load("Backgrounds/ShenEclipse");
        }

        float num = 1200f;

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
            }

            for (int i = 0; i < Meteors.Length; i++)
            {
                Meteor[] expr_60_cp_0_cp_0 = Meteors;
                int expr_60_cp_0_cp_1 = i;
                expr_60_cp_0_cp_0[expr_60_cp_0_cp_1].Position.X = expr_60_cp_0_cp_0[expr_60_cp_0_cp_1].Position.X - num * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Meteor[] expr_8E_cp_0_cp_0 = Meteors;
                int expr_8E_cp_0_cp_1 = i;
                expr_8E_cp_0_cp_0[expr_8E_cp_0_cp_1].Position.Y = expr_8E_cp_0_cp_0[expr_8E_cp_0_cp_1].Position.Y + num * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if ((double)Meteors[i].Position.Y > Main.worldSurface * 16.0)
                {
                    Meteors[i].Position.X = Meteors[i].StartX;
                    Meteors[i].Position.Y = -10000f;
                }
            }
        }

        

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                Vector2 SkyPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                Vector2 PlanetPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 4);
                spriteBatch.Draw(SkyTex, SkyPos, null, Color.DarkMagenta, 0f, new Vector2(SkyTex.Width >> 1, SkyTex.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(PlanetTexture, SkyPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
            }
            int num = -1;
            int num2 = 0;

            for (int i = 0; i < Meteors.Length; i++)
            {
                float depth = Meteors[i].Depth;
                if (num == -1 && depth < maxDepth)
                {
                    num = i;
                }
                if (depth <= minDepth)
                {
                    break;
                }
                num2 = i;
            }
            if (num == -1)
            {
                return;
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int j = (int)num; j < num2; j++)
            {
                Vector2 value4 = new Vector2(1f / Meteors[j].Depth, 0.9f / Meteors[j].Depth);
                Vector2 position = (Meteors[j].Position - value3) * value4 + value3 - Main.screenPosition;
                int num3 = Meteors[j].FrameCounter / 3;
                Meteors[j].FrameCounter = (Meteors[j].FrameCounter + 1) % 12;
                if (rectangle.Contains((int)position.X, (int)position.Y))
                {
                    spriteBatch.Draw(MeteorTexture, position, new Rectangle?(new Rectangle(0, num3 * (MeteorTexture.Height / 4), MeteorTexture.Width, MeteorTexture.Height / 4)), Color.White * scale * Intensity, 0f, Vector2.Zero, value4.X * 5f * Meteors[j].Scale, SpriteEffects.None, 0f);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
            Meteors = new Meteor[150];
            for (int i = 0; i < Meteors.Length; i++)
            {
                float num = (float)i / (float)Meteors.Length;
                Meteors[i].Position.X = num * (Main.maxTilesX * 16f) + this._random.NextFloat() * 40f - 20f;
                Meteors[i].Position.Y = this._random.NextFloat() * -((float)Main.worldSurface * 16f + 10000f) - 10000f;
                if (this._random.Next(3) != 0)
                {
                    Meteors[i].Depth = this._random.NextFloat() * 3f + 1.8f;
                }
                else
                {
                    Meteors[i].Depth = this._random.NextFloat() * 5f + 4.8f;
                }
                Meteors[i].FrameCounter = this._random.Next(12);
                Meteors[i].Scale = this._random.NextFloat() * 0.5f + 1f;
                Meteors[i].StartX = Meteors[i].Position.X;
            }
            Array.Sort(Meteors, new Comparison<Meteor>(this.SortMethod));
        }
        private int SortMethod(Meteor meteor1, Meteor meteor2)
        {
            return meteor2.Depth.CompareTo(meteor1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            Active = false;
        }

        public override void Reset()
        {
            Active = false;
        }

        public override bool IsActive()
        {
            return Active || Intensity > 0.001f;
        }
    }
}