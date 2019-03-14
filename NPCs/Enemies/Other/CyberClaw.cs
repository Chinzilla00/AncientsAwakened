using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Other
{
    public class CyberClaw : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyber Claw");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 24;
            npc.friendly = false;
            npc.damage = 35;
            npc.defense = 4;
            npc.lifeMax = 300;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.value = 1f;
            npc.knockBackResist = 0.6f;
            npc.noGravity = true;
        }

        public override void AI()
        {
            npc.noGravity = true;
            BaseMod.BaseAI.AIEye(npc, ref npc.ai, false, true, 0.13f, 0.08f, 2f, 1.1f, 1.2f, 1.2f);
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
            npc.frameCounter++;
            if (npc.frameCounter >= 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 38;
                if (npc.frame.Y > (38 * 3))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (AAWorld.downedRetriever)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.08f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 15; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.FulguriteDust>(), hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteShard"), Main.rand.Next(2));
        }
    }
}