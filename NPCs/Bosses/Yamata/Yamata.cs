using System;
using System.IO;
using AAMod.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class Yamata : YamataBoss
    {
        public NPC TrueHead;
        public NPC Head2;
        public NPC Head3;
        public NPC Head4;
        public NPC Head5;
        public NPC Head6;
        public NPC Head7;
        public bool HeadsSpawned = false;
        private bool quarterHealth = false;
        private bool threeQuarterHealth = false;
        private bool HalfHealth = false;
        public bool loludide = false;
        public bool flag;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Yamata";
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 80;
            npc.height = 90;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.lifeMax = 400000;
            npc.value = Item.sellPrice(0, 30, 0, 0);
            npc.defense = 999999;
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata");
            musicPriority = MusicPriority.BossHigh;
            npc.noGravity = true;
            npc.netAlways = true;
            frameWidth = 162;
            frameHeight = 118;
            npc.alpha = 255;
            npc.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 54, 0, 2);
            frameHead = BaseDrawing.GetFrame(frameCount, frameWidth, 118, 0, 2);
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/YamataRoar");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.chaseable = false;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            else
            {
                potionType = 0;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = 0;

            if (!AAWorld.downedYamata)
            {
                if (npc.life <= (npc.lifeMax / 4 * 3) && threeQuarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata10"), new Color(45, 46, 70));
                    threeQuarterHealth = true;
                }
                if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata11"), new Color(45, 46, 70));
                    HalfHealth = true;
                }
                if (npc.life <= npc.lifeMax / 4 && quarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata12"), new Color(45, 46, 70));
                    quarterHealth = true;
                }
            }
            if (AAWorld.downedYamata)
            {
                if (npc.life <= (npc.lifeMax / 4 * 3) && threeQuarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata13"), new Color(45, 46, 70));
                    threeQuarterHealth = true;
                }
                if (npc.life <= npc.lifeMax / 2 && HalfHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata14"), new Color(45, 46, 70));
                    HalfHealth = true;
                }
                if (npc.life <= npc.lifeMax / 4 && quarterHealth == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata15"), new Color(45, 46, 70));
                    quarterHealth = true;
                }
            }
            
            return true;
        }

        public bool Dead = false;

        public override void NPCLoot()
        {
            Dead = true;
            if (!Tag)
            {
                npc.DropLoot(Items.Vanity.Mask.YamataMask.type, 1f / 7f);
                if (!Main.expertMode)
                {
                    AAWorld.downedYamata = true;
                    npc.DropLoot(mod.ItemType("DreadScale"), 20, 30);
                    string[] lootTable = { "Flairdra", "Crescent", "Hydraslayer", "AbyssArrow", "HydraStabber", "MidnightWrath", "YamataTerratool" };
                    int loot = Main.rand.Next(lootTable.Length);
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                    npc.DropLoot(Items.Boss.Yamata.YamataTrophy.type, 1f / 10);
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Yamata1"), new Color(45, 46, 70));
                    npc.DropLoot(Items.Vanity.Mask.YamataMask.type, 1f / 7);
                    if (!AAWorld.downedYamata)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("Yamata2"), Color.Indigo);
                    }
                }
                if (Main.expertMode)
                {
                    int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataTransition"), 0, 0, 0, 0, 0, npc.target);
                    Main.npc[npcID].Center = npc.Center;
                    Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                }
                npc.value = 0f;
                npc.boss = false;
            }

        }

        public int playerTooFarDist = 800;
        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1), frameHead = new Rectangle(0, 0, 1, 1);
        public bool prevHalfHPLeft = false, halfHPLeft = false, prevFourthHPLeft = false, fourthHPLeft = false;
        public Player playerTarget = null;
        public static int flyingTileCount = 6, totalMinionCount = 0;
        public int MinionTimer = 0;

        //clientside stuff
        public Vector2 bottomVisualOffset = default;
        public Vector2 topVisualOffset = default;
        public LegInfo[] legs = null;
        public bool[] headsSaidOw = new bool[7];
        public bool Tag = false;
        public bool TeleportMe1 = false;
        public bool TeleportMe2 = false;
        public bool TeleportMe3 = false;
        public bool TeleportMe4 = false;
        public bool TeleportMe5 = false;
        public bool TeleportMe6 = false;
        public static bool TeleportMeBitch = false;

        public int SayTheLineYamata = 300;
        public bool FirstLine = false;
        public bool NoFly4U = false;
        public int NoFlyCountDown = 60;

        public void HandleHeads()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!HeadsSpawned)
                {
                    TrueHead = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataHead"), 0)];
                    TrueHead.ai[0] = npc.whoAmI;
                    Head2 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataHeadF1"), 0)];
                    Head2.ai[0] = npc.whoAmI;
                    Head3 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataHeadF1"), 0)];
                    Head3.ai[0] = npc.whoAmI;
                    Head4 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataHeadF1"), 0)];
                    Head4.ai[0] = npc.whoAmI;
                    Head5 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataHeadF2"), 0)];
                    Head5.ai[0] = npc.whoAmI;
                    Head6 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataHeadF2"), 0)];
                    Head6.ai[0] = npc.whoAmI;
                    Head7 = Main.npc[NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("YamataHeadF2"), 0)];
                    Head7.ai[0] = npc.whoAmI;

                    TrueHead.netUpdate = true;
                    Head2.netUpdate = true;
                    Head3.netUpdate = true;
                    Head4.netUpdate = true;
                    Head5.netUpdate = true;
                    Head6.netUpdate = true;
                    Head7.netUpdate = true;
                    HeadsSpawned = true;
                }
            }
            else
            {
                //the AI[0] checks are so when this is fargo'd into a multispawn it doesn't try to attach all the heads to one enemy if they are too close together.
                if (!HeadsSpawned)
                {
                    int[] npcs = BaseAI.GetNPCs(npc.Center, -1, default, 1000f, null);
                    if (npcs != null && npcs.Length > 0)
                    {
                        foreach (int npcID in npcs)
                        {
                            NPC npc2 = Main.npc[npcID];
                            if (npc2 != null)
                            {
                                if (TrueHead == null && npc2.type == mod.NPCType("YamataHead") && npc2.ai[0] == npc.whoAmI)
                                {
                                    TrueHead = npc2;
                                }
                                else
                                if (Head2 == null && npc2.type == mod.NPCType("YamataHeadF1") && npc2.ai[0] == npc.whoAmI)
                                {
                                    Head2 = npc2;
                                }
                                else
                                if (Head3 == null && npc2.type == mod.NPCType("YamataHeadF1") && npc2.ai[0] == npc.whoAmI)
                                {
                                    Head3 = npc2;
                                }
                                else
                                if (Head4 == null && npc2.type == mod.NPCType("YamataHeadF1") && npc2.ai[0] == npc.whoAmI)
                                {
                                    Head4 = npc2;
                                }
                                else
                                if (Head5 == null && npc2.type == mod.NPCType("YamataHeadF2") && npc2.ai[0] == npc.whoAmI)
                                {
                                    Head5 = npc2;
                                }
                                else
                                if (Head6 == null && npc2.type == mod.NPCType("YamataHeadF2") && npc2.ai[0] == npc.whoAmI)
                                {
                                    Head6 = npc2;
                                }
                                else
                                if (Head7 == null && npc2.type == mod.NPCType("YamataHeadF2") && npc2.ai[0] == npc.whoAmI)
                                {
                                    Head7 = npc2;
                                }
                            }
                        }
                    }
                    if (TrueHead != null && Head2 != null && Head3 != null && Head4 != null && Head5 != null && Head6 != null && Head7 != null)
                    {
                        HeadsSpawned = true;
                    }
                }
            }
        }

        bool title = false;

        public override void AI()
        {
            if (!title)
            {
                AAMod.ShowTitle(npc, 9);
                title = true;
            }

            TargetClosest();
            HandleHeads();

            if (Tag)
            {
                npc.life = 0;
                npc.netUpdate = true;
            }
            if (SayTheLineYamata <= 0)
            {
                SayTheLineYamata = 300;
            }

            if (Main.dayTime)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && !flag)
                {
                    flag = true;
                    AAMod.Chat(Lang.BossChat("Yamata4"), new Color(45, 46, 70));
                }
                npc.alpha += 10;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
                return;
            }

            prevHalfHPLeft = halfHPLeft;
            prevFourthHPLeft = fourthHPLeft;
            halfHPLeft = halfHPLeft || npc.life <= npc.lifeMax / 2;
            fourthHPLeft = fourthHPLeft || npc.life <= npc.lifeMax / 4;

            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            bool foundTarget = TargetClosest();
            if (foundTarget)
            {
                for (int p = 0; p < Main.maxPlayers; p++)
                {
                    Player t = Main.player[p];
                    if (t.active && !t.dead)
                    {
                        Main.player[p].AddBuff(ModContent.BuffType<Buffs.YamataGravity>(), 10, true);
                    }
                }
                NoFlyCountDown--;
                if (!NoFly4U && NoFlyCountDown <= 0 && !AAWorld.downedYamata)
                {
                    NoFlyCountDown = 0;
                    NoFly4U = true;

                    if (npc.type == ModContent.NPCType<Yamata>()) if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata6"), new Color(45, 46, 70));
                }

                float dist = npc.Distance(playerTarget.Center);
                if (dist > 1200 || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient && SayTheLineYamata == 300)
                    {
                        if (!FirstLine)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata8"), new Color(45, 46, 70));
                            FirstLine = true;
                        }
                    }
                    SayTheLineYamata--;
                    npc.alpha += 3;
                    if (npc.alpha >= 255)
                    {
                        npc.alpha = 255;
                        Vector2 tele = playerTarget.Center + new Vector2(0, -200);// +  (playerTarget.velocity == new Vector2(0,0)? new Vector2(0,0) : Vector2.Normalize(playerTarget.velocity) * playerTarget.velocity.Length() * 54.33f);
                        TeleportMe1 = true;
                        TeleportMe2 = true;
                        TeleportMe3 = true;
                        TeleportMe4 = true;
                        TeleportMe5 = true;
                        TeleportMe6 = true;
                        TeleportMeBitch = true;
                        npc.Center = tele;
                        npc.dontTakeDamage = true;
                        TrueHead.dontTakeDamage = true;
                        Head2.dontTakeDamage = true;
                        Head3.dontTakeDamage = true;
                        Head4.dontTakeDamage = true;
                        Head5.dontTakeDamage = true;
                        Head6.dontTakeDamage = true;
                        Head7.dontTakeDamage = true;
                    }
                }
                else
                {
                    npc.alpha -= 8;
                    SayTheLineYamata = 300;
                    if (npc.alpha <= 0)
                    {
                        npc.dontTakeDamage = false;
                        TrueHead.dontTakeDamage = false;
                        Head2.dontTakeDamage = false;
                        Head3.dontTakeDamage = false;
                        Head4.dontTakeDamage = false;
                        Head5.dontTakeDamage = false;
                        Head6.dontTakeDamage = false;
                        Head7.dontTakeDamage = false;
                        npc.alpha = 0;
                    }
                }
                npc.timeLeft = 300;
                float playerDistance = Vector2.Distance(playerTarget.Center, npc.Center);
                if ((playerDistance < playerTooFarDist - 100f) && Math.Abs(npc.velocity.X) > 12f) npc.velocity.X *= 0.8f;
                if ((playerDistance < playerTooFarDist - 100f) && Math.Abs(npc.velocity.Y) > 12f) npc.velocity.Y *= 0.8f;
                if (npc.velocity.Y > 7f) npc.velocity.Y *= 0.75f;
                AIMovementNormal(playerDistance);
            }
            else
            {
                AIMovementRunAway();
            }
            bottomVisualOffset = new Vector2(Math.Min(3f, Math.Abs(npc.velocity.X)), 0f) * (npc.velocity.X < 0 ? 1 : -1);
            UpdateLimbs();
        }

        public void AIMovementRunAway()
        {
            if ((Main.netMode != NetmodeID.MultiplayerClient) && !loludide)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat(Lang.BossChat("Yamata9"), new Color(45, 46, 70));
                loludide = true;
            }

            npc.alpha += 10;
            if (npc.alpha >= 255)
            {
                npc.active = false;
            }
        }

        public void AIMovementNormal(float playerDistance)
        {
            bool playerTooFar = playerDistance > playerTooFarDist;
            YamataBody(npc, ref npc.ai, true, 0.2f, 3.5f, 8f, 0.07f, 1.5f, 4);
            if (playerTooFar) npc.position += playerTarget.position - playerTarget.oldPosition;
            npc.rotation = 0f;
        }

        public void YamataBody(NPC npc, ref float[] ai, bool ignoreWet = true, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
        {
            bool flyUpward = false;
            if (npc.justHit) { ai[2] = 0f; }
            if (ai[2] >= 0f)
            {
                int tileDist = 16;
                bool inRangeX = false;
                bool inRangeY = false;
                if (npc.position.X > ai[0] - tileDist && npc.position.X < ai[0] + tileDist) { inRangeX = true; }
                else
                    if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0)) { inRangeX = true; }
                tileDist += 24;
                if (npc.position.Y > ai[1] - tileDist && npc.position.Y < ai[1] + tileDist)
                {
                    inRangeY = true;
                }
                if (inRangeX && inRangeY)
                {
                    ai[2] += 1f;
                    if (ai[2] >= 30f && tileDist == 16)
                    {
                        flyUpward = true;
                    }
                    if (ai[2] >= 60f)
                    {
                        ai[2] = 0f;
                    }
                }
                else
                {
                    ai[0] = npc.position.X;
                    ai[1] = npc.position.Y;
                    ai[2] = 0f;
                }
                npc.TargetClosest(true);
            }
            else
            {
                ai[2] += 1f;
                if (Main.player[npc.target].position.X + Main.player[npc.target].width / 2 > npc.position.X + npc.width / 2)
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }

            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + npc.height) / 16f);
            bool tileBelowEmpty = true;

            for (int tY = tileY; tY < tileY + hoverHeight; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                {
                    Main.tile[tileX, tY] = new Tile();
                }
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
                {
                    tileBelowEmpty = false;
                    break;
                }
            }
            if (flyUpward)
            {
                tileBelowEmpty = true;
            }

            if (tileBelowEmpty)
            {
                npc.velocity.Y += moveInterval;
                if (npc.velocity.Y > 9f)
                {
                    npc.velocity.Y = 9f;
                }
            }
            else
            {
                if (npc.directionY < 0 && npc.velocity.Y > 0f) { npc.velocity.Y -= moveInterval; }
                if (npc.velocity.Y < -maxSpeedY) { npc.velocity.Y = -maxSpeedY; }
            }

            if (!ignoreWet && npc.wet)
            {
                npc.velocity.Y -= moveInterval;
                if (npc.velocity.Y < -maxSpeedY * 0.75f) { npc.velocity.Y = -maxSpeedY * 0.75f; }
            }


            if (npc.collideY)
            {
                npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 1f) { npc.velocity.Y = 1f; }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -1f) { npc.velocity.Y = -1f; }
            }

            if (!tileBelowEmpty && npc.target > -1 && Main.player[npc.target].active && !Main.player[npc.target].dead && Math.Abs(Main.player[npc.target].Center.X - npc.Center.X) < 50) //force a hover
            {
                if (Math.Abs(npc.velocity.X) > 0.3f) npc.velocity.X *= 0.9f;
                if (Math.Abs(npc.velocity.Y) > 0.3f) npc.velocity.Y *= 0.9f;
            }
            else
            if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
            {
                npc.velocity.X -= moveInterval * 0.5f;
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X -= 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X += 0.05f; }
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
            }
            else
            if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
            {
                npc.velocity.X += moveInterval * 0.5f;
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X += 0.1f; }
                else
                    if (npc.velocity.X < 0f) { npc.velocity.X -= 0.05f; }
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
            }


            if (npc.directionY == -1 && (double)npc.velocity.Y > -hoverMaxSpeed)
            {
                npc.velocity.Y -= hoverInterval;
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y -= 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y += hoverInterval - 0.01f; }
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = -hoverMaxSpeed; }
            }
            else
            if (npc.directionY == 1 && (double)npc.velocity.Y < hoverMaxSpeed)
            {
                npc.velocity.Y += hoverInterval;
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y += 0.05f; }
                else
                if (npc.velocity.Y < 0f) { npc.velocity.Y -= hoverInterval - 0.01f; }
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = hoverMaxSpeed; }
            }

            
        }

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(npc.Center, 4200f);
            float dist = 999999999f;
            int foundPlayer = -1;
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            else
            {
                for (int m = 0; m < players.Length; m++)
                {
                    Player p = Main.player[players[m]];
                    if (Vector2.Distance(p.Center, npc.Center) < dist)
                    {
                        dist = Vector2.Distance(p.Center, npc.Center);
                        foundPlayer = p.whoAmI;
                    }
                }
            }
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale *= 2;
            return true;
        }

        public void UpdateLimbs()
        {
            if (legs == null || legs.Length < 4)
            {
                legs = new LegInfo[4];
                legs[0] = new LegInfo(0, npc.Bottom + new Vector2(60, 0), this);
                legs[1] = new LegInfo(1, npc.Bottom + new Vector2(-82, 0), this);
                legs[2] = new LegInfo(2, npc.Bottom + new Vector2(80, 0), this);
                legs[3] = new LegInfo(3, npc.Bottom + new Vector2(-102, 0), this);
            }
            for (int m = 0; m < 4; m++)
            {
                legs[m].UpdateLeg(npc);
            }
        }

        public Vector2 position, oldPosition;
        private static float X(float t,
    float x0, float x1, float x2)
        {
            return (float)(
                x0 * Math.Pow((1 - t), 2) +
                x1 * 2 * t * Math.Pow((1 - t), 1) +
                x2 * Math.Pow(t, 2)
            );
        }
        private static float Y(float t,
            float y0, float y1, float y2)
        {
            return (float)(
                 y0 * Math.Pow((1 - t), 2) +
                 y1 * 2 * t * Math.Pow((1 - t), 1) +
                 y2 * Math.Pow(t, 2)
             );
        }
        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor, bool DrawUnder)
        {
            Color GlowColor = Color.White;
            if (head != null && head.active && head.modNPC != null && (head.modNPC is YamataHead || head.modNPC is YamataHeadF1))
            {
                string neckTex = "NPCs/Bosses/Yamata/YamataNeck";
                Texture2D neckTex2D = mod.GetTexture(neckTex);
                Vector2 connector = head.Center;
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 40);
                float chainsPerUse = 0.05f;
                for (float i = 0; i <= 1; i += chainsPerUse)
                {
                    Vector2 distBetween;
                    float projTrueRotation;
                    if (i != 0)
                    {
                        distBetween = new Vector2(X(i, neckOrigin.X, (neckOrigin.X + connector.X) / 2, connector.X) -
                        X(i - chainsPerUse, neckOrigin.X, (neckOrigin.X + connector.X) / 2, connector.X),
                        Y(i, neckOrigin.Y, (neckOrigin.Y + 50), connector.Y) -
                        Y(i - chainsPerUse, neckOrigin.Y, (neckOrigin.Y + 50), connector.Y));
                        projTrueRotation = distBetween.ToRotation() - (float)Math.PI / 2;
                        spriteBatch.Draw(neckTex2D, new Vector2(X(i, neckOrigin.X, (neckOrigin.X + connector.X) / 2, connector.X) - Main.screenPosition.X, Y(i, neckOrigin.Y, (neckOrigin.Y + 50), connector.Y) - Main.screenPosition.Y),
                        new Rectangle(0, 0, neckTex2D.Width, neckTex2D.Height), drawColor, projTrueRotation,
                        new Vector2(neckTex2D.Width * 0.5f, neckTex2D.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                    }
                }
                BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture(headTexture), 0, head.position + new Vector2(0f, head.gfxOffY) + topVisualOffset, head.width, head.height, head.scale, head.rotation, head.spriteDirection, Main.npcFrameCount[head.type], head.frame, drawColor, false);
                BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture(glowMaskTexture), 0, head.position + new Vector2(0f, head.gfxOffY) + topVisualOffset, head.width, head.height, head.scale, head.rotation, head.spriteDirection, Main.npcFrameCount[head.type], head.frame, GlowColor, false);
            }
        }

        public override void PostDraw(SpriteBatch sb, Color dColor)
        {
            Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(npc.Center));
            string tailTex = "NPCs/Bosses/Yamata/YamataTail";
            string headTex = "NPCs/Bosses/Yamata/YamataHead";
            BaseDrawing.DrawTexture(sb, mod.GetTexture(tailTex), 0, npc.position + new Vector2(0f, npc.gfxOffY) + bottomVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], frameBottom, lightColor, false);
            if (legs != null && legs.Length == 4)
            {
                legs[2].DrawLeg(sb, npc); //back legs
                legs[3].DrawLeg(sb, npc);
                legs[0].DrawLeg(sb, npc); //front legs
                legs[1].DrawLeg(sb, npc);
            }
            DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF1", "Glowmasks/YamataHeadF1_Glow", Head2, dColor, false);
            DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF1", "Glowmasks/YamataHeadF1_Glow", Head3, dColor, false);
            DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF1", "Glowmasks/YamataHeadF1_Glow", Head4, dColor, false);
            DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF2", "Glowmasks/YamataHeadF2_Glow", Head5, dColor, false);
            DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF2", "Glowmasks/YamataHeadF2_Glow", Head6, dColor, false);
            DrawHead(sb, "NPCs/Bosses/Yamata/YamataHeadF2", "Glowmasks/YamataHeadF2_Glow", Head7, dColor, false);

            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY) + topVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, lightColor, false);
            
            DrawHead(sb, headTex, "Glowmasks/YamataHead_Glow", TrueHead, dColor, false);
        }
    }

    public class AnimationInfo
    {
        public int animType = 0;
        public float movementRatio = 0f, movementRate = 0.01f, animMult = 1f;
        public float halfPI = (float)Math.PI / 2f;
        public bool[] fired = new bool[4];
        public float[] hitRatios = null;
        public bool flatJoint = false;

        public AnimationInfo(int type, float aMult = 1f)
        {
            animType = type;
            animMult = aMult;
        }
    }

    public class LimbInfo
    {
        public int limbType = 0;
        public Vector2 position, oldPosition;
        public Vector2 Center
        {
            get { return new Vector2(position.X + (Hitbox.Width * 0.5f), position.Y + (Hitbox.Height * 0.5f)); }
            set { position = new Vector2(value.X - (Hitbox.Width * 0.5f), value.Y - (Hitbox.Height * 0.5f)); }
        }
        public Rectangle Hitbox;
        public float rotation = 0f, movementRatio = 0f;
        public AnimationInfo overrideAnimation = null;
        public Yamata yamata = null;
    }

    public class LegInfo : LimbInfo
    {
        Vector2 velocity, legOrigin;
        private float velOffsetY = 0f;
        private readonly float distanceToMove = 120f, distanceToMoveX = 50f;
        private readonly bool flying = false;
        private bool leftLeg = false;

        Vector2 pointToStandOn = default;
        Vector2 legJoint = default;
        public Texture2D[] textures = null;

        public LegInfo(int lType, Vector2 initialPos, Yamata m)
        {
            yamata = m;
            position = initialPos;
            pointToStandOn = position;
            limbType = lType;
            Hitbox = new Rectangle(0, 0, 70, 38);
            legOrigin = new Vector2(limbType == 1 || limbType == 3 ? Hitbox.Width - 12 : 12, 12);
        }

        public void MoveLegFlying(NPC npc)
        {
            Vector2 movementSpot = GetBodyConnector(npc) + new Vector2(limbType == 3 ? (-35f - Hitbox.Width) : limbType == 2 ? 35f : limbType == 1 ? (-15f - Hitbox.Width) : 15f, limbType == 1 || limbType == 0 ? 40f : 50f);
            float velLength = (npc.position - npc.oldPos[1]).Length();
            if (velLength > 8f)
            {
                position = movementSpot;
                velocity = default;
            }
            else
            if (Vector2.Distance(movementSpot, position) > (40 + (int)npc.velocity.Length()))
            {
                Vector2 velAddon = movementSpot - position; velAddon.Normalize(); velAddon *= 2f + (velLength * 0.25f);
                velocity += velAddon;
                float velMax = 4f + velLength;
                if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                position += velocity;
            }
            else
            {
                position = movementSpot;
                velocity = default;
            }
        }

        public void UpdateVelOffsetY()
        {
            movementRatio += 0.04f;
            movementRatio = Math.Max(0f, Math.Min(1f, movementRatio));
            velOffsetY = BaseUtility.MultiLerp(movementRatio, 0f, 30f, 0f);
        }

        public void MoveLegWalking(NPC npc, Vector2 standOnPoint)
        {
            UpdateVelOffsetY();
            if (pointToStandOn != default)
            {
                Vector2 velAddon = pointToStandOn - position; velAddon.Normalize(); velAddon *= 1.6f + (npc.velocity.Length() * 0.5f);
                velocity += velAddon;
                float velMax = 4f + npc.velocity.Length();
                if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                if (Vector2.Distance(pointToStandOn, position) <= 15) { position = pointToStandOn; velocity = default; }
                position += velocity;
                if (position == pointToStandOn || Vector2.Distance(standOnPoint, position + new Vector2(Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX)
                {
                    pointToStandOn = default;
                }
            }
            if (pointToStandOn == default)
            {
                if (Vector2.Distance(standOnPoint, position + new Vector2(Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX)
                {
                    movementRatio = 0f;
                    pointToStandOn = standOnPoint;
                }
            }
        }

        public void UpdateLeg(NPC npc)
        {
            leftLeg = limbType == 1 || limbType == 3;
            if (Vector2.Distance(Center, npc.Center) > 499 || Yamata.TeleportMeBitch) position = npc.Center; //prevent issues when the legs are WAY off.
            if (overrideAnimation != null)
            {
                if (overrideAnimation.movementRatio >= 1f) overrideAnimation = null;
            }
            else
            {
                rotation = 0f;
                Vector2 standOnPoint = GetStandOnPoint(npc);
                if (standOnPoint == default) //'flying' behavior but per leg
                {
                    MoveLegFlying(npc);
                }
                else
                {
                    MoveLegWalking(npc, standOnPoint);
                }
            }
            Vector2 bodyConnector = GetBodyConnector(npc);
            legJoint = Vector2.Lerp(position, bodyConnector, 0.3f) + new Vector2(leftLeg ? 30 : 0f, -30);
            oldPosition = position;
        }

        public Vector2 GetStandOnPoint(NPC npc)
        {
            float scalar = npc.velocity.Length();
            float outerLegDefault = 70f + (0.5f * scalar);
            float innerLegDefault = 50f + (0.5f * scalar);
            float standOnX = npc.Center.X + yamata.topVisualOffset.X + (limbType == 3 ? (-outerLegDefault - Hitbox.Width) : limbType == 2 ? (outerLegDefault + Hitbox.Width) : limbType == 1 ? (-innerLegDefault - Hitbox.Width) : (innerLegDefault + Hitbox.Width));

            int defaultTileY = (int)(npc.Bottom.Y / 16f);
            int tileY = BaseWorldGen.GetFirstTileFloor((int)(standOnX / 16f), (int)(npc.Bottom.Y / 16f));
            if (tileY - defaultTileY > Yamata.flyingTileCount) { return default; } //'flying' behavior
            if (!flying)
            {
                tileY = (int)(tileY * 16f) / 16;
                float tilePosY = tileY * 16f;
                if (Main.tile[(int)(standOnX / 16f), tileY] == null || !Main.tile[(int)(standOnX / 16f), tileY].nactive() || !Main.tileSolid[Main.tile[(int)(standOnX / 16f), tileY].type]) tilePosY += 16f;
                return new Vector2(standOnX - (Hitbox.Width * 0.5f), tilePosY - Hitbox.Height);
            }
            return default;
        }

        public Vector2 GetBodyConnector(NPC npc)
        {
            return npc.Center + yamata.topVisualOffset + new Vector2(limbType == 3 || limbType == 1 ? -40f : 40f, 0f);
        }

        public void DrawLeg(SpriteBatch sb, NPC npc)
        {
            Mod mod = AAMod.instance;
            if (textures == null)
            {
                bool awakened = npc.type == mod.NPCType("YamataA");
                string texRoot = "NPCs/Bosses/Yamata/Yamata";
                if (awakened) texRoot = "NPCs/Bosses/Yamata/Awakened/YamataA";
                textures = new Texture2D[5];
                textures[0] = mod.GetTexture(texRoot + "LegCap");
                textures[1] = mod.GetTexture(texRoot + "LegSegment");
                textures[2] = mod.GetTexture(texRoot + "LegCapR");
                textures[3] = mod.GetTexture(texRoot + "LegSegmentR");
                textures[4] = mod.GetTexture(texRoot + "Foot");
            }
            Vector2 drawPos = position - new Vector2(0f, velOffsetY);
            Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(Center));
            if (!leftLeg)
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[3], null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), legJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[2], textures[3], textures[2] }, 0, legJoint, GetBodyConnector(npc), 0f, null, 1f, false, null);
            }
            else
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[1], null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), legJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[0], textures[1], textures[0] }, 0, legJoint, GetBodyConnector(npc), 0f, null, 1f, false, null);
            }
            BaseDrawing.DrawTexture(sb, textures[4], 0, drawPos, Hitbox.Width, Hitbox.Height, npc.scale, rotation, limbType == 1 || limbType == 3 ? 1 : -1, 1, Hitbox, lightColor, false, legOrigin);
        }
    }
}