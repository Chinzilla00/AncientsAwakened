using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace AAMod.Items.Melee
{
    public class Mjolnir : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(@"Forged in the heart of a dying star
Right click to throw
plz dont sue diver");
        }

		public override void SetDefaults()
		{
			item.noMelee = true;
			item.useStyle = 1;
			item.shootSpeed = 16f;
			item.damage = 65;
			item.knockBack = 9f;
			item.width = 14;
			item.height = 28;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 35;
			item.useTime = 35;
			item.noUseGraphic = true;
			item.rare = 9;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.melee = true;
			item.shoot = mod.ProjectileType("Mjolnir");
			item.autoReuse = true;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
			}
		}	
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse != 2)
			{
				item.noMelee = false;
				item.noUseGraphic = false;
				item.shoot = 0;
				return true;
			}
			if (player.altFunctionUse == 2 && player.ownedProjectileCounts[item.shoot] < 1)
			{
				item.noMelee = true;
				item.noUseGraphic = true;
				item.shoot = mod.ProjectileType("Mjolnir");
				return true;
			}
			return false;
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("Electrified"), 300);
			Vector2 vector12 = new Vector2(target.Center.X, target.Center.Y);
			float num75 = 20f;
            Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
			vector2.Y -= 100;
			Vector2 vector13 = vector12 - vector2;
			if (vector13.Y < 0f)
			{
				vector13.Y *= -1f;
			}
			if (vector13.Y < 20f)
			{
				vector13.Y = 20f;
			}
			vector13.Normalize();
			vector13 *= num75;
			float num82 = vector13.X;
			float num83 = vector13.Y;
			float speedX5 = num82;
			float speedY5 = num83 + Main.rand.Next(-5, 5) * 0.02f;
			int L = Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY5, 466, damage, knockback, player.whoAmI, vector13.ToRotation());
			Main.projectile[L].penetrate = -1;
			Main.projectile[L].hostile = false;
			Main.projectile[L].friendly = true;
			Main.projectile[L].melee = true;
			Main.projectile[L].usesLocalNPCImmunity = true;
			Main.projectile[L].localNPCHitCooldown = -1;
		}
	}
}
