using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs
{
    public class MimicSummon : ModPlayer
    {
        int LastChest = 0;

        public override void PreUpdateBuffs()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (player.chest == -1 && LastChest >= 0 && Main.chest[LastChest] != null)
                {
                    int x2 = Main.chest[LastChest].x;
                    int y2 = Main.chest[LastChest].y;
                    ChestItemSummonCheck(x2, y2, mod);
                }
                LastChest = player.chest;
            }
        }

        public override void UpdateAutopause()
        {
            LastChest = player.chest;
        }

        public static void ChestItemSummonCheck(int x, int y, Mod mod)
        {
            if (!Main.hardMode || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int chestIndex = Chest.FindChest(x, y);
            if (chestIndex < 0)
            {
                return;
            }

            ushort tileType = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].type;
            int tileStyle = Main.tile[Main.chest[chestIndex].x, Main.chest[chestIndex].y].frameX / 36;

            if (!TileID.Sets.BasicChest[tileType] || tileStyle == 5 || tileStyle == 6)
            {
                return;
            }

            bool hasInfernoKey = false;
            bool hasMireKey = false;
            bool hasItems = false;

            for (int i = 0; i < 40; i++)
            {
                if (Main.chest[chestIndex].item[i] == null || Main.chest[chestIndex].item[i].type <= 0)
                {
                    continue;
                }

                if (hasItems || Main.chest[chestIndex].item[i].stack != 1)
                {
                    return;
                }

                hasItems = true;

                if (Main.chest[chestIndex].item[i].type == mod.ItemType("KeyOfSmite"))
                {
                    hasInfernoKey = true;
                }
                else if (Main.chest[chestIndex].item[i].type == mod.ItemType("KeyOfSpite"))
                {
                    hasMireKey = true;
                }
            }

            if (!hasItems)
            {
                return;
            }

            for (int j = x; j <= x + 1; j++)
            {
                for (int k = y; k <= y + 1; k++)
                {
                    if (TileID.Sets.BasicChest[Main.tile[j, k].type])
                    {
                        Main.tile[j, k].active(false);
                    }
                }
            }

            for (int l = 0; l < 40; l++)
            {
                Main.chest[chestIndex].item[l] = new Item();
            }

            Chest.DestroyChest(x, y);
            NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 1, x, y, 0f, chestIndex);
            NetMessage.SendTileSquare(-1, x, y, 3);

            int npcToSpawn = -1;
            if (hasInfernoKey)
            {
                npcToSpawn = mod.NPCType("InfernoMimic");
            }
            else if (hasMireKey)
            {
                npcToSpawn = mod.NPCType("MireMimic");
            }

            if (npcToSpawn != -1)
            {
                int npcIndex = NPC.NewNPC(x * 16 + 16, y * 16 + 32, npcToSpawn);
                Main.npc[npcIndex].whoAmI = npcIndex;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex);
                Main.npc[npcIndex].BigMimicSpawnSmoke();
            }
        }
    }
}
