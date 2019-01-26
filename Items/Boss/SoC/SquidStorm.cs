using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.SoC
{
public class SquidStorm : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Squid Storm");
		Tooltip.SetDefault("Casts tentacles from the R'lyehian depths");
	}

    public override void SetDefaults()
    {
        item.damage = 300;
        item.magic = true;
        item.mana = 14;
        item.width = 28;
        item.crit = 3;
        item.height = 32;
        item.useTime = 6;
        item.useAnimation = 28;
        item.useStyle = 5;
        item.noMelee = true; //so the item's animation doesn't do damage
        item.knockBack = 3.5f;
        item.value = Item.buyPrice(1, 0, 0, 0);
        item.rare = 5;
        item.UseSound = SoundID.Item103;
        item.autoReuse = true;
        item.shoot = mod.ProjectileType("CthulhuTentacle");
        item.shootSpeed = 12f;
    }
    
    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
    	int i = Main.myPlayer;
		int num73 = damage;
		float num74 = knockBack;
    	num74 = player.GetWeaponKnockback(item, num74);
    	player.itemTime = item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
    	Vector2 value2 = new Vector2(num78, num79);
		value2.Normalize();
		Vector2 value3 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
		value3.Normalize();
		value2 = value2 * 4f + value3;
		value2.Normalize();
		value2 *= item.shootSpeed;
		float num91 = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num91 *= -1f;
		}
		float num92 = (float)Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num92 *= -1f;
		}
		Projectile.NewProjectile(vector2.X, vector2.Y, value2.X, value2.Y, mod.ProjectileType("CthulhuTentacle"), num73, num74, i, num92, num91);
    	return false;
	}
}}