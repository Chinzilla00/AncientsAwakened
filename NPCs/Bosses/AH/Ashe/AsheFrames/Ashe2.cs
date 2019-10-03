using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;
using System;

namespace AAMod.NPCs.Bosses.AH.Ashe.AsheFrames
{
    [AutoloadBossHead]
    public class Ashe2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe Akuma");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 100;
            npc.damage = 150;
            npc.defense = 40;
            npc.lifeMax = 140000;
            npc.value = Item.sellPrice(0, 12, 0, 0);
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

        public int[] Vortexes = null;

        public static int Idle = 0, CastMagic1 = 1, CastMagic2 = 2, MeleePrep = 3, Melee = 4, Charge = 5, SummonDragon = 6, Vortex = 7;

        public override void AI()
        {
            Player player = Main.player[npc.target];

            npc.direction = npc.spriteDirection = npc.position.X < player.position.X ? 1 : -1;
            Vector2 targetPos;
            RingEffects();

            if (player.dead || !player.active || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(true);
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

            if (npc.ai[0] == Idle || npc.ai[0] == CastMagic1 || npc.ai[0] == CastMagic2 || npc.ai[0] == Vortex)
            {
                Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
                MoveToPoint(wantedVelocity);
            }

            switch (npc.ai[0])
            {
                case 0:
                    npc.ai[1]++;
                    if (npc.ai[1] > 120 / (Main.expertMode? 2 : 1))
                    {
                        AIChange();
                    }
                    break;
                case 1:
                    AIChange();
                    break;
                case 2:
                    AIChange();
                    break;
                case 3: //Melee prep
                    targetPos = player.Center;
                    targetPos.X += 800 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 800;
                    Movement(targetPos, 1.2f);
                    if (++npc.ai[1] > 180 || Math.Abs(npc.Center.Y - targetPos.Y) < 100) //initiate dash
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                        npc.velocity = npc.DirectionTo(player.Center) * 45;
                    }
                    npc.rotation = 0;
                    break;

                case 4: //Melee
                    if (npc.Center.Y > player.Center.Y + 700 || Math.Abs(npc.Center.X - player.Center.X) > 1500)
                    {
                        npc.velocity.Y *= 0.5f;
                        npc.ai[1] = 0;
                        if (++npc.ai[2] >= 2) //repeat three times
                        {
                            AIChange();
                        }
                        else
                            npc.ai[0]--;
                        npc.netUpdate = true;
                    }
                    npc.rotation = npc.velocity.ToRotation();
                    if (npc.velocity.X < 0)
                        npc.rotation += (float)Math.PI;
                    break;
                case 5: //Dash
                    targetPos = player.Center;
                    targetPos.X += 700 * (npc.Center.X < targetPos.X ? -1 : 1);
                    Movement(targetPos, .8f);
                    if (++npc.ai[2] > 80)
                    {
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                        if (Main.netMode != 1)
                        {
                            Vector2 spawnPos = npc.Center;
                            spawnPos.X += 250 * (npc.Center.X < player.Center.X ? 1 : -1);
                            Vector2 vel = (player.Center - spawnPos) / 30;
                            if (vel.Length() < 25)
                                vel = Vector2.Normalize(vel) * 25;
                            Projectile.NewProjectile(spawnPos, vel, mod.ProjectileType<AsheSpark>(), npc.damage / 4, 0f, Main.myPlayer);
                        }
                    }
                    if (++npc.ai[1] > 210)
                    {
                        AIChange();
                        npc.netUpdate = true;
                    }
                    break;
                case 6:
                    npc.velocity *= .97f;
                    if (npc.velocity.X < .1f && npc.velocity.Y < .1f)
                    {
                        npc.velocity *= 0;
                        npc.ai[1]++;
                    }
                    if (npc.ai[1] > 240 / (Main.expertMode ? 2 : 1))
                    {
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<AsheDragon>());
                        AIChange();
                    }
                    break;
                case 7:
                    break;
                default:
                    npc.ai[0] = 0;
                    break;
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
        }

        private void AIChange()
        {
            npc.ai[0] = npc.ai[0] != 0 ? 0 : (NPC.AnyNPCs(mod.NPCType<AsheDragon>()) ? Main.rand.Next(6) : Main.rand.Next(5));
            npc.ai[1] = 0;
            npc.ai[2] = 0;
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

        public static int VortexDamage(Mod mod)
        {
            return  1 + (NPC.CountNPCS(mod.NPCType<AsheOrbiter>()) / 15);
        }

        public int OrbiterCount = Main.expertMode ? 10 : 8;

        public void FireMagic(NPC npc)
        {
            if (internalAI[0] == Vortex)
            {
                if (Main.netMode != 1)
                {
                    const float distance = 125f;
                    float rotation = 2f * (float)Math.PI / OrbiterCount;
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AsheOrbiter"), 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
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
            if (Main.netMode != 1) BaseUtility.Chat("OW..! THAT HURT, YOU KNOW!", new Color(102, 20, 48));
            npc.value = 0f;
            npc.boss = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (NPC.AnyNPCs(mod.NPCType<Haruka.Haruka>()))
            {
                potionType = 0;
            }
            else
            {
                potionType = ItemID.SuperHealingPotion;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  
            npc.damage = (int)(npc.damage * 0.6f);
        }

        #region extra AI slots

        public float[] internalAI = new float[5];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
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
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                internalAI[4] = reader.ReadFloat();
            }
        }

        #endregion

        #region movement stuff

        public bool FlyingBack = false;
        public bool FlyingPositive = false;
        public bool FlyingNegative = false;
        public float pos = 250f;

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
            if (internalAI[0] != Melee)
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

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 16f;
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
            SetTextures();

            if (scale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, RitualTex, blue, npc.position, npc.width, npc.height, scale, RingRotation, 0, 1, new Rectangle(0, 0, RitualTex.Width, RitualTex.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex, red, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, new Rectangle(0, 0, RingTex.Width, RingTex.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingTex1, blue, npc.position, npc.width, npc.height, scale, -RingRotation, 0, 1, new Rectangle(0, 0, RingTex1.Width, RingTex1.Height), dColor, true);
            }
            if (scale2 > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, Barrier, red, npc.position, npc.width, npc.height, scale2, -RingRotation2, 0, 1, new Rectangle(0, 0, Barrier.Width, Barrier.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, ShieldTex, blue, npc.position, npc.width, npc.height, scale2, RingRotation2, 0, 1, new Rectangle(0, 0, ShieldTex.Width, ShieldTex.Height), dColor, true);
            }

            BaseDrawing.DrawTexture(spritebatch, Tex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, FrameCount, npc.frame, dColor, true);
            BaseDrawing.DrawTexture(spritebatch, Glow1, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, FrameCount, npc.frame, Color.White, true);
            if (Glow2 != null)
            {
                BaseDrawing.DrawTexture(spritebatch, Glow2, red, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, FrameCount, npc.frame, Color.White, true);
            }

            return false;
        }

        public float scale = 0;
        public float RingRotation = 0;

        public float scale2 = 0;
        public float RingRotation2 = 0;

        private void RingEffects()
        {
            if (npc.ai[0] == SummonDragon || NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()))
            {
                RingRotation += 0.02f;
                if (scale < 1f)
                {
                    scale += .02f;
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

            if (npc.ai[0] == SummonDragon)
            {
                if (scale2 < 1f)
                {
                    scale2 += .02f;
                }
                if (scale2 >= 1f)
                {
                    scale2 = 1f;
                }
            }
            else
            {
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

        int FrameCount;
        Texture2D Tex;
        Texture2D Glow1;
        Texture2D Glow2;
        Texture2D RingTex;
        Texture2D RingTex1;
        Texture2D RitualTex;
        Texture2D ShieldTex;
        Texture2D Barrier;
        int red;
        int blue;

        public void SetTextures()
        {
            RingTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing1");
            RingTex1 = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRing2");
            RitualTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheRitual");
            ShieldTex = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheShield");
            Barrier = mod.GetTexture("NPCs/Bosses/AH/Ashe/AsheBarrier");

            blue = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);

            if (npc.ai[0] == CastMagic1)
            {
                Tex = mod.GetTexture("NPCs/Bosses/Ashe/AsheFrames/AsheMagic1");
                Glow1 = mod.GetTexture("Glowmasks/Sisters/AsheMagic1_Glow");
                Glow2 = mod.GetTexture("Glowmasks/Sisters/AsheMagic1_Glow2");
                FrameCount = 8;
            }
            else if (npc.ai[0] == CastMagic2)
            {
                Tex = mod.GetTexture("NPCs/Bosses/Ashe/AsheFrames/AsheMagic2");
                Glow1 = mod.GetTexture("Glowmasks/Sisters/AsheMagic2_Glow");
                Glow2 = mod.GetTexture("Glowmasks/Sisters/AsheMagic2_Glow2");
                FrameCount = 8;
            }
            else if (npc.ai[0] == Charge)
            {
                Tex = mod.GetTexture("NPCs/Bosses/Ashe/AsheFrames/AsheCharge");
                Glow1 = mod.GetTexture("Glowmasks/Sisters/AsheCharge_Glow");
                Glow2 = mod.GetTexture("Glowmasks/Sisters/AsheCharge_Glow2");
                FrameCount = 8;
            }
            else
            {
                if (!FlyingBack)
                {
                    Tex = Main.npcTexture[npc.type];
                    Glow1 = mod.GetTexture("Glowmasks/Sisters/Ashe_Glow");
                }
                else
                {

                    Tex = mod.GetTexture("NPCs/Bosses/Ashe/AsheFrames/AsheBack");
                    Glow1 = mod.GetTexture("Glowmasks/Sisters/AsheBack_Glow");
                }
                Glow2 = null;
                FrameCount = 4;
            }
        }

        public int Frame = 0;

        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ >= 8)
            {
                npc.frameCounter = 0;
                Frame++;
            }
            if (Frame >= FrameCount)
            {
                Frame = 0;
            }
            npc.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        #endregion
    }
}


