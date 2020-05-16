using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Vanity.Eliza.Dragon
{
    [AutoloadEquip(EquipType.Head)]
	public class LizHood : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Dark Dragoness' Hood");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(121, 21, 214);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = ItemRarityID.Purple;
            item.vanity = true;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = false;
            drawAltHair = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LizEars");
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}