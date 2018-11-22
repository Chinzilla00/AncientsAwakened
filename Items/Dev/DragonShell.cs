using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    [AutoloadEquip(EquipType.Shield)]
    public class DragonShell : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Turtle Shell");
            Tooltip.SetDefault(
@"Allows you to dash into enemies, damaging them
Enemies that hit you take 1.5 times the damage done to you and are inflicted with daybroken
Spiked Turtle Shell EX");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.expert = true;
            item.accessory = true;
            item.defense = 30;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(18, 89, 24);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CharlieShell");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>(mod).DragonShell = true;
            player.thorns = 1f;
            player.turtleThorns = true;
            player.dash = 3;
        }
    }
}