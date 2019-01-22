using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;

namespace AAMod.Backgrounds
{
    public class ShenASky : CustomSky
    {
        
        public static Texture2D PlanetTexture;
        public static Texture2D BGTexture;
        public bool Active;
        public float Intensity;

        public override void OnLoad()
        {
            PlanetTexture = TextureManager.Load("Backgrounds/ShenEclipse");
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
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 1), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 2), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 3), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 4), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 5), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 6), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 7), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 8), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 9), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 10), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 11), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 12), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 13), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 14), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 15), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 16), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 17), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 18), Color.DarkMagenta * 0.05f * Intensity);
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, (Main.screenHeight / 20) * 19), Color.DarkMagenta * 0.05f * Intensity);
                var planetPos = new Vector2((Main.screenWidth / 2), Main.screenHeight / 4);
                spriteBatch.Draw(PlanetTexture, planetPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
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