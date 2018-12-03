using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    class FogScreenShaderData : ScreenShaderData
    {
        private int _fogTimer = 300;
        private int _fogTimer2 = 300;
        private Vector2 texturePostion = Vector2.Zero;
        private readonly int textureWidth;
        private readonly int textureHeight;
        private readonly float fogOpacity1 = 0.5f;
        private readonly float fogOpacity2 = 0.4f;
        private Texture2D texture = AAMod.instance.GetTexture("Backgrounds/fog");


        public FogScreenShaderData() : base("foggy")
        {
            Texture2D fogTexture = AAMod.instance.GetTexture("Backgrounds/fog");
            textureWidth = fogTexture.Width;
            textureHeight = fogTexture.Height;
        }

        public override void Apply()
        {

        }

        public override void Update(GameTime gameTime)
        {
            _fogTimer--;
            _fogTimer2 -= 2;
            if (_fogTimer <= 0)
            {
                _fogTimer = texture.Width;
            }

            if (_fogTimer2 <= 0)
            {
                _fogTimer2 = texture.Width;
            }

            Viewport dimension = Main.graphics.GraphicsDevice.Viewport;
            Main.spriteBatch.Begin();
            for (int i = 0; i < dimension.Width + _fogTimer; i += texture.Width)
            {
                for (int j = 0; j < dimension.Height; j += texture.Height)
                {
                    Main.spriteBatch.Draw(texture, new Rectangle(i - _fogTimer, j, 1920, 1080), null, Color.White * fogOpacity1, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }


            for (int i = 0; i < Main.screenWidth + _fogTimer2; i += texture.Width)
            {
                for (int j = 0; j < Main.screenHeight; j += texture.Height)
                {
                    Main.spriteBatch.Draw(texture, new Rectangle(i - _fogTimer2, j, 1920, 1080), null, Color.White * fogOpacity2, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
            Main.spriteBatch.End();
        }
    }
}
