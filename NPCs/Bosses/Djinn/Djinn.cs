using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Djinn
{
    [AutoloadBossHead]
    public class Djinn : ModNPC
    {
        public int damage = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Djinn");
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.width = 70;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.damage = 30;
            npc.defense = 15;
            npc.lifeMax = 6000;
            npc.buffImmune[20] = true;
            npc.buffImmune[44] = true;
            npc.value = Item.sellPrice(0, 5, 0, 0);
            npc.HitSound = SoundID.NPCHit23;
            npc.DeathSound = SoundID.NPCDeath39;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Djinn");
            bossBag = mod.ItemType<Items.Boss.Djinn.DjinnBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
            npc.defense = (int)(npc.defense * 1.2f);
        }

        public int runonce = 0;
        public int FrameHeight = 130;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
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
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        bool selectPoint = false;
        Vector2 MovePoint;

        public override void AI()
        {
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            Player player = Main.player[npc.target];
            if (runonce == 0)
            {
                StartSandstorm();
                runonce += 1;
            }

            if (internalAI[0] == 2 && npc.ai[3] > 120)
            {
                if (npc.velocity.X > 0)
                {
                    npc.direction = 1;
                }
                else
                {
                    npc.direction = -1;
                }
            }
            else
            {
                if (player.Center.X > npc.Center.X)
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
            }

            if (!player.ZoneDesert || player.dead || !Main.dayTime)
            {
                npc.TargetClosest(true);
                if (!player.ZoneDesert || player.dead || !Main.dayTime)
                {
                    FuriousFlexing();
                    return;
                }
            }
            else
            {
                Sandstorm.TimeLeft = 10;
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                }
                else
                {
                    npc.alpha -= 2;
                }
            }


            if (Main.netMode != 1)
            {
                internalAI[1]++;
                if (internalAI[1] >= 300)
                {
                    selectPoint = true; ;
                    internalAI[0] = Main.rand.Next(3);
                    internalAI[1] = 0;
                    npc.ai[3] = 0;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }

            if (internalAI[0] == 0)
            {
                npc.ai[3]++;
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
                if (npc.ai[3] == 9)
                {
                    if (Main.netMode != 1 && AAGlobalProjectile.CountProjectiles(658) < 5)
                    {
                        FireProjectile();
                        npc.netUpdate = true;
                    }
                }
                if (npc.ai[3] == 36)
                {
                    if (Main.netMode != 1 && AAGlobalProjectile.CountProjectiles(658) < 5)
                    {
                        FireProjectile();
                        npc.netUpdate = true;
                    }
                }
                if (npc.ai[3] == 72)
                {
                    if (Main.netMode != 1 && AAGlobalProjectile.CountProjectiles(658) < 5)
                    {
                        FireProjectile();
                        npc.netUpdate = true;
                    }
                }
                if (npc.ai[3] > 81)
                {
                    if (Main.netMode != 1 && AAGlobalProjectile.CountProjectiles(658) < 5)
                    {
                        FireProjectile();
                        npc.netUpdate = true;
                    }
                }
                if (npc.ai[3] > 90 && Main.netMode != 1)
                {
                    internalAI[0] = 10;
                    internalAI[1] = 0;
                    npc.ai = new float[4];
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == 1)
            {
                npc.damage = 60;
                npc.ai[3]++;
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.1f, 0.1f, 6f, 6f, false, 300);
                npc.damage = 40;
                if (npc.ai[3] > 200 && Main.netMode != 1)
                {
                    internalAI[0] = 10;
                    internalAI[1] = 0;
                    npc.ai = new float[4];
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
            }
            else if (internalAI[0] == 2)
            {
                npc.ai[3]++;

                npc.damage = 50;
                if (npc.ai[3] < 120)
                {
                    if (Main.netMode != 1)
                    {
                        if (selectPoint)
                        {
                            float point = 500 * -npc.direction;
                            MovePoint = player.Center + new Vector2(-point, 0);
                            selectPoint = false;
                            npc.netUpdate = true;
                        }
                        npc.damage = 20;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    if (Main.netMode != 1)
                    {
                        if (npc.ai[3] == 120)
                        {
                            float point = 500 * npc.direction;
                            MovePoint = player.Center + new Vector2(point, 0);
                            npc.netUpdate = true;
                        }
                        npc.damage = 40;
                        npc.netUpdate = true;
                    }
                }

                MoveToPoint(MovePoint, 10f);

                if (npc.ai[3] > 160 && Main.netMode != 1)
                {
                    npc.damage = 30;
                    internalAI[0] = 10;
                    internalAI[1] = 0;
                    npc.ai = new float[4];
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
            }
            else
            {
                npc.damage = 30;
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.1f, 0.04f, 4f, 2f, false, 300);
            }
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (internalAI[0] == 0)
            {
                if (Frame < 6 || Frame > 14)
                {
                    Frame = 6;
                }
                if (npc.ai[3] > 0)
                {
                    Frame = 6;
                }
                if (npc.ai[3] > 9)
                {
                    Frame = 7;
                }
                if (npc.ai[3] > 18)
                {
                    Frame = 8;
                }
                if (npc.ai[3] > 27)
                {
                    Frame = 9;
                }
                if (npc.ai[3] > 36)
                {
                    Frame = 10;
                }
                if (npc.ai[3] > 45)
                {
                    Frame = 11;
                }
                if (npc.ai[3] > 54)
                {
                    Frame = 12;
                }
                if (npc.ai[3] > 63)
                {
                    Frame = 13;
                }
                if (npc.ai[3] > 72)
                {
                    Frame = 14;
                }
                npc.frame.Y = Frame * frameHeight;
                return;
            }
            else if (internalAI[0] == 1)
            {
                if (npc.frameCounter > 9)
                {
                    npc.frame.Y += frameHeight;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y > FrameHeight * 5)
                {
                    npc.frame.Y = 0;
                }
                return;
            }
            else if (internalAI[0] == 2)
            {
                if (npc.ai[3] < 180)
                {
                    if (npc.frameCounter > 9)
                    {
                        npc.frame.Y += frameHeight;
                        npc.frameCounter = 0;
                    }
                    if (npc.frame.Y > FrameHeight * 3)
                    {
                        npc.frame.Y = 0;
                    }
                }
                else
                {
                    if (npc.frame.Y < FrameHeight * 4)
                    {
                        npc.frame.Y = 4;
                    }
                    if (npc.frameCounter > 9)
                    {
                        npc.frame.Y += frameHeight;
                        npc.frameCounter = 0;
                    }
                }
                if (npc.frame.Y > FrameHeight * 7)
                {
                    npc.frame.Y = 4;
                }
                return;
            }
            else
            {
                if (npc.frameCounter > 9)
                {
                    npc.frame.Y += 130;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y > FrameHeight * 5)
                {
                    npc.frame.Y = 0;
                }
                return;
            }
        }

        public void FuriousFlexing()
        {
            npc.velocity.X *= .85f;
            npc.velocity.Y *= .85f;
            npc.alpha += 2;
            if (npc.alpha >= 255)
            {
                npc.active = false;
            }
            if (npc.ai[3] < 300)
            {
                npc.ai[3] = 300;
            }
            if (npc.frameCounter > 5)
            {
                npc.frame.Y += 130;
                npc.frameCounter = 0;
                if (npc.ai[3] > 381)
                {
                    npc.ai[3] = 300;
                }
            }
        }

        public void FireProjectile()
        {
            List<Point> list4 = new List<Point>();
            Vector2 vec5 = Main.player[npc.target].Center + new Vector2(Main.player[npc.target].velocity.X * 30f, 0f);
            Point point14 = vec5.ToTileCoordinates();
            int num1468 = 0;
            while (num1468 < 1000 && list4.Count < 1)
            {
                bool flag118 = false;
                int num1469 = Main.rand.Next(point14.X - 30, point14.X + 30 + 1);
                foreach (Point current in list4)
                {
                    if (Math.Abs(current.X - num1469) < 10)
                    {
                        flag118 = true;
                        break;
                    }
                }
                if (!flag118)
                {
                    int startY = point14.Y - 20;
                    int num1470;
                    int num1471;
                    Collision.ExpandVertically(num1469, startY, out num1470, out num1471, 1, 51);
                    if (StrayMethods.CanSpawnSandstormHostile(new Vector2(num1469, num1471 - 15) * 16f, 15, 15))
                    {
                        list4.Add(new Point(num1469, num1471 - 15));
                    }
                }
                num1468++;
            }
            foreach (Point current2 in list4)
            {
                Projectile.NewProjectile(current2.X * 16, current2.Y * 16, 0f, 0f, 658, damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            npc.position.X = npc.position.X + npc.width / 2;
            npc.position.Y = npc.position.Y + npc.height / 2;
            npc.position.X = npc.position.X - npc.width / 2;
            npc.position.Y = npc.position.Y - npc.height / 2;
            int dust = mod.DustType<Dusts.SandDust>();
            for (int Loop = 0; Loop < 5; Loop++)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust, 0f, 0f, 0);
                Main.dust[dust].velocity.Y = hitDirection * 0.1F;
                Main.dust[dust].noGravity = false;
            }
            if (npc.life <= 0)
            {
                for (int Loop = 0; Loop < 5; Loop++)
                {
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust, 0f, 0f, 0);
                    Main.dust[dust].velocity.X *= 0f;
                    Main.dust[dust].noGravity = false;
                }
            }
        }

        public void MoveToPoint(Vector2 point, float moveSpeed)
        {
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
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

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }


        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
            AAWorld.downedDjinn = true;
        }

        public override void NPCLoot()
        {
            Sandstorm.TimeLeft = 0;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DjinnTrophy"));
            }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DjinnMask"));
                }
                npc.DropLoot(mod.ItemType("DesertMana"), 10, 15);
                string[] lootTable = { "Djinnerang", "SandLamp", "SandScepter", "SandstormCrossbow", "SultanScimitar" };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(Items.Vanity.Mask.DjinnMask.type, 1f / 7);
                if (Main.rand.Next(6) == 0)
                {
                    npc.DropLoot(mod.ItemType("Sandagger"), 90, 120);
                }
                else
                {
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                }
            }
            else
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D CurrentTex;
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D MudaMuda = mod.GetTexture("NPCs/Bosses/Djinn/DesertDjinnMudaMuda");
            Texture2D Punch = mod.GetTexture("NPCs/Bosses/Djinn/DesertDjinnPunch");

            if (internalAI[0] == 1)
            {
                CurrentTex = MudaMuda;
            }
            else if (internalAI[0] == 2)
            {
                CurrentTex = Punch;
            }
            else
            {
                CurrentTex = texture;
            }

            var effects = npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(CurrentTex, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);

            return false;
        }

        private static void StartSandstorm()
        {
            Sandstorm.Happening = true;
            Sandstorm.TimeLeft = (int)(3600f * (8f + Main.rand.NextFloat() * 16f));
            ChangeSeverityIntentions();
        }

        private static void ChangeSeverityIntentions()
        {
            if (Sandstorm.Happening)
            {
                Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat();
            }
            else if (Main.rand.Next(3) == 0)
            {
                Sandstorm.IntendedSeverity = 0f;
            }
            else
            {
                Sandstorm.IntendedSeverity = Main.rand.NextFloat() * 0.3f;
            }
            if (Main.netMode != 1)
            {
                NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}
