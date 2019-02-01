using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Retriever
{
    [AutoloadBossHead]
    public class Retriever : ModNPC
    {
        private Player player;
        private float speed;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Retriever");
            Main.npcFrameCount[npc.type] = 14;    //boss frame/animation 

        }
        public override void SetDefaults()
        {
            npc.aiStyle = 5;  //5 is the flying AI
            npc.lifeMax = 25000;   //boss life
            npc.damage = 80;  //boss damage
            npc.defense = 30;    //boss defense
            npc.knockBackResist = 0f;
            npc.width = 92;
            npc.height = 54;
            npc.friendly = false;
            npc.value = Item.buyPrice(0, 10, 50, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
            npc.buffImmune[BuffID.Ichor] = true;
            npc.netAlways = true;
            bossBag = mod.ItemType("RetrieverBag");
        }

        public static Texture2D glowTex = null;
        public static Texture2D glowTex1 = null;
        public Color color;

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
        private int LaserTimer = 1000;

        public Projectile laser;

        public override void AI()
        {
            npc.TargetClosest();
            Player targetPlayer = Main.player[npc.target];

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.velocity.Y -= 5;
            }

            LaserTimer--;

            if (LaserTimer <= 300)
            {
                moveSpeed = 11f;
                Vector2 point = targetPlayer.Center + offsetBasePoint + new Vector2(0f, -250f);
                MoveToPoint(point);
                BaseAI.LookAt(targetPlayer.Center, npc, 0, 0f, 0.1f, false);
                npc.frame.Y = (62 * 4);
                if (LaserTimer >= 293)
                {
                    npc.frame.Y = (62 * 5);

                    return;
                }
                else if (LaserTimer >= 286)
                {
                    npc.frame.Y = (62 * 6);

                    return;
                }
                else if (LaserTimer >= 279)
                {
                    npc.frame.Y = (62 * 7);
                    return;
                }
                else if (LaserTimer >= 272)
                {
                    npc.frame.Y = (62 * 8);
                    return;
                }
                else if (LaserTimer >= 265)
                {
                    npc.frame.Y = (62 * 9);
                    return;
                }
                else if (LaserTimer >= 258)
                {
                    npc.frame.Y = (62 * 10);
                    return;
                }
                else if (LaserTimer >= 251)
                {
                    npc.frame.Y = (62 * 11);
                    return;
                }
                else if (LaserTimer >= 244 && LaserTimer <= 60)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter >= 7)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y += 62;
                    }
                    if (npc.frame.Y > (62 * 13))
                    {
                        npc.frame.Y = 62 * 11;
                    }
                    npc.defense = 999;

                    int num429 = 1;
                    if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
                    {
                        num429 = -1;
                    }
                    Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
                    float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                    float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
                    float num433 = 6f;
                    PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
                    PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
                    PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
                    PlayerPos = num433 / PlayerPos;
                    PlayerPosX *= PlayerPos;
                    PlayerPosY *= PlayerPos;
                    PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
                    PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
                    PlayerPosY += npc.velocity.Y * 0.5f;
                    PlayerPosX += npc.velocity.X * 0.5f;
                    PlayerDistance.X -= PlayerPosX * 1f;
                    PlayerDistance.Y -= PlayerPosY * 1f;
                    Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX, PlayerPosY, mod.ProjectileType("RetrieverShot"), (int)(npc.damage * 1.4f), 0f, Main.myPlayer);
                    return;
                }
                else if (LaserTimer >= 59)
                {
                    npc.frame.Y = (38 * 10);
                    return;
                }
                else if (LaserTimer == 0)
                {
                    LaserTimer = 1000;
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
                    npc.frame.Y += 62;
                    if (npc.frame.Y > (62 * 3))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                }
            }

            bool forceChange = false;
            if (Main.netMode != 1 && npc.ai[0] != 2 && npc.ai[0] != 3)
            {
                int stopValue = 175;
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
                moveSpeed = 15;
                Vector2 targetCenter = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 point = targetCenter - offsetBasePoint + new Vector2(0f, 250f);
                MoveToPoint(point);
                if (Main.netMode != 1 && Vector2.Distance(npc.Center, point) < 10f)
                {
                    bool doubleDive = (npc.life < npc.lifeMax / 2);
                    if (doubleDive)
                    {
                        npc.ai[0] = 3;
                        npc.ai[1] = targetPlayer.Center.X;
                        npc.ai[2] = targetPlayer.Center.Y;
                    }
                    else
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                    }
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                }
                BaseAI.Look(npc, 0, 0f, 0.1f, false);
            }
            else
            if (npc.ai[0] == 3) //dive up
            {
                moveSpeed = 14;
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