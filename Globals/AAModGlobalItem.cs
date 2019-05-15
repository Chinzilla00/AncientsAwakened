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

        public static void OpenAACrate(Player player, int CrateType)
        {
            Mod mod = AAMod.instance;
            bool flag4 = true;
            while (flag4)
            {
                if (Main.rand.Next(4) == 0)
                {
                    int type21 = 73;
                    int stack25 = Main.rand.Next(5, 13);
                    int number36 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, type21, stack25, false, 0, false, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, number36, 1f, 0f, 0f, 0, 0, 0);
                    }
                    flag4 = false;
                }
                if (Main.rand.Next(4) == 0)
                {
                    int num17 = Main.rand.Next(6);
                    if (num17 == 0)
                    {
                        num17 = 22;
                    }
                    else if (num17 == 1)
                    {
                        num17 = 21;
                    }
                    else if (num17 == 2)
                    {
                        num17 = 19;
                    }
                    else if (num17 == 3)
                    {
                        num17 = 704;
                    }
                    else if (num17 == 4)
                    {
                        num17 = 705;
                    }
                    else if (num17 == 5)
                    {
                        num17 = 706;
                    }
                    int num18 = Main.rand.Next(10, 21);
                    if (Main.hardMode && Main.rand.Next(3) != 0)
                    {
                        num17 = Main.rand.Next(3);
                        if (num17 == 0)
                        {
                            num17 = mod.ItemType<Items.Materials.YtriumBar>();
                        }
                        else if (num17 == 1)
                        {
                            num17 = mod.ItemType<Items.Materials.UraniumBar>(); ;
                        }
                        else if (num17 == 2)
                        {
                            num17 = mod.ItemType<Items.Materials.TechneciumBar>(); ;
                        }
                        num18 -= Main.rand.Next(3);
                    }
                    int number37 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, num17, num18, false, 0, false, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, number37, 1f, 0f, 0f, 0, 0, 0);
                    }
                    flag4 = false;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                int num19 = Main.rand.Next(6);
                if (num19 == 0)
                {
                    num19 = 288;
                }
                else if (num19 == 1)
                {
                    num19 = 296;
                }
                else if (num19 == 2)
                {
                    num19 = 304;
                }
                else if (num19 == 3)
                {
                    num19 = 305;
                }
                else if (num19 == 4)
                {
                    num19 = 2322;
                }
                else if (num19 == 5)
                {
                    num19 = 2323;
                }
                int stack26 = Main.rand.Next(2, 5);
                int number38 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, num19, stack26, false, 0, false, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, number38, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (Main.rand.Next(2) == 0)
            {
                int type22 = Main.rand.Next(188, 190);
                int stack27 = Main.rand.Next(5, 18);
                int number39 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, type22, stack27, false, 0, false, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, number39, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (Main.rand.Next(2) == 0)
            {
                int type23;
                if (Main.rand.Next(2) == 0)
                {
                    type23 = 2676;
                }
                else
                {
                    type23 = 2675;
                }
                int stack28 = Main.rand.Next(2, 7);
                int number40 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, type23, stack28, false, 0, false, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, number40, 1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (CrateType < 2)
            {
                if (Main.hardMode && Main.rand.Next(2) == 0)
                {
                    int type24 = mod.ItemType<Items.Materials.SoulOfSmite>(); ;
                    if (CrateType == 1)
                    {
                        type24 = mod.ItemType<Items.Materials.SoulOfSpite>();
                    }
                    int stack29 = Main.rand.Next(2, 6);
                    int number41 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, type24, stack29, false, 0, false, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, number41, 1f, 0f, 0f, 0, 0, 0);
                    }
                }
                if (Main.hardMode && Main.rand.Next(2) == 0)
                {
                    int type25 = mod.ItemType<Items.Materials.DragonFire>();
                    int stack30 = Main.rand.Next(2, 6);
                    if (CrateType == 1)
                    {
                        type25 = mod.ItemType<Items.Materials.HydraToxin>();
                    }
                    int number42 = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, type25, stack30, false, 0, false, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, number42, 1f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
        }
    }
}
