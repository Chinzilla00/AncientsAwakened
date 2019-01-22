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
            npc.lifeMax = 14000;   //boss life
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
            npc.noTileCollide = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TODE");
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
            float num230 = 1f;
            bool flag8 = false;
            bool flag9 = false;
            npc.aiAction = 0;
            if (npc.ai[3] == 0f && npc.life > 0)
            {
                npc.ai[3] = npc.lifeMax;
            }
            if (npc.localAI[3] == 0f && Main.netMode != 1)
            {
                npc.ai[0] = -100f;
                npc.localAI[3] = 1f;
                npc.TargetClosest(true);
                npc.netUpdate = true;
            }
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.timeLeft = 0;
                    if (Main.player[npc.target].Center.X < npc.Center.X)
                    {
                        npc.direction = 1;
                    }
                    else
                    {
                        npc.direction = -1;
                    }
                }
            }
            if (!Main.player[npc.target].dead && npc.ai[2] >= 300f && npc.ai[1] < 5f && npc.velocity.Y == 0f)
            {
                npc.ai[2] = 0f;
                npc.ai[0] = 0f;
                npc.ai[1] = 5f;
                if (Main.netMode != 1)
                {
                    npc.TargetClosest(false);
                    Point point3 = npc.Center.ToTileCoordinates();
                    Point point4 = Main.player[npc.target].Center.ToTileCoordinates();
                    Vector2 vector26 = Main.player[npc.target].Center - npc.Center;
                    int num231 = 10;
                    int num232 = 0;
                    int num233 = 7;
                    int num234 = 0;
                    bool flag10 = false;
                    if (vector26.Length() > 2000f)
                    {
                        flag10 = true;
                        num234 = 100;
                    }
                    while (!flag10 && num234 < 100)
                    {
                        num234++;
                        npc.alpha += 10;
                        if (npc.alpha >= 255)
                        {
                            npc.alpha = 255;
                        }
                        int num235 = Main.rand.Next(point4.X - num231, point4.X + num231 + 1);
                        int num236 = Main.rand.Next(point4.Y - num231, point4.Y + 1);
                        if ((num236 < point4.Y - num233 || num236 > point4.Y + num233 || num235 < point4.X - num233 || num235 > point4.X + num233) && (num236 < point3.Y - num232 || num236 > point3.Y + num232 || num235 < point3.X - num232 || num235 > point3.X + num232) && !Main.tile[num235, num236].nactive())
                        {
                            int num237 = num236;
                            int num238 = 0;
                            bool flag11 = Main.tile[num235, num237].nactive() && Main.tileSolid[(int)Main.tile[num235, num237].type] && !Main.tileSolidTop[(int)Main.tile[num235, num237].type];
                            if (flag11)
                            {
                                num238 = 1;
                            }
                            else
                            {
                                while (num238 < 150 && num237 + num238 < Main.maxTilesY)
                                {
                                    int num239 = num237 + num238;
                                    bool flag12 = Main.tile[num235, num239].nactive() && Main.tileSolid[(int)Main.tile[num235, num239].type] && !Main.tileSolidTop[(int)Main.tile[num235, num239].type];
                                    if (flag12)
                                    {
                                        num238--;
                                        break;
                                    }
                                    num238++;
                                }
                            }
                            num236 += num238;
                            bool flag13 = true;
                            if (flag13 && Main.tile[num235, num236].lava())
                            {
                                flag13 = false;
                            }
                            if (flag13 && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                            {
                                flag13 = false;
                            }
                            if (flag13)
                            {
                                npc.localAI[1] = (num235 * 16 + 8);
                                npc.localAI[2] = (num236 * 16 + 16);
                                break;
                            }
                        }
                    }
                    if (num234 >= 100)
                    {
                        npc.alpha -= 20;
                        Vector2 bottom = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
                        npc.localAI[1] = bottom.X;
                        npc.localAI[2] = bottom.Y;
                    }
                }
            }
            if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
            {
                npc.ai[2] += 1f;
            }
            if (Math.Abs(npc.Top.Y - Main.player[npc.target].Bottom.Y) > 320f)
            {
                npc.ai[2] += 1f;
            }
            if (npc.ai[1] == 5f)
            {
                flag8 = true;
                npc.aiAction = 1;
                npc.ai[0] += 1f;
                num230 = MathHelper.Clamp((60f - npc.ai[0]) / 60f, 0f, 1f);
                num230 = 0.5f + num230 * 0.5f;
                if (npc.ai[0] >= 60f)
                {
                    flag9 = true;
                }
                if (npc.ai[0] == 60f)
                {
                    Gore.NewGore(npc.Center + new Vector2(-40f, (float)(-(float)npc.height / 2)), npc.velocity, 734, 1f);
                }
                if (npc.ai[0] >= 60f && Main.netMode != 1)
                {
                    npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                }
                if (Main.netMode == 1 && npc.ai[0] >= 120f)
                {
                    npc.ai[1] = 6f;
                    npc.ai[0] = 0f;
                }
                if (!flag9)
                {
                    for (int num240 = 0; num240 < 10; num240++)
                    {
                        int num241 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, mod.DustType<Dusts.ShroomDust>(), npc.velocity.X, npc.velocity.Y, 150, default(Color), 2f);
                        Main.dust[num241].noGravity = true;
                        Main.dust[num241].velocity *= 0.5f;
                    }
                }
            }
            else if (npc.ai[1] == 6f)
            {
                flag8 = true;
                npc.aiAction = 0;
                npc.ai[0] += 1f;
                num230 = MathHelper.Clamp(npc.ai[0] / 30f, 0f, 1f);
                num230 = 0.5f + num230 * 0.5f;
                if (npc.ai[0] >= 30f && Main.netMode != 1)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.netUpdate = true;
                    npc.TargetClosest(true);
                }
                if (Main.netMode == 1 && npc.ai[0] >= 60f)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 0f;
                    npc.TargetClosest(true);
                }
                for (int num242 = 0; num242 < 10; num242++)
                {
                    int num243 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, mod.DustType<Dusts.ShroomDust>(), npc.velocity.X, npc.velocity.Y, 150, default(Color), 2f);
                    Main.dust[num243].noGravity = true;
                    Main.dust[num243].velocity *= 2f;
                }
            }
            npc.dontTakeDamage = (npc.hide = flag9);
            if (npc.velocity.Y == 0f)
            {
                npc.velocity.X = npc.velocity.X * 0.8f;
                if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
                {
                    npc.velocity.X = 0f;
                }
                if (!flag8)
                {
                    npc.ai[0] += 2f;
                    if ((double)npc.life < (double)npc.lifeMax * 0.8)
                    {
                        npc.ai[0] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.6)
                    {
                        npc.ai[0] += 1f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.4)
                    {
                        npc.ai[0] += 2f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.2)
                    {
                        npc.ai[0] += 3f;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.1)
                    {
                        npc.ai[0] += 4f;
                    }
                    if (npc.ai[0] >= 0f)
                    {
                        npc.netUpdate = true;
                        npc.TargetClosest(true);
                        if (npc.ai[1] == 3f)
                        {
                            npc.velocity.Y = -26f;
                            npc.velocity.X = npc.velocity.X + 3.5f * (float)npc.direction;
                            npc.ai[0] = -200f;
                            npc.ai[1] = 0f;
                        }
                        else if (npc.ai[1] == 2f)
                        {
                            npc.velocity.Y = -10f;
                            npc.velocity.X = npc.velocity.X + 8.5f * (float)npc.direction;
                            npc.ai[0] = -120f;
                            npc.ai[1] += 1f;
                        }
                        else
                        {
                            npc.velocity.Y = -15f;
                            npc.velocity.X = npc.velocity.X + 6f * (float)npc.direction;
                            npc.ai[0] = -120f;
                            npc.ai[1] += 1f;
                        }
                    }
                    else if (npc.ai[0] >= -30f)
                    {
                        npc.aiAction = 1;
                    }
                }
            }
            else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
            {
                if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
                {
                    npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
                }
                else
                {
                    npc.velocity.X = npc.velocity.X * 0.93f;
                }
            }
            int num244 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.ShroomDust>(), npc.velocity.X, npc.velocity.Y, 255, default(Color), 1f);
            Main.dust[num244].noGravity = true;
            Main.dust[num244].velocity *= 0.5f;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
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
                        if (npc.frame.Y > (108 * 3))
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y = 108 * 3;
                        }
                    }
                }
                else if (npc.velocity.Y > 0)
                {
                    npc.frame.Y = 108 * 4;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            AAWorld.downedToad = true;
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.1f);  //boss damage increase in expermode
        }
    }
}


