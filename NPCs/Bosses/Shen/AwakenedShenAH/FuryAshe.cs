using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;
using AAMod.NPCs.Bosses.AH.Ashe;

namespace AAMod.NPCs.Bosses.Shen.AwakenedShenAH
{
    [AutoloadBossHead]
    public class FuryAshe : ModNPC
    {
        public int OrbiterCount = Main.expertMode ? 16 : 10;
        private float moveSpeed = 15f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Ashe");
            Main.npcFrameCount[npc.type] = 24;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 80;
            npc.damage = 150;
            npc.defense = 150;
            npc.lifeMax = 100000;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.knockBackResist = 0f;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
        }
        
        public int[] Vortexes = null;

        public bool RuneCrash = false;
        public bool Health = false;

        public override bool CheckActive()
        {
            return !NPC.AnyNPCs(ModContent.NPCType<ShenA>());
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);

            npc.direction = npc.spriteDirection = npc.position.X < player.position.X ? 1 : -1;
            RingEffects();

            npc.damage = npc.defDamage * VortexDamage();

            Vector2 targetPos;

            switch (npc.ai[0])
            {
                case 0:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 1:
                    if (!AliveCheck(player))
                        break;

                    MoveToPoint(wantedVelocity);

                    BaseAI.ShootPeriodic(npc, player.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), player.width, player.height, ModContent.ProjectileType<AsheShot>(), ref npc.ai[2], 18, npc.damage / 2, 9, false);
                    if (npc.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        AIChange();
                    }
                    break;
                case 2:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 3:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 4:
                    if (!AliveCheck(player))
                        break;

                    int firepos = 0;
                    if (player.Center.X > npc.Center.X) //If NPC's X position is less than the player's
                    {
                        firepos = 200;
                    }
                    else
                    {
                        firepos = -200;
                    }

                    wantedVelocity = player.Center - new Vector2(firepos, 0);

                    MoveToPoint(wantedVelocity);

                    BaseAI.ShootPeriodic(npc, player.Center, player.width, player.height, ModContent.ProjectileType<AsheFlamethrower>(), ref npc.ai[2], 5, npc.damage / 2, 24, false);
                    if (npc.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        npc.ai[1] = 0;
                        AIChange();
                    }
                    break;
                case 5: //draw dash frame
                    if (!AliveCheck(player))
                        break;
                    MoveToPoint(wantedVelocity);
                    if (++npc.ai[1] > 30)
                    {
                        npc.ai[1] = 0;
                        npc.ai[0]++;
                        npc.netUpdate = true;
                    }
                    break;
                case 6: //prepare for fishron dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(npc.Center) * 600;
                    Movement(targetPos, 0.8f);
                    if (++npc.ai[1] > 20)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                        npc.velocity = npc.DirectionTo(player.Center) * (npc.life < npc.lifeMax/3 ? 50:40);
                        if(npc.velocity.Length() < 40f)
                        {
                            npc.velocity = Vector2.Normalize(npc.oldVelocity) * (npc.life < npc.lifeMax/3 ? 50:40);
                        }
                    }
                    break;

                case 7: //dashing
                    if (++npc.ai[2] > 3)
                    {
                        npc.ai[2] = 0;
                        if (Main.netMode != 1)
                        {
                            const float ai0 = 0.01f;
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(npc.velocity).RotatedBy(Math.PI / 2), mod.ProjectileType("AsheSpell"), npc.damage / 4, 0f, Main.myPlayer, ai0);
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(npc.velocity).RotatedBy(-Math.PI / 2), mod.ProjectileType("AsheSpell"), npc.damage / 4, 0f, Main.myPlayer, ai0);
                        }
                    }
                    if (++npc.ai[1] > 40)
                    {
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        if (++npc.ai[3] >= (npc.life < npc.lifeMax/3 ? 4:3)) //dash three/Four times
                        {
                            if(npc.life < npc.lifeMax / 3)
                            {
                                npc.ai[0] = Main.rand.Next(4) == 0? 4:9;
                            }
                            else
                            {
                                npc.ai[0]++;
                            }
                            npc.ai[3] = 0;
                        }
                        else
                        {
                            npc.ai[0]--;
                        }
                        npc.netUpdate = true;
                    }
                    npc.rotation = npc.velocity.ToRotation();
                    if (npc.velocity.X < 0)
                        npc.rotation += (float)Math.PI;
                    break;
                case 8:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 9:
                    if (!AliveCheck(player))
                        break;
                    
                    if (npc.ai[1] >= 100)
                    {
                        MoveToPoint(player.Center + new Vector2((player.velocity.X > 0? 1 : -1) * 350, -200));
                    }
                    else
                    {
                        MoveToPoint(wantedVelocity);
                    }
                    if (npc.life > npc.lifeMax / 3 || npc.ai[1] < 100)
                    {
                        BaseAI.ShootPeriodic(npc, player.Center, player.width, player.height, ModContent.ProjectileType<AsheFire>(), ref npc.ai[2], npc.life < npc.lifeMax * 0.666f ? 20 : 60, npc.damage / 2, 8, false);
                    }
                    if (npc.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        if (npc.life < npc.lifeMax / 3)
                        {
                            for(int i = 0; i < 8; i++)
                            {
                                Vector2 shoot = new Vector2((float)Math.Sin(i * 0.25f * 3.1415926f), (float)Math.Cos(i * 0.25f * 3.1415926f));
                                shoot *= 8f;
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, shoot.X, shoot.Y, ModContent.ProjectileType<AsheFire>(), npc.damage / 2, 5, Main.myPlayer);
                            }
                            if(Main.rand.Next(3) == 0) goto case 5;
                        }
                        AIChange();
                    }
                    break;
                case 10:
                    if (NPC.CountNPCS(ModContent.NPCType<AsheDragon>()) < 2)
                    {
                        npc.ai[0] = 12;
                        goto case 12;
                    }
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 11:
                    if (!AliveCheck(player))
                        break;
                    MoveToPoint(wantedVelocity);
                    if (npc.ai[1]++ > 200)
                    {
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y - 100, ModContent.NPCType<Shenling>());
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y + 100, ModContent.NPCType<Shenling>());
                        AIChange();
                    }
                    break;
                case 12:
                    if (!AliveCheck(player))
                        break;
                    FireMagic(npc);
                    IdlePhase();
                    break;
                default:
                    npc.ai[0] = 0;
                    goto case 0;
            }

            if (npc.ai[0] != 6 && npc.ai[0] != 7)
            {
                npc.rotation = 0;
            }

            if (npc.ai[0] == 2 || npc.ai[0] == 3 || npc.ai[0] == 8)
            {
                if((npc.ai[1] == 0 && (Main.rand.Next(6) == 0 || npc.life < npc.lifeMax * 0.66f && Main.rand.Next(3) == 0)) || npc.life < npc.lifeMax * 0.33f) RuneCrash = true;
            }
            else
            {
                RuneCrash = false;
            }

            if (RuneCrash)
            {
                if(npc.ai[2]++ > 5)
                {
                    Vector2 Runeposition = new Vector2(0,0);
                    Runeposition = player.Center + new Vector2((250f + 4f * Main.rand.Next(-7, 7)) * (float)Math.Sin(5.18f * Main.rand.Next(30) * 3.1415926f), (250f + 4f * Main.rand.Next(-7, 7)) * (float)Math.Cos(5.18f * Main.rand.Next(30) * 3.1415926f));
                    
                    float RunepositionX = Runeposition.X;
                    float RunepositionY = Runeposition.Y;
                    NPC.NewNPC((int)RunepositionX, (int)RunepositionY, ModContent.NPCType<AsheRune>(), 0, RunepositionX, RunepositionY, npc.damage, npc.whoAmI, player.whoAmI);
                    npc.ai[2] = 0;
                }
            }
        }

        public void IdlePhase()
        {
            Player player = Main.player[npc.target];
            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
            MoveToPoint(wantedVelocity);

            if (npc.ai[1]++ > (Main.expertMode ? 180 : 280))
            {
                AIChange();
            }
        }

        public int Frame = 0;

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 1 || npc.ai[0] == 4 || npc.ai[0] == 9)
            {
                if (npc.frameCounter++ >= 10)
                {
                    npc.frameCounter = 0;
                    Frame++;
                }
            }
            else if (npc.ai[0] == 5)
            {
                if (npc.frameCounter++ >= 10)
                {
                    npc.frameCounter = 0;
                    Frame++;
                }
            }
            else
            {
                if (npc.frameCounter++ >= 7)
                {
                    npc.frameCounter = 0;
                    Frame++;
                }
            }
            
            if (npc.ai[0] == 5)
            {
                if (Frame < 8 || Frame > 10)
                {
                    Frame = 8;
                }
            }
            else if (npc.ai[0] == 6 || npc.ai[0] == 7)
            {
                if (Frame >= 14 || Frame < 11)
                {
                    Frame = 11;
                }
            }
            else if (npc.ai[0] == 1 || npc.ai[0] == 4)
            {
                if (Frame < 15 || Frame > 18)
                {
                    Frame = 15;
                }
            }
            else if (npc.ai[0] == 9)
            {
                if (npc.life < npc.lifeMax / 3 && npc.ai[1] >= (Main.expertMode ? 140 : 240))
                {
                    if (Frame > 23 || Frame < 20)
                    {
                        Frame = 20;
                    }
                }
                else
                {
                    if (Frame < 15 || Frame > 18)
                    {
                        Frame = 15;
                    }
                }
            }
            else if (RuneCrash)
            {
                if (Frame < 19 || Frame > 20)
                {
                    Frame = 19;
                }
            }
            else if (!FlyingBack)
            {
                if (Frame > 3)
                {
                    Frame = 0;
                }
            }
            else
            {
                if (Frame >= 8 || Frame < 4)
                {
                    Frame = 4;
                }
            }

            if (Frame > 23)
            {
                Frame = 0;
            }

            npc.frame.Y = Frame * frameHeight;
        }

        private void Movement(Vector2 targetPos, float speedModifier)
        {
            if (npc.Center.X < targetPos.X)
            {
                npc.velocity.X += speedModifier;
                if (npc.velocity.X < 0)
                    npc.velocity.X += speedModifier * 2;
            }
            else
            {
                npc.velocity.X -= speedModifier;
                if (npc.velocity.X > 0)
                    npc.velocity.X -= speedModifier * 2;
            }
            if (npc.Center.Y < targetPos.Y)
            {
                npc.velocity.Y += speedModifier;
                if (npc.velocity.Y < 0)
                    npc.velocity.Y += speedModifier * 2;
            }
            else
            {
                npc.velocity.Y -= speedModifier;
                if (npc.velocity.Y > 0)
                    npc.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(npc.velocity.X) > 30)
                npc.velocity.X = 30 * Math.Sign(npc.velocity.X);
            if (Math.Abs(npc.velocity.Y) > 30)
                npc.velocity.Y = 30 * Math.Sign(npc.velocity.Y);
        }

        private bool AliveCheck(Player player)
        {
            if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(true);
                if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
                    if (Main.netMode != 1)
                    {
                        int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<AsheVanish>(), 0);
                        Main.npc[DeathAnim].velocity = npc.velocity;
                        Main.npc[DeathAnim].netUpdate = true;
                    }
                    npc.active = false;
                }
                return false;
            }
            if (npc.timeLeft < 600)
                npc.timeLeft = 600;
            return true;
        }

        private void AIChange()
        {
            npc.ai[0]++;
            npc.ai[1] = 0;
            npc.ai[2] = 0;
            npc.ai[3] = 0;
        }

        public static int VortexDamage()
        {
            return  1 + (NPC.CountNPCS(ModContent.NPCType<AsheOrbiter>()) / 15);
        }

        public void FireMagic(NPC npc)
        {
            if (Main.netMode != 1)
            {
                if (Health)
                {
                    for(int i = 0; i < 200; i++)
                    {
                        if(Main.npc[i].type == mod.NPCType("AsheOrbiter"))
                        {
                            Main.npc[i].life = 0;
                            Main.npc[i].active = false;
                        } 
                    }
                }
                const float distance = 125f;
                float rotation = 2f * (float)Math.PI / OrbiterCount;
                if (Health && npc.life >= npc.lifeMax * .66f)
                {
                    Health = false;
                    rotation = 2f * (float)Math.PI / 4;
                    for (int m = 0; m < 4; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AsheOrbiter"), 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                }
                if (Health && npc.life < npc.lifeMax * .66f && npc.life >= npc.lifeMax * .33f)
                {
                    Health = false;
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AsheOrbiter"), 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                }
                if (Health && npc.life < npc.lifeMax * .33f)
                {
                    OrbiterCount += 2;
                    Health = false;
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AsheOrbiter"), 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                    OrbiterCount -= 2;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  
            npc.damage = (int)(npc.damage * 0.6f);
        }

        #region movement stuff

        public bool FlyingBack = false;
        public bool FlyingPositive = false;
        public bool FlyingNegative = false;
        public float pos = 350f;

        public void ChangePos()
        {
            pos = - pos;
            npc.netUpdate = false;
        }

        public override void PostAI()
        {
            Player player = Main.player[npc.target];

            if (npc.velocity.X > 0) //Flying in the positive X direction
            {
                FlyingPositive = true;
                FlyingNegative = false;
            }
            else //Flying in the nagative X direction
            {
                FlyingPositive = false;
                FlyingNegative = true;
            }
            if (npc.ai[0] == 6 || npc.ai[0] == 7)
            {
                npc.direction = npc.velocity.X > 0 ? 1 : -1;
            }
            else
            {
                if (player.Center.X > npc.Center.X) //If NPC's X position is less than the player's
                {
                    if (pos == -350)
                    {
                        pos = 350;
                    }

                    npc.direction = 1;

                    if (FlyingPositive)
                    {
                        FlyingBack = true;
                    }
                    else
                    {
                        FlyingBack = false;
                    }
                }
                else //If NPC's X position is higher than the player's
                {
                    if (pos == 350)
                    {
                        pos = -350;
                    }

                    npc.direction = -1;

                    if (FlyingNegative)
                    {
                        FlyingBack = true;
                    }
                    else
                    {
                        FlyingBack = false;
                    }
                }
                npc.direction = player.position.X > npc.position.X ? 1 : -1;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 25f;
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

        #endregion

        #region draw stuff

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D Tex = Main.npcTexture[npc.type];

            Texture2D RingTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing1");
            Texture2D RingTex1 = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing2");
            Texture2D RitualTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRitual");
            Texture2D ShieldTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheShield");

            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);

            if (scale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, blue, npc.position, npc.width, npc.height, scale, RingRotation, 0, 1, new Rectangle(0, 0, RitualTex.Width, RitualTex.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, new Rectangle(0, 0, RingTex.Width, RingTex.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, blue, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, new Rectangle(0, 0, RingTex1.Width, RingTex1.Height), dColor, true);
            }
            if (scale2 > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, ShieldTex, red, npc.position, npc.width, npc.height, scale2, RingRotation2, 0, 1, new Rectangle(0, 0, ShieldTex.Width, ShieldTex.Height), dColor, true);
            }

            BaseDrawing.DrawTexture(spritebatch, Tex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, Main.npcFrameCount[npc.type], npc.frame, dColor, true);

            return false;
        }

        public float scale = 0;
        public float RingRotation = 0;

        public float scale2 = 0;
        public float RingRotation2 = 0;

        private void RingEffects()
        {
            RingRotation += 0.02f;
            if (npc.ai[0] == 12 || NPC.AnyNPCs(ModContent.NPCType<AsheOrbiter>()))
            {
                if (scale >= 1f)
                {
                    scale = 1f;

                    if(NPC.CountNPCS(ModContent.NPCType<AsheOrbiter>()) < OrbiterCount)
                    {
                        Health = true;
                    }
                }
                else
                {
                    scale += .02f;
                }
            }
            else
            {
                RingRotation -= 0.02f;
                if (scale > .1f)
                {
                    scale -= .02f;
                }
                else
                {
                    scale = 0;
                }
            }

            if (npc.ai[0] == 1 || npc.ai[0] == 6 || npc.ai[0] == 11 || RuneCrash)
            {
                if (scale2 >= 1f)
                {
                    scale2 = 1f;
                }
                else
                {
                    scale2 += .02f;
                }
            }
            else
            {
                if (scale2 > .1f)
                {
                    scale2 -= .02f;
                }
                else
                {
                    scale2 = 0;
                }
            }

            if(scale >= 1f || scale2 >= 1f)
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        #endregion
    }
}


