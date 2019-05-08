using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Dusts;

namespace AAMod.Projectiles.Flasks
{
    public class CorruptionFlask : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flask");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.damage = 0;
            projectile.penetrate = -1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            aiType = 48;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = mod.GetTexture("Projectiles/Flasks/CorruptionFlask");
            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, tex.Width, tex.Height, 0, 2);
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 7, frame, lightColor, true);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Shatter, (int)position.X, (int)position.Y);

            int radius = 6;
            int FlaskDust = 111;

            for (int m = 0; m < 20; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, FlaskDust, 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (m / 20) * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < 20; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, FlaskDust, 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (m / 20) * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //circle
                    {
                        WorldGen.Convert(xPosition, yPosition, 1, 1);
                    }
                }
            }
        }
    }
}