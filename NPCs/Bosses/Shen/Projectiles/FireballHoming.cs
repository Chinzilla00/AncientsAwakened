using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen.Projectiles
{
    public class FireballHomingR : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/Projectiles/FireballHomingR";

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
            projectile.scale = 4f;
            projectile.aiStyle = -1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            projectile.velocity = projectile.DirectionTo(Main.player[(int)projectile.ai[0]].Center) * projectile.ai[1];
            if (++projectile.localAI[0] > 60)
            {
                projectile.localAI[0] = 0;
                if (Main.netMode != 1)
                {
                    Vector2 vel = Vector2.Normalize(projectile.velocity);
                    const float ai = 0.015f;
                    for (int i = 0; i < 16; ++i)
                    {
                        vel = vel.RotatedBy(Math.PI / 8);
                        Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelR"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                        Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelR"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    }
                }
            }
            projectile.scale -= 3f / 300f;
            if (projectile.scale <= 1)
                projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
            {
                Vector2 vel = Vector2.Normalize(projectile.velocity);
                const float ai = 0.015f;
                for (int i = 0; i < 16; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 8);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelR"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelR"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                }
            }
        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.HydraToxin>(), 180);
        }
    }

    public class FireballHomingB : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Shen/Projectiles/FireballHomingB";

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
            projectile.scale = 4f;
            projectile.aiStyle = -1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            projectile.velocity = projectile.DirectionTo(Main.player[(int)projectile.ai[0]].Center) * projectile.ai[1];
            if (++projectile.localAI[0] > 60)
            {
                projectile.localAI[0] = 0;
                if (Main.netMode != 1)
                {
                    Vector2 vel = Vector2.Normalize(projectile.velocity);
                    const float ai = 0.015f;
                    for (int i = 0; i < 16; ++i)
                    {
                        vel = vel.RotatedBy(Math.PI / 8);
                        Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelB"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                        Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelB"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    }
                }
            }
            projectile.scale -= 3f / 300f;
            if (projectile.scale <= 1)
                projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
            {
                Vector2 vel = Vector2.Normalize(projectile.velocity);
                const float ai = 0.015f;
                for (int i = 0; i < 16; ++i)
                {
                    vel = vel.RotatedBy(Math.PI / 8);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelB"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("FireballAccelB"), projectile.damage, 0f, Main.myPlayer, Math.Abs(ai), 0f);
                }
            }
        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 180);
        }
    }
}