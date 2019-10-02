using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Enemies.Cavern
{
    public class Scavenger : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scavenger");
        }

        public override void SetDefaults()
        {
            npc.damage = 40;
            npc.width = 50;
            npc.height = 50;
            npc.defense = 0;
            npc.lifeMax = 1000;
            npc.aiStyle = 6;
            aiType = -1;
            animationType = 10;
            npc.knockBackResist = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = new LegacySoundStyle(21, 1);
            npc.DeathSound = new LegacySoundStyle(2, 14);
            npc.netAlways = true;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (npc.target < 0 || npc.target == 255 || player.dead)
            {
                npc.TargetClosest(true);
            }
            npc.velocity.Length();
            if (npc.ai[2] != 1)
            {
                int Previous = npc.whoAmI;
                for (int num36 = 0; num36 < 6; num36++)
                {
                    int a;
                    if (num36 >= 0 && num36 < 5)
                    {
                        a = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("ScavengerBody"), npc.whoAmI);
                    }
                    else
                    {
                        a = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), mod.NPCType("ScavengerTail"), npc.whoAmI);
                    }
                    Main.npc[a].realLife = npc.whoAmI;
                    Main.npc[a].ai[2] = npc.whoAmI;
                    Main.npc[a].ai[1] = Previous;
                    Main.npc[Previous].ai[0] = a;
                    NetMessage.SendData(23, -1, -1, null, a, 0f, 0f, 0f, 0);
                    Previous = a;
                }
                npc.ai[2] = 1;
            }
            int num50 = (int)(npc.position.X / 16f) - 1;
            int num51 = (int)((npc.position.X + npc.width) / 16f) + 2;
            int num52 = (int)(npc.position.Y / 16f) - 1;
            int num53 = (int)((npc.position.Y + npc.height) / 16f) + 2;
            if (num50 < 0)
            {
                num50 = 0;
            }
            if (num51 > Main.maxTilesX)
            {
                num51 = Main.maxTilesX;
            }
            if (num52 < 0)
            {
                num52 = 0;
            }
            if (num53 > Main.maxTilesY)
            {
                num53 = Main.maxTilesY;
            }
            bool flies = false;
            if (!flies)
            {
                for (int num952 = num50; num952 < num51; num952++)
                {
                    for (int num953 = num52; num953 < num53; num953++)
                    {
                        if (Main.tile[num952, num953] != null && ((Main.tile[num952, num953].nactive() && (Main.tileSolid[Main.tile[num952, num953].type] || (Main.tileSolidTop[Main.tile[num952, num953].type] && Main.tile[num952, num953].frameY == 0))) || Main.tile[num952, num953].liquid > 64))
                        {
                            Vector2 vector105;
                            vector105.X = num952 * 16;
                            vector105.Y = num953 * 16;
                            if (npc.position.X + npc.width > vector105.X && npc.position.X < vector105.X + 16f && npc.position.Y + npc.height > vector105.Y && npc.position.Y < vector105.Y + 16f)
                            {
                                flies = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flies)
            {
                npc.localAI[1] = 1f;
                Rectangle rectangle12 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                bool flag95 = true;
                if (npc.position.Y > player.position.Y)
                {
                    for (int num955 = 0; num955 < 255; num955++)
                    {
                        if (Main.player[num955].active)
                        {
                            Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - 1000, (int)Main.player[num955].position.Y - 1000, 2000, 2000);
                            if (rectangle12.Intersects(rectangle13))
                            {
                                flag95 = false;
                                break;
                            }
                        }
                    }
                    if (flag95)
                    {
                        flies = true;
                    }
                }
            }
            else
            {
                npc.localAI[1] = 0f;
            }
            if (player.dead)
            {
                flies = false;
                npc.velocity.Y = npc.velocity.Y + 1f;
                if (npc.position.Y > Main.worldSurface * 16.0)
                {
                    npc.velocity.Y = npc.velocity.Y + 1f;
                }
                if (npc.position.Y > Main.rockLayer * 16.0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == npc.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }
            float speed = 12.5f;
            float turnSpeed = 0.125f;
            float num58 = speed;
            float num59 = turnSpeed;
            Vector2 vector18 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num61 = player.position.X + player.width / 2;
            float num62 = player.position.Y + player.height / 2;
            num61 = (int)(num61 / 16f) * 16;
            num62 = (int)(num62 / 16f) * 16;
            vector18.X = (int)(vector18.X / 16f) * 16;
            vector18.Y = (int)(vector18.Y / 16f) * 16;
            num61 -= vector18.X;
            num62 -= vector18.Y;
            float num63 = (float)System.Math.Sqrt(num61 * num61 + num62 * num62);
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num61 = Main.npc[(int)npc.ai[1]].position.X + Main.npc[(int)npc.ai[1]].width / 2 - vector18.X;
                    num62 = Main.npc[(int)npc.ai[1]].position.Y + Main.npc[(int)npc.ai[1]].height / 2 - vector18.Y;
                }
                catch
                {
                }
                npc.rotation = (float)System.Math.Atan2(num62, num61) + 1.57f;
                num63 = (float)System.Math.Sqrt(num61 * num61 + num62 * num62);
                int num64 = npc.width;
                num63 = (num63 - num64) / num63;
                num61 *= num63;
                num62 *= num63;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num61;
                npc.position.Y = npc.position.Y + num62;
            }
            else
            {
                if (!flies)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + (turnSpeed * 0.75f);
                    if (npc.velocity.Y > num58)
                    {
                        npc.velocity.Y = num58;
                    }
                    if (System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y) < num58 * 0.4)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num59 * 1.1f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num59 * 1.1f;
                        }
                    }
                    else if (npc.velocity.Y == num58)
                    {
                        if (npc.velocity.X < num61)
                        {
                            npc.velocity.X = npc.velocity.X + num59;
                        }
                        else if (npc.velocity.X > num61)
                        {
                            npc.velocity.X = npc.velocity.X - num59;
                        }
                    }
                    else if (npc.velocity.Y > 4f)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num59 * 0.9f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num59 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (!flies && npc.behindTiles && npc.soundDelay == 0)
                    {
                        float num65 = num63 / 40f;
                        if (num65 < 10f)
                        {
                            num65 = 10f;
                        }
                        if (num65 > 20f)
                        {
                            num65 = 20f;
                        }
                        npc.soundDelay = (int)num65;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
                    }
                    num63 = (float)System.Math.Sqrt(num61 * num61 + num62 * num62);
                    float num66 = System.Math.Abs(num61);
                    float num67 = System.Math.Abs(num62);
                    float num68 = num58 / num63;
                    num61 *= num68;
                    num62 *= num68;
                    bool flag21 = false;
                    if (!flag21)
                    {
                        if ((npc.velocity.X > 0f && num61 > 0f) || (npc.velocity.X < 0f && num61 < 0f) || (npc.velocity.Y > 0f && num62 > 0f) || (npc.velocity.Y < 0f && num62 < 0f))
                        {
                            if (npc.velocity.X < num61)
                            {
                                npc.velocity.X = npc.velocity.X + num59;
                            }
                            else
                            {
                                if (npc.velocity.X > num61)
                                {
                                    npc.velocity.X = npc.velocity.X - num59;
                                }
                            }
                            if (npc.velocity.Y < num62)
                            {
                                npc.velocity.Y = npc.velocity.Y + num59;
                            }
                            else
                            {
                                if (npc.velocity.Y > num62)
                                {
                                    npc.velocity.Y = npc.velocity.Y - num59;
                                }
                            }
                            if (System.Math.Abs(num62) < num58 * 0.2 && ((npc.velocity.X > 0f && num61 < 0f) || (npc.velocity.X < 0f && num61 > 0f)))
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y + num59 * 2f;
                                }
                                else
                                {
                                    npc.velocity.Y = npc.velocity.Y - num59 * 2f;
                                }
                            }
                            if (System.Math.Abs(num61) < num58 * 0.2 && ((npc.velocity.Y > 0f && num62 < 0f) || (npc.velocity.Y < 0f && num62 > 0f)))
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X + num59 * 2f;
                                }
                                else
                                {
                                    npc.velocity.X = npc.velocity.X - num59 * 2f;
                                }
                            }
                        }
                        else
                        {
                            if (num66 > num67)
                            {
                                if (npc.velocity.X < num61)
                                {
                                    npc.velocity.X = npc.velocity.X + num59 * 1.1f;
                                }
                                else if (npc.velocity.X > num61)
                                {
                                    npc.velocity.X = npc.velocity.X - num59 * 1.1f;
                                }
                                if (System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y) < num58 * 0.5)
                                {
                                    if (npc.velocity.Y > 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + num59;
                                    }
                                    else
                                    {
                                        npc.velocity.Y = npc.velocity.Y - num59;
                                    }
                                }
                            }
                            else
                            {
                                if (npc.velocity.Y < num62)
                                {
                                    npc.velocity.Y = npc.velocity.Y + num59 * 1.1f;
                                }
                                else if (npc.velocity.Y > num62)
                                {
                                    npc.velocity.Y = npc.velocity.Y - num59 * 1.1f;
                                }
                                if (System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y) < num58 * 0.5)
                                {
                                    if (npc.velocity.X > 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X + num59;
                                    }
                                    else
                                    {
                                        npc.velocity.X = npc.velocity.X - num59;
                                    }
                                }
                            }
                        }
                    }
                }
                npc.rotation = (float)System.Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
                if (flies)
                {
                    if (npc.localAI[0] != 1f)
                    {
                        npc.netUpdate = true;
                    }
                    npc.localAI[0] = 1f;
                }
                else
                {
                    if (npc.localAI[0] != 0f)
                    {
                        npc.netUpdate = true;
                    }
                    npc.localAI[0] = 0f;
                }
                if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
                {
                    npc.netUpdate = true;
                    return;
                }
            }
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 10, hitDirection, -1f, 0, default, 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 10, hitDirection, -1f, 0, default, 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            BaseAI.DropItem(npc, mod.ItemType("CovetiteCrystal"), Main.expertMode ? 1 + Main.rand.Next(1) : 1, 5, Main.expertMode ? 40 : 30, true);
            npc.DropLoot(mod.ItemType<Items.Usable.GreedKey>(), .05f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, drawColor, true);
            return false;
        }
    }


    public class ScavengerBody : Scavenger
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scavenger");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 36;
            npc.height = 36;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void AI()
        {
            if (!Main.npc[(int)npc.ai[1]].active)
            {
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.active = false;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 10, hitDirection, -1f, 0, default, 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 10, hitDirection, -1f, 0, default, 1f);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, drawColor, true);
            return false;
        }
    }

    public class ScavengerTail : Scavenger
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scavenger");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 44;
            npc.height = 44;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void AI()
        {
            if (!Main.npc[(int)npc.ai[1]].active)
            {
                npc.life = 0;
                npc.HitEffect(0, 10.0);
                npc.active = false;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 10, hitDirection, -1f, 0, default, 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 10; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 10, hitDirection, -1f, 0, default, 1f);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, drawColor, true);
            return false;
        }
    }
}

