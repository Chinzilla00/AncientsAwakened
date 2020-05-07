using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Core.Projectiles
{
    class Snowflakes : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(344);
            projectile.aiStyle = 1;
            aiType = 344;
            projectile.friendly = false;
            projectile.hostile = true;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 1000);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item27, projectile.position);
            int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.SnowDustLight>(), 0f, 0f, 100, Color.White, 1f);
            Main.dust[dustID].noLight = false;
            Main.dust[dustID].noGravity = true;
        }
    }
}