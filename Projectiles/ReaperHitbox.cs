using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles
{
	public class ReaperHitbox : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reaper Hitbox");
		}

		public override void SetDefaults()
		{
			projectile.width = 120;
			projectile.height = 120;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
			projectile.tileCollide = false;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.Center = player.Center;
		}
		
		public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner]; 
			if (player.HasBuff(mod.BuffType("ReaperImmune2")))
			{
				damage *= 15;
			}
			else if (player.HasBuff(mod.BuffType("ReaperImmune")))
			{
				damage *= 10;
			}
		}
	}
}
