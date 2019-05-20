using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles
{
    public class ChaosShotP : ModProjectile
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/ChaosShot_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;

            DisplayName.SetDefault("Chaos Beam");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.penetrate = 1;  
            projectile.width = 30;
            projectile.height = 30;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 600;
            projectile.aiStyle = -1;
        }
		
		public override void AI()
		{
            for (int dust = 0; dust < 5; dust++)
            {
                int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, mod.DustType<Dusts.AkumaADust>(), projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[dustId].noGravity = true;
                int dustId3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, mod.DustType<Dusts.YamataADust>(), projectile.velocity.X * 0.2f,
                    projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[dustId3].noGravity = true;
            }
            
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            
            
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.785f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            
            const int aislotHomingCooldown = 0;
            const int homingDelay = 10;
            const float desiredFlySpeedInPixelsPerFrame = 60;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

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
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB));
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB));
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, 0, Main.DiscoB));
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("PerfectChaosE"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
        
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}