using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Vanity.Moon.Shiny
{
    [AutoloadEquip(EquipType.Legs)]
	public class ShinyMoonBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lunar Boots");
            Tooltip.SetDefault(@"The boots of a legendary lunar mage
'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(159, 207, 190);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MoonBoots", 1);
            recipe.AddRecipeGroup("AAMod:ShinyCharm");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}