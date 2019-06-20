using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;

namespace AAMod.Items.Armor.Dev.Moon
{
    [AutoloadEquip(EquipType.Body)]
    public class MoonRobeA : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Lunar Mage Robe");
            Tooltip.SetDefault(@"24% increased Magic damage & critical strike chance
+200 Max Mana
15% decreased mana consumption");
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
            item.width = 26;
            item.height = 20;
            item.rare = 9;
            item.defense = 42;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .24f;
            player.magicCrit += 24;
            player.statManaMax2 += 200;
            player.manaCost -= .15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MoonRobe", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}