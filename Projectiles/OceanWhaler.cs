using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class OceanWhaler : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Harpoon");
        }
        

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.ranged = true;
        }

        public override void AI()
        {
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            if (projectile.type != 481)
            {
                Main.player[projectile.owner].itemAnimation = 5;
                Main.player[projectile.owner].itemTime = 5;
            }
            if (projectile.alpha == 0)
            {
                if (projectile.position.X + projectile.width / 2 > Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2)
                {
                    Main.player[projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[projectile.owner].ChangeDir(-1);
                }
            }
            Vector2 vector14 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
            float num166 = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - vector14.X;
            float num167 = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - vector14.Y;
            float num168 = (float)Math.Sqrt(num166 * num166 + num167 * num167);
            if (projectile.ai[0] == 0f)
            {
                if (num168 > 700f)
                {
                    projectile.ai[0] = 1f;
                }
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                projectile.ai[1] += 1f;
                if (projectile.ai[1] > 5f)
                {
                    projectile.alpha = 0;
                }
                if (projectile.ai[1] >= 10f)
                {
                    projectile.ai[1] = 15f;
                    projectile.velocity.Y = projectile.velocity.Y + 0.3f;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
                projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 20f;
                if (projectile.type == 262)
                {
                    num169 = 30f;
                }
                if (num168 < 50f)
                {
                    projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                projectile.velocity.X = num166;
                projectile.velocity.Y = num167;
                if (projectile.type == 262 && projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = 1;
                }
                else if (projectile.type == 262)
                {
                    projectile.spriteDirection = -1;
                }
                if (projectile.type == 271 && projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = 1;
                    return;
                }
                if (projectile.type == 271)
                {
                    projectile.spriteDirection = -1;
                    return;
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1f;
            projectile.netUpdate = true;
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("AAMod/Projectiles/OceanWhaler_Chain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }

    }
}
         