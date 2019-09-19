using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Graphics.Shaders;

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

        public static int Hover = 0, CastMagic1 = 1, CastMagic2 = 2, Melee = 3, Charge = 4, SummonDragon = 5, Vortex = 6;

        public override void AI()
        {
            if (Main.netMode != -1 && npc.ai[0] == Hover)
            {
                npc.ai[1]++;
            }

            if (npc.ai[1] > (Main.expertMode ? 300 : 240))
            {
                npc.ai[1] = 0;
                npc.ai[0] = NPC.AnyNPCs(mod.NPCType<AsheDragon>()) ? Main.rand.Next(5) : Main.rand.Next(6);
                npc.ai = new float[4];
                npc.netUpdate = true;
            }

            switch (npc.ai[0])
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    npc.ai[0] = 0;
                    break;
            }
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
                    for (int m = 0; m < OrbiterCount; m++)
                    {
                        int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AsheOrbiter"), 0);
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
            if (Main.netMode != 1) BaseUtility.Chat("OW..! THAT HURT, YOU KNOW!", new Color(102, 20, 48));
            npc.value = 0f;
            npc.boss = false;
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
            if (internalAI[0] == SummonDragon || NPC.AnyNPCs(mod.NPCType<AsheOrbiter>()))
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

            if (npc.ai[0] == 1)
            {
                Tex = mod.GetTexture("NPCs/Bosses/Ashe/AsheFrames/AsheMagic1");
                Glow1 = mod.GetTexture("Glowmasks/Sisters/AsheMagic1_Glow");
                Glow2 = mod.GetTexture("Glowmasks/Sisters/AsheMagic1_Glow2");
                FrameCount = 8;
            }
            else if (npc.ai[0] == 2 || npc.ai[0] == 3 || npc.ai[0] == 6)
            {
                Tex = mod.GetTexture("NPCs/Bosses/Ashe/AsheFrames/AsheMagic2");
                Glow1 = mod.GetTexture("Glowmasks/Sisters/AsheMagic2_Glow");
                Glow2 = mod.GetTexture("Glowmasks/Sisters/AsheMagic2_Glow2");
                FrameCount = 8;
            }
            else if (npc.ai[0] == 4)
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


