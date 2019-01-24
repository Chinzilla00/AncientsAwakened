using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.SoC
{
public class CthulhuCannon : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Cthulhu Cannon");
		Tooltip.SetDefault("Fires reality-breaking bombs");
	}

    public override void SetDefaults()
    {
        item.damage = 400;
        item.ranged = true;
        item.width = 98;
        item.height = 32;
        item.useTime = 30;
        item.useAnimation = 30;
        item.useStyle = 5;
        item.noMelee = true;
        item.knockBack = 0f;
        item.value = 5000000;
        item.UseSound = SoundID.Item11;
        item.autoReuse = true;
        item.shootSpeed = 14f;
        item.shoot = mod.ProjectileType("CthulhuBomb");
        item.useAmmo = 771;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.overrideColor = AAColor.Cthulhu;
            }
        }
    }
    
    public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
    	Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("CthulhuBomb"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}