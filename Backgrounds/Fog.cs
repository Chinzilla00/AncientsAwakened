using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;

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

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (Main.dayTime /*&& ((!AAWorld.downedYamata && !Main.expertMode) || (!AAWorld.downedYamataA && Main.expertMode))*/)
                {
                    Player player = Main.player[Main.myPlayer];
                    var FogPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                    spriteBatch.Draw(FogTexture, FogPos, null, Color.White * FogTime * Intensity, 0f, new Vector2(player.Center.X, player.Center.Y), 1f, SpriteEffects.None, 1f);
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