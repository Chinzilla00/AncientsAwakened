using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.SoC.Bosses;

namespace AAMod.NPCs.Bosses.SoC
{
    [AutoloadBossHead]
    public class SoC : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Cthulhu");
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 54;
            npc.height = 54;
            npc.aiStyle = -1;
            npc.damage = 100;
            npc.defense = 150;
            npc.lifeMax = 1000000;
            npc.value = Item.buyPrice(35, 0, 0, 0);
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/SoC");
            npc.noGravity = true;
            npc.netAlways = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
        }

        public bool LeaveLine = false;
        public bool Pinch = false;
        public bool Eye = false;
        public bool Eater = false;
        public bool Skull = false;
        public bool Rose = false;
        public bool Leviathan = false;
        public bool Summon = false;
        public bool Boss1 = false;
        public bool Boss2 = false;
        public bool Boss3 = false;
        public bool Boss4 = false;

        public float Rotation = 0;
        public float AlphaTimer = 0;
        public float alpha = 255;
        public float scale = 0;
        public float RingRotation = 0;
        public float morphTimer = 0;
        public bool Morph = false;
        public float RiftSpin = 0;
        public bool Morphed = false;
        public static bool ComeBack = false;
        public int ReturnTimer = 100;


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        

        int oneTime = 0;

        public int EnemyTimer = 0;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            AAPlayer modPlayer = Main.player[npc.target].GetModPlayer<AAPlayer>();
            modPlayer.Leave = false;
            npc.rotation = npc.velocity.X / 15f;
            Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
            float EyeSummon = npc.lifeMax * .8f;
            float EaterSummon = npc.lifeMax * .6f;
            float SkullSummon = npc.lifeMax * .4f;
            float LeviathanSummon = npc.lifeMax * .2f;
            bool BossAlive = NPC.AnyNPCs(mod.NPCType<DeityEye>()) || NPC.AnyNPCs(mod.NPCType<DeityEater>()) || NPC.AnyNPCs(mod.NPCType<DeitySkull>()) || NPC.AnyNPCs(mod.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(mod.NPCType<DeityRose>());
            EnemyTimer++;

            if (EnemyTimer >= 600)
            {
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("Portal"), 0, -npc.velocity.X, -npc.velocity.Y);
                EnemyTimer = 0;
            }


            if (oneTime == 0)
            {
                RainStart();
                oneTime++;
            }

            if (BossAlive)
            {
                Morphed = true;
                return;
            }
            else
            {
                Morphed = false;
            }

            if (Morphed)
            {
                npc.alpha += 12;
                if (npc.alpha >= 140)
                {
                    npc.alpha = 140;
                }
                npc.dontTakeDamage = true;

                npc.netUpdate = true;
                return;
            }
            else
            {
                npc.alpha -= 30;
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                }
                npc.dontTakeDamage = false;

                npc.netUpdate = true;
            }

            if (npc.ai[1] == 1f || npc.ai[1] == 0f)
            {
                npc.dontTakeDamage = false;
            }
            else
            {
                npc.dontTakeDamage = true;
            }

            if (npc.life < EyeSummon && !Boss1) //Spawn Eye boi
            {
                Boss1 = true;
                npc.ai[1] = 2f;
                npc.dontTakeDamage = true;
                morphTimer = 0;
            }
            else if (npc.life < EaterSummon && !Boss2)
            {
                Boss2 = true;
                npc.ai[1] = 3f;
                npc.dontTakeDamage = true;
                morphTimer = 0;
            }
            else if (npc.life < SkullSummon && !Boss3)
            {
                Boss3 = true;
                npc.ai[1] = 4f;
                npc.dontTakeDamage = true;
                morphTimer = 0;
            }
            else if (npc.life < LeviathanSummon && !Boss4)
            {
                Boss4 = true;
                npc.ai[1] = 7f;
                npc.dontTakeDamage = true;
                morphTimer = 0;
            }

            if (npc.life <= npc.lifeMax / 10)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/LastStand");
                if (!Pinch)
                {
                    Pinch = true;
                    Main.NewText("YOU", Color.DarkCyan);
                    Main.NewText("WILL", Color.DarkCyan);
                    Main.NewText("PERISH", Color.DarkCyan);
                }
            }
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.ai[1] = 3f;
                }
            }
            if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.ai[1] = 4f;
                }
            }
            if (npc.ai[1] == 0f)
            {
                npc.damage = 100;

                Rotation += npc.velocity.X * .01f;
                RiftSpin -= npc.velocity.X * .01f;

                
                npc.rotation = npc.velocity.X / 15f;
                if (npc.position.Y > Main.player[npc.target].position.Y - 200f)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.98f;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                    if (npc.velocity.Y > 2f)
                    {
                        npc.velocity.Y = 2f;
                    }
                }
                else if (npc.position.Y < Main.player[npc.target].position.Y - 300f)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.98f;
                    }
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                    if (npc.velocity.Y < -2f)
                    {
                        npc.velocity.Y = -2f;
                    }
                }
                if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + 100f)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.98f;
                    }
                    npc.velocity.X = npc.velocity.X - 0.1f;
                    if (npc.velocity.X > 8f)
                    {
                        npc.velocity.X = 8f;
                    }
                }
                if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.98f;
                    }
                    npc.velocity.X = npc.velocity.X + 0.1f;
                    if (npc.velocity.X < -8f)
                    {
                        npc.velocity.X = -8f;
                        return;
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= 600f)
                {
                    npc.ai[2] = 0f;
                    npc.ai[1] = 1f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (npc.ai[1] == 1f)
                {
                    npc.defense = 180;
                    npc.damage = 200;
                    npc.ai[2] += 1f;
                    if (npc.ai[2] == 2f)
                    {
                        Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 92);
                    }
                    if (npc.ai[2] >= 400f)
                    {
                        npc.ai[2] = 0f;
                        npc.ai[1] = 0f;
                    }
                    npc.rotation += npc.direction * 0.7f;
                    Vector2 vector44 = new Vector2(npc.position.X + ((float)npc.width * 0.5f), npc.position.Y + ((float)npc.height * 0.5f));
                    float num441 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector44.X;
                    float num442 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
                    float num443 = (float)Math.Sqrt((double)((num441 * num441) + (num442 * num442)));
                    float num4 = 5f + num443 / 100f;
                    if (num4 < 8.0)
                        num4 = 8f;
                    if (num4 > 32.0)
                        num4 = 32f;
                    float num5 = num4 / num443;
                    npc.velocity.X = num441 * num5;
                    npc.velocity.Y = num442 * num5;
                    Rotation += npc.velocity.X * .08f;
                    RiftSpin -= npc.velocity.X * .08f;
                    return;

                }
                if (npc.ai[1] == 2f)
                {
                    Summon = true;
                    npc.velocity *= .8f;
                    if (npc.velocity.X < .5f || npc.velocity.X > -.5f)
                    {
                        npc.velocity.X = 0;
                    }
                    if (npc.velocity.Y < .5f || npc.velocity.Y > -.5f)
                    {
                        npc.velocity.Y = 0;
                    }

                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {

                        Rotation += .2f;
                        RiftSpin -= .2f;
                        morphTimer++;

                        if (morphTimer > 300)
                        {

                            if (Eye == false)
                            {
                                Eye = true;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityEye"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                            }
                            
                        }
                    }
                    return;
                }
                if (npc.ai[1] == 3f)
                {
                    Summon = true;
                    npc.velocity *= .8f;
                    if (npc.velocity.X < .5f || npc.velocity.X > -.5f)
                    {
                        npc.velocity.X = 0;
                    }
                    if (npc.velocity.Y < .5f || npc.velocity.Y > -.5f)
                    {
                        npc.velocity.Y = 0;
                    }

                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {
                        Rotation += .2f;
                        RiftSpin -= .2f;
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Eater == false)
                            {
                                Eater = true;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityEater"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                            }
                        }

                    }

                    return;
                }
                if (npc.ai[1] == 4f)
                {
                    Summon = true;
                    npc.velocity *= .8f;
                    if (npc.velocity.X < .5f || npc.velocity.X > -.5f)
                    {
                        npc.velocity.X = 0;
                    }
                    if (npc.velocity.Y < .5f || npc.velocity.Y > -.5f)
                    {
                        npc.velocity.Y = 0;
                    }

                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {

                        Rotation += .2f;
                        RiftSpin -= .2f;
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Skull == false)
                            {
                                Skull = true;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeitySkull"), 0, 0, 1);
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                            }
                        }

                    }


                    return;
                }

                if (npc.ai[1] == 6f)
                {
                    Summon = true;
                    npc.velocity *= .8f;
                    if (npc.velocity.X < .5f || npc.velocity.X > -.5f)
                    {
                        npc.velocity.X = 0;
                    }
                    if (npc.velocity.Y < .5f || npc.velocity.Y > -.5f)
                    {
                        npc.velocity.Y = 0;
                    }

                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {
                        Rotation += .2f;
                        RiftSpin -= .2f;
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Rose == false)
                            {
                                Rose = true;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityRose"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                            }
                        }
                    }


                    return;
                }

                if (npc.ai[1] == 7f)
                {
                    Summon = true;
                    npc.velocity *= .8f;
                    if (npc.velocity.X < .5f || npc.velocity.X > -.5f)
                    {
                        npc.velocity.X = 0;
                    }
                    if (npc.velocity.Y < .5f || npc.velocity.Y > -.5f)
                    {
                        npc.velocity.Y = 0;
                    }

                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {
                        Rotation += .2f;
                        RiftSpin -= .2f;
                        morphTimer++;
                        if (morphTimer > 300)
                        {
                            if (Leviathan == false)
                            {
                                Leviathan = true;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityLeviathan"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                            }
                        }
                    }


                    return;
                }
                if (npc.ai[1] == 8f)
                {
                    Main.NewText("...good riddance...", Color.DarkCyan);
                    npc.ai[1] = 9f;
                }
                if (npc.ai[1] == 9f)
                {
                    Main.NewText("...do not return...", Color.DarkCyan);
                    npc.ai[1] = 9F;
                }
                if (npc.ai[1] == 10f)
                {
                    npc.alpha += 5;
                    {
                        if (npc.alpha >= 255)
                        {
                            npc.active = false;
                        }
                    }
                }
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true)
            {
                if (damage > npc.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    damage = 0;
                }

                return false;
            }

            return true;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture2D13 = Main.npcTexture[npc.type];
            Texture2D WheelTex = mod.GetTexture("NPCs/Bosses/SoC/SoC_Wheel");
            Texture2D RingTex = mod.GetTexture("NPCs/Bosses/SoC/DeityCircle");
            Texture2D RitualTex = mod.GetTexture("NPCs/Bosses/SoC/DeityRitual");
            Texture2D Rift = mod.GetTexture("NPCs/Bosses/SoC/Rift");
            Vector2 vector38 = npc.position + new Vector2(npc.width, npc.height) / 2f + Vector2.UnitY * npc.gfxOffY - Main.screenPosition;
            Vector2 origin8 = new Vector2((float)RitualTex.Width, (float)RitualTex.Height) / 2f;
            int num214 = Main.npcTexture[npc.type].Height;
            int y6 = 0;
            Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            Color? alpha4 = GetAlpha(color25);
            Color color;
            Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
            if (Summon)
            {
                Rotation += .2f;
                RiftSpin -= .2f;
                if (morphTimer < 300f)
                {
                    alpha -= 5;
                }
                else
                {
                    alpha += 12;
                }
                if (alpha < 0)
                {
                    alpha = 0;
                }
                if (alpha > 255)
                {
                    alpha = 255;
                }
                scale = 1f - alpha / 255f;
                RingRotation += 0.0149599658f;
                Main.spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(RitualTex, vector38, null, AAColor.Cthulhu, RingRotation, origin8, scale * 0.42f, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale * 0.42f, SpriteEffects.None, 0f);
            }
            if (npc.alpha > 0)
            {
                color = AAColor.Cthulhu;
            }
            else
            {
                color = drawColor;
            }
            Main.spriteBatch.Draw(Rift, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, Rift.Width, Rift.Height)), AAColor.Cthulhu, RiftSpin, new Vector2(Rift.Width / 2f, Rift.Height / 2f), 1.5f, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(WheelTex, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, WheelTex.Width, WheelTex.Height)), color, Rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), npc.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture2D13, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, texture2D13.Width, texture2D13.Height)), color, npc.rotation, new Vector2(texture2D13.Width / 2f, texture2D13.Height / 2f), npc.scale, SpriteEffects.None, 0f);
            return false;
        }

        private void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}