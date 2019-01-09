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

namespace AAMod.NPCs.Bosses.Toad
{
    [AutoloadBossHead]
    public class TruffleToad : ModNPC
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
            DisplayName.SetDefault("Truffle Toad");
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 20000;   //boss life
            npc.damage = 30;  //boss damage
            npc.defense = 20;    //boss defense
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.aiStyle = -1;
            npc.width = 98;
            npc.height = 72;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Monarch");
            npc.netAlways = true;
            bossBag = mod.ItemType("ToadBag");
            npc.alpha = 255;
        }
      
		public static int AISTATE_JUMP = 0, AISTATE_NOM = 1;
		public float[] internalAI = new float[2];
        public int NOM = 0;
        public bool tonguespawned = false;
        public bool TongueAttack = false;
        
        private int tongueTimer;

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            AAModGlobalNPC.Toad = npc.whoAmI;
            npc.frameCounter++;
            if (!player.dead)
            {
                npc.alpha -= 3;
            }
            else
            {
                npc.alpha += 3;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
            if (npc.alpha > 0)
            {
                if (internalAI[1] != AISTATE_NOM) //Mad Hops
                {
                    if (npc.velocity.Y == 0)
                    {
                        npc.frame.Y = 0;
                    }
                    else
                    {
                        if (npc.velocity.Y < 0)
                        {
                            if (npc.frameCounter >= 10)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y += 72;
                                if (npc.frame.Y > (72 * 3))
                                {
                                    npc.frameCounter = 0;
                                    npc.frame.Y = 72 * 3;
                                }
                            }
                        }
                        else
                        if (npc.velocity.Y > 0)
                        {
                            npc.frame.Y = 72 * 0;
                        }
                    }
                }
                else //Eat Pant
                {
                    NOM++;
                    if (npc.frameCounter < 5)
                    {
                        npc.frame.Y = 5 * 72;
                    }
                    else if (npc.frameCounter < 10)
                    {
                        npc.frame.Y = 6 * 72;
                    }
                    else if (npc.frameCounter < 15)
                    {
                        npc.frame.Y = 7 * 72;
                    }
                    else if (npc.frameCounter < 20)
                    {
                        npc.frame.Y = 8 * 72;
                    }
                    else if (npc.frameCounter < 25)
                    {
                        npc.frame.Y = 9 * 72;
                    }
                    else if (npc.frameCounter < 30)
                    {
                        npc.frame.Y = 10 * 72;
                    }
                    else if (npc.frameCounter < 35 && npc.frameCounter > 65)
                    {
                        npc.frame.Y = 11 * 72;
                        npc.velocity.X = 0;
                        if (npc.frameCounter < 35 && npc.frameCounter > 65)
                        {
                            // projectile code, donno how to do it though, so it just throws up dirt ¯\_(ツ)_/¯
                            if (npc.direction == -1)
                            {
                                //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                                Projectile.NewProjectile((new Vector2(npc.position.X + 17f, npc.position.Y + 18f)), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 15, 3);
                            }
                            else
                            {
                                //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                                Projectile.NewProjectile((new Vector2(npc.position.X + 57f, npc.position.Y + 18f)), new Vector2(6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 15, 3);
                            }
                        }
                        if (tongueTimer >= 100)
                        {
                            tongueTimer = 0;
                        }
                    }
                    else if (npc.frameCounter < 65)
                    {
                        tongueTimer = 0;
                        npc.frame.Y = 10 * 72;
                    }
                    else if (npc.frameCounter < 68)
                    {
                        npc.frame.Y = 9 * 72;
                    }
                    else if (npc.frameCounter < 71)
                    {
                        npc.frame.Y = 8 * 72;
                    }
                    else if (npc.frameCounter < 74)
                    {
                        npc.frame.Y = 8 * 72;
                    }
                    else if (npc.frameCounter < 77)
                    {
                        npc.frame.Y = 7 * 72;
                    }
                    else if (npc.frameCounter < 80)
                    {
                        npc.frame.Y = 6 * 72;
                    }
                    else
                    {
                        npc.frame.Y = 5 * 72;
                        internalAI[1] = AISTATE_JUMP;
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
                if (Main.netMode != 1)
                {
                    internalAI[0]++;
                    if (internalAI[0] >= 180)
                    {
                        internalAI[0] = 0;
                        internalAI[1] = Main.rand.Next(2);
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[1] == AISTATE_JUMP)//jumper
                {
                    if (npc.ai[0] < -10) npc.ai[0] = -10; //force rapid jumping
                    BaseAI.AISlime(npc, ref npc.ai, false, 100, 6f, -8f, 6f, -10f);
                }
                else //Tongue
                {

                }
            }
            else
            {
                npc.frame.Y = 72 * 0;
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
            npc.lifeMax = (int)(npc.lifeMax * 1.1f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }
    }
}


