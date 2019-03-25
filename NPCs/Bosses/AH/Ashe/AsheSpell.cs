using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    internal class AsheSpell : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            DisplayName.SetDefault("Inferno Magic");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.damage *= 0;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            projectile.velocity *= 0.96f;

            projectile.ai[0]++;

            if (projectile.ai[0] >= 61)
            {
                projectile.alpha += 1;
            }
            else
            {
                projectile.ai[0]++;
            }

            if (projectile.velocity == new Vector2(0, 0) && projectile.ai[1] != 1)
            {
                projectile.ai[1] = 1;
                Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), mod.ProjectileType<AsheSpark>(), 50, 0, projectile.owner);
            }

            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            
        }
        

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 300);

            Kill(0);
        }
    }
}