using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod;

namespace AAMod.Items.Boss.GripsShen
{
    [AutoloadEquip(EquipType.Shield)]
    public class DiscordianRampart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Rampart");
            Tooltip.SetDefault(@"Grants a strong dash that shreds through enemies
Grants effects of the Storm Riot Shield");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 44;
            item.value = 5000000;
            item.rare = 9;
            item.defense = 5;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            modPlayer.clawsOfChaos = true;
            modPlayer.StormClaw = true;
            player.dash = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DiscordianShredder", 1);
            recipe.AddIngredient(null, "StormRiot", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}