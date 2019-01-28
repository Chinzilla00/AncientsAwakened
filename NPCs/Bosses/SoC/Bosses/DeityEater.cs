using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    [AutoloadBossHead]
    public class DeityEater : ModNPC
	{
        public bool HeadsSpawned = false;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crom Cruach");
        }


        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 38;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.damage = 90;
            npc.defense = 100;
            npc.lifeMax = 150000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.behindTiles = true;
            npc.scale = 1f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/SoC");
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            npc.alpha = 255;
        }
        
        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void AI()
        {
            if (Main.netMode != 1)
            {
                if (npc.ai[0] == 0)
                {
                    npc.realLife = npc.whoAmI;
                    int latestNPC = npc.whoAmI;
                    int Length = 30;
                    for (int i = 0; i < Length; ++i)
                    {
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DeityEaterBody"), npc.whoAmI, 0, latestNPC);
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    }

                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DeityEaterTail"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("CthulhuDust"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            npc.ai[1] = (float)Main.rand.Next(-2, 0);
            npc.netUpdate = true;
            bool flag = false;
            float num4 = 0.2f;
            int num5 = npc.type;
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || (flag && (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0))
            {
                npc.TargetClosest(true);
            }
            if (Main.player[npc.target].dead || (flag && (double)Main.player[npc.target].position.Y < Main.worldSurface * 16.0))
            {
                if (npc.timeLeft > 300)
                {
                    npc.timeLeft = 300;
                }
                if (flag)
                {
                    npc.velocity.Y = npc.velocity.Y + num4;
                }
            }
            int num29 = (int)(npc.position.X / 16f) - 1;
            int num30 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
            int num31 = (int)(npc.position.Y / 16f) - 1;
            int num32 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
            if (num29 < 0)
            {
                num29 = 0;
            }
            if (num30 > Main.maxTilesX)
            {
                num30 = Main.maxTilesX;
            }
            if (num31 < 0)
            {
                num31 = 0;
            }
            if (num32 > Main.maxTilesY)
            {
                num32 = Main.maxTilesY;
            }
            float num37 = 9f;
            float num38 = 0.16f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num40 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
            float num41 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
            num40 = ((int)(num40 / 16f) * 16);
            num41 = ((int)(num41 / 16f) * 16);
            vector2.X = ((int)(vector2.X / 16f) * 16);
            vector2.Y = ((int)(vector2.Y / 16f) * 16);
            num40 -= vector2.X;
            num41 -= vector2.Y;
            float num53 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
            float num55 = num53 / 40f;
            if (num55 < 10f)
            {
                num55 = 10f;
            }
            if (num55 > 20f)
            {
                num55 = 20f;
            }
            num53 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
            float num56 = Math.Abs(num40);
            float num57 = Math.Abs(num41);
            float num58 = num37 / num53;
            num40 *= num58;
            num41 *= num58;
            bool flag6 = false;
            if (!flag6)
            {
                if ((npc.velocity.X > 0f && num40 > 0f) || (npc.velocity.X < 0f && num40 < 0f) || (npc.velocity.Y > 0f && num41 > 0f) || (npc.velocity.Y < 0f && num41 < 0f))
                {
                    if (npc.velocity.X < num40)
                    {
                        npc.velocity.X = npc.velocity.X + num38;
                    }
                    else if (npc.velocity.X > num40)
                    {
                        npc.velocity.X = npc.velocity.X - num38;
                    }
                    if (npc.velocity.Y < num41)
                    {
                        npc.velocity.Y = npc.velocity.Y + num38;
                    }
                    else if (npc.velocity.Y > num41)
                    {
                        npc.velocity.Y = npc.velocity.Y - num38;
                    }
                    if ((double)Math.Abs(num41) < (double)num37 * 0.2 && ((npc.velocity.X > 0f && num40 < 0f) || (npc.velocity.X < 0f && num40 > 0f)))
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num38 * 2f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - num38 * 2f;
                        }
                    }
                    if ((double)Math.Abs(num40) < (double)num37 * 0.2 && ((npc.velocity.Y > 0f && num41 < 0f) || (npc.velocity.Y < 0f && num41 > 0f)))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num38 * 2f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num38 * 2f;
                        }
                    }
                }
                else if (num56 > num57)
                {
                    if (npc.velocity.X < num40)
                    {
                        npc.velocity.X = npc.velocity.X + num38 * 1.1f;
                    }
                    else if (npc.velocity.X > num40)
                    {
                        npc.velocity.X = npc.velocity.X - num38 * 1.1f;
                    }
                    if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num37 * 0.5)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num38;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - num38;
                        }
                    }
                }
                else
                {
                    if (npc.velocity.Y < num41)
                    {
                        npc.velocity.Y = npc.velocity.Y + num38 * 1.1f;
                    }
                    else if (npc.velocity.Y > num41)
                    {
                        npc.velocity.Y = npc.velocity.Y - num38 * 1.1f;
                    }
                    if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num37 * 0.5)
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num38;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num38;
                        }
                    }
                }
            }
            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
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

        public override void HitEffect(int hitDirection, double dmg)
        {
            if (npc.life > 0)
            {
                AAWorld.SoCBossDeathPoint = npc.Center;
                SoC.ComeBack = true;
                int num121 = 0;
                while ((double)num121 < dmg / (double)npc.lifeMax * 3.0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, Color.Transparent, 0.75f);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Dust dust39 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                        dust39.noGravity = true;
                    }
                    for (int num122 = 0; num122 < npc.oldPos.Length; num122++)
                    {
                        if (Main.rand.Next(4) == 0)
                        {
                            if (npc.oldPos[num122] == Vector2.Zero)
                            {
                                break;
                            }
                            if (Main.rand.Next(3) == 0)
                            {
                                Dust.NewDust(npc.oldPos[num122], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, Color.Transparent, 0.75f);
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                Dust dust40 = Main.dust[Dust.NewDust(npc.oldPos[num122], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                                dust40.noGravity = true;
                            }
                        }
                    }
                    num121++;
                }
            }
            else
            {
                for (int num123 = 0; num123 < 5; num123++)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                    }
                    Dust dust41 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                    dust41.noGravity = true;
                    dust41.velocity *= 3f;
                }
                for (int num124 = 0; num124 < npc.oldPos.Length; num124++)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        if (npc.oldPos[num124] == Vector2.Zero)
                        {
                            break;
                        }
                        for (int num125 = 0; num125 < 2; num125++)
                        {
                            if (Main.rand.Next(3) == 0)
                            {
                                Dust.NewDust(npc.oldPos[num124], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                            }
                            Dust dust42 = Main.dust[Dust.NewDust(npc.oldPos[num124], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                            dust42.noGravity = true;
                            dust42.velocity *= 3f;
                        }
                    }
                }
            }
        }

        /*public const string BodyTex = "AAMod/NPCs/Bosses/SoC/Bosses/DeityEater_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(BodyTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }*/
    }
}
