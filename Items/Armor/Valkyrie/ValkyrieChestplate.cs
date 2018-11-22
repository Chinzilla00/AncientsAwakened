/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Valkyrie
{
    [AutoloadEquip(EquipType.Body)]
	public class ValkyrieChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Valkyrie Chestplate");
			Tooltip.SetDefault(@"27% increased melee and throwing damage
Enemies are more likely to target you
Hard as ice, light as snow");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 22;
			item.value = 3000000;
			item.rare = 11;
			item.defense = 40;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(102, 204, 255);
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.27f;
			player.thrownDamage *= 1.27f;
			player.aggro += 5;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddIngredient(ItemID.FrostCore, 2);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}*/