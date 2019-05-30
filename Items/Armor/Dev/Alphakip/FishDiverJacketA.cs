using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Armor.Dev.Alphakip
{
    [AutoloadEquip(EquipType.Body)]
    public class FishDiverJacketA : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fish Diver's Jacket EX");
            Tooltip.SetDefault(@"This jacket is so well insulated, you could sit in the ocean and still come out dry
20% increased Melee/Ranged damage & critical strike chance
13% increased damage resistance and melee speed
25% reduced ammo consumption");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(39, 115, 189);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 9;
            item.defense = 50;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.2f;
            player.rangedDamage *= 1.2f;
            player.endurance *= 1.13f;
            player.meleeSpeed *= 1.13f;
            player.ammoCost75 = true;
            player.buffImmune[BuffID.Wet] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FishDiverJacket", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}