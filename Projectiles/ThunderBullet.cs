using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ThunderBullet : ModProjectile
	{
        //Thank you Qwerty3.14 for letting us use his Oricalcum bullet code.
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thundershot");
		}

		public override void SetDefaults()
		{
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 5;
            projectile.ranged = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.extraUpdates = 8;
        }

        public bool runOnce = true;
        float maxSpeed;
        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Electric, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].noGravity = false;
            }
            if (runOnce)
            {
                maxSpeed = projectile.velocity.Length();
                runOnce = false;
            }
        }
        public bool firstHit = true;

        NPC ConfirmedTarget;
        NPC possibleTarget;
        float distance;
        float maxDistance = 2000;
        bool foundTarget;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[projectile.owner] = 0;

            for (int k = 0; k < 200; k++)
            {
                possibleTarget = Main.npc[k];
                distance = (possibleTarget.Center - projectile.Center).Length();
                if (distance < maxDistance && possibleTarget.active && !possibleTarget.dontTakeDamage && projectile.localNPCImmunity[k] >= 0 && !possibleTarget.friendly && possibleTarget.lifeMax > 5 && !possibleTarget.immortal && Collision.CanHit(projectile.Center, 0, 0, possibleTarget.Center, 0, 0))
                {
                    ConfirmedTarget = Main.npc[k];
                    foundTarget = true;


                    maxDistance = (ConfirmedTarget.Center - projectile.Center).Length();
                }

            }
            if (foundTarget)
            {
                projectile.velocity = PolarVector(maxSpeed, (ConfirmedTarget.Center - projectile.Center).ToRotation());

            }
            else
            {
                projectile.Kill();
            }
            foundTarget = false;
            maxDistance = 1000;
        }

        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Electric, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 0f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Electric, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 2f;
            }
        }
    }
}

