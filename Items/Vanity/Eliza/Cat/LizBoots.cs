using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Eliza.Cat
{
    [AutoloadEquip(EquipType.Legs)]
	public class LizBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Midnight Cat Boots");
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
            item.width = 22;
            item.height = 18;
            item.rare = ItemRarityID.Purple;
            item.vanity = true;
        }

        public override bool DrawLegs()
        {
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "LizSkirt");
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}