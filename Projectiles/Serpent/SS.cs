using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Serpent
{
	public class SS : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SS");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Shuriken);
			projectile.width = 15;
			projectile.height = 15;
            projectile.ranged = true;
        }
        

		public override void Kill(int timeLeft)
		{
			if (Main.rand.Next(0, 4) == 0)
				Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("SnowflakeSuriken"), 1, false, 0, false, false);

			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.IceDust>());
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
		}

	}
}