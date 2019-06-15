using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using BaseMod;
//using AAMod.NPCs.Bosses.Infinity;

namespace AAMod.Backgrounds
{
    public class VoidSky2 : CustomSky
    {
        private readonly UnifiedRandom random = new UnifiedRandom();

        private struct Bolt
        {
            public Vector2 Position;

            public float Depth;

            public int Life;

            public bool IsAlive;
        }

        public static Texture2D Asteroids1;
        public static Texture2D Asteroids2;
        public static Texture2D Asteroids3;
        public static Texture2D Stars;
        private Bolt[] bolts;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;
        public float Alpha = 0;

        public override void OnLoad()
        {
            Asteroids1 = TextureManager.Load("Backgrounds/Asteroids1");
            Asteroids2 = TextureManager.Load("Backgrounds/Asteroids2");
            Asteroids3 = TextureManager.Load("Backgrounds/Asteroids3");
            Stars =  TextureManager.Load("Backgrounds/Void_Starfield");
        }

        public override void Update(GameTime gameTime)
        {
            if (AAWorld.downedZero)
            {
                Alpha += 0.05f;
                if (Alpha > 1f) Alpha = 1f;
            }

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
        public float LBRotation = 0;
        public NPC IZ;


        public Color infinityGlowRed = new Color(233, 53, 53);
        public Color GetGlowAlpha(bool aura)
        {
            return (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);
        }

        public Color GetAlpha(Color newColor, float alph)
        {
            int alpha = 255 - (int)(255 * alph);
            float alphaDiff = (float)(255 - alpha) / 255f;
            int newR = (int)((float)newColor.R * alphaDiff);
            int newG = (int)((float)newColor.G * alphaDiff);
            int newB = (int)((float)newColor.B * alphaDiff);
            int newA = (int)newColor.A - alpha;
            if (newA < 0) newA = 0;
            if (newA > 255) newA = 255;
            return new Color(newR, newG, newB, newA);
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                var Asteroidpos1 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos2 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos3 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                asteroidPercent1 += 0.004f;
                asteroidPercent2 += 0.005f;
                asteroidPercent3 += 0.006f;
				if(asteroidPercent1 > (float)Math.PI) asteroidPercent1 = 0f;
				if(asteroidPercent2 > (float)Math.PI) asteroidPercent2 = 0f;
				if(asteroidPercent3 > (float)Math.PI) asteroidPercent3 = 0f;
                Rotation -= .0008f;
                LBRotation += .0005f;
                Asteroidpos1.Y += (float)Math.Sin(asteroidPercent1) * 16f;
                Asteroidpos2.Y += (float)Math.Sin(asteroidPercent2) * -30f;
                Asteroidpos3.Y += (float)Math.Sin(asteroidPercent3) * 20f;
                spriteBatch.Draw(Stars, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
                Color astroGlow = Color.White * MathHelper.Lerp(0.7f, 1f, (float)(Main.mouseTextColor / 255f));
				astroGlow.A = (byte)(255f * Intensity);
                spriteBatch.Draw(Asteroids1, Asteroidpos1, null, NPC.downedMoonlord ? astroGlow : Color.White, 0f, new Vector2(Asteroids1.Width >> 1, Asteroids1.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids2, Asteroidpos2, null, NPC.downedMoonlord ? astroGlow : Color.White, 0f, new Vector2(Asteroids2.Width >> 1, Asteroids2.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids3, Asteroidpos3, null, NPC.downedMoonlord ? astroGlow : Color.White, 0f, new Vector2(Asteroids3.Width >> 1, Asteroids3.Height >> 1), 1f, SpriteEffects.None, 1f);
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

            bolts = new Bolt[500];
            for (int i = 0; i < bolts.Length; i++)
            {
                bolts[i].IsAlive = false;
            }
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