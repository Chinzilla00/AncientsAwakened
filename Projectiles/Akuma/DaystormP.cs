using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class DaystormP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daystorm");

            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.ai[0] += 1f;
            int num2 = 0;
            if (projectile.ai[0] >= 40f)
            {
                num2++;
            }
            if (projectile.ai[0] >= 80f)
            {
                num2++;
            }
            if (projectile.ai[0] >= 120f)
            {
                num2++;
            }
            int num3 = 24;
            int num4 = 6;
            projectile.ai[1] += 1f;
            bool flag = false;
            if (projectile.ai[1] >= num3 - num4 * num2)
            {
                projectile.ai[1] = 0f;
                flag = true;
            }
            projectile.frameCounter += 1 + num2;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.soundDelay <= 0)
            {
                projectile.soundDelay = num3 - num4 * num2;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(SoundID.Item91, projectile.position);
                }
            }
            if (projectile.ai[1] == 1f && projectile.ai[0] != 1f)
            {
                Vector2 vector2 = Vector2.UnitX * 24f;
                vector2 = vector2.RotatedBy(projectile.rotation - 1.57079637f, default);
                Vector2 value = projectile.Center + vector2;
                for (int i = 0; i < 2; i++)
                {
                    int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, 135, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 100);
                    Main.dust[num5].velocity *= 0.66f;
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].scale = 1.4f;
                }
            }
            if (flag && Main.myPlayer == projectile.owner)
            {
                bool flag2 = player.channel && player.CheckMana(player.inventory[player.selectedItem].mana, true, false) && !player.noItems && !player.CCed;
                if (flag2)
                {
                }
                else
                {
                    projectile.Kill();
                }
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D14 = mod.GetTexture("Projectiles/Akuma/Daystorm");
            Texture2D Glow = mod.GetTexture("Glowmasks/DaystormP_Glow");
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Color color25 = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            int num215 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y7 = num215 * projectile.frame;
            Vector2 vector27 = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
            float scale5 = 1f;
            if (Main.player[projectile.owner].shroomiteStealth && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].ranged)
            {
                float num216 = Main.player[projectile.owner].stealth;
                if (num216 < 0.03)
                {
                    num216 = 0.03f;
                }
                float arg_97B3_0 = (1f + num216 * 10f) / 11f;
                color25 *= num216;
                scale5 = num216;
            }
            if (Main.player[projectile.owner].setVortex && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].ranged)
            {
                float num217 = Main.player[projectile.owner].stealth;
                if (num217 < 0.03)
                {
                    num217 = 0.03f;
                }
                float arg_9854_0 = (1f + num217 * 10f) / 11f;
                color25 = color25.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num217)));
                scale5 = num217;
            }
            Main.spriteBatch.Draw(texture2D14, vector27, new Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), projectile.GetAlpha(color25), projectile.rotation, new Vector2(texture2D14.Width / 2f, num215 / 2f), projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(Glow, vector27, new Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), Globals.AAColor.Glow * scale5, projectile.rotation, new Vector2(texture2D14.Width / 2f, num215 / 2f), projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}
