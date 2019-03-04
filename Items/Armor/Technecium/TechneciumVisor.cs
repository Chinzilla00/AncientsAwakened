using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Technecium
{
	[AutoloadEquip(EquipType.Head)]
	public class TechneciumVisor : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Technecium Visor");
            Tooltip.SetDefault(@"4% increased damage resistance
12% increased melee damage & critical strike chance");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = Item.sellPrice(0, 1, 80, 0);
            item.rare = 4;
            item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance *= 1.04f;
            player.meleeDamage *= 1.12f;
            player.meleeCrit += 12;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TechneciumPlate") && legs.type == mod.ItemType("TechneciumGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = @"Enemies that hit you are inflicted with electrified";


            player.GetModPlayer<AAPlayer>(mod).techneciumSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "TechneciumBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}