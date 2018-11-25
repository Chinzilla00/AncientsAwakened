using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class ZeroStarP : ModProjectile
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
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;
        }
        public override void SetDefaults()
		{

            projectile.CloneDefaults(ProjectileID.LightDisc);
            aiType = ProjectileID.LightDisc;
            // while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
            projectile.width = 46;
			projectile.height = 46;
			projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
        }
	}
}
