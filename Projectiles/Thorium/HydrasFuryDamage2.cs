using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod;

namespace AAMod.Projectiles.Thorium
{
    public class HydrasFuryDamage2 : ModProjectile
    {

		public override void SetDefaults()
		{
			projectile.width = 130;
			projectile.height = 130;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 4;
			aiType = ProjectileID.Bullet;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantCrit)
			{
				crit = true;
			}
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];	
			
			projectile.position.X = player.Center.X - ((float)projectile.width / 2f);
			projectile.position.Y = player.Center.Y - ((float)projectile.height / 2f);
		}
	}
}