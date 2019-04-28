using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items.Boss.Akuma;
using AAMod.Items.Boss.Retriever;
using AAMod.Items.Boss.Grips;
using AAMod.Items.Dev;
using System;
using Terraria.Localization;

namespace AAMod
{
    public class AAModGlobalItem : GlobalItem
	{
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SoulofNight)
            {
                if (WorldGen.crimson)
                {
                    item.color = Color.Firebrick;
                }
                else
                {
                    item.color = Color.Violet;
                }
            }
            if (item.type == ItemID.LunarOre)
            {
                item.createTile = mod.TileType("LuminiteOre");
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ItemID.AnkhShield)
            {
                player.meleeSpeed += 0.07f;
            }
        }

        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (item.type == ItemID.AnkhShield || item.type == ItemID.ObsidianShield|| item.type == mod.ItemType<TaiyangBaolei>())
            {
                if (slot < 10)
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == ItemID.AnkhShield)
                        {
                            return false;
                        }
                        if (slot != i && player.armor[i].type == ItemID.ObsidianShield)
                        {
                            return false;
                        }
                        if (slot != i && player.armor[i].type == mod.ItemType<TaiyangBaolei>())
                        {
                            return false;
                        }
                    }
                }
            }
            if (item.type == ItemID.EoCShield || item.type == mod.ItemType<StormRiot>() || item.type == mod.ItemType<BulwarkOfChaos>())
            {
                if (slot < 10)
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == ItemID.EoCShield)
                        {
                            return false;
                        }
                        if (slot != i && player.armor[i].type == mod.ItemType<StormRiot>())
                        {
                            return false;
                        }
                        if (slot != i && player.armor[i].type == mod.ItemType<BulwarkOfChaos>())
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static void SpawnBoss(Mod mod, Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center + new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 600f);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                string npcName = (!String.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
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
}
