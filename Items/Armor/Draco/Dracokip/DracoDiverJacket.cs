using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Armor.Draco.Dracokip

{
    [AutoloadEquip(EquipType.Body)]
    public class DracoDiverJacket : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Draconian Diver Jacket");
            Tooltip.SetDefault(@"25% increased melee and magic damage
10% increased damage resistance");
        }


        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 9;
            item.value = 3000000;
            item.defense = 49;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(180, 41, 32);
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.25f;
            player.magicDamage *= 1.25f;
            player.endurance *= 1.1f;
            player.statManaMax2 += 100;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DracoPlate", 1);
            recipe.AddIngredient(null, "FishDiverJacket", 1);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}