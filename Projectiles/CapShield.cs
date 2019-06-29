using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class CapShield : ModProjectile
	{
        //Thank you Qwerty3.14 for letting us use his Oricalcum bullet code.
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield");
		}

		public override void SetDefaults()
		{
            projectile.aiStyle = -1;
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ranged = true;
        }

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 20f, 30, .3f, .3f, false);
        }
    }
}

