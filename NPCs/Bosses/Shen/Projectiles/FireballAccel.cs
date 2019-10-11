using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen.Projectiles
{
    public class FireballAccelR : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/Projectiles/FireballAccelR";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
            Main.projFrames[projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.timeLeft = 720;
            projectile.aiStyle = -1;
            projectile.extraUpdates = 1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            projectile.velocity *= 1f + Math.Abs(projectile.ai[0]);

            Vector2 acceleration = projectile.velocity.RotatedBy(Math.PI / 2);
            acceleration *= projectile.ai[1];
            projectile.velocity += acceleration;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 180);
        }
    }

    public class FireballAccelB : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/Projectiles/FireballAccelB";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
            Main.projFrames[projectile.type] = 4;
        }

        public override void PostAI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hostile = true;
            projectile.timeLeft = 720;
            projectile.aiStyle = -1;
            projectile.extraUpdates = 1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            projectile.velocity *= 1f + Math.Abs(projectile.ai[0]);

            Vector2 acceleration = projectile.velocity.RotatedBy(Math.PI / 2);
            acceleration *= projectile.ai[1];
            projectile.velocity += acceleration;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire>(), 180);
        }
    }
}