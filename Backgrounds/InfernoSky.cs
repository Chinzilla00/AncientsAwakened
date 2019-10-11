using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.Backgrounds
{
    public class InfernoSky : CustomSky
    {
        public static Texture2D demonSun;
        public static Texture2D PlanetTexture;
        public static Texture2D BGTexture;
        public bool Active;
        public float Intensity;
        private struct Meteor
        {
            public Vector2 Position;

            public float Depth;

            public int FrameCounter;

            public float Scale;

            public float StartX;
        }
        private Meteor[] Meteors;
        public static Texture2D MeteorTexture;
        public static Texture2D SkyTex;
        private UnifiedRandom _random = new UnifiedRandom();

        public override void OnLoad()
        {
            PlanetTexture = TextureManager.Load("Backgrounds/Sun");
            demonSun = TextureManager.Load("Backgrounds/DemonSun");
            MeteorTexture = TextureManager.Load("Backgrounds/AkumaMeteors");
            SkyTex = TextureManager.Load("Backgrounds/SkyTex");
        }

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
                if (Main.dayTime)
                {
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                    spriteBatch.Draw(SkyTex, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.OrangeRed * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * Intensity));
                    float num64 = 1f;
                    num64 -= Main.cloudAlpha * 1.5f;
                    if (num64 < 0f)
                    {
                        num64 = 0f;
                    }
                    int num20 = (int)(Main.time / 54000.0 * (Main.screenWidth + Main.sunTexture.Width * 2)) - Main.sunTexture.Width;
                    int num21 = 0;
                    float num22 = 1f;
                    float rotation = (float)(Main.time / 54000.0) * 2f - 7.3f;
                    double bgTop = (-Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0;
                    float rotation2 = (float)(Main.time / 32400.0) * 2f - 7.3f;
                    if (Main.dayTime)
                    {
                        double num26;
                        if (Main.time < 27000.0)
                        {
                            num26 = Math.Pow(1.0 - Main.time / 54000.0 * 2.0, 2.0);
                            num21 = (int)(bgTop + num26 * 250.0 + 180.0);
                        }
                        else
                        {
                            num26 = Math.Pow((Main.time / 54000.0 - 0.5) * 2.0, 2.0);
                            num21 = (int)(bgTop + num26 * 250.0 + 180.0);
                        }
                        num22 = (float)(1.2 - num26 * 0.4);
                    }
                    Color color6 = new Color((byte)(255f * num64), (byte)(Color.White.G * num64), (byte)(Color.White.B * num64), (byte)(255f * num64));
                    if (BaseMod.BasePlayer.HasAccessory(Main.LocalPlayer, AAMod.instance.ItemType<Items.Vanity.HappySunSticker>(), true, true))
                    {
                        Main.spriteBatch.Draw(demonSun, new Vector2(num20, num21 + Main.sunModY), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, demonSun.Width, demonSun.Height)), color6, rotation, new Vector2(PlanetTexture.Width / 2, PlanetTexture.Height / 2), num22, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        Main.spriteBatch.Draw(PlanetTexture, new Vector2(num20, num21 + Main.sunModY), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, PlanetTexture.Width, PlanetTexture.Height)), color6, rotation, new Vector2(PlanetTexture.Width / 2, PlanetTexture.Height / 2), num22, SpriteEffects.None, 0f);
                    }
                }
            }
            int num = -1;
            int num2 = 0;
            Mod mod = AAMod.instance;
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.Akuma.Akuma>()))
            {
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
                Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
                Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
                for (int j = num; j < num2; j++)
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
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
            Meteors = new Meteor[150];
            for (int i = 0; i < Meteors.Length; i++)
            {
                float num = i / (float)Meteors.Length;
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