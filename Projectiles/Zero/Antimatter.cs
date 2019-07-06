using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class Antimatter : ModProjectile
    {
        //Thank you Qwerty3.14 for letting us use his Oricalcum bullet code.
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.extraUpdates = 50;
            projectile.timeLeft = 1000;
            projectile.penetrate = 3;
            projectile.tileCollide = false;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Antimatter");
		}

        public bool runOnce = true;
        float maxSpeed;
        public override void AI()
        {
            if (runOnce)
            {
                maxSpeed = projectile.velocity.Length();
                runOnce = false;
            }
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 9f)
            {
                for (int num447 = 0; num447 < 4; num447++)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * (num447 * 0.25f);
                    projectile.alpha = 255;
                    int num448 = Dust.NewDust(vector33, projectile.width, projectile.height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 200, default(Color), 1f);
                    Main.dust[num448].position = vector33;
                    Main.dust[num448].scale = Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[num448].velocity *= 0.2f;
                    Main.dust[num448].noGravity = true;
                }
                return;
            }
        }
        public bool firstHit = true;

        NPC ConfirmedTarget;
        NPC possibleTarget;
        float distance;
        float maxDistance = 1200;
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
            maxDistance = 300;
        }

        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }

    }
}
