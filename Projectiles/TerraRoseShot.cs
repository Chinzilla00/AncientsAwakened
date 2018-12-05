using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TerraRoseShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.MagicMissile);
            projectile.penetrate = 1;  
            projectile.width = 18;
            projectile.height = 18;
            projectile.tileCollide = false;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
			if (Main.rand.NextFloat() < 0.9210526f)
			{
				Vector2 position = projectile.position;
                int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 107, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                Main.dust[dustId].noGravity = true;
			}
		}

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

            DisplayName.SetDefault("TerraPetal");
		}
    }
}
