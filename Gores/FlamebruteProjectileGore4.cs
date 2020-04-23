using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Gores
{
    public class FlamebruteProjectileGore4 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamebrute Wing");
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.thrown = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 9999;
            aiType = 510;
            projectile.tileCollide = true;
        }
        public void TheStrideGore()
        {
            for (int i = 0; i < 30; i++)
            {
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.RealityDust>(), Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(0, -10), 6, new Color(255, 0, 0, 255), 1f);
                Main.dust[num].noGravity = false;
                Main.dust[num].velocity *= 2.5f;
                Main.dust[num].noLight = true;
            }
            for (int i = 0; i < 30; i++)
            {
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(0, -10), 6, new Color(255, 0, 0, 255), 1f);
                Main.dust[num].noGravity = false;
                Main.dust[num].velocity *= 2.5f;
                Main.dust[num].noLight = true;
            }
            Gore.NewGore(projectile.position, new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), mod.GetGoreSlot("Gores/FlamebruteGore4"), 1f);
            Gore.NewGore(projectile.position, new Vector2(Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(0, -40)), mod.GetGoreSlot("Gores/FlamebruteGore2"), 1f);
        }

        public override void AI()
        {
            if (projectile.scale >= 1.1f)
                projectile.velocity = new Vector2(Main.rand.NextFloat(-.4f, .4f), Main.rand.NextFloat(-.4f, .4f));
            projectile.rotation += projectile.velocity.X/4;
            projectile.velocity.Y += 0.4f;
            projectile.velocity.X *= 0.98f;
            float magVel = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            if (magVel <= 0.4f)
            {
                projectile.ai[0] += 0.004f;
            }
            projectile.scale = 1 + projectile.ai[0];
            if (projectile.scale >= 1.2f)
            {
                projectile.Kill();
            }
            if (Main.rand.Next(2) == 0)
            {
                //int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 123, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-1f, 1f), 6, new Color(255, 217, 184, 255), projectile.scale * 0.5f);
            }
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }
            for (var i = 0; i < 2; i++)
            {
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.RealityDust>(), 0, 0, 6, default, projectile.velocity.X / 10);
                Main.dust[num].noGravity = true;
                Main.dust[num].noLight = false;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition, null, projectile.GetAlpha(lightColor), projectile.rotation, texture2D.Size() / 2f, projectile.scale, 0, 0f);
            return true;
        }

        public override void Kill(int timeLeft)
        {
            TheStrideGore();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity.Y *= -.5f;
            projectile.velocity.X *= -.5f;
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
    }
}
