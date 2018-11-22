using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Dragon
{
	[AutoloadEquip(EquipType.Head)]
	public class Dragon_Helmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Helmet");
			Tooltip.SetDefault("15% increased thrown damage");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 10;
			item.defense = 8;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("Dragon_Breastplate") && legs.type == mod.ItemType("Dragon_Greaves");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.15f;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 0.20f; 
			player.setBonus = "+20% movement speed";
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Dragon_Scale", 6); //24        10, 8, 6
            recipe.AddIngredient(ItemID.HellstoneBar, 3);//12      3, 5, 4
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}