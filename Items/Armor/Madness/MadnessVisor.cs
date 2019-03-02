using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Madness
{
    [AutoloadEquip(EquipType.Head)]
    public class MadnessVisor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Visor");
            Tooltip.SetDefault("4% increased damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 20000;
            item.rare = 1;
            item.defense = 5; //8
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MadnessPlate") && legs.type == mod.ItemType("MadnessBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+5 damage";
            player.meleeDamage += 5;
            player.rangedDamage += 5;
            player.magicDamage += 5;
            player.minionDamage += 5;
            player.thrownDamage += 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.04f;
            player.rangedDamage *= 1.04f;
            player.magicDamage *= 1.04f;
            player.minionDamage *= 1.04f;
            player.thrownDamage *= 1.04f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MadnessFragment", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}