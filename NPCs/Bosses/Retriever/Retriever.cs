using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Retriever
{
    [AutoloadBossHead]
    public class Retriever : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Retriever");
            Main.npcFrameCount[npc.type] = 14;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 30000;
            npc.damage = 80;
            npc.defense = 30;
            npc.buffImmune[BuffID.Ichor] = true;
            npc.value = Item.buyPrice(0, 10, 50, 0);
            npc.knockBackResist = 0f;
            npc.width = 92;
            npc.height = 54;
            npc.friendly = false;
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.netAlways = true;
            bossBag = mod.ItemType("RetrieverBag");

            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
        }

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();
            }
        }

        public static Texture2D glowTex = null;
        public static Texture2D glowTex1 = null;
        public Color color;

        int LaserTime = 0;
        Projectile laser;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/Retriever_Glow1");
                glowTex1 = mod.GetTexture("Glowmasks/Retriever_Glow2");
            }
            color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, npc, Color.White);
            return false;
        }

        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RetrieverGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RetrieverGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RetrieverGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RetrieverGore4"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RetrieverGore5"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RetrieverGore6"), 1f);

            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RetrieverTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofSight, Main.rand.Next(20, 40));
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofSight, Main.rand.Next(25, 40));
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RetrieverMask"));
                }
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteBar"), Main.rand.Next(30, 64));
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
            AAWorld.downedRetriever = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.8f);  //boss damage increase in expermode
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public Vector2 offsetBasePoint = new Vector2(240f, 0f);

        public float moveSpeed = 10f;


        public float[] shootAI = new float[4];

        public override void AI()
        {
            npc.TargetClosest();
            Player targetPlayer = Main.player[npc.target];

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.velocity.Y -= 5;
            }
            
            customAI[0]--;

            if (Main.dayTime)
            {
                npc.velocity.Y -= 4;
                if (npc.position.Y + npc.velocity.Y <= 0f && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
            }

            if (customAI[0] <= 300)
            {
                moveSpeed = 11f;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);
                if (customAI[0] >= 293)
                {
                    npc.frame.Y = (100 * 5);

                    return;
                }
                else if (customAI[0] >= 286)
                {
                    npc.frame.Y = (100 * 6);

                    return;
                }
                else if (customAI[0] >= 279)
                {
                    npc.frame.Y = (100 * 7);
                    return;
                }
                else if (customAI[0] >= 272)
                {
                    npc.frame.Y = (100 * 8);
                    return;
                }
                else if (customAI[0] >= 265)
                {
                    npc.frame.Y = (100 * 9);
                    return;
                }
                else if (customAI[0] >= 258)
                {
                    npc.frame.Y = (100 * 10);
                    return;
                }
                else if (customAI[0] >= 251)
                {
                    npc.frame.Y = (100 * 11);
                    return;
                }
                else if (customAI[0] >= 60)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter >= 7)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 100;
                    }
                    if (npc.frame.Y > (100 * 13))
                    {
                        npc.frame.Y = 100 * 11;
                    }
                    npc.defense = 999;

                    Player player = Main.player[npc.target];

                    BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, mod.ProjectileType<RetrieverShot>(), ref customAI[3], 5, npc.damage / 2, 12);
                    return;
                }
                else if (customAI[0] >= 59)
                {
                    npc.frame.Y = (100 * 10);
                    return;
                }
                else if (customAI[0] == 1)
                {
                    npc.frame.Y = (100 * 7);
                    return;
                }
                else if (customAI[0] <= 0)
                {
                    customAI[0] = 1200;
                    return;
                }
            }
            else
            {

                npc.frameCounter++;
                npc.defense = 30;
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 100;
                    if (npc.frame.Y > (100 * 3))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
            }

            bool forceChange = false;

            bool Dive1 = npc.life < npc.lifeMax * .8f;
            bool Dive2 = npc.life < npc.lifeMax * .5f;
            bool Dive3 = npc.life < npc.lifeMax * .2f;
            int DiveSpeed = Dive1 ? 14 : Dive2 ? 17 : 20;
            if (Main.netMode != 1 && npc.ai[0] != 2 && npc.ai[0] != 3)
            {
                int stopValue = 60;
                npc.ai[3]++;
                if (npc.ai[3] > stopValue) npc.ai[3] = stopValue;
                forceChange = npc.ai[3] >= stopValue;
            }
            if (npc.ai[0] == 1) //move to starting charge position
            {
                moveSpeed = 11f;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 10f || forceChange))
                {
                    npc.ai[0] = 2;
                    npc.ai[1] = targetPlayer.Center.X;
                    npc.ai[2] = targetPlayer.Center.Y;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 2) //dive down
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    bool doubleDive = (npc.life < npc.lifeMax / 2);

                    npc.ai[0] = Dive1 ? 3 : 0;
                    npc.ai[1] = Dive1 ? targetPlayer.Center.X : 0;
                    npc.ai[2] = Dive1 ? targetPlayer.Center.Y : 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 3) //dive up
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    npc.ai[0] = Dive2 ? 4 : 0;
                    npc.ai[1] = Dive2 ? targetPlayer.Center.X : 0;
                    npc.ai[2] = Dive2 ? targetPlayer.Center.Y : 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 4) //dive down
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    npc.ai[0] = Dive3 ? 5 : 0;
                    npc.ai[1] = Dive3 ? targetPlayer.Center.X : 0;
                    npc.ai[2] = Dive3 ? targetPlayer.Center.Y : 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 5) //dive up
            {
                moveSpeed = DiveSpeed;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else //standard movement
            {
                moveSpeed = 8;
                Vector2 point = targetPlayer.Center + offsetBasePoint;
                MoveToPoint(point);
                if (Main.netMode != 1 && (Vector2.Distance(npc.Center, point) < 50f || forceChange))
                {
                    npc.ai[1]++;
                    if (npc.ai[1] > 150)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            offsetBasePoint.X = 240;
                        }
                        else
                        {
                            offsetBasePoint.X = -240;
                        }

                        npc.ai[0] = 1;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                }
                BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);
            }
        }
        
        
            

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
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
            npc.velocity *= velMultiplier;
        }
    }
}
