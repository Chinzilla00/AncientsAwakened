using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class ArchwitchBlast : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 240;
            projectile.friendly = true;
            projectile.hostile = false;
        }

        public override void AI()
        {
            for (int i = 0; i < 2; i++)
            {
                int dustId = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.ArchwitchDust>(), projectile.velocity.X * 0.2f,
                    projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[dustId].noGravity = true;
            }

            const float desiredFlySpeedInPixelsPerFrame = 20;
            const float amountOfFramesToLerpBy = 30;

            projectile.ai[0]++;
            if (projectile.ai[0] >= 45)
            {
                projectile.ai[0] = 45; 

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
            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= 400 && ( selectedTarget == -1 || projectile.Distance(Main.npc[selectedTarget].Center) > distance))
                    {
                        selectedTarget = i;
                    }
                }
            }
            return selectedTarget;
        }

        public override void Kill(int timeleft)
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<AmphibiousBoom>(), projectile.damage, projectile.knockBack, projectile.owner, 2, 0);
            int pieCut = 20;
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.ArchwitchDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.ArchwitchDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
        }
    }
}