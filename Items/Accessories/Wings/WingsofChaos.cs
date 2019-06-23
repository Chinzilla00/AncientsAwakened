using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class WingsofChaos : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wings of Chaos");
            Tooltip.SetDefault("Allows flight and slow fall");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 2;
			item.accessory = true;
            
        }
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 220;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 16f;
			acceleration *= 3.5f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DraconianWings", 1);
            recipe.AddIngredient(null, "DreadWings", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}