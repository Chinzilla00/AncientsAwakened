using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using AAMod.NPCs.Bosses.Akuma;
using AAMod.NPCs.Bosses.Akuma.Awakened;
using System.Collections.Generic;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class DraconianSigil : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Sun Sigil");
            Tooltip.SetDefault(@"An ornate tablet said to contain the radiant power of a thousand suns
Summons Akuma
Only Usable during the day in the inferno
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
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (!Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DraconianDayTimeFalse"), new Color(180, 41, 32), false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                if (!AAWorld.downedAkuma && !player.GetModPlayer<AAPlayer>().ZoneRisingSunPagoda)
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DraconianRuneInfernoFalse2"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<Akuma>()))
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DraconianSigilFalse"), new Color(180, 41, 32), false);
                    return false;
                }
                if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
                {
                    if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DraconianSigilFalse"), new Color(0, 191, 255), false);
                    return false;
                }
                for (int m = 0; m < Main.maxProjectiles; m++)
                {
                    Projectile p = Main.projectile[m];
                    if (p != null && p.active && p.type == mod.ProjectileType("AkumaTransition"))
                    {
                        return false;
                    }
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DraconianSigilInfernoFalse"), new Color(180, 41, 32), false);
            return false;
        }

        public override bool UseItem(Player player)
        {

            if (!AAWorld.downedAkuma)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DraconianSignalTrue1"), new Color(180, 41, 32));
            }
            if (AAWorld.downedAkuma)
            {
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossSummonsInfo("DraconianSignalTrue2"), new Color(180, 41, 32));
            }
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Akuma"), true, 0, 0, Language.GetTextValue("Mods.AAMod.Common.Akuma"), false);
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/AkumaRoar"), player.position);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-2000, 2000, (float)Main.rand.NextDouble()), 1200f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 10);
            recipe.AddIngredient(null, "RadiumBar", 5);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}