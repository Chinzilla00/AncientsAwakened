using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Hallam.Shiny
{
    [AutoloadEquip(EquipType.Body)]
    public class ShinyHalTux : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hallam's Velvet Tux");
            Tooltip.SetDefault(
@"This tux was woven with pure class
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
            item.width = 30;
            item.height = 24;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HalTux", 1);
            recipe.AddRecipeGroup("AAMod:ShinyCharm");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}