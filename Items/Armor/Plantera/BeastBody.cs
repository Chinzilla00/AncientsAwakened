using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Plantera
{
	[AutoloadEquip(EquipType.Body)]
	public class BeastBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Beast Breastplate");
			Tooltip.SetDefault("+6% thrown damage"
				+ "\n+24% thrown velocity");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
            item.value = Item.buyPrice(0, 1, 50, 0);
            item.rare = 7;
			item.defense = 26;

        }

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.8f;
			player.thrownVelocity += 0.24f;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PlanteraPetal", 16); //24        10, 8, 6
            recipe.AddIngredient(ItemID.ChlorophyteBar, 14);//30      14, 10, 6
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}