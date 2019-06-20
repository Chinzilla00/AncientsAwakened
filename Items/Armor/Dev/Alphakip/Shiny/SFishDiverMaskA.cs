using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;

namespace AAMod.Items.Armor.Dev.Alphakip.Shiny
{
    [AutoloadEquip(EquipType.Head)]
	public class SFishDiverMaskA : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Shiny Fish Diver's Mask");
            Tooltip.SetDefault(@"So I heard you like mudkips
20% increased Melee/Ranged damage & critical strike chance
13% increased damage resistance and melee speed
Allows for underwater breathing");
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
            item.width = 18;
            item.height = 20;
            item.rare = 9;
            item.defense = 40;
        }

        public override void UpdateEquip(Player player)
        {
            player.breath = player.breathMax;
            player.meleeDamage *= 1.2f;
            player.endurance *= 1.13f;
            player.meleeSpeed *= 1.13f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("SFishDiverJacketA") && legs.type == mod.ItemType("SFishDiverBootsA");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"'Hosing time.'
All of your attacks inflict wet to non-boss enemies
Grants uninhibited liquid movement
The Infinity Gauntlet is now at its max potential
You gain a fishy companion";
            player.gills = true;
            player.GetModPlayer<AAPlayer>(mod).Alpha = true;
            player.AddBuff(mod.BuffType("MudkipS"), 18000);
            player.ignoreWater = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ShinyFishDiverMask", 1);
                recipe.AddIngredient(null, "EXArmor", 1);
                recipe.AddTile(null, "AncientForge");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "FishDiverMaskA", 1);
                recipe.AddIngredient(null, "ShinyCharm", 1);
                recipe.AddTile(null, "AncientForge");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}