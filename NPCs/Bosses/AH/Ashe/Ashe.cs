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

        public static int Idle = 0, CastMagic0 = 1, CastMagic1 = 2, CastMagic2 = 3, MeleePrep = 4, Melee = 5, ChargePrep = 6, Charge = 7, SummonDragon = 8, Vortex = 9;

        public override void AI()
        {
            Player player = Main.player[npc.target];

            npc.direction = npc.spriteDirection = npc.position.X < player.position.X ? 1 : -1;
            RingEffects();

            if (npc.ai[0] == Idle || npc.ai[0] == CastMagic1 || npc.ai[0] == CastMagic2 || npc.ai[0] == Vortex)
            {
                Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
                MoveToPoint(wantedVelocity);
            }

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
                    IdlePhase();
                    break;
            }
        }

        public void IdlePhase()
        {
            if (npc.ai[1]++ > (Main.expertMode ? 180 : 280))
            {
                AIChange();
            }
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

        public int OrbiterCount = Main.expertMode ? 10 : 8;

        public void FireMagic(NPC npc)
        {
            Player player = Main.player[npc.target];

            if (npc.ai[0] == Vortex)
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
            else if (npc.ai[0] == CastMagic0)
            {
                float spread = 60f * 0.0174f;
                double startAngle = Math.Atan2(npc.velocity.X, -npc.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 6 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 6 : 4); i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)(Math.Sin(offsetAngle) * 9f), (float)(Math.Cos(offsetAngle) * 9f), ModContent.ProjectileType<AsheSpell>(), npc.damage / 4, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else if (npc.ai[0] == CastMagic1)
            {
                int speedX = 11;
                int speedY = 11;
                float spread = 75f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
                double startAngle = Math.Atan2(speedX, speedY) - .1d;
                double deltaAngle = spread / 6f;
                for (int i = 0; i < 5; i++)
                {
                    double offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * npc.direction, baseSpeed * (float)Math.Cos(offsetAngle), ModContent.ProjectileType<AsheShot>(), npc.damage / 4, 4);
                }
            }
            else if (npc.ai[0] == CastMagic2)
            {
                BaseAI.FireProjectile(player.Center, npc, ModContent.ProjectileType<AsheFire>(), npc.damage / 4, 3, 16f, 0, 0, -1);
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
            if (Main.netMode != 1) BaseUtility.Chat("OW..! THAT HURT, YOU KNOW!", new Color(102, 20, 48));
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
            Texture2D Glow = mod.GetTexture("Glowmasks/Sisters/Ashe2_Glow");

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
            if (npc.ai[0] == SummonDragon || NPC.AnyNPCs(ModContent.NPCType<AsheOrbiter>()))
            {
                RingRotation += 0.02f;
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

            if (npc.ai[0] == SummonDragon || npc.ai[0] == CastMagic0 || npc.ai[0] == CastMagic1 || npc.ai[0] == CastMagic2)
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

        public int Frame = 0;

        public override void FindFrame(int frameHeight)
        {
            if (npc.frameCounter++ >= 10)
            {
                npc.frameCounter = 0;
                Frame++;
            }
            if (npc.ai[0] == Idle || npc.ai[0] == MeleePrep || (npc.ai[0] == SummonDragon && npc.ai[1] < 70))
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
            else if (npc.ai[0] == ChargePrep)
            {
                if (Frame >= 11)
                {
                    Frame = 11;
                }
                if (Frame < 8)
                {
                    Frame = 8;
                }
            }
            else if (npc.ai[0] == Charge)
            {
                if (Frame < 12)
                {
                    Frame = 12;
                }
                if (Frame >= 16)
                {
                    Frame = 12;
                }
            }
            else if (npc.ai[0] == CastMagic0 || npc.ai[0] == CastMagic2)
            {
                if (Frame < 16)
                {
                    Frame = 16;
                }
                if (npc.frameCounter++ >= 10)
                {
                    npc.frameCounter = 0;
                    Frame++;
                }
                if (Frame > 23)
                {
                    Frame = 23;
                }
            }
            else if (npc.ai[0] == Vortex || npc.ai[0] == Melee || npc.ai[0] == CastMagic1 || (npc.ai[0] == SummonDragon && npc.ai[1] > 70))
            {
                if (Frame < 24)
                {
                    Frame = 24;
                }
                if (Frame >= 31)
                {
                    Frame = 31;
                }
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


