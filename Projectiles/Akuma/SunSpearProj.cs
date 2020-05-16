using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class SunSpearProj : ModProjectile
    {
        public int noTileHitCounter = 120;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.extraUpdates = 2;
            projectile.aiStyle = -1;
        }

        public override void AI()
        {
            if (projectile.position.Y > Main.player[projectile.owner].position.Y - 300f)
            {
                projectile.tileCollide = true;
            }
            if (projectile.position.Y < Main.worldSurface * 16.0)
            {
                projectile.tileCollide = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            Vector2 position = projectile.Center + (Vector2.Normalize(projectile.velocity) * 10f);
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0);
                
                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
			
			int foundTarget = HomeOnTarget();
			if (foundTarget != -1)
			{
				NPC n = Main.npc[foundTarget];
				Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * 30;
				projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / 30);
			}
            if (projectile.numUpdates == 0)
            {
                int num185 = -1;
                float num186 = 60f;
                for (int num187 = 0; num187 < 200; num187++)
                {
                    NPC nPC2 = Main.npc[num187];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num188 = projectile.Distance(nPC2.Center);
                        if (num188 < num186 && Collision.CanHitLine(projectile.Center, 0, 0, nPC2.Center, 0, 0))
                        {
                            num186 = num188;
                            num185 = num187;
                        }
                    }
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 300;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, 1, ModContent.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
			for (int k = 0; k < 8; k++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.283f;
				vel = vel.RotatedBy(rand);
				vel *= 8f;
				int i = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vel.X, vel.Y, mod.ProjectileType("FireTentacle"), projectile.damage/3, 0, Main.myPlayer);
				Main.projectile[i].usesLocalNPCImmunity = true;
				Main.projectile[i].localNPCHitCooldown = 6;
				Main.projectile[i].penetrate = -1;
			}
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
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