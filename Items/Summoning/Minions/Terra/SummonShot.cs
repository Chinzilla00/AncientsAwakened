using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions.Terra
{
    public class SummonShot : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Summon Blast");
            Main.projFrames[projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (projectile.position.Y > Main.player[projectile.owner].position.Y - 300f)
            {
                projectile.tileCollide = true;
            }
            if (projectile.position.Y < Main.worldSurface * 16.0)
            {
                projectile.tileCollide = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            Vector2 position = projectile.Center + (Vector2.Normalize(projectile.velocity) * 10f);
            Dust dust20 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.SummonDust>(), 0f, 0f, 0)];
            dust20.position = position;
            dust20.velocity = (projectile.velocity.RotatedBy(1.5707963705062866) * 0.33f) + (projectile.velocity / 4f);
            dust20.position += projectile.velocity.RotatedBy(1.5707963705062866);
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
            dust20 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.SummonDust>(), 0f, 0f, 0)];
            dust20.position = position;
            dust20.velocity = (projectile.velocity.RotatedBy(-1.5707963705062866) * 0.33f) + (projectile.velocity / 4f);
            dust20.position += projectile.velocity.RotatedBy(-1.5707963705062866);
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
        }

        public override void Kill(int timeLeft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.SummonDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.SummonDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.Terrablaze>(), 600);
        }
        

        public override bool PreDraw(SpriteBatch sb, Color lightColor) //this is where the animation happens
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}