using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Dev.Moon
{
    [AutoloadEquip(EquipType.Legs)]
	public class MoonBootsA : ModItem
	{
		public override void SetStaticDefaults()
		{
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lunar Mage Boots");
            Tooltip.SetDefault(@"24% increased Magic damage & critical strike chance
+200 Max Mana
15% decreased mana consumption
28% increased movement speed");
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
            item.rare = 9;
            item.defense = 29;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .24f;
            player.meleeCrit += 24;
            player.statManaMax2 += 200;
            player.manaCost -= .15f;
            player.moveSpeed += .28f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MoonBoots", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}