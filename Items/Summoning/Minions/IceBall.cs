﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Summoning.Minions
{
    internal class IceBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Ball");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(309);
			projectile.minion = true;
			aiType = 309;
        }

        public override void Kill(int timeLeft)
        {
            int num500 = 10;
			Main.PlaySound(SoundID.Item27, projectile.position);
			int num3;
			for (int num501 = 0; num501 < num500; num501 = num3 + 1)
			{
				int num502 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 185, 0f, 0f, 0);
				if (Main.rand.Next(2) == 0)
				{
					Dust dust = Main.dust[num502];
					dust.velocity *= 2f;
					Main.dust[num502].noGravity = true;
					dust = Main.dust[num502];
					dust.scale *= 1.75f;
				}
				num3 = num501;
			}
			
			Player player = Main.player[projectile.owner];
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = 10f;
			float num82 = projectile.Center.X - vector2.X;
			float num83 = projectile.Center.Y - vector2.Y;
			float num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
			float num85 = num84;
			if ((float.IsNaN(num82) && float.IsNaN(num83)) || (num82 == 0f && num83 == 0f))
			{
				num82 = projectile.direction;
				num83 = 0f;
				num84 = 11f;
			}
			else
			{
				num84 = 11f / num84;
			}
			num82 *= num84;
			num83 *= num84;
			int num117 = 6;
			for (int num118 = 0; num118 < num117; num118++)
			{
				vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (projectile.Center.X - player.position.X), projectile.Center.Y - 600f);
				vector2.X = (vector2.X + player.Center.X) / 2f + Main.rand.Next(-350, 351);
				vector2.Y -= 100 * num118;
				num82 = projectile.Center.X - vector2.X;
				num83 = projectile.Center.Y - vector2.Y;
				float ai2 = num83 + vector2.Y;
				if (num83 < 0f)
				{
					num83 *= -1f;
				}
				if (num83 < 20f)
				{
					num83 = 20f;
				}
				num84 = (float)Math.Sqrt(num82 * num82 + num83 * num83);
				num84 = num75 / num84;
				num82 *= num84;
				num83 *= num84;
				Vector2 vector11 = new Vector2(num82, num83) / 2f;
				int p = Projectile.NewProjectile(vector2.X, vector2.Y, vector11.X*3f, vector11.Y*3f, ProjectileID.Blizzard, projectile.damage, 8f, player.whoAmI);
				Main.projectile[p].usesLocalNPCImmunity = true;
				Main.projectile[p].localNPCHitCooldown = 1;
				Main.projectile[p].tileCollide = false;
				Main.projectile[p].magic = false;
				Main.projectile[p].minion = true;
			}
        }
    }
}