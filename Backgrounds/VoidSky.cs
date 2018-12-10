using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using BaseMod;

namespace AAMod.Backgrounds
{
    public class VoidSky : CustomSky
    {

        private UnifiedRandom random = new UnifiedRandom();

        private struct Bolt
        {
            public Vector2 Position;

            public float Depth;

            public int Life;

            public bool IsAlive;
        }

        public static Texture2D PlanetTexture;
        public static Texture2D Echo;
        public static Texture2D Asteroids1;
        public static Texture2D Asteroids2;
        public static Texture2D Asteroids3;
        public static Texture2D BGTexture;
        public static Texture2D LB;
        public static Texture2D boltTexture;
        public static Texture2D flashTexture;
        private Bolt[] bolts;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;

        public override void OnLoad()
        {
            PlanetTexture = TextureManager.Load("Backgrounds/VoidBH");
            Asteroids1 = TextureManager.Load("Backgrounds/Asteroids1");
            Asteroids2 = TextureManager.Load("Backgrounds/Asteroids2");
            Asteroids3 = TextureManager.Load("Backgrounds/Asteroids3");
            Echo = TextureManager.Load("Backgrounds/Echo");
            LB = TextureManager.Load("Backgrounds/LB");
            boltTexture = TextureManager.Load("Backgrounds/VoidBolt");
            flashTexture = TextureManager.Load("Backgrounds/VoidFlash");
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
            if (true) //NPC.downedMoonlord)
            {
                if (ticksUntilNextBolt <= 0)
                {
                    ticksUntilNextBolt = random.Next(5, 20);
                    int num = 0;
                    while (bolts[num].IsAlive && num != bolts.Length - 1)
                    {
                        num++;
                    }
                    bolts[num].IsAlive = true;
                    bolts[num].Position.X = random.NextFloat() * ((float)Main.maxTilesX * 16f + 4000f) - 2000f;
                    bolts[num].Position.Y = random.NextFloat() * 500f;
                    bolts[num].Depth = random.NextFloat() * 8f + 2f;
                    bolts[num].Life = 30;
                }
                ticksUntilNextBolt--;
                for (int i = 0; i < bolts.Length; i++)
                {
                    if (bolts[i].IsAlive)
                    {
                        Bolt[] expr168cp0 = bolts;
                        int expr168cp1 = i;
                        expr168cp0[expr168cp1].Life = expr168cp0[expr168cp1].Life - 1;
                        if (bolts[i].Life <= 0)
                        {
                            bolts[i].IsAlive = false;
                        }
                    }
                }
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
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * Intensity);
                var planetPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var echoPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos1 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos2 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                var Asteroidpos3 = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                asteroidPercent1 += 0.002f;
                asteroidPercent2 += 0.003f;
                asteroidPercent3 += 0.004f;
				if(asteroidPercent1 > (float)Math.PI) asteroidPercent1 = 0f;
				if(asteroidPercent2 > (float)Math.PI) asteroidPercent2 = 0f;
				if(asteroidPercent3 > (float)Math.PI) asteroidPercent3 = 0f;
                Rotation -= .0008f;
                LBRotation += .0005f;
                Asteroidpos1.Y += (float)Math.Sin(asteroidPercent1) * 16f;
                Asteroidpos2.Y += (float)Math.Sin(asteroidPercent2) * -30f;
                Asteroidpos3.Y += (float)Math.Sin(asteroidPercent3) * 20f;
                spriteBatch.Draw(PlanetTexture, planetPos, null, Color.White * 0.9f * Intensity, Rotation, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
				float lightningIntensity = BaseUtility.MultiLerp(((float)Main.player[Main.myPlayer].miscCounter % 100f) / 100f, 0.2f, 0.8f, 0.2f);
                spriteBatch.Draw(LB, planetPos, null, Color.White * 0.9f * Intensity * lightningIntensity, LBRotation, new Vector2(PlanetTexture.Width >> 1, PlanetTexture.Height >> 1), 1f, SpriteEffects.None, 1f);
                if ((AAWorld.downedZero && !Main.expertMode) || (AAWorld.downedZeroA && Main.expertMode))
                {
                    if (!AAWorld.downedIZ)
                    {
                        spriteBatch.Draw(Echo, echoPos, null, GetGlowAlpha(true), 0f, new Vector2(Echo.Width >> 1, Echo.Height >> 1), .6f, SpriteEffects.None, 1f);
                    }
                }
				Color astroGlow = Color.White * MathHelper.Lerp(0.7f, 1f, (float)(Main.mouseTextColor / 255f));
				astroGlow.A = (byte)(255f * Intensity);
                spriteBatch.Draw(Asteroids1, Asteroidpos1, null, astroGlow, 0f, new Vector2(Asteroids1.Width >> 1, Asteroids1.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids2, Asteroidpos2, null, astroGlow, 0f, new Vector2(Asteroids2.Width >> 1, Asteroids2.Height >> 1), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Asteroids3, Asteroidpos3, null, astroGlow, 0f, new Vector2(Asteroids3.Width >> 1, Asteroids3.Height >> 1), 1f, SpriteEffects.None, 1f);
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive && bolts[i].Depth > minDepth && bolts[i].Depth < maxDepth)
                {
                    Vector2 value4 = new Vector2(1f / bolts[i].Depth, 0.9f / bolts[i].Depth);
                    Vector2 position = (bolts[i].Position - value3) * value4 + value3 - Main.screenPosition;
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        Texture2D texture = boltTexture;
                        int life = bolts[i].Life;
                        if (life > 26 && life % 2 == 0)
                        {
                            texture = flashTexture;
                        }
                        float scale2 = (float)life / 30f;
                        spriteBatch.Draw(texture, position, null, Color.White * scale * scale2 * Intensity, 0f, Vector2.Zero, value4.X * 5f, SpriteEffects.None, 0f);
                    }
                }
            }
        }

		public Color infinityGlowRed = new Color(233, 53, 53);
        public Color GetGlowAlpha(bool aura)
        {
            return (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);
        }		
		
        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;

            bolts = new VoidSky.Bolt[500];
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