using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Dream
{
	[AutoloadEquip(EquipType.Body)]
	public class Dream_Breastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Dream Breastplate");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(0, 4, 50, 0);
			item.rare = 5;
			item.defense = 16;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.24f;
			player.thrownVelocity += 0.30f;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Nightmare_Bar", 10); //20      04, 10, 06
            recipe.AddIngredient(ItemID.CrystalShard, 20);//48                  12, 20, 16
			recipe.AddIngredient(ItemID.HallowedBar, 16);//36                 08, 16, 12
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}