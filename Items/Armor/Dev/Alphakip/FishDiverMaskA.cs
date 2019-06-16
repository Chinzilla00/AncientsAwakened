using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;


namespace AAMod.Items.Armor.Dev.Alphakip
{
    [AutoloadEquip(EquipType.Head)]
	public class FishDiverMaskA : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fish Diver's Mask");
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
            player.meleeDamage += .2f;
            player.rangedDamage += .2f;
            player.endurance *= 1.13f;
            player.meleeSpeed *= 1.13f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("FishDiverJacketA") && legs.type == mod.ItemType("FishDiverBootsA");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"'Hosing time.'
All of your attacks inflict wet to non-boss enemies
While submerged in water, defense is increased by 5
While submerged in water all stats, melee damage, and ranged damage are increased by 25%
Grants uninhibited liquid movement
The Infinity Gauntlet is now at its max potential
You gain a fishy companion";
            player.gills = true;
            player.GetModPlayer<AAPlayer>(mod).Alpha = true;
            if (!player.HasBuff(mod.BuffType("Mudkip")))
            {
                player.AddBuff(mod.BuffType("Mudkip"), 2);
            }
            player.ignoreWater = true;
            if (player.wet && !player.lavaWet && !player.honeyWet)
            {
                player.meleeDamage += .25f;
                player.rangedDamage += .25f;
                player.statDefense += 5;
                player.moveSpeed += .25f;
                player.lifeRegen += 5;
                player.meleeSpeed += 0.3f;
                player.pickSpeed -= 0.30f;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FishDiverMask", 1);
            recipe.AddIngredient(null, "EXArmor", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}