using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class GrimReaperScytheEX : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grim Reaper Scythe EX");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(274);
			projectile.width = 60;
			projectile.height = 52;
			projectile.penetrate = 12;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 5;
			aiType = 274;
		}
		
		public override void AI()
		{
			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].type != NPCID.TargetDummy)
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 10f / magnitude;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			for (int num298 = 0; num298 < 30; num298++)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 184, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
			}
		}
	}
}