using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class MS : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 70;
            projectile.friendly = true;
            projectile.penetrate = 1;                     
            Main.projFrames[projectile.type] = 1;           
            projectile.hostile = false;
            projectile.magic = true;                        
            projectile.tileCollide = false;                 
            projectile.ignoreWater = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Wave");
        }

 
        public override void AI()
        {
                                            
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            projectile.localAI[0] += 1f;
            projectile.alpha = (int)projectile.localAI[0] * 2;
           
            if (projectile.localAI[0] > 130f) 
            {
                projectile.Kill();
            }

        }

        public override void Kill(int timeleft)
        {
            Projectile.NewProjectile((int)projectile.position.X, (int)projectile.position.Y, 0, 0, ModContent.ProjectileType<MSRT>(), projectile.damage, projectile.knockBack, Main.myPlayer);
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -projectile.velocity.X * 0.6f, -projectile.velocity.Y * 0.6f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 1.5f;
                num469 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -projectile.velocity.X * 0.6f, -projectile.velocity.Y * 0.6f, 100);
                Main.dust[num469].velocity *= 1.5f;
            }
        }

          public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
          {
                int num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -projectile.velocity.X * 0.6f, -projectile.velocity.Y * 0.6f, 100, default, 2f);
                Main.dust[num580].noGravity = true;
                Main.dust[num580].velocity *= 1.5f;
                num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.DarkmatterDust>(), -projectile.velocity.X * 0.6f, -projectile.velocity.Y * 0.6f, 100);
                Main.dust[num580].velocity *= 1.5f;
          }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile, Color.White, true);
            projectile.frameCounter++;
            if (projectile.frameCounter >= 10)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3) 
                    projectile.frame = 0; 
            }
            return false;
        }
    }
}
