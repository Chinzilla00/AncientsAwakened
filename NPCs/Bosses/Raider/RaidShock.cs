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
            projectile.scale = 1f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
        }

        public override void AI()
        {
            projectile.frame = 0;
            if (projectile.alpha != 0)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] >= 4f)
                {
                    projectile.alpha -= 90;
                    if (projectile.alpha < 0)
                    {
                        projectile.alpha = 0;
                        projectile.localAI[0] = 2f;
                    }
                }
            }
            if (Vector2.Distance(projectile.Center, new Vector2(projectile.ai[0], projectile.ai[1]) * 16f + Vector2.One * 8f) <= 16f)
            {
                projectile.Kill();
                return;
            }
            if (projectile.alpha == 0)
            {
                projectile.localAI[1] += 1f;
                if (projectile.localAI[1] >= 120f)
                {
                    projectile.Kill();
                    return;
                }
                Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.8f, 0.3f, 0.8f);
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] == 3f)
                {
                    projectile.localAI[0] = 0f;
                    for (int num53 = 0; num53 < 8; num53++)
                    {
                        Vector2 vector7 = Vector2.UnitX * -8f;
                        vector7 += -Vector2.UnitY.RotatedBy((double)((float)num53 * 3.14159274f / 4f), default(Vector2)) * new Vector2(2f, 4f);
                        vector7 = vector7.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
                        int num54 = Dust.NewDust(projectile.Center, 0, 0, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num54].scale = 1.5f;
                        Main.dust[num54].noGravity = true;
                        Main.dust[num54].position = projectile.Center + vector7;
                        Main.dust[num54].velocity = projectile.velocity * 0.66f;
                    }
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