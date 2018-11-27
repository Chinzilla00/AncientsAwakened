using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class ZeroWings : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Gravity Nullifiers");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 400000;
            item.rare = 2;
			item.accessory = true;
            
        }
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 180;
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
			speed = 14f;
			acceleration *= 3.5f;
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(120, 0, 30);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 15);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}