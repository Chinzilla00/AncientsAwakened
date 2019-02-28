using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.EFish
{
    public class Fishnado : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishnado");
			Main.projFrames[projectile.type] = 6;
        }
		
        public override void SetDefaults()
        {
            projectile.CloneDefaults(407);
			aiType = 407;
        }
		
		public override void AI()
		{
			if (++projectile.frameCounter >= 60)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame > 5)
                {
                    projectile.frame = 0;
                }
            }
		}
    }
}