using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Rajah
{
    public abstract class Rajah : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 200;
            npc.height = 220;
            npc.aiStyle = -1;
            npc.damage = 130;
            npc.defense = 80;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 1000f;
            npc.HitSound = SoundID.NPCHit14;
            npc.DeathSound = SoundID.NPCDeath20;
            npc.value = 10000f;
            npc.boss = true;
            npc.netAlways = true;
            npc.timeLeft = NPC.activeTime * 30;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RajahTheme");
        }

        public float[] internalAI = new float[5];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat(); //Minion Timer
                internalAI[3] = reader.ReadFloat(); //Arm Weapon Timer
                internalAI[4] = reader.ReadFloat(); //Is Flying
            }
        }

        private Texture2D CapeTex;
        private Texture2D ArmTex;

        /*
         * npc.ai[0] = Jump Timer
         * npc.ai[1] = Jumping
         * npc.ai[2] = Weapon Change timer
         * npc.ai[3] = Weapon type
         */

        public override void AI()
        {
            AAModGlobalNPC.Rajah = npc.whoAmI;

            if (!NPC.AnyNPCs(mod.NPCType<PunisherR>()))
            {
                NPC.NewNPC((int)npc.Center.X + 78, (int)npc.Center.Y - 9, mod.NPCType<PunisherR>(), 0, -1f, 0f, 0f, 0f, 255);
            }

            if (CapeTex == null)
            {
                CapeTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahCape");
            }
            if (ArmTex == null)
            {
                ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArms");
            }

            Player player = Main.player[npc.target];
            if (npc.target >= 0 && Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.noTileCollide = true;
                }
            }

            if (player.Center.Y < npc.position.Y || TileBelowEmpty())
            {
                FlyAI();
                internalAI[4] = 0;
                CapeTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahCape");
            }
            else
            {
                JumpAI();
                internalAI[4] = 1;
                CapeTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahCape");
            }
            
            if (npc.target <= 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            int num627 = 3000;
            if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num627)
            {
                npc.TargetClosest(true);
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num627)
                {
                    npc.active = false;
                    return;
                }
            }

            npc.ai[2]++;
            if (npc.ai[2] > 600)
            {
                npc.ai[3] = Main.rand.Next(4);
                npc.ai[2] = 0;
            }
            if (npc.ai[3] == 0) //No Weapon
            {
                if (internalAI[4] == 0)
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArms_Fly");
                }
                else
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArms");
                }
            }
            else if (npc.ai[3] == 1) //Bunzooka
            {
                if (internalAI[4] == 0)
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArmsB_Fly");
                }
                else
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArmsB");
                }
            }
            else if (npc.ai[3] == 2) //Punisher
            {
                if (internalAI[4] == 0)
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArms_Fly");
                }
                else
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArms");
                }
            }
            else //Royal Scepter
            {
                if (internalAI[4] == 0)
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArmsR_Fly");
                }
                else
                {
                    ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArmsR");
                }
            }
        }

        public bool TileBelowEmpty()
        {
            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + (float)npc.height) / 16f);

            for (int tY = tileY; tY < tileY + 17; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                {
                    Main.tile[tileX, tY] = new Tile();
                }
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[(int)Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void JumpAI()
        {
            if (npc.ai[0] == 0f)
            {
                npc.noTileCollide = false;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] > 0f)
                    {
                        if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more frequent the jumps
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .7f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .65f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .4f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .25f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .1f))
                        {
                            npc.ai[1] += 2;
                        }
                    }
                    if (npc.ai[1] >= 300f)
                    {
                        npc.ai[1] = -20f;
                        npc.frameCounter = 0;
                    }
                    else if (npc.ai[1] == -1f)
                    {
                        npc.TargetClosest(true);
                        npc.velocity.X = (float)(4 * npc.direction);
                        npc.velocity.Y = -12.1f;
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                    }
                }
            }
            else if (npc.ai[0] == 1f)
            {
                if (npc.velocity.Y == 0f)
                {
                    Main.PlaySound(SoundID.Item14, npc.position);
                    npc.ai[0] = 0f;
                    for (int num622 = (int)npc.position.X - 20; num622 < (int)npc.position.X + npc.width + 40; num622 += 20)
                    {
                        for (int num623 = 0; num623 < 4; num623++)
                        {
                            int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + (float)npc.height), npc.width + 20, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
                            Main.dust[num624].velocity *= 0.2f;
                        }
                        int num625 = Gore.NewGore(new Vector2((float)(num622 - 20), npc.position.Y + (float)npc.height - 8f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num625].velocity *= 0.4f;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                    if (npc.position.X < Main.player[npc.target].position.X && npc.position.X + (float)npc.width > Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                        npc.velocity.Y = npc.velocity.Y + 0.2f;
                    }
                    else
                    {
                        if (npc.direction < 0)
                        {
                            npc.velocity.X = npc.velocity.X - 0.2f;
                        }
                        else if (npc.direction > 0)
                        {
                            npc.velocity.X = npc.velocity.X + 0.2f;
                        }
                        float num626 = 3f;
                        if (npc.life < npc.lifeMax)
                        {
                            num626 += 1f;
                        }
                        if (npc.life < npc.lifeMax / 2)
                        {
                            num626 += 1f;
                        }
                        if (npc.life < npc.lifeMax / 4)
                        {
                            num626 += 1f;
                        }
                        if (npc.velocity.X < -num626)
                        {
                            npc.velocity.X = -num626;
                        }
                        if (npc.velocity.X > num626)
                        {
                            npc.velocity.X = num626;
                        }
                    }
                }
            }
        }

        public void FlyAI()
        {
            BaseAI.AISpaceOctopus(npc, ref internalAI, .25f, 7, 300, 0, null);
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            if (!Main.expertMode)
            {
                string[] lootTableA = { "BaneOfTheBunny", "Bunnyzooka", "RoyalScepter"};
                int lootA = Main.rand.Next(lootTableA.Length);
                npc.DropLoot(mod.ItemType(lootTableA[lootA]));
            }
            Projectile.NewProjectile(npc.Center, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
            npc.value = 0f;
            npc.boss = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.3f);  //boss damage increase in expermode
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, CapeTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            return false;
        }
    }
}