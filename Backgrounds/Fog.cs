using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using BaseMod;

namespace AAMod.Backgrounds
{
    public class Fog : CustomSky
    {
        public static Texture2D FogTexture;
        public bool Active;
        public float Intensity;
        private float FogTime = 0f;

        public override void OnLoad()
        {
            FogTexture = TextureManager.Load("Backgrounds/fog");
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Intensity = 4f;
                if (FogTime < 1f)
                {
                    FogTime += 0.1f;
                }
            }
            else
            {
                Intensity = 0f;
                if (FogTime > 0f)
                {
                    FogTime -= 0.1f;
                }
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }
        int fogTimer = 0;
        
        public void FogProcess(Mod mod, SpriteBatch spriteBatch)
        {
            fogTimer++;
            Texture2D tex = mod.GetTexture("Backgrounds/fog");
            int tileHeight = tex.Height;
            int tileWidth = tex.Width;
            if (fogTimer > tileWidth/2)
            {
                fogTimer = 0;
            }
            /*for (int y = 0; y < Main.screenHeight; y += tileHeight)
            {
                for (int x = 0; x < Main.screenWidth + fogTimer; x += tileWidth)
                {
                    Color fogColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    BaseDrawing.DrawTexture(spriteBatch, tex, 0, Main.screenPosition + new Vector2(fogTimer + 2, 0), Main.screenWidth, Main.screenHeight, 1f, 0f, 1, 1, new Rectangle(0, 0, tileWidth, tileHeight), fogColor);
                }
            }
            for (int y = 0; y < Main.screenHeight; y += tileHeight)
            {
                for (int x = 0; x < Main.screenWidth + fogTimer; x += tileWidth)
                {
                    Color fogColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    fogColor *= 0.5f;
                    BaseDrawing.DrawTexture(spriteBatch, tex, 0, Main.screenPosition + new Vector2(fogTimer - 2, 0), Main.screenWidth, Main.screenHeight, 1f, 0f, 1, 1, new Rectangle(0, 0, tileWidth, tileHeight), fogColor);
                }
            }*/
            Viewport dimension = Main.graphics.GraphicsDevice.Viewport;

            for (int i = 0; i < dimension.Width + fogTimer; i += tex.Width)
            {
                for (int j = 0; j < dimension.Height; j += tex.Height)
                {
                    spriteBatch.Draw(tex, new Rectangle(i - fogTimer, j, 1920, 1080), null, Color.White * 0.4f, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }


            for (int i = 0; i < Main.screenWidth + fogTimer; i += tex.Width)
            {
                for (int j = 0; j < Main.screenHeight; j += tex.Height)
                {
                    spriteBatch.Draw(tex, new Rectangle(i - fogTimer, j, 1920, 1080), null, Color.White * 0.5f, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (Main.dayTime /*&& ((!AAWorld.downedYamata && !Main.expertMode) || (!AAWorld.downedYamataA && Main.expertMode))*/)
                {
                    /*Player player = Main.player[Main.myPlayer];
                    var FogPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                    spriteBatch.Draw(FogTexture, FogPos, null, Color.White * FogTime * Intensity, 0f, new Vector2(player.Center.X, player.Center.Y), 1f, SpriteEffects.None, 1f);*/
                    FogProcess(AAMod.instance, spriteBatch);
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 4f;
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
            return Active || Intensity == 4f;
        }
    }
}