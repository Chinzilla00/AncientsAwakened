using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;


namespace AAMod.Items.Armor.Doomsday.Zerokip
{
    [AutoloadEquip(EquipType.Body)]
	public class ZeroDiverJacket : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Zero Diver Jacket");
			Tooltip.SetDefault(@"20% increased melee and ranged damage
8% increased damage resistance");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = 3000000;
			item.rare = 11;
			item.defense = 42;
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.20f;
			player.rangedDamage *= 1.20f;
			player.endurance *= 1.08f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "DoomsdayChestplate", 1);
			recipe.AddIngredient(null, "FishDiverJacket", 1);
			recipe.AddTile(null, "ACS");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}