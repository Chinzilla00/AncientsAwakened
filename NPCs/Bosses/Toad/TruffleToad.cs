using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Toad
{
    [AutoloadBossHead]
    public class TruffleToad : ModNPC
    {
        public float bossLife;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);

                writer.Write(Minion[0]);
                writer.Write(Minion[1]);
                writer.Write(Minion[2]);
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

                Minion[0] = reader.ReadBool();
                Minion[1] = reader.ReadBool();
                Minion[2] = reader.ReadBool();
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Toad");
            Main.npcFrameCount[npc.type] = 12;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 3000;
            npc.damage = 30;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.value = Item.sellPrice(0, 5, 0, 0);
            npc.aiStyle = -1;
            npc.width = 98;
            npc.height = 72;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/TODE");
            npc.netAlways = true;
            bossBag = mod.ItemType("ToadBag");
            npc.alpha = 255;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = new LegacySoundStyle(29, 13, Terraria.Audio.SoundType.Sound);
            if (Main.expertMode)
            {
                npc.defense = 20;
            }
        }

        public static int AISTATE_JUMP = 0, AISTATE_BARF = 1, AISTATE_JUMPALOT = 2, AISTATE_BUBBLES = 3, AISTATE_SEED = 4, AISTATE_STOMP = 5, AISTATE_TOADS = 6, AISTATE_BUBBLES2 = 7;
        public float[] internalAI = new float[4];
        public bool[] Minion = new bool[3];
        public bool tonguespawned = false;
        public bool TongueAttack = false;
        public float AIChangeRate = 180;
        public float JumpX = 6f, JumpY = -8f, JumpX2 = 6f, JumpY2 = -10f;

        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting
            AAModGlobalNPC.Toad = npc.whoAmI;

            if (player.dead || !player.active || !player.ZoneGlowshroom)
            {
                npc.TargetClosest();
                if (player.dead || !player.active || !player.ZoneGlowshroom)
                {
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        npc.active = false;
                        npc.netUpdate = true;
                    }
                }
            }

            if (player != null)
            {
                float dist = npc.Distance(player.Center);
                if (dist > 800)
                {
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        Vector2 tele = new Vector2(player.Center.X + (Main.rand.Next(2) == 0 ? 300 : -300), player.Center.Y - 16);
                        npc.Center = tele;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    npc.alpha -= 3;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                    }
                }
            }

            int[] Shrooms = BaseAI.GetNPCs(npc.Center, mod.NPCType("ShroomGlow"), 1000);
            if (Shrooms != null && Shrooms.Length > 0)
            {
                float ShroomCount = 1 + (Shrooms.Length / 10);
                npc.damage = (int)(npc.defDamage * ShroomCount);
                npc.defense = (int)(npc.defDefense * ShroomCount);
                AIChangeRate = 120;
                JumpX = 8f; JumpY = -10f; JumpX2 = 10f; JumpY2 = -14f;
                if (Main.netMode != 2 && Main.player[Main.myPlayer].miscCounter % 2 == 0)
                {
                    for (int m = 0; m < Shrooms.Length; m++)
                    {
                        NPC npc2 = Main.npc[Shrooms[m]];
                        if (npc2 != null && npc2.active)
                        {
                            int dustID = Dust.NewDust(npc2.position, npc2.width, npc2.height, mod.DustType<Dusts.ShroomDust>());
                            Main.dust[dustID].position += npc.position - npc.oldPosition;
                            Main.dust[dustID].velocity = (npc.Center - npc2.Center) * 0.10f;
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
            }
            else
            {
                npc.damage = npc.defDamage;
                npc.defense = npc.defDefense;
                AIChangeRate = 180;
                JumpX = 6f; JumpY = -8f; JumpX2 = 6f; JumpY2 = -10f;
            }

            if (npc.velocity.Y != 0)
            {
                if (npc.velocity.X < 0)
                {
                    npc.spriteDirection = 1;
                }
                else if (npc.velocity.X > 0)
                {
                    npc.spriteDirection = -1;
                }
            }
            else
            {
                if (player.position.X < npc.position.X)
                {
                    npc.spriteDirection = 1;
                }
                else if (player.position.X > npc.position.X)
                {
                    npc.spriteDirection = -1;
                }
            }

            if (internalAI[0] == AISTATE_JUMP)
            {
                npc.wet = false;
                BaseAI.AISlime(npc, ref npc.ai, false, 20, JumpX, JumpY, JumpX2, JumpY2);
                internalAI[1]++;
                if (internalAI[1] == 179)
                {
                    Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 13);
                }
                if (internalAI[1] >= AIChangeRate && Main.netMode != 1)
                {
                    internalAI[1] = 0;
                    internalAI[0] = Main.rand.Next(Main.expertMode ? 8 : 7);
                    internalAI[2] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_BARF)
            {
                if (Main.netMode != 1 && npc.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                npc.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (npc.velocity.Y == 0 && Main.netMode != 1)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 5)
                    {
                        internalAI[2] = 0;
                        if (npc.direction == -1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("ToadBomb"), 35, 3);
                        }
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_JUMPALOT)
            {
                internalAI[1]++; if (npc.ai[0] < -10) npc.ai[0] = -10; //force rapid jumping
                npc.wet = false;
                BaseAI.AISlime(npc, ref npc.ai, false, -10, JumpX, JumpY, JumpX2, JumpY2);
                if (Main.netMode != 1)
                {
                    internalAI[1]++;
                }
                if (internalAI[1] >= 300)
                {
                    internalAI[1] = 0;
                    internalAI[0] = 0;
                    internalAI[2] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_BUBBLES)
            {
                if (Main.netMode != 1 && npc.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                npc.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (npc.velocity.Y == 0 && Main.netMode != 1)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 8)
                    {
                        internalAI[2] = 0;
                        if (npc.direction == -1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("FungusBubble"), 35, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("FungusBubble"), 35, 3);
                        }
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_SEED)
            {
                if (Main.netMode != 1 && npc.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                npc.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (npc.velocity.Y == 0 && Main.netMode != 1)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 25)
                    {
                        internalAI[2] = 0;
                        if (npc.direction == -1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("Seed"), 0, 0);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-2, 0)), mod.ProjectileType("Seed"), 0, 0);
                        }
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_STOMP)
            {
                if (internalAI[2] == 0)
                {
                    if (Main.netMode != 1 && npc.velocity.Y == 0)
                    {
                        npc.TargetClosest(true);
                        npc.velocity.X = 6 * npc.direction;
                        npc.velocity.Y = -10f;
                        internalAI[2] = 1f;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    if (npc.velocity.Y == 0f)
                    {
                        Main.PlaySound(SoundID.Item14, npc.position);
                        npc.ai[0] = 0f;
                        for (int num622 = (int)npc.position.X - 20; num622 < (int)npc.position.X + npc.width + 40; num622 += 20)
                        {
                            for (int num623 = 0; num623 < 4; num623++)
                            {
                                int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + npc.height), npc.width + 20, 4, 31, 0f, 0f, 100, default, 1.5f);
                                Main.dust[num624].velocity *= 0.2f;
                            }
                            int num625 = Gore.NewGore(new Vector2(num622 - 20, npc.position.Y + npc.height - 8f), default, Main.rand.Next(61, 64), 1f);
                            Main.gore[num625].velocity *= 0.4f;
                        }
                        for (int a = 0; a < 4; a++)
                        {
                            NPC.NewNPC((int)(npc.position.X + Main.rand.Next(40)), (int)(npc.position.Y + npc.height), mod.NPCType<GlowshroomGrow>());
                        }
                        internalAI[0] = AISTATE_JUMP;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        npc.ai = new float[4];
                        npc.netUpdate = true;
                    }
                    else
                    {
                        npc.TargetClosest(true);
                        if (npc.position.X < Main.player[npc.target].position.X && npc.position.X + npc.width > Main.player[npc.target].position.X + Main.player[npc.target].width)
                        {
                            npc.velocity.X = npc.velocity.X * 0.9f;
                            npc.velocity.Y = npc.velocity.Y + 0.4f;
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
            else if (internalAI[0] == AISTATE_BUBBLES2)
            {
                if (Main.netMode != 1 && npc.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                npc.velocity.X *= .98f;
                if (internalAI[1] >= 35)
                {
                    if (npc.velocity.Y == 0 && Main.netMode != 1)
                    {
                        internalAI[2]++;
                    }
                    if (internalAI[2] > 20)
                    {
                        internalAI[2] = 0;
                        if (npc.direction == -1)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(-6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-1, 0)), mod.ProjectileType("ToadBubble"), 35, 3);
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-1, 0)), mod.ProjectileType("ToadBubble"), 35, 3);
                        }
                        npc.netUpdate = true;
                    }
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == AISTATE_TOADS)
            {
                if (Main.netMode != 1 && npc.velocity.Y == 0)
                {
                    internalAI[1]++;
                }
                npc.velocity.X *= .98f;
                if (internalAI[1] == 35)
                {
                    NPC.NewNPC((int)(npc.Center.X - 30f), (int)(npc.Center.Y - 16), mod.NPCType<TinyToad>());
                    NPC.NewNPC((int)npc.Center.X, (int)(npc.Center.Y - 16), mod.NPCType<TinyToad>());
                    NPC.NewNPC((int)(npc.Center.X + 30f), (int)(npc.Center.Y - 16), mod.NPCType<TinyToad>());
                }
                if (internalAI[1] >= 100)
                {
                    internalAI[0] = AISTATE_JUMP;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    npc.netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.velocity.Y == 0)
            {
                if (internalAI[0] == AISTATE_BARF || internalAI[0] == AISTATE_BUBBLES || internalAI[0] == AISTATE_BUBBLES2)
                {
                    if (npc.frame.Y < frameHeight * 6)
                    {
                        npc.frame.Y = frameHeight * 6;
                    }
                    if (npc.frameCounter >= 10)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += frameHeight;
                        if (npc.frame.Y > frameHeight * 11)
                        {
                            npc.frame.Y = frameHeight * 11;
                        }
                    }
                }
                else
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y > (frameHeight * 4))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 4;
                    }
                }
            }
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ToadTrophy"));
            }
            AAWorld.downedToad = true;
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ToadMask"));
                }
                string[] lootTable = { "MushrockStaff", "ToadTongue", "Todegun" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * .8f);
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            Texture2D GlowTex = mod.GetTexture("Glowmasks/TruffleToad_Glow");

            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor, true);
            BaseDrawing.DrawTexture(sb, GlowTex, 0, npc, GenericUtils.COLOR_GLOWPULSE, true);
            return false;
        }
    }
}


