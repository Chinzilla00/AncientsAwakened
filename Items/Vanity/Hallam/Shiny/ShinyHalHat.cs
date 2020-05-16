using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Hallam.Shiny
{
    [AutoloadEquip(EquipType.Head)]
	public class ShinyHalHat : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hallam's Velvet Top Hat");
            Tooltip.SetDefault(
@"You can't help but feel fancy just wearing this
'Great for impersonating Ancients Awakened Devs!'");
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 8, 251);
                }
            }
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 14;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HalHat", 1);
            recipe.AddRecipeGroup("AAMod:ShinyCharm");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}