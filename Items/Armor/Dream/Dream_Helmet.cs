using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Dream
{
	[AutoloadEquip(EquipType.Head)]
	public class Dream_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dream Helmet");
			Tooltip.SetDefault("15% increased thrown damage");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 50, 0);
			item.rare = 5;
			item.defense = 10;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("Dream_Breastplate") && legs.type == mod.ItemType("Dream_Greaves");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.15f;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 0.15f; 
			player.setBonus = "+15% movement speed";
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Nightmare_Bar", 4); //20       04, 10, 06
            recipe.AddIngredient(ItemID.CrystalShard, 12);//48                  12, 20, 16
			recipe.AddIngredient(ItemID.HallowedBar, 8);//36                  08, 16, 12
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}