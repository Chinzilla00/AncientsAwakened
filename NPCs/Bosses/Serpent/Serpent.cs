using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
	public class SerpentHead : ModNPC
	{
		bool TailSpawned = false;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subzero Serpent");
		}
		
		public override void SetDefaults()
		{
			npc.damage = 40;
			npc.npcSlots = 5f;
            npc.damage = 35;
            npc.width = 38;
            npc.height = 38;
            npc.defense = 25;
            npc.lifeMax = 6000;
            npc.knockBackResist = 0f;
            npc.aiStyle = 6;
            aiType = -1;
            animationType = 10;
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 0, 10, 0);
		}
		
		public override void AI()
		{
			if (!TailSpawned)
            {
                int Previous = npc.whoAmI;
                for (int num36 = 0; num36 < 9; num36++)
                {
                    int Segment = 0;
                    if (num36 >= 0 && num36 < 8)
                    {
                        Segment = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("SerpentBody"), npc.whoAmI);
                    }
                    else
                    {
                        Segment = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("SerpentTail"), npc.whoAmI);
                    }
                    Main.npc[Segment].realLife = npc.whoAmI;
                    Main.npc[Segment].ai[2] = (float)npc.whoAmI;
                    Main.npc[Segment].ai[1] = (float)Previous;
                    Main.npc[Previous].ai[0] = (float)Segment;
                    NetMessage.SendData(23, -1, -1, null, Segment, 0f, 0f, 0f, 0);
                    Previous = Segment;
                }
                TailSpawned = true;
            }
		}
		
		
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode)
			{
				player.AddBuff(BuffID.Chilled, 200, true);
			}
			else
			{
				player.AddBuff(BuffID.Chilled, 100, true);
			}
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.85f);
		}

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.IceDust>(), hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.SnowDustLight>(), hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (!Main.expertMode)
            {
                AAWorld.downedSerpent = true;
                npc.DropLoot(mod.ItemType("SnowMana"), 10, 15);
                string[] lootTable = { "BlizardBuster", "SerpentSpike", "Icepick", "SerpentSting", "Sickle", "SickleShot", "SnakeStaff", "SubzeroSlasher" };
                int loot = Main.rand.Next(lootTable.Length);
                if (Main.rand.Next(9) == 0)
                {
                    npc.DropLoot(mod.ItemType("SnowflakeShuriken"), 90, 120);
                }
                else
                {
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                }
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;
        }
    }
}