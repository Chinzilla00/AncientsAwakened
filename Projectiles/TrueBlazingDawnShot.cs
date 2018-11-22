using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TrueBlazingDawnShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.LightBeam);
            projectile.penetrate = 4;  
            projectile.width = 42;
            projectile.height = 42;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust;
                Vector2 position = projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 29, 0f, 0f, 60, new Color(0, 242, 255), 1.381579f)];
                dust.noGravity = true;
                dust.fadeIn = 1.421053f;
            }
            {
                Dust dust;
                Vector2 position = projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 29, 0f, 0f, 60, new Color(0, 242, 255), 1.381579f)];
                dust.noGravity = true;
                dust.fadeIn = 1.421053f;
            }
            {
                Dust dust;
                Vector2 position = projectile.position;
                dust = Main.dust[Dust.NewDust(position, 30, 30, 29, 0f, 0f, 60, new Color(0, 242, 255), 1.381579f)];
                dust.noGravity = true;
                dust.fadeIn = 1.421053f;
            }
        }

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
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Projectiles/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
            DisplayName.SetDefault("Dawn Ray");
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 500);
        }
    }
}