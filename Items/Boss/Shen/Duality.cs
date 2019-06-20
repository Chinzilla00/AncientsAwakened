using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class Duality : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Duality");
            Tooltip.SetDefault(@"Chaos flares from this ancient talisman
Combines the effects of the Taiyang Baolei and the Naitokurosu, while granting their strongest effects at all times
Your attacks inflict Discordian Inferno
Enemies that hit you are inflicted with Discordian Inferno
You are immune to Terrablaze, Dragonfire, Hydratoxin, Discordian Inferno
Attack is multiplied by 13%
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
            item.rare = 1;
            item.expert = true;
            item.accessory = true;
            item.defense = 8;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.DarkMagenta.ToVector3() * 0.55f * Main.essScale);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
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
            modPlayer.StormClaw = true;
            modPlayer.AADash = 1;
            player.moveSpeed += 2f;
            player.endurance += 0.2f;
            player.dash = 3;
            if (player.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                player.moveSpeed += .5f;
            }
            else
            {
                player.moveSpeed += 0f;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
            {
                item.defense = 18;
            }
            else
            {
                item.defense = 8;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneInferno || player.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                player.meleeDamage += .3f;
                player.magicDamage += .3f;
                player.rangedDamage += .3f;
                player.minionDamage += .3f;
                player.thrownDamage += .3f;
            }
            else
            {
                player.meleeDamage += .13f;
                player.magicDamage += .13f;
                player.rangedDamage += .13f;
                player.minionDamage += .13f;
                player.thrownDamage += .13f;
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

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TaiyangBaolei", 1);
            recipe.AddIngredient(null, "Naitokurosu", 1);
            recipe.AddIngredient(null, "ChaosSoul", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}