using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;

namespace AAMod.NPCs.Bosses.Truffle
{
    [AutoloadBossHead]
    public class TechnoTruffle : ModNPC
    {
        public bool AIType = true;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(AIType);
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
                AIType = reader.ReadBool();
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Techno Truffle");
            Main.npcFrameCount[npc.type] = 17;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 30000;
            npc.damage = 50;
            npc.defense = 40;
            npc.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            npc.value = Item.sellPrice(0, 12, 0, 0);
            npc.aiStyle = 0;
            npc.width = 66;
            npc.height = 104;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.buffImmune[46] = true;
            npc.buffImmune[47] = true;
            npc.netAlways = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            bossBag = mod.ItemType("TruffleBag");
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public static int AISTATE_HOVER = 0, AISTATE_FLIER = 1, AISTATE_SHOOT = 2, AISTATE_ROCKET = 3;
        public static int AISTATE_DASH = 0, AISTATE_CHARGE = 1, AISTATE_FLY = 2;
        public float[] internalAI = new float[4];
        bool HasStopped = false;
        bool SelectPoint = false;
        Vector2 MovePoint = new Vector2(0, 0);

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (internalAI[1] == AISTATE_ROCKET || internalAI[3] == AISTATE_FLY)
                {
                    if (npc.frame.Y > (frameHeight * 11) && npc.frame.Y < (frameHeight * 8))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 8;
                    }
                }
                else if (!AIType)
                {
                    if (npc.frame.Y > (frameHeight * 16) && npc.frame.Y < (frameHeight * 12))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 12;
                    }
                }
                else
                {
                    if (npc.frame.Y > (frameHeight * 7))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
            }
            if (npc.frame.Y > (frameHeight * 16))
            {
                npc.frame.Y = frameHeight * 12;
            }
        }

        public override void AI()
        {
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            Lighting.AddLight((int)(npc.Center.X + (npc.width / 2)) / 16, (int)(npc.position.Y + (npc.height / 2)) / 16, color.R / 255, color.G / 255, color.B / 255);

            if (Main.dayTime)
            {
                npc.active = false;
                Projectile.NewProjectile(npc.Center, new Vector2(0f, 0f), mod.ProjectileType("TruffleBookIt"), 0, 0);
                return;
            }
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.active = false;
                    Projectile.NewProjectile(npc.Center, new Vector2(0f, 0f), mod.ProjectileType("TruffleBookIt"), 0, 0);
                }
            }
            if (Main.netMode != 1)
            {
                internalAI[2]++;
            }

            if (internalAI[2] > 900)
            {
                if (!TileBelowEmpty(player))
                {
                    if (AIType)
                    {
                        AIType = false;
                    }
                    else
                    {
                        npc.noGravity = true;
                        AIType = true;
                    }
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[3] = 0;
                }
                else
                {
                    AIType = true;
                }
                internalAI[2] = 0;
                npc.netUpdate = true;
            }

            if (!AIType)
            {
                MonarchAI();
            }
            else
            {
                FungusAI();
            }
        }

        public bool TileBelowEmpty(Player player)
        {
            int tileX = (int)(player.Center.X / 16f) + player.direction * 2;
            int tileY = (int)((player.position.Y + player.height) / 16f);

            for (int tY = tileY; tY < tileY + 17; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                {
                    Main.tile[tileX, tY] = new Tile();
                }
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[Main.tile[tileX, tY].type] && !TileID.Sets.Platforms[Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void MonarchAI()
        {
            AIType = false;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }

            if (Main.netMode != 1)
            {
                if (internalAI[3] != AISTATE_FLY)
                {
                    npc.noGravity = false;
                    internalAI[0]++;
                }
                else
                {
                    npc.noGravity = true;
                }
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[3] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
                else if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    internalAI[3] = AISTATE_FLY;
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            if (internalAI[3] == AISTATE_DASH)
            {
                if (SelectPoint && Main.netMode != 1)
                {
                    float Point = 300 * npc.direction;
                    MovePoint = player.Center + new Vector2(Point, 0);
                    SelectPoint = false;
                    npc.netUpdate = true;
                }

                if (Main.netMode != 1)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 120)
                {
                    MoveToPoint(MovePoint);
                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
                    Lighting.AddLight((int)(npc.Center.X + (npc.width / 2)) / 16, (int)(npc.position.Y + (npc.height / 2)) / 16, Color.LightCyan.R / 255, Color.LightCyan.G / 255, Color.LightCyan.B / 255);
                    if (Vector2.Distance(npc.Center, MovePoint) <= 0 && Main.netMode != 1)
                    {
                        npc.rotation = 0;
                        internalAI[0] = 0;
                        internalAI[3] = Main.rand.Next(3);
                        npc.netUpdate = true;
                    }
                }
            }
            else if (internalAI[3] == AISTATE_FLY)
            {
                npc.noTileCollide = true;
                npc.noGravity = true;
                BaseAI.AISpaceOctopus(npc, ref npc.ai, .05f, 8, 250, 0, null);
                npc.rotation = 0;
                if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.rotation = 0;
                    npc.noGravity = false;
                    internalAI[0] = 0;
                    internalAI[3] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                    npc.noTileCollide = false;
                }
                npc.rotation = 0;
            }
            else
            {
                BaseAI.AICharger(npc, ref npc.ai, 0.07f, 10f, false, 30);
                npc.rotation = 0;
            }
        }

        public void FungusAI()
        {
            AIType = true;
            if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                npc.noTileCollide = false;
            }
            else
            {
                npc.noTileCollide = true;
            }
            npc.TargetClosest();
            Player player = Main.player[npc.target];
            if (Main.netMode != 1 && internalAI[1] != AISTATE_SHOOT)
            {
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    npc.ai = new float[4];
                    npc.netUpdate = true;
                }
            }
            if (internalAI[1] == AISTATE_HOVER)
            {
                BaseAI.AISpaceOctopus(npc, ref npc.ai, player.Center, 0.15f, 4f, 170, 56f, FireMagic);
            }
            else if (internalAI[1] == AISTATE_FLIER)
            {
                BaseAI.AIFlier(npc, ref npc.ai, true, 0.1f, 0.04f, 5f, 3f, false, 1);
            }
            else if (internalAI[1] == AISTATE_SHOOT)
            {
                if (HasStopped)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 60)
                {
                    int attack = Main.rand.Next(4);
                    internalAI[1] = Main.rand.Next(3);
                    internalAI[0] = 0;
                    FungusAttack(attack);
                    npc.netUpdate = true;
                }

                npc.velocity *= 0.7f;

                if (npc.velocity.X <= .1f && npc.velocity.X >= -.1f)
                {
                    npc.velocity.X = 0;
                }
                if (npc.velocity.Y <= .1f && npc.velocity.Y >= -.1f)
                {
                    npc.velocity.Y = 0;
                }
                if (npc.velocity == new Vector2(0, 0))
                {
                    HasStopped = true;
                }
            }
            npc.rotation = 0;
        }

        public float[] shootAI = new float[4];

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjType("TruffleShot"), ref shootAI[0], 5, (int)(npc.damage * (Main.expertMode ? 0.25f : 0.5f)), 8f, true, new Vector2(20f, 15f));
            npc.netUpdate = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {   //boss drops
            potionType = ItemID.GreaterHealingPotion;
            AAWorld.downedTruffle = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TruffleTrophy"));
            }
            Projectile.NewProjectile(npc.Center, npc.velocity, mod.ProjectileType("TruffleBookIt"), 0, 0, 255, npc.scale);
            if (Main.expertMode == true)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TruffleMask"));
                }
                for (int i = 0; i < Main.rand.Next(15, 20); i++)
                {
                    int type = ItemID.SoulofFright;
                    if (Main.rand.Next(3) == 0)
                    {
                        type = ItemID.SoulofSight;
                    }
                    else if (Main.rand.Next(3) == 1)
                    {
                        type = ItemID.SoulofMight;
                    }
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, type, 1);
                }
            }
        }

        public void FungusAttack(int Attack)
        {
            if (Attack == 0)
            {
                if (NPC.CountNPCS(mod.NPCType<Truffling>()) < 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 1)
                        {
                            NPC.NewNPC((int)npc.Center.X + 40, (int)npc.Center.Y - 40, mod.NPCType<Truffling>());
                        }
                        if (i == 2)
                        {
                            NPC.NewNPC((int)npc.Center.X + 40, (int)npc.Center.Y + 40, mod.NPCType<Truffling>());
                        }
                        if (i == 3)
                        {
                            NPC.NewNPC((int)npc.Center.X - 40, (int)npc.Center.Y - 40, mod.NPCType<Truffling>());
                        }
                        else
                        {
                            NPC.NewNPC((int)npc.Center.X - 40, (int)npc.Center.Y + 40, mod.NPCType<Truffling>());
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {

                    if (i == 1)
                    {
                        NPC.NewNPC((int)npc.Center.X + 10, (int)npc.Center.Y - 10, mod.NPCType<TruffleProbe>());
                    }
                    if (i == 2)
                    {
                        NPC.NewNPC((int)npc.Center.X + 10, (int)npc.Center.Y + 10, mod.NPCType<TruffleProbe>());
                    }
                    if (i == 3)
                    {
                        NPC.NewNPC((int)npc.Center.X - 10, (int)npc.Center.Y - 10, mod.NPCType<TruffleProbe>());
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.Center.X - 10, (int)npc.Center.Y + 10, mod.NPCType<TruffleProbe>());
                    }
                }
                npc.netUpdate = true;
            }
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 14f;
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
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
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            if (npc.direction == -1)
                npc.velocity *= velMultiplier;
            else
                npc.velocity *= -velMultiplier;
        }


        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/TechnoTruffle_Glow1");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/TechnoTruffle_Glow2");
            Color color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));

            if (internalAI[1] == AISTATE_ROCKET)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 5, false, 0f, 0f, Color.LightCyan);
            }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, npc, Color.White);
            return false;
        }
    }
}


