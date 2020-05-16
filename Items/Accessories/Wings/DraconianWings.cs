using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class DraconianWings : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Sun Wings");
            Tooltip.SetDefault("Allows flight and slow fall");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = ItemRarityID.Red;
            item.accessory = true;
            
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 220;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
            ascentWhenFalling = 0.95f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 4f;
            constantAscend = 0.135f;
        }

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 14f;
			acceleration *= 3.5f;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 15);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}