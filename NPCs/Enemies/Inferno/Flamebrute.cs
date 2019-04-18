using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class Flamebrute : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Brute");
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 120;
            npc.damage = 12;
            npc.defense = 6;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 0, 6, 45);
            npc.aiStyle = -1;
            npc.width = 40;
            npc.height = 60;
			npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;		
            npc.lavaImmune = true;
        }

		const int frameWidth = 82;
		const int frameHeight = 76;
		const int frameHeightPlusFluff = 78; //the 2 pixels per frame

        public override void AI()
        {
			Player player = Main.player[npc.target];
			float playerDistX = Math.Abs(player.Center.X - npc.Center.X);
			float playerDistY = Math.Abs(player.Center.Y - npc.Center.Y);
			bool smashAttack = playerDistX < 35f && playerDistY < 40f;
            Lighting.AddLight(npc.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);

            if (smashAttack) //Stop moving to smash players
			{
				npc.velocity.X *= 0.9f;
				if(npc.velocity.X < 0.2f) npc.velocity.X = 0;
				npc.spriteDirection = (npc.Center.X < player.Center.X ? 1 : -1);	
			}else
			{
				BaseMod.BaseAI.AIZombie(npc, ref npc.ai, false, true, -1, 0.1f, 2f, 5, 7, 120);	
				npc.spriteDirection = (npc.velocity.X > 0 ? 1 : -1);				
			}

			int frameMax = (smashAttack ? 8 : 5);
			npc.frameCounter++;
			if (npc.frameCounter >= frameMax)
			{
				npc.frameCounter = 0;
				if(smashAttack)
				{
					npc.frame.Y += frameHeightPlusFluff;
					if (npc.frame.Y < frameHeightPlusFluff * 6 || npc.frame.Y > frameHeightPlusFluff * 8)
					{
						npc.frame.Y = frameHeightPlusFluff * 6;
					}
				}else
				{
					npc.frame.Y += frameHeightPlusFluff;
					if (npc.frame.Y > frameHeightPlusFluff * 5)
					{
						npc.frame.Y = 0;
					}
				}
			}
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreBackArm"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreBackLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreFrontArm"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreFrontLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FlamebruteGoreHead"), 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && Main.dayTime ? 1f : 0f;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DragonScale"));
        }
    }
}


