using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class SoulSiphon : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Siphon");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 360;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.ai[0] == 0)
            {
                projectile.ai[1]++;
                projectile.alpha -= 5;
            }
            else
            {
                projectile.alpha += 3;
                projectile.velocity *= .98f;
            }
            
            if (projectile.ai[1] > 180)
            {
                projectile.ai[0] = 1;
            }

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.TargetDummy)
            {
                return;
            }
            float Heal = damage * 0.075f;
            if ((int)Heal == 0)
            {
                return;
            }
            if (Main.player[Main.myPlayer].lifeSteal <= 0f)
            {
                return;
            }
            Main.player[Main.myPlayer].lifeSteal -= Heal;
            int num2 = projectile.owner;
            Projectile.NewProjectile(target.position.X, target.position.Y, 0f, 0f, mod.ProjectileType("SoulSiphonHeal"), 0, 0f, projectile.owner, num2, Heal);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = oldVelocity.X * -1f;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = oldVelocity.Y * -1f;
            }
            return false;
        }
    }
}