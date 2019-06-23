using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Rajah;
using Terraria.Localization;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.Items.BossSummons
{
    public class DiamondCarrot : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ten Carat Carrot");
            Tooltip.SetDefault(@"The fury of the Raging Rajah can be felt radiating from this ornate carrot...");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.rare = 2;
            item.maxStack = 20;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.noUseGraphic = true;
            item.consumable = true;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah");
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType<Rajah>());
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("GRAVE MISTAKE, TERRARIAN!", 107, 137, 179);
            int overrideDirection = (Main.rand.Next(2) == 0 ? -1 : 1);
            SpawnBoss(player, mod.NPCType("Rajah"), false, player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, -1200), "Rajah Rabbit");
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldenCarrot", 1);
            recipe.AddIngredient(null, "RoyalRabbit", 1);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 npcCenter = default(Vector2), string overrideDisplayName = "", bool namePlural = false)
        {
            if (Main.netMode != 1)
            {
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)npcCenter.X, (int)npcCenter.Y, bossType, 0);
                Main.npc[npcID].ai[3] = .1f;
                Main.npc[npcID].Center = npcCenter;
                Main.npc[npcID].netUpdate2 = true;
                Main.npc[npcID].damage = 450;
                Main.npc[npcID].defense = 350;
                Main.npc[npcID].lifeMax = 4000000;
                if (spawnMessage)
                {
                    string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName);
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
                        npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();
                    if (namePlural)
                    {
                        if (Main.netMode == 0) { Main.NewText(npcName + " have awoken!", 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " have awoken!"), new Color(175, 75, 255), -1);
                        }
                    }
                    else
                    {
                        if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                            {
                            NetworkText.FromLiteral(npcName)
                            }), new Color(175, 75, 255), -1);
                        }
                    }
                }
            }
            else
            {
                //I have no idea how to convert this to the standard system so im gonna post this method too lol
                AANet.SendNetMessage(AANet.SummonNPCFromClient, (byte)player.whoAmI, (short)bossType, (bool)spawnMessage, (int)npcCenter.X, (int)npcCenter.Y, (string)overrideDisplayName, (bool)namePlural);
            }
        }
    }
}