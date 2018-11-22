using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class EnderSickle : ModProjectile
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
		    DisplayName.SetDefault("Ender Sickle");
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Projectiles/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.DeathSickle);
            projectile.penetrate = -1;  
            projectile.width = 50;
            projectile.height = 54;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;

            projectile.glowMask = customGlowMask;
        }
    }
}
