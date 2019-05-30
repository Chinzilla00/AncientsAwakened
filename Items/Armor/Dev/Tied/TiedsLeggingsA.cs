using System.Collections.Generic;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.Items.Armor.Dev.Tied
{
    [AutoloadEquip(EquipType.Legs)]
    public class TiedsLeggingsA : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Trousers EX");
            Tooltip.SetDefault(@"Perfect for spooky scary stalking
25% increased Melee damage & critical strike chance
13% increased melee speed
50% increased movement speed");
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
            item.width = 22;
            item.height = 18;
            item.rare = 9;
            item.defense = 26;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .25f;
            player.meleeCrit += 25;
            player.meleeSpeed *= 1.13f;
            player.meleeSpeed *= 1.13f;
            player.moveSpeed *= 1.50f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TiedsLeggings", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}