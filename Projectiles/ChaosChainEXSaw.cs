using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;
using System;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class ChaosChainEXSaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Saw");
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 5;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
        }

        private float RingRotation = 0f;

        public bool runOnce = true;
        float maxSpeed;
        public override void AI()
        {
            if (projectile.velocity.X < 0)
            {
                projectile.direction = -1;
            }
            RingRotation += 0.03f * projectile.direction;
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
        float maxDistance = 1200;
        bool foundTarget;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
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
        }

        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }

        public override void Kill(int timeleft)
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<ChaosBoomEX>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 0);
            int pieCut = 20;
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.Discord>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.Discord>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            Texture2D Tex = Main.projectileTexture[projectile.type];
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            BaseDrawing.DrawTexture(spritebatch, Tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, RingRotation, projectile.direction, 1, frame, lightColor, true);
            return false;
        }
    }
}
