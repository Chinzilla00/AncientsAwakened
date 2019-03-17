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

namespace AAMod.NPCs.Bosses.MushroomMonarch
{
    [AutoloadBossHead]
    public class MushroomMonarch : ModNPC
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
            DisplayName.SetDefault("Mushroom Monarch");
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 1200;   //boss life
            npc.damage = 12;  //boss damage
            npc.defense = 12;    //boss defense
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 0, 75, 45);
            npc.aiStyle = 26;
            npc.width = 74;
            npc.height = 108;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            bossBag = mod.ItemType("MonarchBag");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch");

        }
      
		public static int AISTATE_WALK = 0, AISTATE_JUMP = 1, AISTATE_CHARGE = 2, AISTATE_FLY = 3;
		public float[] internalAI = new float[2];
		
        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting


            float dist = npc.Distance(player.Center);

            npc.frameCounter++;
            if (internalAI[1] != AISTATE_JUMP && internalAI[1] != AISTATE_FLY) //walk or charge
            {
                int FrameSpeed = 10;
                if (internalAI[1] == AISTATE_CHARGE)
                {
                    FrameSpeed = 6;
                }

                if (npc.frameCounter >= FrameSpeed)
				{
					npc.frameCounter = 0;
					npc.frame.Y += 108;
					if (npc.frame.Y > (108 * 4))
					{
						npc.frameCounter = 0;
						npc.frame.Y = 0;
					}
				}
                if(npc.velocity.Y != 0)
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 648;
                    }else
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 756;
                    }
                }
            }
            else if (internalAI[1] == AISTATE_FLY)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 108;
                if (npc.frame.Y > (108 * 11) || npc.frame.Y < (108 * 8))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 108 * 8;
                }

            }
            else //jump
            {
                if (npc.velocity.Y == 0)
                {
                    npc.frame.Y = 540;
                }else
                {
                    if (npc.velocity.Y < 0)
                    {
                        npc.frame.Y = 648;
                    }else
                    if (npc.velocity.Y > 0)
                    {
                        npc.frame.Y = 756;
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
                if (!Main.dayTime)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(0f, 0f), mod.ProjectileType("MonarchRUNAWAY"), 0, 0);
                    npc.active = false;
                    return;
                }
                if (internalAI[1] != AISTATE_FLY)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
                else if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    internalAI[1] = AISTATE_FLY;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
			}
			if(internalAI[1] == AISTATE_WALK) //fighter
			{
                AAAI.InfernoFighterAI(npc, ref npc.ai, true, false, 0, 0.07f, 3f, 3, 4, 60, true, 10, 60, true, null, false);				
			}else
			if(internalAI[1] == AISTATE_JUMP)//jumper
			{
				if(npc.ai[0] < -10) npc.ai[0] = -10; //force rapid jumping
                BaseAI.AISlime(npc, ref npc.ai, true, 30, 6f, -8f, 6f, -10f);				
			}
            else if (internalAI[1] == AISTATE_FLY)//fly
            {
                npc.noTileCollide = true;
                npc.noGravity = true;
                BaseAI.AISpaceOctopus(npc, ref npc.ai, .05f, 8, 250, 0, null);
                if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.rotation = 0;
                    npc.noGravity = false;
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                    npc.noTileCollide = false;
                }
            }else //charger
			{			
                BaseAI.AICharger(npc, ref npc.ai, 0.07f, 10f, false, 30);				
			}
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            AAWorld.downedMonarch = true;
            Projectile.NewProjectile(npc.Center, new Vector2(0f, 0f), mod.ProjectileType("MonarchRUNAWAY"), 0, 0);
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


