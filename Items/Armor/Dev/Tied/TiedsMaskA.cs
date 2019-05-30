using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Armor.Dev.Tied
{ 
    [AutoloadEquip(EquipType.Head)]
    public class TiedsMaskA : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Skull");
            Tooltip.SetDefault(@"Perfect for spooky scary stalking
25% increased Melee damage & critical strike chance
13% increased melee speed");

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
            item.width = 18;
            item.height = 20;
            item.rare = 9;
            item.defense = 40;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .25f;
            player.meleeSpeed += .13f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TiedsSuitA") && legs.type == mod.ItemType("TiedsLeggingsA");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"'The soviets have got nothing on me'
All of your melee attacks inflict cursed inferno
A spooky friend lights your way";
            player.GetModPlayer<AAPlayer>(mod).Tied = true;
            if (!player.HasBuff(mod.BuffType("TiedemiesBuff")))
            {
                player.AddBuff(mod.BuffType("TiedemiesBuff"), 2);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TiedsMask", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}