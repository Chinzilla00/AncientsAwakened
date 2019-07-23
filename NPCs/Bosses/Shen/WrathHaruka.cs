using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using BaseMod;
using AAMod.NPCs.Bosses.AH.Haruka;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class WrathHaruka : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath Haruka");
            Main.npcFrameCount[npc.type] = 27;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 60;
            npc.friendly = false;
            npc.damage = 120;
            npc.defense = 180;
            npc.lifeMax = 130000;
            npc.HitSound = SoundID.NPCHit1;
            npc.value = Item.sellPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.lavaImmune = true;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
            npc.noGravity = false;
            npc.noTileCollide = false;
        }


        public int Frame = 0;
        public int[] internalAI = new int[6];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]); //Used as the AI selector
                writer.Write(internalAI[1]); //Used as the Frame Counter
                writer.Write(internalAI[2]); //Used for current frame
                writer.Write(internalAI[3]); //Used to count down to AI change
                writer.Write(internalAI[4]); //Used as an AI Timer
                writer.Write(internalAI[5]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadInt();
                internalAI[1] = reader.ReadInt();
                internalAI[2] = reader.ReadInt();
                internalAI[3] = reader.ReadInt();
                internalAI[4] = reader.ReadInt();
                internalAI[5] = reader.ReadInt();
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, mod.DustType<Dusts.AcidDust>(), npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
            if (npc.life <= 0)
            {
                DontSayDeathLine = false;
            }
        }

        private bool DontSayDeathLine = true;

        public override void NPCLoot()
        {
            if (DontSayDeathLine)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Father! Rrgh..! Next time we meet, I'll strike you down!", new Color(72, 78, 117));
            }
            else
            {
                if (Main.netMode != 1) BaseUtility.Chat("Ngh...sorry father...I can't carry on...", new Color(72, 78, 117));
            }
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<WrathHarukaVanish>());
            npc.value = 0f;
            npc.boss = false;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * .9f);
        }

        public override bool CheckActive()
        {
            return !NPC.AnyNPCs(mod.NPCType<ShenA>());
        }

        public bool SetMovePos = false;
        public float XPos = 20f;

        public float pos = 250f;

        public static int AISTATE_PROJ = 0, AISTATE_SLASH = 1, AISTATE_SPIN = 2, AISTATE_IDLE = 3;

        public int ProjectileShoot = -1;
        public int repeat = 10;
        public bool isSlashing = false;

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public bool Invisible = false;


        public override void AI()
        {
            Player player = Main.player[npc.target];

            npc.frame.Y = 74 * internalAI[2];

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 0);

            if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(false);
                if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
                    if (Main.netMode != 1)
                    {
                        int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<WrathHarukaVanish>(), 0);
                        Main.npc[DeathAnim].velocity = npc.velocity;
                        Main.npc[DeathAnim].netUpdate = true;
                        npc.active = false;
                        npc.netUpdate = true;
                    }
                    return;
                }
            }
            if (Invisible)
            {
                if (npc.alpha < 255)
                {
                    npc.alpha += 5;
                }
                else
                {
                    npc.chaseable = false;
                    npc.alpha = 255;
                }
            }
            else
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 8;
                }
                else
                {
                    npc.chaseable = true;
                    npc.alpha = 0;
                }
            }

            if (Main.netMode != 1)
            {
                internalAI[1]++;
                internalAI[5]++;
            }

            int InvisTimer1 = 1000;

            int InvisTimer2 = 1300;

            if (npc.life < npc.lifeMax * .66f)
            {
                InvisTimer1 = 800;

                InvisTimer2 = 1100;
            }
            if (npc.life < npc.lifeMax * .33f)
            {
                InvisTimer1 = 600;

                InvisTimer2 = 900;
            }
            if (internalAI[5] > InvisTimer1)
            {
                if (!Invisible)
                {
                    Invisible = true;
                    npc.netUpdate = true;
                }
            }
            if (internalAI[5] > InvisTimer2 && Main.netMode != 1)
            {
                Invisible = false;
                internalAI[5] = 0;
                npc.netUpdate = true;
            }



            if (ProjectileShoot == 0 || internalAI[0] == AISTATE_SLASH)
            {
                if (Main.netMode != 1)
                {
                    if (internalAI[1] > 4)
                    {
                        internalAI[1] = 0;
                        internalAI[2]++;
                    }
                }
            }
            else
            {
                if (Main.netMode != 1)
                {
                    if (internalAI[1] > 8)
                    {
                        internalAI[1] = 0;
                        internalAI[2]++;
                    }
                }
            }


            if (internalAI[0] == AISTATE_IDLE)
            {
                if (Main.netMode != 1)
                {
                    internalAI[3]++;
                    if (internalAI[3] >= 90)
                    {
                        internalAI[3] = 0;
                        internalAI[0] = Main.rand.Next(3);
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }

                if (internalAI[2] > 3 && Main.netMode != 1)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_PROJ)
            {
                if (ProjectileShoot == -1 && Main.netMode != 1)
                {
                    ProjectileShoot = Main.rand.Next(2);
                    npc.netUpdate = true;
                }
                if (ProjectileShoot == 0)
                {
                    if (internalAI[2] == 5 && internalAI[1] == 3 && Main.netMode != 1)
                    {
                        repeat -= 1;
                        int projType = mod.ProjectileType<HarukaKunai>();
                        float spread = 45f * 0.0174f;
                        Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                        dir *= 14f;
                        float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                        double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                        double deltaAngle = spread / 6f;
                        for (int i = 0; i < 3; i++)
                        {
                            double offsetAngle = startAngle + (deltaAngle * i);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projType, (int)(npc.damage / 1.5f), 5, Main.myPlayer);
                        }
                        npc.netUpdate = true;
                    }
                    if ((internalAI[2] < 4 || internalAI[2] > 6) && Main.netMode != 1)
                    {
                        internalAI[1] = 0;
                        internalAI[2] = 4;
                        npc.netUpdate = true;
                    }
                    if (repeat <= 0)
                    {
                        npc.frameCounter = 0;
                        Frame = 0;
                        if (Main.netMode != 1)
                        {
                            internalAI[0] = 3;
                            internalAI[1] = 0;
                            internalAI[2] = 0;
                            internalAI[3] = 0;
                            internalAI[4] = 0;
                            ProjectileShoot -= 1;
                            repeat = 12;
                            npc.ai = new float[4];
                            npc.netUpdate = true;
                        }
                    }
                }
                else if (ProjectileShoot == 1)
                {
                    internalAI[3]++;
                    if (Main.netMode != 1)
                    {
                        if (internalAI[3] == 100 || internalAI[3] == 200 || internalAI[3] == 299)
                        {
                            isSlashing = true;
                            npc.netUpdate = true;
                        }
                        if (isSlashing)
                        {
                            if (internalAI[2] < 7 || internalAI[2] > 9)
                            {
                                internalAI[1] = 0;
                                internalAI[2] = 7;
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            if (internalAI[2] > 3)
                            {
                                internalAI[1] = 0;
                                internalAI[2] = 0;
                                npc.netUpdate = true;
                            }
                        }
                    }

                    if (internalAI[2] == 8 && internalAI[1] == 4 && Main.netMode != 1)
                    {
                        Vector2 targetCenter = player.position + new Vector2(player.width * 0.5f, player.height * 0.5f);
                        Vector2 fireTarget = npc.Center;
                        int projType = mod.ProjectileType<HarukaProj>();
                        BaseAI.FireProjectile(targetCenter, fireTarget, projType, (int)(npc.damage * 1.3f), 0f, 18f);
                        npc.netUpdate = true;
                    }
                    if (isSlashing && internalAI[2] > 9 && Main.netMode != 1)
                    {
                        isSlashing = false;
                        npc.netUpdate = true;
                    }
                    if (internalAI[3] > 300)
                    {
                        npc.frameCounter = 0;
                        Frame = 0;
                        if (Main.netMode != 1)
                        {
                            internalAI[0] = 3;
                            internalAI[1] = 0;
                            internalAI[2] = 0;
                            internalAI[3] = 0;
                            internalAI[4] = 0;
                            ProjectileShoot -= 1;
                            npc.ai = new float[4];
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            else if (internalAI[0] == AISTATE_SLASH)
            {
                internalAI[3]++;

                if (internalAI[2] < 17)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 17;
                }
                if (internalAI[2] > 26)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 17;
                    internalAI[4] += 1;
                }
                if (internalAI[4] > 5)
                {
                    npc.frameCounter = 0;
                    Frame = 0;
                    if (Main.netMode != 1)
                    {
                        internalAI[0] = 3;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
            }
            else if (internalAI[0] == AISTATE_SPIN)
            {
                if (internalAI[2] < 10)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 10;
                }
                if (internalAI[2] > 16)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 13;
                }

                internalAI[4]++;

                if (SelectPoint)
                {
                    float Point = 500 * -npc.direction;
                    MovePoint = player.Center + new Vector2(Point, 0);
                    SelectPoint = false;
                    npc.netUpdate = true;
                }

                if (Vector2.Distance(npc.Center, player.Center) > 300f || internalAI[4] > 120)
                {
                    npc.frameCounter = 0;
                    Frame = 0;
                    if (Main.netMode != 1)
                    {
                        internalAI[0] = 3;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        internalAI[4] = 0;
                        pos *= -1f;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
            }
            else
            {
                if (Main.netMode != 1)
                {
                    internalAI[0] = 3;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[3] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }

            if (internalAI[0] == AISTATE_SLASH || internalAI[0] == AISTATE_SPIN) //Melee Damage/Speed boost
            {
                npc.damage = 300;
                npc.defense = 300;
            }
            else //Reset Stats
            {
                npc.defense = npc.defDefense;
                npc.damage = 80;
            }


            if (internalAI[0] == AISTATE_IDLE || internalAI[0] == AISTATE_PROJ) //When charging the player
            {
                MoveToPoint(wantedVelocity);
            }
            else if (internalAI[0] == AISTATE_SPIN)
            {
                MoveToPoint(MovePoint);
            }
            else if (internalAI[0] == AISTATE_SLASH) //When charging the player
            {
                MoveToPoint(npc.Center);
            }

            npc.rotation = 0;

            npc.noTileCollide = true;
        }


        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (ProjectileShoot == 0 || internalAI[0] == AISTATE_SLASH)
            {
                if (npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    Frame++;
                }
            }
            else
            {
                if (npc.frameCounter > 8)
                {
                    npc.frameCounter = 0;
                    Frame++;
                }
            }
            if (internalAI[0] == AISTATE_IDLE)
            {
                if (Frame > 3)
                {
                    npc.frameCounter = 0;
                    Frame = 0;
                }
            }
            else if (internalAI[0] == AISTATE_PROJ)
            {
                if (ProjectileShoot == 0)
                {
                    if (Frame < 4 || Frame > 6)
                    {
                        npc.frameCounter = 0;
                        Frame = 4;
                    }
                }
                else if (ProjectileShoot == 1)
                {
                    internalAI[3]++;
                    if (isSlashing)
                    {
                        if (Frame < 7 || Frame > 9) //Sets to frame 16
                        {
                            npc.frameCounter = 0;
                            Frame = 7;
                        }
                    }
                    else
                    {
                        if (Frame > 3)
                        {
                            npc.frameCounter = 0;
                            Frame = 0;
                        }
                    }
                }
            }
            else if (internalAI[0] == AISTATE_SLASH)
            {
                if (Frame < 17)
                {
                    npc.frameCounter = 0;
                    Frame = 17;
                }
                if (Frame > 26)
                {
                    npc.frameCounter = 0;
                    Frame = 17;
                }
            }
            else if (internalAI[0] == AISTATE_SPIN)
            {
                if (Frame < 10)
                {
                    npc.frameCounter = 0;
                    Frame = 10;
                }
                if (Frame > 16)
                {
                    npc.frameCounter = 0;
                    Frame = 13;
                }
            }
            npc.frame.Y = Frame * frameHeight;
        }

        public override void PostAI()
        {
            Player player = Main.player[npc.target];
            if (internalAI[0] != AISTATE_SPIN)
            {
                if (player.Center.X > npc.Center.X) //If NPC's X position is higher than the player's
                {
                    if (pos == -250)
                    {
                        pos = 250;
                    }
                    npc.direction = 1;
                }
                else //If NPC's X position is lower than the player's
                {
                    if (pos == 250)
                    {
                        pos = -250;
                    }
                    npc.direction = -1;
                }
            }
            else
            {
                npc.direction = npc.velocity.X > 0 ? 1 : -1;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 12f;
            if (Vector2.Distance(npc.Center, point) > 500)
            {
                moveSpeed = 16;
            }
            if (internalAI[0] == AISTATE_SPIN)
            {
                moveSpeed = 20f;
            }
            if (internalAI[0] == AISTATE_SLASH)
            {
                moveSpeed = 30f;
            }
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/WrathHaruka_Glow");

            if (internalAI[0] == AISTATE_SPIN)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 3, false, 0f, 0f, Color.Red);
            }

            Texture2D Slash = mod.GetTexture("NPCs/Bosses/AH/Haruka/HarukaSlash");

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, new Vector2(npc.position.X, npc.position.Y + 10), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 27, npc.frame, npc.GetAlpha(dColor), false);
            BaseDrawing.DrawTexture(spritebatch, Slash, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 27, npc.frame, dColor, false);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, new Vector2 (npc.position.X, npc.position.Y + 10), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 27, npc.frame, Color.White, false);
            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 27);
            return false;
        }
    }
}