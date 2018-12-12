using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Infinity
{
    public class NovaBurst : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.penetrate = 1;                       //this is the projectile penetration           //this is projectile frames
            projectile.hostile = false;
            projectile.magic = true;                        //this make the projectile do magic damage
            projectile.tileCollide = true;                 //this make that the projectile does not go thru walls
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.aiStyle = -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void Kill(int timeleft)
        {

            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("Supernova"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            float num689 = 500f;
            int num690 = -1;
            for (int num691 = 0; num691 < 200; num691++)
            {
                NPC nPC5 = Main.npc[num691];
                if (nPC5.CanBeChasedBy(this, false) && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC5.position, nPC5.width, nPC5.height))
                {
                    float num692 = (nPC5.Center - projectile.Center).Length();
                    if (num692 < num689)
                    {
                        num690 = num691;
                        num689 = num692;
                    }
                }
            }
        }
    }
}
