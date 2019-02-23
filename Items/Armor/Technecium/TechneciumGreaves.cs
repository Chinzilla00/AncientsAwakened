using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Technecium
{
	[AutoloadEquip(EquipType.Legs)]
	public class TechneciumGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technecium Greaves");
			Tooltip.SetDefault(@"4% Damage resistance
5% increased movement speed");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 1, 80, 0);
			item.rare = 4;
			item.defense = 13;
		}

		public override void UpdateEquip(Player player)
		{
			player.endurance *= 1.04f;
			player.moveSpeed += 0.5f;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TechneciumBar", 18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}