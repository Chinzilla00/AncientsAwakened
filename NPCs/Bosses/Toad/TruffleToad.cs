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
        public float bossLife;

        public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if((Main.netMode == 2 || Main.dedServ))
			{
				writer.Write((float)internalAI[0]);
				writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
            }
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if(Main.netMode == 1)
			{
				internalAI[0] = reader.ReadFloat();
				internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }	
		}	

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Toad");
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 5000;
            npc.damage = 30;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.value = Item.buyPrice(0, 1, 0, 0);
            npc.aiStyle = -1;
            npc.width = 98;
            npc.height = 72;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TODE");
            npc.netAlways = true;
            bossBag = mod.ItemType("ToadBag");
            npc.alpha = 255;
        }
      
		public static int AISTATE_JUMP = 0, AISTATE_BARF = 1, AISTATE_TONGUE = 2;
		public float[] internalAI = new float[4];
        public int NOM = 0;
        public bool tonguespawned = false;
        public bool TongueAttack = false;

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            AAModGlobalNPC.Toad = npc.whoAmI;

            if (player != null)
            {
                float dist = npc.Distance(player.Center);
                if (dist > 500 || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        Vector2 tele = new Vector2(player.Center.X + (Main.rand.Next(2) == 0 ? 400 : -400), player.Center.Y - 16);
                        npc.Center = tele;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    npc.alpha -= 3;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                    }
                }
            }

            if (npc.velocity.X < 0)
            {
                npc.spriteDirection = 1;
            }
            else if (npc.velocity.X > 0)
            {
                npc.spriteDirection = -1;
            }

            if (internalAI[0] == AISTATE_JUMP)
            {
                BaseAI.AISlime(npc, ref npc.ai, false, 60, 5, -5, 13, -13);
                if (npc.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                if (internalAI[1] >= 180)
                {
                    internalAI[1] = 0;
                    internalAI[0] = Main.rand.Next(2);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_BARF)
            {
                internalAI[1]++;
                npc.velocity.X = 0;
                if (internalAI[1] >= 35)
                {
                    internalAI[2]++;
                    if (internalAI[2] > 5)
                    {
                        internalAI[2] = 0;
                        if (npc.direction == -1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                }
            }
            else if (internalAI[0] == AISTATE_TONGUE)
            {
                /*if (npc.ai[2] >= 120f)
                {
                    num1160 = npc.ai[2] - 120f;
                    num1161 = 555f;
                    num1162 = 2;
                    if (num1160 == 0f && Main.netMode != 1)
                    {
                        Vector2 value25 = new Vector2(36f, 0f);
                        value25.X = value25.X * npc.direction;
                        Vector2 value26 = npc.Center + value25;
                        for (int num1169 = 0; num1169 < 255; num1169++)
                        {
                            Player player5 = Main.player[num1169];
                            if (player5.active && !player5.dead && Vector2.Distance(player5.Center, value26) <= 2000f)
                            {
                                Vector2 value27 = Main.player[npc.target].Center - value26;
                                if (value27 != Vector2.Zero)
                                {
                                    value27.Normalize();
                                }
                                Projectile.NewProjectile(value26.X, value26.Y, value27.X, value27.Y, mod.ProjectileType<TruffleToadTongue>(), 0, 0f, Main.myPlayer, (npc.whoAmI + 1), num1169);
                            }
                        }
                    }
                    if ((num1160 == 120f || num1160 == 180f || num1160 == 240f) && Main.netMode != 1)
                    {
                        for (int num1170 = 0; num1170 < 1000; num1170++)
                        {
                            Projectile projectile5 = Main.projectile[num1170];
                            if (projectile5.active && projectile5.type == 456 && Main.player[(int)projectile5.ai[1]].FindBuffIndex(145) != -1)
                            {
                                Vector2 center19 = Main.player[npc.target].Center;
                                int num1171 = NPC.NewNPC((int)center19.X, (int)center19.Y, 401, 0, 0f, 0f, 0f, 0f, 255);
                                Main.npc[num1171].netUpdate = true;
                                Main.npc[num1171].ai[0] = (float)(npc.whoAmI + 1);
                                Main.npc[num1171].ai[1] = (float)num1170;
                            }
                        }
                    }
                }*/
                
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.velocity.Y == 0)
            {
                npc.frame.Y = 0;
		        if (internalAI[0] == AISTATE_BARF)
		        {
		            if (npc.frameCounter < 648)
		            {
		                npc.frameCounter = 648;
		            }
		            if (npc.frameCounter >= 10)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 72;
                        if (npc.frame.Y > 864)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y = 864;
                        }
                    }
		        }
            }
            else
            {
                if (npc.velocity.Y < 0)
                {
                    if (npc.frameCounter >= 10)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 72;
                        if (npc.frame.Y > (108 * 3))
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y = 108 * 3;
                        }
                    }
                }
                else
                {
                    npc.frame.Y = 108 * 4;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            AAWorld.downedToad = true;
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {
                string[] lootTable = { "MushrockStaff", "ToadTongue", "Todegun" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.4f);  //boss damage increase in expermode
        }
    }
}


