using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Terrarium
{
    [AutoloadBossHead]
    public class TerraWarlock : ModNPC
    {
		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if((Main.netMode == 2 || Main.dedServ))
			{
				writer.Write((float)internalAI[0]);
				writer.Write((float)internalAI[1]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if(Main.netMode == 1)
			{
				internalAI[0] = reader.ReadFloat();
				internalAI[1] = reader.ReadFloat();
			}	
		}	

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Warlock");
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 600;
            npc.defense = 40;
            npc.damage = 120;
            npc.width = 38;
            npc.height = 60;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.4f;

        }
      
		public static int AISTATE_WALK = 0, AISTATE_SUMMON = 1;
		public float[] internalAI = new float[2];
        public int SummonThis = 0;
		
        public override void AI()
        {
            SummonThis = Main.rand.Next(4);

            switch (SummonThis)
            {
                case 0:
                    SummonThis = mod.NPCType("Minion1");
                    break;
                case 1:
                    SummonThis = mod.NPCType("Minion2");
                    break;
                case 2:
                    SummonThis = mod.NPCType("Minion3");
                    break;
                default:
                    SummonThis = mod.NPCType("Minion4");
                    break;
            }
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            npc.frameCounter++;
            if (internalAI[1] != AISTATE_SUMMON) //walk or charge
            {
				if (npc.frameCounter >= 10)
				{
					npc.frameCounter = 0;
					npc.frame.Y += 60;
					if (npc.frame.Y > (60 * 7))
					{
						npc.frameCounter = 0;
						npc.frame.Y = 0;
					}
				}
                if(npc.velocity.Y != 0)
                {
                    npc.frame.Y = 0;
                }
            }
            else //jump
            {
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 60;
                    if (npc.frame.Y > (60 * 14) || npc.frame.Y < (60 * 8))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = (60 * 8);
                    }
                    if (npc.frame.Y > (60 * 14))
                    {

                        Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
                        if (Main.expertMode)
                        {
                            NPC.NewNPC((int)spawnAt.X - 10, (int)spawnAt.Y - 10, SummonThis);
                        }
                        internalAI[1] = AISTATE_WALK;
                    }
                }
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }else
            {
                npc.spriteDirection = 1;
            }
			if(Main.netMode != 1)
			{
				internalAI[0]++;
				if (internalAI[0] >= 300)
				{
					internalAI[0] = 0;
					internalAI[1] = Main.rand.Next(2);
					npc.ai = new float[4];
					npc.netUpdate = true;
				}
			}
			if(internalAI[1] == AISTATE_WALK) //fighter
			{
                BaseAI.AIZombie(npc, ref npc.ai, false, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);				
			}
            else
			{
                
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            AAWorld.downedMonarch = true;
            Projectile.NewProjectile(new Vector2(npc.position.X, npc.position.Y - 2), new Vector2(0f, 0f), mod.ProjectileType("MonarchRUNAWAY"), 0, 0);
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mushium"), Main.rand.Next(25, 35));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }
    }
}


