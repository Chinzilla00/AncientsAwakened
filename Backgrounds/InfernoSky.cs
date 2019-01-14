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
        private Texture2D MeteorTexture;
        private UnifiedRandom _random = new UnifiedRandom();

        public override void OnLoad()
        {
            PlanetTexture = TextureManager.Load("Backgrounds/InfernoSun");
            PlanetTexture = TextureManager.Load("Backgrounds/AkumaMeteors");
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
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 1), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 2), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 3), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 4), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 5), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 6), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 7), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 8), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 9), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 10), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 11), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 12), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 13), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 14), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 15), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 16), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 17), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 18), Color.Firebrick * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 19), Color.Firebrick * 0.05f * Intensity);

                    var planetPos = new Vector2((Main.screenWidth / 4) * 1, Main.screenHeight / 4);
                    spriteBatch.Draw(PlanetTexture, planetPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
                }
            }
            int num = -1;
            int num2 = 0;
            Mod mod = AAMod.instance;
            if (NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Akuma.Akuma>()))
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