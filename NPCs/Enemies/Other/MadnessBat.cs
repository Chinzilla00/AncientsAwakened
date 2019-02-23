using BaseMod;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Other
{
    public class MadnessBat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Flier");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 30;
            npc.aiStyle = -1;
            npc.damage = 5;
            npc.defense = 4;
            npc.lifeMax = 30;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.knockBackResist = 0.05f;
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.netAlways = true;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            BaseAI.AIFlier(npc, ref npc.ai, true, 0.4f, 0.04f, 6f, 1.5f, false, 300);
            npc.frameCounter++;
            if (npc.frameCounter >= 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 30;
                if (npc.frame.Y > (30 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }

            if (player.Center.X > npc.Center.X)
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || Main.hardMode)
            {
                return 0f;
            }
            return SpawnCondition.Underground.Chance * 0.1f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, Main.rand.Next(2) == 0 ? mod.DustType<Dusts.InfinityOverloadR>() : mod.DustType<Dusts.InfinityOverloadP>(), hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, Main.rand.Next(2) == 0 ? mod.DustType<Dusts.InfinityOverloadR>() : mod.DustType<Dusts.InfinityOverloadP>(), hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MadnessFragment"), Main.rand.Next(3));
        }

    }
}