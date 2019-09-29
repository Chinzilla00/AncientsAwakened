using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Shen;
using System.Collections.Generic;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class ChaosSigil : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Sigil");
            Tooltip.SetDefault(@"A cursed tablet filled with unstable magic
Summons the chaos emperor
Non-Consumable");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(176, 39, 157);
                }
            }
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Yamata.Yamata>()) || NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Yamata.Awakened.YamataA>()))
            {
                if (Main.netMode != 1) BaseUtility.Chat("Only the blue half of the sigil is lit up...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Akuma.Akuma>()) || NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Akuma.Awakened.AkumaA>()))
            {
                if (Main.netMode != 1) BaseUtility.Chat("Only the red half of the sigil is lit up...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<Shen>()))
            {
                if (Main.netMode != 1) BaseUtility.Chat("HAH! I WISH there were two of me to smash you into the ground!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<ShenA>()))
            {
                if (Main.netMode != 1) BaseUtility.Chat("HAH! I WISH there were two of me to smash you into the ground!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            if (!AAWorld.downedShen && !player.GetModPlayer<AAPlayer>(mod).ZoneRisingSunPagoda && !player.GetModPlayer<AAPlayer>(mod).ZoneRisingMoonLake)
            {
                if (Main.netMode != 1) BaseUtility.Chat("The Chaos Sigil glows, and imagery of the chaos pedestals flash through your mind", Color.DarkMagenta, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<ShenSpawn>()) || NPC.AnyNPCs(mod.NPCType<ShenTransition>()) || NPC.AnyNPCs(mod.NPCType<ShenDefeat>()) || NPC.AnyNPCs(mod.NPCType<ShenDeath>()))
            {
                return false;
            }
            if (!AAWorld.downedAllAncients)
            {
                if (Main.netMode != 1) BaseUtility.Chat("The sigil does nothing...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            if (AAWorld.ShenSummoned)
            {
                if (Main.netMode != 1) BaseUtility.Chat(AAWorld.downedShen ? "Big mistake, child..." : "Hmpf...again..? Alright, let's just get this done and overwith.", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

                AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Shen"), true, 0, 0, "Shen Doragon; Discordian Doomsayer", false);
            }
            if (!AAWorld.ShenSummoned)
            {
                AAModGlobalNPC.SpawnBoss(player, mod.NPCType("ShenSpawn"), false, 0, 0);
                AAWorld.ShenSummoned = true;
            }

            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/ShenRoar"), player.position);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DraconianSigil", 1);
            recipe.AddIngredient(null, "DreadSigil", 1);
            recipe.AddIngredient(null, "Discordium", 10);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
