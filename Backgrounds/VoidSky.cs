using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace AAMod.Backgrounds
{
    public class VoidSky : CustomSky
    {

        private UnifiedRandom random = new UnifiedRandom();


        public static Texture2D PlanetTexture;
        public static Texture2D Echo;
        public static Texture2D Asteroids1;
        public static Texture2D Asteroids2;
        public static Texture2D Asteroids3;
        public static Texture2D BGTexture;
        public Texture2D boltTexture;
        public Texture2D flashTexture;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;

        public override void OnLoad()
        {
            PlanetTexture = TextureManager.Load("Backgrounds/VoidBH");
            Asteroids1 = TextureManager.Load("Backgrounds/Asteroids1");
            Asteroids2 = TextureManager.Load("Backgrounds/Asteroids2");
            Asteroids3 = TextureManager.Load("Backgrounds/Asteroids3");
            boltTexture = TextureManager.Load("Backgrounds/VoidBolt");
            flashTexture = TextureManager.Load("Backgrounds/VoidFlash");
            Echo = TextureManager.Load("Backgrounds/Echo");
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
        public float asteroidPercent1 = 0f;
        public float asteroidPercent2 = 0f;
        public float asteroidPercent3 = 0f;
        public float Rotation = 0;
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this.Intensity);
                var planetPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var echoPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos1 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos2 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos3 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                asteroidPercent1 += 0.04f;
                asteroidPercent2 += 0.05f;
                asteroidPercent3 += 0.06f;
                Rotation += .001f;
                Asteroidpos1.Y += (float)Math.Sin(asteroidPercent1);
                Asteroidpos1.Y += (float)Math.Sin(asteroidPercent2);
                Asteroidpos1.Y += (float)Math.Sin(asteroidPercent3);
                spriteBatch.Draw(PlanetTexture, planetPos, null, Color.White * 0.9f * Intensity, Rotation, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
                if ((AAWorld.downedZero && !Main.expertMode) || (AAWorld.downedZeroA && Main.expertMode))
                {
                    spriteBatch.Draw(Echo, echoPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(Echo.Width >> 1, Echo.Height >> 1), .6f, SpriteEffects.None, 1f);
                }
                spriteBatch.Draw(Asteroids1, Asteroidpos1, null, Color.White * 0.9f * Intensity, 0f, new Vector2(Asteroids1.Width >> 1, Asteroids1.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids2, Asteroidpos2, null, Color.White * 0.9f * Intensity, 0f, new Vector2(Asteroids2.Width >> 1, Asteroids2.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids3, Asteroidpos3, null, Color.White * 0.9f * Intensity, 0f, new Vector2(Asteroids3.Width >> 1, Asteroids3.Height >> 1), 1f, SpriteEffects.None, 1f);
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