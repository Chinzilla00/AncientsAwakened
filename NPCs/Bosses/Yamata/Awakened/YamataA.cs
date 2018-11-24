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
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using AAMod;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    //[AutoloadBossHead]
    public class YamataA : YamataBoss
    {
        public NPC TrueHead;
        public bool HeadsSpawned = false;
        private bool Panic = false;
        bool cheated = false;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
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
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override void SetStaticDefaults()
        {
            displayName = "Yamata Awakened";

            Main.npcFrameCount[npc.type] = 7;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 80;
            npc.height = 90;
            npc.aiStyle = -1;
            if (!AAWorld.downedYamataA)
            {
                npc.lifeMax = 140000;
                if (npc.life > npc.lifeMax / 5)
                {
                    npc.damage = 80;
                    npc.defense = 80;
                }
                if (npc.life <= npc.lifeMax / 5)
                {
                    npc.damage = 100;
                    npc.defense = 90;
                }
            }
            if (AAWorld.downedYamataA)
            {
                npc.lifeMax = 150000;
                if (npc.life > npc.lifeMax / 5)
                {
                    npc.damage = 90;
                    npc.defense = 80;
                }
                if (npc.life <= npc.lifeMax / 5)
                {
                    npc.damage = 110;
                    npc.defense = 90;
                }
            }
            if (Main.expertMode)
            {
                npc.value = Item.buyPrice(20, 0, 0, 0);
            }
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Yamata2");
            npc.noGravity = true;
            npc.netAlways = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            frameWidth = 154;
            frameHeight = 118;
            npc.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 54, 0, 2);
            frameHead = BaseDrawing.GetFrame(frameCount, frameWidth, 118, 0, 2);
            bossBag = mod.ItemType("YamataBag");
            if (Main.expertMode)
            {
                int playerCount = 0;
                float bossHPScalar = 1f, scalarIncrement = 0.35f;
                if (Main.netMode != 0)
                {
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active)
                        {
                            playerCount++;
                        }
                    }
                    for (int j = 1; j < playerCount; j++)
                    {
                        bossHPScalar += scalarIncrement;
                        scalarIncrement += (1f - scalarIncrement) / 3f;
                    }
                }
                ScaleExpertStats(playerCount, bossHPScalar);
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                cheated = true;
                Main.NewText("CHEATER CHEATER PUMPKIN EATER! THAT HURT YOU KNOW!!!", new Color(146, 30, 68));
            }
            return false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
                AAWorld.downedYamataA = true;
            }

            if (!AAWorld.downedYamataA && Main.expertMode && !cheated)
            {
                Main.NewText("NO…! IMPOSSIBLE! EVEN IN MY AWAKENED FORM?! YOU MUST HAVE CHEATED! GYAAAAAAH..! FINE! TAKE YOUR LOOT AND GO AWAY..!", new Color(146, 30, 68));


            }
            if (AAWorld.downedYamataA && Main.expertMode && !cheated)
            {
                Main.NewText("NOOOOOOOOOOOOOO!!! YOU LITTLE BRAT!!! I ALMOST HAD YOU THIS TIME!!! FINE, take your stuff, See if I care!", new Color(146, 30, 68));

            }
            if (!Main.expertMode && !cheated)
            {
                Main.NewText("HAH! Nice try! Come back in Expert mode when you don’t have to cheat to beat me! All your loot is MINE still!", new Color(146, 30, 68));
            }
        }



        public override bool G_CanSpawn(int x, int y, int type, Player player)
        {
            return false;
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                BaseAI.DropItem(npc, mod.ItemType("YamataTrophy"), 1, 1, 15, true);
                npc.DropBossBags();
                AAWorld.downedYamataA = true;
            }
        }

        public float[] internalAI = new float[4];
        public int playerTooFarDist = 800;
        public int[] totalHealers = null;
        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1), frameHead = new Rectangle(0, 0, 1, 1);
        public bool flying = false;
        public Player playerTarget = null;
        public static int flyingTileCount = 4;
        public int MinionTimer = 0;

        //clientside stuff

        public Vector2 bottomVisualOffset = default(Vector2);
        public Vector2 topVisualOffset = default(Vector2);
        public LegInfo[] legs = null;
        public NPC dustMantid = null;

        public override void AI()
        {
            Main.dayTime = false;
            Main.time = 20000;

            MinionTimer++;
            if (MinionTimer == 300 && NPC.CountNPCS(mod.NPCType<YamataSoul>()) < 6)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<YamataSoul>());

                MinionTimer = 0;
            }
            
                if (!HeadsSpawned)
                {
                    if (Main.netMode != 1)
                    {
                        npc.realLife = npc.whoAmI;
                        int latestNPC = npc.whoAmI;
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 100, mod.NPCType("YamataAHead"), 0, npc.whoAmI);
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                        TrueHead = Main.npc[latestNPC];
                    }
                    HeadsSpawned = true;
                }

            if (npc.life <= npc.lifeMax / 5)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
            }
            dustMantid = npc;
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            bool foundTarget = TargetClosest();
            if (foundTarget)
            {
                int tileY = BaseWorldGen.GetFirstTileFloor((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f));
                npc.timeLeft = 300;

                float playerDistance = Vector2.Distance(playerTarget.Center, npc.Center);
                if ((playerDistance < playerTooFarDist - 100f) && Math.Abs(npc.velocity.X) > 12f) npc.velocity.X *= 0.8f;
                if ((playerDistance < playerTooFarDist - 100f) && Math.Abs(npc.velocity.Y) > 12f) npc.velocity.Y *= 0.8f;
                if (!flying && npc.velocity.Y > 7f) npc.velocity.Y *= 0.75f;
                internalAI[0]--; if (internalAI[0] <= 0) { internalAI[0] = 0; if (Main.netMode != 1) SwapAI(ref internalAI[0]); }
                AIMovementNormal();
            }
            else
            {
                flying = true;
                AIMovementRunAway();
            }
            //topVisualOffset = new Vector2(Math.Min(8f, Math.Abs(npc.velocity.X) * 2f), 0f) * (npc.velocity.X < 0 ? 1 : -1);
            bottomVisualOffset = new Vector2(Math.Min(3f, Math.Abs(npc.velocity.X)), 0f) * (npc.velocity.X < 0 ? 1 : -1);
            UpdateLimbs();
        }



        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaADust>();
            if (npc.life <= 0)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;

            }
            if (npc.life > npc.lifeMax / 5)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 5 && Panic == false && !AAWorld.downedYamataA && Main.expertMode && npc.type == mod.NPCType<YamataA>())
            {
                Panic = true;
                Main.NewText("Wh-WHA?! DIE! DIE YOU LITTLE TWERP! DIEDIEDIEDIEDIEDIEDIE!!!!", new Color(146, 30, 68));
            }
            if (npc.life <= npc.lifeMax / 5 && Panic == false && AAWorld.downedYamataA && Main.expertMode && npc.type == mod.NPCType<YamataA>())
            {
                Panic = true;
                Main.NewText("NO NO NO!!! NOT AGAIN!!! THIS TIME IMMA STOMP YOU RIGHT INTO THE GROUND!!!", new Color(146, 30, 68));
            }
        }

        public void AIMovementRunAway()
        {
            Main.NewText("NYEHEHEHEHEHEHEH..! And don’t come back!", new Color(45, 46, 70));
            npc.velocity.X *= 0.9f;
            if (Math.Abs(npc.velocity.X) < 0.01f) npc.velocity.X = 0f;
            npc.velocity.Y += 0.25f;
            npc.rotation = 0f;
            flying = true;
            if (npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; } //if out of map, kill mantid
        }

        public void AIMovementNormal(float movementScalar = 1f, float playerDistance = -1f)
        {
            float movementScalar2 = Math.Min(4f, Math.Max(1f, (playerDistance / (float)playerTooFarDist) * 4f));
            bool playerTooFar = playerDistance > playerTooFarDist;
            YamataBody(npc, ref npc.ai, true, 0.2f, 2f, 1.5f, 0.04f, 1.5f, 3);
            if (playerTooFar) npc.position += (playerTarget.position - playerTarget.oldPosition);
            npc.rotation = 0f;
        }

        public void YamataBody(NPC npc, ref float[] ai, bool ignoreWet = false, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
        {
            bool flyUpward = false;
            if (npc.justHit) { ai[2] = 0f; }
            if (ai[2] >= 0f)
            {
                int tileDist = 16;
                bool inRangeX = false;
                bool inRangeY = false;
                if (npc.position.X > ai[0] - (float)tileDist && npc.position.X < ai[0] + (float)tileDist) { inRangeX = true; }
                else
                    if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0)) { inRangeX = true; }
                tileDist += 24;
                if (npc.position.Y > ai[1] - (float)tileDist && npc.position.Y < ai[1] + (float)tileDist)
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
                        ai[2] = -200f;
                        npc.direction *= -1;
                        npc.velocity.X = npc.velocity.X * -1f;
                        npc.collideX = false;
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
                if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }

            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + (float)npc.height) / 16f);
            bool tileBelowEmpty = true;

            for (int tY = tileY; tY < tileY + hoverHeight; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                {
                    Main.tile[tileX, tY] = new Tile();
                }
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[(int)Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
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
                npc.velocity.X -= (moveInterval * 0.5f);
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = npc.velocity.X - 0.1f; }
                else
                    if (npc.velocity.X > 0f) { npc.velocity.X = npc.velocity.X + 0.05f; }
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = -maxSpeedX; }
            }
            else
            if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
            {
                npc.velocity.X += (moveInterval * 0.5f);
                if (npc.velocity.X < -maxSpeedX) { npc.velocity.X = npc.velocity.X + 0.1f; }
                else
                    if (npc.velocity.X < 0f) { npc.velocity.X = npc.velocity.X - 0.05f; }
                if (npc.velocity.X > maxSpeedX) { npc.velocity.X = maxSpeedX; }
            }


            if (npc.directionY == -1 && (double)npc.velocity.Y > -hoverMaxSpeed)
            {
                npc.velocity.Y = npc.velocity.Y - hoverInterval;
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = npc.velocity.Y - 0.05f; }
                else
                    if (npc.velocity.Y > 0f) { npc.velocity.Y = npc.velocity.Y + (hoverInterval - 0.01f); }
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = -hoverMaxSpeed; }
            }
            else
            if (npc.directionY == 1 && (double)npc.velocity.Y < hoverMaxSpeed)
            {
                npc.velocity.Y = npc.velocity.Y + hoverInterval;
                if ((double)npc.velocity.Y < -hoverMaxSpeed) { npc.velocity.Y = npc.velocity.Y + 0.05f; }
                else
                if (npc.velocity.Y < 0f) { npc.velocity.Y = npc.velocity.Y - (hoverInterval - 0.01f); }
                if ((double)npc.velocity.Y > hoverMaxSpeed) { npc.velocity.Y = hoverMaxSpeed; }
            }
        }


        public Rectangle topHitbox = default(Rectangle), bottomHitbox = default(Rectangle), leftHitbox = default(Rectangle), rightHitbox = default(Rectangle);
        public const int stateIdle = 0, stateMovementOnly = 1, stateArmScythes = 2, stateArmSpawns = 3, stateArmCombo = 4, stateFireEggs = 5, stateArmAndEggs = 6;
        public int[] timers = new int[] { 100, 60, 80, 170, 170, 160, 170 };
        public int[] statesToChangeTo = new int[] { stateMovementOnly, stateArmSpawns, stateArmCombo, stateFireEggs };
        public int[] statesToChangeToExpert = new int[] { stateMovementOnly, stateArmCombo, stateArmSpawns };
        public void SwapAI(ref float aiTime)
        {
            topHitbox = bottomHitbox = leftHitbox = rightHitbox = default(Rectangle);
            float prevAI = internalAI[1];
            if (prevAI == stateMovementOnly) //if the previous AI was movement only, choose a random attack AI sequence.
            {
                //internalAI[1] = (Main.expertMode ? statesToChangeToExpert[Main.rand.Next(statesToChangeToExpert.Length)] : statesToChangeTo[Main.rand.Next(statesToChangeTo.Length)]);
                //if (internalAI[1] != stateFireEggs && totalMinionCount < 2 && Main.rand.Next(3) != 0) internalAI[1] = stateFireEggs; //favor firing eggs if there are not many minions left
                //if (((internalAI[1] == stateArmCombo && Main.rand.Next(2) == 0) || internalAI[1] == stateFireEggs) && Main.expertMode && fourthHPLeft) internalAI[1] = stateArmAndEggs; //get serious at 25% hp on expert mode.
            }
            else// otherwise go to idle phase.
            {
                internalAI[1] = stateMovementOnly;
            }
            aiTime = timers[(int)internalAI[1]];
            npc.ai = new float[4];
            npc.netUpdate2 = true;
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
            else //found no jungle targets, you must be outside of it, time to make them pay :)
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

        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head.active)
            {
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 50);
                Vector2 center = head.Center;
                Vector2 distToProj = neckOrigin - head.Center;
                float projRotation = distToProj.ToRotation() - 1.57f;
                float distance = distToProj.Length();
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataANeck"), neckOrigin - Main.screenPosition,
                new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);
                Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(center));
                while (distance > 30f && !float.IsNaN(distance))
                {
                    distToProj.Normalize();                 //get unit vector
                    distToProj *= 30f;                      //speed = 30
                    center += distToProj;                   //update draw position
                    distToProj = neckOrigin - center;    //update distance
                    distance = distToProj.Length();


                    //Draw chain
                    spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataANeck"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
                        new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                        new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);

                }
                spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataANeck"), neckOrigin - Main.screenPosition,
                            new Rectangle(0, 0, 26, 40), drawColor, projRotation,
                            new Vector2(26 * 0.5f, 40 * 0.5f), 1f, SpriteEffects.None, 0f);

                spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y),
                            head.frame, drawColor, head.rotation,
                            new Vector2(64 * 0.5f, 80 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y),
                        head.frame, Color.White, head.rotation,
                        new Vector2(64 * 0.5f, 80 * 0.5f), 1f, SpriteEffects.None, 0f);
            }
        }

        public override void FindFrame(int frameHeight)
        {

            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            else if (npc.frameCounter < 10)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else if (npc.frameCounter < 15)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            else if (npc.frameCounter < 20)
            {
                npc.frame.Y = 3 * frameHeight;
            }
            else if (npc.frameCounter < 25)
            {
                npc.frame.Y = 4 * frameHeight;
            }
            else if (npc.frameCounter < 30)
            {
                npc.frame.Y = 5 * frameHeight;
            }
            else if (npc.frameCounter < 35)
            {
                npc.frame.Y = 6 * frameHeight;
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            BaseDrawing.DrawTexture(sb, mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataATail"), 0, npc.position + new Vector2(0f, npc.gfxOffY) + bottomVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], frameBottom, dColor, false);
            legs[2].DrawLeg(sb, npc, dColor); //back legs
            legs[3].DrawLeg(sb, npc, dColor);
            legs[0].DrawLeg(sb, npc, dColor); //front legs
            legs[1].DrawLeg(sb, npc, dColor);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY) + topVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);
            if (Main.netMode == 0)
            {
                DrawHead(sb, "NPCs/Bosses/Yamata/Awakened/YamataAHead", "NPCs/Bosses/Yamata/Awakened/YamataAHead_Glow", TrueHead, dColor);
            }
            return false;
        }
    }

    public class AnimationInfo
    {
        public int animType = 0;
        Vector2[] leftPos = null, left2Pos = null;
        Vector2[] rightPos = null, right2Pos = null;
        float[] leftRotations = null, left2Rotations = null;
        float[] rightRotations = null, right2Rotations = null;
        public float movementRatio = 0f, movementRate = 0.01f, animMult = 1f;
        public float halfPI = (float)Math.PI / 2f;
        public bool[] fired = new bool[4];
        public float[] hitRatios = null;
        public bool flatJoint = false;

        public AnimationInfo(int type, float speedMult = 1f, float aMult = 1f)
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
            get { return new Vector2(position.X + ((float)Hitbox.Width * 0.5f), position.Y + ((float)Hitbox.Height * 0.5f)); }
            set { position = new Vector2(value.X - ((float)Hitbox.Width * 0.5f), value.Y - ((float)Hitbox.Height * 0.5f)); }
        }
        public Rectangle Hitbox;
        public float rotation = 0f, movementRatio = 0f;
        public AnimationInfo overrideAnimation = null;
        public YamataA yamataA = null;
    }

    public class LegInfo : LimbInfo
    {
        Vector2 velocity, oldVelocity, legOrigin;
        float velOffsetY = 0f, distanceToMove = 120f, distanceToMoveX = 50f;
        bool flying = false, leftLeg = false;

        Vector2 pointToStandOn = default(Vector2);
        Vector2 legJoint = default(Vector2);
        public Texture2D[] textures = null;

        public LegInfo(int lType, Vector2 initialPos, YamataA m)
        {
            yamataA = m;
            position = initialPos;
            pointToStandOn = position;
            limbType = lType;
            Hitbox = new Rectangle(0, 0, 70, 38);
            legOrigin = new Vector2(limbType == 1 || limbType == 3 ? Hitbox.Width - 12 : 12, 12);
        }

        public void MoveLegFlying(NPC npc, bool leftLeg)
        {
            Vector2 movementSpot = GetMantidConnector(npc) + new Vector2((limbType == 3 ? (-35f - Hitbox.Width) : limbType == 2 ? 35f : limbType == 1 ? (-15f - Hitbox.Width) : 15f), (limbType == 1 || limbType == 0 ? 10f : 15f));
            float velLength = (npc.position - npc.oldPos[1]).Length();
            if (velLength > 8f)
            {
                position = movementSpot;
                velocity = default(Vector2);
            }
            else
            if (Vector2.Distance(movementSpot, position) > (40 + (int)npc.velocity.Length()))
            {
                Vector2 velAddon = (movementSpot - position); velAddon.Normalize(); velAddon *= (2f + (velLength * 0.25f));
                velocity += velAddon;
                float velMax = 4f + velLength;
                if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                position += velocity;
            }
            else
            {
                position = movementSpot;
                velocity = default(Vector2);
            }
        }

        public void UpdateVelOffsetY()
        {
            movementRatio += 0.04f;
            movementRatio = Math.Max(0f, Math.Min(1f, movementRatio));
            velOffsetY = BaseUtility.MultiLerp(movementRatio, 0f, 30f, 0f);
        }

        public void MoveLegWalking(NPC npc, bool leftLeg, Vector2 standOnPoint)
        {
            UpdateVelOffsetY();
            if (pointToStandOn != default(Vector2))
            {
                Vector2 velAddon = (pointToStandOn - position); velAddon.Normalize(); velAddon *= (1.6f + (npc.velocity.Length() * 0.5f));
                velocity += velAddon;
                float velMax = 4f + npc.velocity.Length();
                if (velocity.Length() > velMax) { velocity.Normalize(); velocity *= velMax; }
                if (Vector2.Distance(pointToStandOn, position) <= 15) { position = pointToStandOn; velocity = default(Vector2); }
                position += velocity;
                if ((position == pointToStandOn || Vector2.Distance(standOnPoint, position + new Vector2((float)Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX))
                {
                    pointToStandOn = default(Vector2);
                }
            }
            if (pointToStandOn == default(Vector2))
            {
                if (Vector2.Distance(standOnPoint, position + new Vector2((float)Hitbox.Width * 0.5f, 0f)) > distanceToMove || Math.Abs(position.X - standOnPoint.X) > distanceToMoveX)
                {
                    movementRatio = 0f;
                    pointToStandOn = standOnPoint;
                }
            }
        }

        public void UpdateLeg(NPC npc)
        {
            leftLeg = limbType == 1 || limbType == 3;
            if (Vector2.Distance(Center, npc.Center) > 1000f) position = npc.Center; //prevent issues when the legs are WAY off.
            if (overrideAnimation != null)
            {
                if (overrideAnimation.movementRatio >= 1f) overrideAnimation = null;
            }
            else
            if (((YamataA)npc.modNPC).flying)
            {
                rotation = leftLeg ? -0.25f : 0.25f;
                MoveLegFlying(npc, leftLeg);
            }
            else
            {
                rotation = 0f;
                Vector2 standOnPoint = GetStandOnPoint(npc);
                if (standOnPoint == default(Vector2)) //'flying' behavior but per leg
                {
                    MoveLegFlying(npc, leftLeg);
                }
                else
                {
                    MoveLegWalking(npc, leftLeg, standOnPoint);
                }
            }
            Vector2 mantidConnector = GetMantidConnector(npc);
            legJoint = Vector2.Lerp(position, mantidConnector, 0.5f) + new Vector2(leftLeg ? 30 : 0f, -40);
            oldPosition = position;
            oldVelocity = velocity;
        }

        public Vector2 GetStandOnPoint(NPC npc)
        {
            float scalar = npc.velocity.Length();
            float outerLegDefault = 70f + (0.5f * scalar);
            float innerLegDefault = 50f + (0.5f * scalar);
            float rightLegScalar = 1f + (npc.velocity.X > 2f ? (scalar * 0.2f) : 0f); //fixes an offset problem when the matriarch walks right
            float standOnX = npc.Center.X + yamataA.topVisualOffset.X + (limbType == 3 ? (-outerLegDefault - Hitbox.Width) : limbType == 2 ? (outerLegDefault + Hitbox.Width) : limbType == 1 ? (-innerLegDefault - Hitbox.Width) : (innerLegDefault + Hitbox.Width));
            Vector2 defaultPlacement = default(Vector2);
            int defaultTileY = (int)(npc.Bottom.Y / 16f);
            int tileY = BaseWorldGen.GetFirstTileFloor((int)(standOnX / 16f), (int)(npc.Bottom.Y / 16f));
            if (tileY - defaultTileY > YamataA.flyingTileCount) { return default(Vector2); } //'flying' behavior
            if (!flying && !yamataA.flying)
            {
                tileY = (int)((int)((float)tileY * 16f) / 16);
                float tilePosY = ((float)tileY * 16f);
                if (Main.tile[(int)(standOnX / 16f), tileY] == null || !Main.tile[(int)(standOnX / 16f), tileY].nactive() || !Main.tileSolid[Main.tile[(int)(standOnX / 16f), tileY].type]) tilePosY += 16f;
                return new Vector2(standOnX - (Hitbox.Width * 0.5f), tilePosY - Hitbox.Height);
            }
            return ((flying || yamataA.flying) ? default(Vector2) : defaultPlacement);
        }

        public Vector2 GetMantidConnector(NPC npc)
        {
            return npc.Center + yamataA.topVisualOffset + new Vector2((limbType == 3 || limbType == 1 ? -40f : 40f), 0f);
        }

        public void DrawLeg(SpriteBatch sb, NPC npc, Color dColor)
        {
            Mod mod = AAMod.instance;
            if (textures == null)
            {
                textures = new Texture2D[5];
                textures[0] = mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataALegCap");
                textures[1] = mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataALegSegment");
                textures[2] = mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataALegCapR");
                textures[3] = mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataALegSegmentR");
                textures[4] = mod.GetTexture("NPCs/Bosses/Yamata/Awakened/YamataAFoot");
            }
            Vector2 drawPos = position - new Vector2(0f, velOffsetY);
            Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(Center));
            if (!leftLeg)
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[3], null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), legJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[2], textures[3], textures[2] }, 0, legJoint, GetMantidConnector(npc), 0f, null, 1f, false, null);
            }
            else
            {
                BaseDrawing.DrawChain(sb, new Texture2D[] { null, textures[1], null }, 0, drawPos + new Vector2(Hitbox.Width * 0.5f, 6f), legJoint, 0f, null, 1f, false, null);
                BaseDrawing.DrawChain(sb, new Texture2D[] { textures[0], textures[1], textures[0] }, 0, legJoint, GetMantidConnector(npc), 0f, null, 1f, false, null);
            }
            BaseDrawing.DrawTexture(sb, textures[4], 0, drawPos, Hitbox.Width, Hitbox.Height, npc.scale, rotation, limbType == 1 || limbType == 3 ? 1 : -1, 1, Hitbox, lightColor, false, legOrigin);
        }

    }
}