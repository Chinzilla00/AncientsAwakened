using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles.Zero   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class Vortex : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
            DisplayName.SetDefault("Vortex");
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 5;
            projectile.width = 54;
            projectile.height = 54;          
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 60f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 1000f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 15f;
        }
        int ProjTimer = 0;

        public override void PostAI()
        {
            if (Main.netMode != 1)
            {
                ProjTimer++;
                if (ProjTimer >= 20)
                {
                    ProjTimer = 0;
                    int NPCTarget = Target();

                    if (NPCTarget != -1 && AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<VortexProj>()) < 5)
                    {
                        Projectile.NewProjectile(projectile.position, projectile.velocity, ModContent.ProjectileType<VortexProj>(), projectile.damage, projectile.knockBack, projectile.owner);
                    }
                }
            }
        }

        private int Target()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

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


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 1, frame, lightColor, true);
            return false;
        }
    }
}
