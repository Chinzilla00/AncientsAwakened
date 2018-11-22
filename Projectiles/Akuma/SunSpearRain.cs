using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class SunSpearRain : ModProjectile
    {

        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Projectiles/Akuma/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            DisplayName.SetDefault("Ashes");

            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.scale = 0.9f;
            projectile.alpha = 255;
            projectile.melee = true;
            projectile.tileCollide = false;
        }
		
		public override void AI()
		{
            if (WorldGen.SolidTile((int)projectile.position.X / 16, (int)(projectile.position.Y + projectile.velocity.Y) / 16 + 1) || WorldGen.SolidTile((int)(projectile.position.X + (float)projectile.width) / 16, (int)(projectile.position.Y + projectile.velocity.Y) / 16 + 1))
            {
                projectile.Kill();
                return;
            }
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > 5f)
            {
                projectile.alpha -= 50;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            projectile.frame = (int)projectile.ai[1];
            if (projectile.localAI[1] > 20f)
            {
                projectile.localAI[1] = 20f;
                projectile.velocity.Y = projectile.velocity.Y + 0.15f;
            }
            projectile.rotation += Main.windSpeed * 0.2f;
            projectile.velocity.X = projectile.velocity.X + Main.windSpeed * 0.1f;
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}