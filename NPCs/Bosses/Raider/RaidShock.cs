using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Raider
{
    public class RaidShock : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.scale = .1f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (projectile.frameCounter++ > 7)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            projectile.ai[0]++;
            if (projectile.ai[0]++ < 300)
            {
                if (projectile.scale < 1)
                {
                    projectile.scale += .05f;
                }
                if (projectile.alpha > 0)
                {
                    projectile.alpha -= 10;
                }
            }
            else
            {
                if (projectile.scale > 0)
                {
                    projectile.scale -= .1f;
                }
                else
                {
                    projectile.active = false;
                }
                if (projectile.alpha < 255)
                {
                    projectile.alpha += 5;
                }
            }

        }


        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item94, projectile.position);
            int num290 = Main.rand.Next(3, 7);
            for (int num291 = 0; num291 < num290; num291++)
            {
                int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 100, default(Color), 2.1f);
                Main.dust[num292].velocity *= 2f;
                Main.dust[num292].noGravity = true;
            }
            if (Main.myPlayer == projectile.owner)
            {
                Rectangle value19 = new Rectangle((int)projectile.Center.X - 40, (int)projectile.Center.Y - 40, 80, 80);
                for (int num293 = 0; num293 < 1000; num293++)
                {
                    if (num293 != projectile.whoAmI && Main.projectile[num293].active && Main.projectile[num293].owner == projectile.owner && Main.projectile[num293].type == 443 && Main.projectile[num293].getRect().Intersects(value19))
                    {
                        Main.projectile[num293].ai[1] = 1f;
                        Main.projectile[num293].velocity = (projectile.Center - Main.projectile[num293].Center) / 5f;
                        Main.projectile[num293].netUpdate = true;
                    }
                }
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, 443, projectile.damage, 0f, projectile.owner, 0f, 0f);
            }
        }



        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 10)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}