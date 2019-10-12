using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;
using System;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    [AutoloadBossHead]
    public class Ashe : ModNPC
    {
        public int OrbiterCount = Main.expertMode ? 10 : 8;

        bool Health1 = false;
        bool Health2 = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe Akuma");
            Main.npcFrameCount[npc.type] = 19;
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

        public override void AI()
        {
            Player player = Main.player[npc.target];

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);

            npc.direction = npc.spriteDirection = npc.position.X < player.position.X ? 1 : -1;
            RingEffects(); FireMagic(npc);

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

                    BaseAI.ShootPeriodic(npc, player.Center + new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), player.width, player.height, ModContent.ProjectileType<AsheShot>(), ref npc.ai[2], 60, npc.damage / 2, 12, false);

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

                    wantedVelocity = player.Center - new Vector2(pos, 0);

                    MoveToPoint(wantedVelocity);

                    BaseAI.ShootPeriodic(npc, player.Center, player.width, player.height, ModContent.ProjectileType<Akuma.AkumaBreath>(), ref npc.ai[2], 10, npc.damage / 2, 12, false);
                    if (npc.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        AIChange();
                    }
                    break;
                case 5:
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
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
                        npc.velocity = npc.DirectionTo(player.Center) * 40;
                    }
                    npc.rotation = 0;
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
                        if (++npc.ai[3] >= 3) //dash three times
                        {
                            npc.ai[0]++;
                            npc.ai[3] = 0;
                        }
                        else
                            npc.ai[0]--;
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
                    MoveToPoint(wantedVelocity);
                    BaseAI.ShootPeriodic(npc, player.Center, player.width, player.height, ModContent.ProjectileType<AsheFire>(), ref npc.ai[2], 60, npc.damage / 2, 12, false);
                    if (npc.ai[1]++ > (Main.expertMode ? 180 : 280))
                    {
                        AIChange();
                    }
                    break;
                case 10:
                    if (NPC.AnyNPCs(ModContent.NPCType<AsheDragon>()))
                    {
                        npc.ai[0] = 0;
                        goto case 0;
                    }
                    if (!AliveCheck(player))
                        break;
                    IdlePhase();
                    break;
                case 11:
                    if (!AliveCheck(player))
                        break;
                    if (npc.ai[1]++ > 200)
                    {
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<AsheDragon>());
                        AIChange();
                    }
                    break;
                default:
                    npc.ai[0] = 0;
                    goto case 0;
            }

            if (npc.ai[0] != 4 || npc.ai[0] != 9)
            {
                npc.rotation = 0;
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
            if (npc.frameCounter++ >= 7)
            {
                npc.frameCounter = 0;
                Frame++;
            }
            else if (npc.ai[0] == 4 || npc.ai[0] == 7)
            {
                if (Frame < 8)
                {
                    Frame = 8;
                }
                if (Frame >= 14)
                {
                    Frame = 12;
                }
            }
            else if (npc.ai[0] == 1 || npc.ai[0] == 4 || npc.ai[0] == 8)
            {
                if (Frame < 15 || Frame > 18)
                {
                    Frame = 16;
                }
                if (npc.frameCounter++ >= 10)
                {
                    npc.frameCounter = 0;
                    Frame++;
                }
            }
            else
            {
                if (!FlyingBack)
                {
                    if (Frame > 3)
                    {
                        Frame = 0;
                    }
                }
                else
                {
                    if (Frame >= 8)
                    {
                        Frame = 0;
                    }
                    if (Frame < 4)
                    {
                        Frame = 4;
                    }
                }
            }

            if (Frame > 18)
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
                const float distance = 125f;
                float rotation = 2f * (float)Math.PI / OrbiterCount;
                if (Health1 && npc.life < npc.lifeMax * (2 / 3))
                {
                    Health1 = true;
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AsheOrbiter"), 0, npc.whoAmI, distance, 300, rotation * m);
                        if (Main.netMode == 2 && n < 200)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                }
                if (Health2 && npc.life < npc.lifeMax / 3)
                {
                    OrbiterCount += 2;
                    Health2 = true;
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
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<AHDeath>());
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
            int DeathAnim = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<AsheVanish>(), 0);
            Main.npc[DeathAnim].velocity = npc.velocity;
            if (Main.netMode != 1) BaseUtility.Chat("OW..! THAT HURT, YOU MEANIE!", new Color(102, 20, 48));
            npc.value = 0f;
            npc.boss = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Haruka.Haruka>()))
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
            if (npc.ai[0] == 4 || npc.ai[0] == 7)
            {
                npc.direction = npc.velocity.X > 0 ? 1 : -1;
            }
            else
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
                npc.direction = player.position.X > npc.position.X ? 1 : -1;
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 20f;
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
            Texture2D Glow = mod.GetTexture("Glowmasks/Ashe_Glow");

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
            BaseDrawing.DrawTexture(spritebatch, Glow, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, Main.npcFrameCount[npc.type], npc.frame, Color.White, true);

            return false;
        }

        public float scale = 0;
        public float RingRotation = 0;

        public float scale2 = 0;
        public float RingRotation2 = 0;

        private void RingEffects()
        {
            RingRotation += 0.02f;
            if (npc.ai[0] == 13 || NPC.AnyNPCs(ModContent.NPCType<AsheOrbiter>()))
            {
                if (scale >= 1f)
                {
                    scale = 1f;
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

            if (npc.ai[0] == 1 || npc.ai[0] == 6 || npc.ai[0] == 11 || npc.ai[0] == 13)
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
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        #endregion
    }
}


