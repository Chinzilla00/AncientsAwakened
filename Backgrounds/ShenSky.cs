using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;

namespace AAMod.Backgrounds
{
    public class ShenSky : CustomSky
    {
        
        public static Texture2D Sun;
        public static Texture2D Moon;
        public static Texture2D BGTexture;
        public bool Active;
        public float Intensity;

        public override void OnLoad()
        {
            Moon = TextureManager.Load("Backgrounds/MireMoon");
            Sun = TextureManager.Load("Backgrounds/InfernoSun");
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

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (!Main.dayTime)
                {
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 1), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 2), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 3), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 4), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 5), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 6), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 7), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 8), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 9), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 10), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 11), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 12), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 13), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 14), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 15), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 16), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 17), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 18), Color.Indigo * 0.05f * Intensity);
                    spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 19), Color.Indigo * 0.05f * Intensity);
                    var planetPos = new Vector2((Main.screenWidth / 4f) * 2, Main.screenHeight / 4);
                    spriteBatch.Draw(Moon, planetPos, null, Color.White * 0.9f * this.Intensity, 0f, new Vector2(Moon.Width >> 1, Moon.Height >> 1), 1f, SpriteEffects.None, 1f);
                }
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

                    var planetPos = new Vector2((Main.screenWidth / 4), Main.screenHeight / 4);
                    spriteBatch.Draw(Sun, planetPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(Sun.Width >> 1, Sun.Height >> 1), 1f, SpriteEffects.None, 1f);
                }
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }
        

        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
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