using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ElectricitySpellP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 11;
            projectile.friendly = true;
            projectile.penetrate = -1;                       //this is the projectile penetration
            Main.projFrames[projectile.type] = 4;           //this is projectile frames
            projectile.hostile = false;
            projectile.magic = true;                        //this make the projectile do magic damage
            projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            projectile.ignoreWater = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("ESP");
    }

 
        public override void AI()
        {
                                                          //this make that the projectile faces the right way
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            projectile.localAI[0] += 1f;
            projectile.alpha = (int)projectile.localAI[0] * 2;
           
            if (projectile.localAI[0] > 200f) //projectile time left before disappears
            {
                projectile.Kill();
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
