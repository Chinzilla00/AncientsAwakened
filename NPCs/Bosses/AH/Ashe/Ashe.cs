using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    [AutoloadBossHead]
    public class Ashe : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe Akuma");
            Main.npcFrameCount[npc.type] = 24;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 100;
            npc.damage = 150;
            npc.defense = 40;
            npc.lifeMax = 140000;
            npc.value = Item.sellPrice(0, 4, 0, 0);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.knockBackResist = 0f;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AH");
            bossBag = mod.ItemType("AHBag");
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
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                internalAI[4] = reader.ReadFloat();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public float moveSpeed;
        public bool FlyingBack = false;
        public bool FlyingPositive = false;
        public bool FlyingNegative = false;
        public float MeleeSpeed = 6f;
        public float pos = 250f;
        private bool HasFiredProj = false;

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public int[] Vortexes = null;

        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;
        public int Frame = 0;


        public static int AISTATE_HOVER = 0, AISTATE_CAST1 = 1, AISTATE_CAST2 = 2, AISTATE_CAST3 = 3, AISTATE_CAST4 = 4, AISTATE_MELEE = 5, AISTATE_DRAGON = 6, AISTATE_VORTEX = 7;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            bool AsheType = npc.type == mod.NPCType<Ashe>();

            RingEffects();
            RingEffects2();
            if (Main.netMode != 1)
            {
                if (internalAI[1]++ >= 8)
                {
                    internalAI[1] = 0;
                    internalAI[2]++;
                    npc.netUpdate = true;
                }
            }

            if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(false);
                if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
                    if (Main.netMode != 1)
                    {
                        int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheVanish>(), 0);
                        Main.npc[DeathAnim].velocity = npc.velocity;
                        Main.npc[DeathAnim].netUpdate = true;
                    }
                    npc.active = false;
                }
                return;
            }

            if (Main.netMode != 1)
            {
                if (npc.life <= (npc.lifeMax * .75f) && !Health3 && !NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()) && AsheType)
                {
                    Health3 = true;
                    internalAI[0] = AISTATE_VORTEX;
                }
                if (npc.life <= (npc.lifeMax * .5f) && !Health2 && !NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()) && AsheType)
                {
                    Health2 = true;
                    internalAI[0] = AISTATE_VORTEX;
                }
                if (npc.life <= (npc.lifeMax * .25f) && !Health1 && !NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()) && AsheType)
                {
                    Health1 = true;
                    internalAI[0] = AISTATE_VORTEX;
                }
                npc.netUpdate = true;
            }

            Vortexes = BaseAI.GetNPCs(npc.Center, mod.NPCType("AsheOrbiter"), 1500f);

            if (Vortexes != null && Vortexes.Length > 0)
            {
                if (Main.netMode != 2 && Main.player[Main.myPlayer].miscCounter % 2 == 0)
                {
                    for (int m = 0; m < Vortexes.Length; m++)
                    {
                        NPC npc2 = Main.npc[Vortexes[m]];
                        if (npc2 != null && npc2.active)
                        {
                            int dustID = Dust.NewDust(npc2.position, npc2.width, npc2.height, mod.DustType<Dusts.AkumaDustLight>());
                            Main.dust[dustID].position += (npc.position - npc.oldPosition);
                            Main.dust[dustID].velocity = (npc.Center - npc2.Center) * 0.10f;
                            Main.dust[dustID].alpha = 100;
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
            }

            if (NPC.AnyNPCs(mod.NPCType<AsheDragon>()))
            {
                internalAI[4] = 1200;
            }

            if (internalAI[4] > 0)
            {
                internalAI[4]--;
            }

            if (internalAI[0] == AISTATE_HOVER || internalAI[0] == AISTATE_DRAGON) //Hovering/Summoning Dragon
            {
                if (Main.netMode != 1 && internalAI[0] == AISTATE_HOVER) //Only randomly select AI if not doing a dragon summon
                {
                    internalAI[3]++;
                    if (internalAI[3] >= 90)
                    {
                        internalAI[3] = 0;
                        if (NPC.CountNPCS(mod.NPCType<AsheDragon>()) < 1 && internalAI[4] <= 0)
                        {
                            internalAI[0] = Main.rand.Next(7);
                        }
                        else
                        {
                            internalAI[0] = Main.rand.Next(6);
                        }
                        if (internalAI[0] == AISTATE_MELEE)
                        {
                            moveSpeed = 6f;
                            SelectPoint = true;
                        }
                        if (internalAI[0] == AISTATE_HOVER)
                        {
                            ChangePos();
                        }
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
                

            }
            else if (internalAI[0] == AISTATE_CAST4 || internalAI[0] == AISTATE_MELEE || internalAI[0] == AISTATE_VORTEX) //Strong
            {
                if (internalAI[2] == 20 && internalAI[1] == 4 && internalAI[0] != AISTATE_MELEE && !HasFiredProj) //Only Shoot if not in melee mode
                {
                    if (Main.netMode != 1)
                    {
                        FireMagic(npc);
                        HasFiredProj = true;
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[2] < 16) 
                {
                    internalAI[1] = 0;
                    internalAI[2] = 16;
                }
                if (internalAI[2] > 23)
                {
                    if (internalAI[0] == AISTATE_MELEE)
                    {
                        pos = -pos;
                    }
                    npc.frameCounter = 0;
                    Frame = 0;
                    if (Main.netMode != 1)
                    {
                        HasFiredProj = false;
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        moveSpeed = 16f;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
            }
            else if (internalAI[0] == AISTATE_CAST2)
            {
                if (internalAI[2] > 11)
                {
                    if (Main.netMode != 1)
                    {
                        FireMagic(npc);
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[2] < 8)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 8;
                }
                if (internalAI[2] > 15)
                {
                    npc.frameCounter = 0;
                    Frame = 0;
                    if (Main.netMode != 1)
                    {
                        HasFiredProj = false;
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
            }
            else
            {
                if (internalAI[2] == 12 && internalAI[1] == 4 && !HasFiredProj)
                {
                    if (Main.netMode != 1)
                    {
                        FireMagic(npc);
                        HasFiredProj = true;
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[2] < 8)
                {
                    internalAI[1] = 0;
                    internalAI[2] = 8;
                }
                if (internalAI[2] > 15)
                {
                    npc.frameCounter = 0;
                    Frame = 0;
                    if (Main.netMode != 1)
                    {
                        HasFiredProj = false;
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
            }


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

            if (internalAI[0] == AISTATE_MELEE) //Melee Damage/Speed boost
            {
                npc.damage++;
                if (npc.damage > 160)
                {
                    npc.damage = 160;
                }
                if (internalAI[2] > 21)
                {
                    MeleeSpeed -= .01f;
                    npc.damage = 100;
                }
            }
            else //Reset Stats
            {
                npc.damage = 100;
                MeleeSpeed = 0;
            }


            if (internalAI[0] == AISTATE_MELEE) //When charging the player
            {
                if (Main.netMode != 1)
                {
                    if (SelectPoint)
                    {
                        float Point = 500 * npc.direction;
                        MovePoint = player.Center + new Vector2(Point, 500f);
                        SelectPoint = false;
                        npc.netUpdate = true;
                    }
                }
                MeleeMovement(MovePoint);
            }
            else //Anything else
            {
                Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
                MoveToPoint(wantedVelocity);
            }

            
            if (internalAI[0] == AISTATE_DRAGON) //Summoning a dragon
            {
                internalAI[3]++;
                if (internalAI[3] > 240)
                {
                    npc.frameCounter = 0;
                    Frame = 0;
                    if (Main.netMode != 1)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheDragon>(), 0);
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        internalAI[3] = 0;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                }
            }

            if (NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()) || internalAI[0] == AISTATE_DRAGON)
            {
                npc.dontTakeDamage = true;
                npc.reflectingProjectiles = true;
            }
            else
            {
                npc.dontTakeDamage = false;
                npc.reflectingProjectiles = false;
            }

            npc.rotation = 0; //No ugly rotation.
        }

        public void ChangePos()
        {
            npc.ai[1] = Main.rand.Next(2);
            if (npc.ai[1] == 0)
            {
                pos = -250;
            }
            else
            {
                pos = 250;
            }
            npc.netUpdate = false;
        }

        public override void PostAI()
        {
            Player player = Main.player[npc.target];
            if (internalAI[0] != AISTATE_MELEE)
            {
                if (player.Center.X > npc.Center.X) //If NPC's X position is less than the player's
                {
                    if (pos == -250)
                    {
                        pos = 250;
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
                    if (pos == 250)
                    {
                        pos = -250;
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
            }
            else
            {
                npc.direction = npc.velocity.X > 0 ? 1 : -1;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ >= 8) //IAI[1] is the frame counter
            {
                npc.frameCounter = 0;
                Frame++;
            }
            if (internalAI[0] == AISTATE_HOVER || internalAI[0] == AISTATE_DRAGON) //Hovering/Summoning Dragon
            {
                if (FlyingBack)
                {
                    if (Frame > 3)
                    {
                        npc.frameCounter = 0;
                        Frame = 0;
                    }
                }
                else
                {
                    if (Frame > 7 || Frame < 4)
                    {
                        npc.frameCounter = 0;
                        Frame = 4;
                    }
                }
            }
            else if (internalAI[0] == AISTATE_CAST4 || internalAI[0] == AISTATE_MELEE || internalAI[0] == AISTATE_VORTEX)
            {
                if (Frame < 16)
                {
                    npc.frameCounter = 0;
                    Frame = 16;
                }
            }
            else if (internalAI[0] == AISTATE_CAST2)
            {
                if (Frame < 8)
                {
                    npc.frameCounter = 0;
                    Frame = 8;
                }
            }
            else
            {
                if (Frame < 8)
                {
                    npc.frameCounter = 0;
                    Frame = 8;
                }
            }
            npc.frame.Y = Frame * frameHeight;
        }

        public static int VortexDamage(Mod mod)
        {
            return  1 + (NPC.CountNPCS(mod.NPCType<AsheOrbiter>()) / 15);
        }

        public float[] shootAI = new float[4];

        public int OrbiterCount = Main.expertMode ? 10 : 8;

        public void FireMagic(NPC npc)
        {
            Player player = Main.player[npc.target];
            int VortexType = mod.NPCType("AsheOrbiter");
            if (internalAI[0] == 1)
            {
                int speedX = 14;
                int speedY = 14;
                float spread = 75f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                double startAngle = Math.Atan2(speedX, speedY) - .1d;
                double deltaAngle = spread / 6f;
                for (int i = 0; i < 5; i++)
                {
                    double offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * npc.direction, baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<AsheShot>(), npc.damage, 4);
                }
            }
            else if (internalAI[0] == 2)
            {
                BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjectileType<AsheFlamethrower>(), ref shootAI[0], 5, npc.damage / 2, 12);
            }
            else if (internalAI[0] == 3)
            {
                float spread = 60f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, -npc.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 6 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 6 : 4); i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 7f), (float)(Math.Cos(offsetAngle) * 7f), mod.ProjectileType<AsheSpell>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (internalAI[0] == AISTATE_CAST4)
            {
                BaseAI.FireProjectile(player.Center, npc, mod.ProjectileType<AsheFire>(), npc.damage, 3, 14f, 0, 0, -1);
            }

            else if (internalAI[0] == AISTATE_VORTEX)
            {
                if (Main.netMode != 1)
                {
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, VortexType, 0);
                        Main.npc[npcID].Center = npc.Center;
                        Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                        Main.npc[npcID].velocity *= 8f;
                        Main.npc[npcID].ai[0] = m;
                        Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                    }
                }
            }
        }


        public override void NPCLoot()
        {
            int Haruka = NPC.CountNPCS(mod.NPCType("Haruka"));
            if (Haruka == 0)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AHDeath>());
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
            }
            if (!Main.expertMode)
            {
                string[] lootTableA = { "AshRain", "FuryFlame", "FireSpiritStaff", "AsheSatchel" };
                int lootA = Main.rand.Next(lootTableA.Length);
                npc.DropLoot(mod.ItemType(lootTableA[lootA]));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("AsheTrophy"));
            }
            int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<AsheVanish>(), 0);
            Main.npc[DeathAnim].velocity = npc.velocity;
            BaseUtility.Chat("OW..! THAT HURT, YOU KNOW!", new Color(102, 20, 48));
            npc.value = 0f;
            npc.boss = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 0;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.3f);  //boss damage increase in expermode
        }


        public bool Summon = false;
        
        public float scale = 0;
        public float RingRotation = 0;
        
        public float scale2 = 0;
        public float RingRotation2 = 0;

        private void RingEffects()
        {
            if (internalAI[0] == AISTATE_DRAGON) //If summoning noodle
            {
                RingRotation += 0.02f;
                if (scale < 1f)
                {
                    scale += .02f; //Raise Scale
                }
                if (scale >= 1f)
                {
                    scale = 1f;
                }
            }
            else
            {
                RingRotation -= 0.02f;
                if (scale < .1f)
                {
                    scale = 0;
                }
                if (scale > 0)
                {
                    scale -= .02f;
                }
            }
        }

        private void RingEffects2()
        {
            if (internalAI[0] == AISTATE_DRAGON || NPC.AnyNPCs(mod.NPCType<AsheOrbiter>())) //If summoning noodle
            {
                RingRotation2 += 0.02f;
                if (scale2 < 1f)
                {
                    scale2 += .02f; //Raise Scale
                }
                if (scale2 >= 1f)
                {
                    scale2 = 1f;
                }
            }
            else
            {
                RingRotation2 -= 0.02f;
                if (scale2 < .1f)
                {
                    scale2 = 0;
                }
                if (scale2 > 0)
                {
                    scale2 -= .02f;
                }
            }
        }


        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Ashe_Glow");
            Texture2D eyeTex = mod.GetTexture("Glowmasks/AsheEyes");

            Texture2D RingTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing1");
            Texture2D RingTex1 = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing2");
            Texture2D RitualTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRitual");
            Texture2D ShieldTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheShield");
            Texture2D Barrier = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheBarrier");
            Rectangle RingFrame = new Rectangle(0, 0, RingTex.Width, RingTex.Height);
            Rectangle RitualFrame = new Rectangle(0, 0, RitualTex.Width, RitualTex.Height);
            Rectangle BarrierFrame = new Rectangle(0, 0, ShieldTex.Width, ShieldTex.Height);
            Rectangle ShieldFrame = new Rectangle(0, 0, Barrier.Width, Barrier.Height);

            int blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);

            Color alphaColor = new Color(Color.White.R, Color.White.G, Color.White.B);

            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            if (internalAI[0] == AISTATE_MELEE)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 5, false, 0f, 0f, Color.Orange);
            }

            if (scale > 0) //Only draw if summoning a noodle
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, blue, npc.position, npc.width, npc.height, scale, RingRotation, 0, 1, RitualFrame, alphaColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, alphaColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, blue, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, RingFrame, alphaColor, true);
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, dColor, true);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawTexture(spritebatch, eyeTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 24, npc.frame, Color.White, true);
            BaseDrawing.DrawAfterimage(spritebatch, eyeTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 24);

            if (NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()))
            {
                DrawAfterimage(spritebatch, eyeTex, 0, npc.position, npc.width, npc.height, npc.oldPos, 1f, 0f, npc.direction, 24, npc.frame, 1f, 1f, 7, true, 0f, 0f, true, Color.DeepSkyBlue);
                //BaseDrawing.DrawAfterimage(spritebatch, eyeTex, 0, npc, 1f, 1f, 7, false, 0f, 0f, Color.DeepSkyBlue);
            }
            if (scale2 > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, Barrier, red, npc.position, npc.width, npc.height, scale2, -RingRotation2, 0, 1, BarrierFrame, dColor, true);
                BaseDrawing.DrawTexture(spritebatch, ShieldTex, blue, npc.position, npc.width, npc.height, scale2, RingRotation2, 0, 1, ShieldFrame, dColor, true);
            }
            return false;
        }

        public static void DrawAfterimage(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float distanceScalar = 1.0F, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, bool drawCentered = false, Color? overrideColor = null)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / framecount / 2);
            Color lightColor = overrideColor != null ? (Color)overrideColor : BaseDrawing.GetLightColor(position + new Vector2(width * 0.5f, height * 0.5f));
            Vector2 velAddon = default;
            Vector2 originalpos = position;
            Vector2 offset = new Vector2(offsetX, offsetY);
            for (int m = 1; m <= imageCount; m++)
            {
                scale *= sizeScalar;
                Color newLightColor = lightColor;
                newLightColor.R = (byte)(newLightColor.R * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.G = (byte)(newLightColor.G * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.B = (byte)(newLightColor.B * (imageCount + 3 - m) / (imageCount + 9));
                newLightColor.A = (byte)(newLightColor.A * (imageCount + 3 - m) / (imageCount + 9));
                if (useOldPos)
                {
                    position = Vector2.Lerp(originalpos, (m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1]), distanceScalar);
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
                else
                {
                    Vector2 velocity = (m - 1 >= oldPoints.Length ? oldPoints[oldPoints.Length - 1] : oldPoints[m - 1]);
                    velAddon += velocity * distanceScalar;
                    BaseDrawing.DrawTexture(sb, texture, shader, position + offset - velAddon, width, height, scale, rotation, direction, framecount, frame, newLightColor, drawCentered ? true : false);
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
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
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public void MeleeMovement(Vector2 point)
        {
            if (MeleeSpeed < 25f)
            {
                MeleeSpeed += .5f;
            }
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < MeleeSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / MeleeSpeed);
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= MeleeSpeed;
            npc.velocity *= velMultiplier;
        }
    }
}


