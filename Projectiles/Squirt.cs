using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Squirt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.penetrate = -1;                       //this is the projectile penetration
            Main.projFrames[projectile.type] = 6;           //this is projectile frames
            projectile.hostile = false;
            projectile.ranged = true;                        //this make the projectile do magic damage
            projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            projectile.ignoreWater = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("sP");
    }

 
        public override void AI()
        {
                                                          //this make that the projectile faces the right way
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            projectile.localAI[0] += 1f;
            projectile.alpha = (int)projectile.localAI[0] * 2;
           
            if (projectile.localAI[0] > 600f) //projectile time left before disappears
            {
                projectile.Kill();
            }
           
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 4, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, Color.White, 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 4, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, Color.White, 1f);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 10)
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
