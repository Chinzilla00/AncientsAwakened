﻿ using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Shen
{
    public class Duality : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Duality");
            Tooltip.SetDefault(@"Chaos flares from this ancient talisman
Combines the effects of the Taiyang Baolei and the Naitokurosu, while granting their strongest effects at all times
Your attacks inflict Discordian Inferno
You are immune to Terrablaze, Dragonfire, Hydratoxin, Discordian Inferno
Attack is multiplied by 15%
While in the chaos biomes, your attack multiplier is increased to 30%
While in the Inferno, your defense is increased by 10
While in the Mire, your speed is increased by 50%
Grants a strong dash that shreds through enemies in a fiery blaze of glory");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 34;
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = ItemRarityID.Cyan;
            item.expert = true; item.expertOnly = true;
            item.accessory = true;
            item.defense = 8;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.DarkMagenta.ToVector3() * 0.55f * Main.essScale);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.buffImmune[20] = true;
            player.buffImmune[22] = true;
            player.buffImmune[23] = true;
            player.buffImmune[30] = true;
            player.buffImmune[31] = true;
            player.buffImmune[32] = true;
            player.buffImmune[33] = true;
            player.buffImmune[35] = true;
            player.buffImmune[36] = true;
            player.buffImmune[38] = true;
            player.buffImmune[44] = true;
            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
            player.buffImmune[67] = true;
            player.buffImmune[69] = true;
            player.buffImmune[70] = true;
            player.buffImmune[120] = true;
            player.buffImmune[144] = true;
            player.buffImmune[153] = true;
            player.buffImmune[156] = true;
            player.buffImmune[195] = true;
            player.buffImmune[196] = true;
            player.buffImmune[197] = true;
            player.buffImmune[203] = true;
            player.buffImmune[mod.BuffType("DragonFire")] = true;
            player.buffImmune[mod.BuffType("BurningAsh")] = true;
            player.buffImmune[mod.BuffType("HydraToxin")] = true;
            player.buffImmune[mod.BuffType("Clueless")] = true;
            player.buffImmune[mod.BuffType("Terrablaze")] = true;
            player.buffImmune[mod.BuffType("DiscordInferno")] = true;
            player.noKnockback = true;
            player.blackBelt = true;
            player.spikedBoots = 2;
            modPlayer.clawsOfChaos = true;
            player.moveSpeed += 2f;
            player.endurance += 0.06f;
            player.dash = 3;
            player.moveSpeed += player.GetModPlayer<AAPlayer>().ZoneMire ? .5f : 0f;
            item.defense = player.GetModPlayer<AAPlayer>().ZoneInferno ? 18 : 8;

            if (player.GetModPlayer<AAPlayer>().ZoneInferno || player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                player.allDamage += .3f;
            }
            else
            {
                player.allDamage += .15f;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TaiyangBaolei", 1);
            recipe.AddIngredient(null, "Naitokurosu", 1);
            recipe.AddIngredient(null, "ChaosSoul", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}