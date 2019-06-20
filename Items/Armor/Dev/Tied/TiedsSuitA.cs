using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;

namespace AAMod.Items.Armor.Dev.Tied
{
    [AutoloadEquip(EquipType.Body)]
	class TiedsSuitA : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Suit");
            Tooltip.SetDefault(@"Perfect for spooky scary stalking
24% increased Melee damage & critical strike chance");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 105, 0);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 9;
            item.defense = 42;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .25f;
            player.meleeCrit += 25;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TiedsSuit", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = false;
		}
    }
}
