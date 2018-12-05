using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using BaseMod;
using System;

namespace AAMod.NPCs.Critters
{
    public class ShroomGrub : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ShroomGrub");
            Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 10;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 0.15f;
            npc.catchItem = (short)mod.ItemType("ShroomGrub");
        }

        public override void AI()
        {
            if (npc.velocity.Y == 0f)
            {
                if (npc.ai[0] == 1f)
                {
                    if (npc.direction == 0)
                    {
                        npc.TargetClosest(true);
                    }
                    if (npc.collideX)
                    {
                        npc.direction *= -1;
                    }
                    float num961 = 0.2f;
                    npc.velocity.X = num961 * (float)npc.direction;
                    npc.velocity.X = npc.velocity.X * 3f;
                }
                else
                {
                    npc.velocity.X = 0f;
                }
                if (Main.netMode != 1)
                {
                    npc.localAI[1] -= 1f;
                    if (npc.localAI[1] <= 0f)
                    {
                        if (npc.ai[0] == 1f)
                        {
                            npc.ai[0] = 0f;
                            npc.localAI[1] = (float)Main.rand.Next(300, 900);
                        }
                        else
                        {
                            npc.ai[0] = 1f;
                            npc.localAI[1] = (float)Main.rand.Next(600, 1800);
                        }
                        npc.netUpdate = true;
                    }
                }
            }
            npc.spriteDirection = npc.direction;
            bool flag58 = false;
            for (int num962 = 0; num962 < 255; num962++)
            {
                Player player = Main.player[num962];
                if (player.active && !player.dead && Vector2.Distance(player.Center, npc.Center) <= 160f)
                {
                    flag58 = true;
                    break;
                }
            }
            int num963 = 90;
            if (flag58 && npc.ai[1] < (float)num963)
            {
                npc.ai[1] += 1f;
            }
            if (npc.ai[1] == (float)num963 && Main.netMode != 1)
            {
                npc.position.Y = npc.position.Y + 16f;
                npc.Transform(mod.NPCType<ShroomGrubDig>());
                npc.netUpdate = true;
                return;
            }
        }

        
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMush && spawnInfo.spawnTileY < Main.worldSurface && Main.hardMode && NPC.downedMoonlord ? .5f : 0f;
        }
	}

    public class ShroomGrubDig : ModNPC
    {
        public override string Texture { get { return "AAMod/NPCs/Critters/ShroomGrub_Dig"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ShroomGrub");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 10;
            npc.height = 10;
            npc.aiStyle = 6;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.behindTiles = true;
            npc.npcSlots = 0.15f;
            npc.catchItem = (short)mod.ItemType("ShroomGrub"); ;
        }

        public override void AI()
        {

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
            bool flag2 = false;
            if (!flag2)
            {
                for (int num33 = num29; num33 < num30; num33++)
                {
                    for (int num34 = num31; num34 < num32; num34++)
                    {
                        if (Main.tile[num33, num34] != null && ((Main.tile[num33, num34].nactive() && (Main.tileSolid[(int)Main.tile[num33, num34].type] || (Main.tileSolidTop[(int)Main.tile[num33, num34].type] && Main.tile[num33, num34].frameY == 0))) || Main.tile[num33, num34].liquid > 64))
                        {
                            Vector2 vector;
                            vector.X = (float)(num33 * 16);
                            vector.Y = (float)(num34 * 16);
                            if (npc.position.X + (float)npc.width > vector.X && npc.position.X < vector.X + 16f && npc.position.Y + (float)npc.height > vector.Y && npc.position.Y < vector.Y + 16f)
                            {
                                flag2 = true;
                                if (Main.rand.Next(100) == 0 && Main.tile[num33, num34].nactive())
                                {
                                    WorldGen.KillTile(num33, num34, true, true, false);
                                }
                            }
                        }
                    }
                }
            }
            if (!flag2)
            {
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int num35 = 1000;
                bool flag3 = true;
                for (int num36 = 0; num36 < 255; num36++)
                {
                    if (Main.player[num36].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[num36].position.X - num35, (int)Main.player[num36].position.Y - num35, num35 * 2, num35 * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            flag3 = false;
                            break;
                        }
                    }
                }
                if (flag3)
                {
                    flag2 = true;
                }
            }
            float num37 = 6f;
            float num38 = 0.15f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num40 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
            float num41 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);

            num40 = (float)((int)(num40 / 16f) * 16);
            num41 = (float)((int)(num41 / 16f) * 16);
            vector2.X = (float)((int)(vector2.X / 16f) * 16);
            vector2.Y = (float)((int)(vector2.Y / 16f) * 16);
            num40 -= vector2.X;
            num41 -= vector2.Y;
            num40 *= -1f;
            num41 *= -1f;
            float num53 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
            if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    num40 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector2.X;
                    num41 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector2.Y;
                }
                catch
                {
                }
                npc.rotation = (float)Math.Atan2((double)num41, (double)num40) + 1.57f;
                num53 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
                int num54 = npc.width;
                num53 = (num53 - (float)num54) / num53;
                num40 *= num53;
                num41 *= num53;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num40;
                npc.position.Y = npc.position.Y + num41;
            }
            else
            {
                if (!flag2)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > num37)
                    {
                        npc.velocity.Y = num37;
                    }
                    if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num37 * 0.4)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num38 * 1.1f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num38 * 1.1f;
                        }
                    }
                    else if (npc.velocity.Y == num37)
                    {
                        if (npc.velocity.X < num40)
                        {
                            npc.velocity.X = npc.velocity.X + num38;
                        }
                        else if (npc.velocity.X > num40)
                        {
                            npc.velocity.X = npc.velocity.X - num38;
                        }
                    }
                    else if (npc.velocity.Y > 4f)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num38 * 0.9f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num38 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (npc.soundDelay == 0)
                    {
                        float num55 = num53 / 40f;
                        if (num55 < 10f)
                        {
                            num55 = 10f;
                        }
                        if (num55 > 20f)
                        {
                            num55 = 20f;
                        }
                        npc.soundDelay = (int)num55;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
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
                }
                npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;

            }
        }
    }
}