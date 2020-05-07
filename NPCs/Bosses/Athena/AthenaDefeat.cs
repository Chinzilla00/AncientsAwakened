
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Athena
{
    public class AthenaDefeat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.width = 132;
            npc.height = 104;
            npc.npcSlots = 1000;
            npc.aiStyle = -1;
            npc.defense = 1;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.boss = true;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.damage = 0;
            npc.value = 0;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
        }

        public override void AI()
        {
            Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;
            Vector2 Acropolis = new Vector2(Origin.X + (80 * 16), Origin.Y + (79 * 16));
            npc.TargetClosest();
            if (Main.netMode != 1)
            {
                if (Vector2.Distance(npc.Center, Acropolis) < 5 && Main.netMode != 1)
                {
                    npc.velocity.X *= 0;
                    npc.ai[1] = 1;
                    npc.noTileCollide = false;
                    npc.noGravity = false;
                    npc.netUpdate = true;
                }
                if (npc.ai[1] == 0)
                {
                    MoveToPoint(Acropolis);
                }
                else
                {
                    npc.ai[0]++;
                    if (Main.netMode != 1)
                    {
                        if (npc.ai[2] == 0)
                        {
                            if (npc.ai[0] == 120)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat1"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 240)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat2"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 360)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("...", Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 480)
                            {
                                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AthenaA");
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat3"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 600)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat4"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 720)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat5"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 840)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat6"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 960)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat7"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] == 1080)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat8"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else
                            if (npc.ai[0] >= 1200)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("AthenaDefeat9"), Color.CornflowerBlue);
                                AAModGlobalNPC.SpawnBoss(Main.player[npc.target], ModContent.NPCType<Olympian.AthenaA>(), false, npc.Center);
                                AAMod.ShowTitle(npc, 5);

                                int b = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ShockwaveBoom"), 0, 1, Main.myPlayer);
                                Main.projectile[b].Center = npc.Center;

                                npc.active = false;
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            if (npc.ai[0] == 120)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("...", Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 240)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat1"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 360)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat2"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 480)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat("...", Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 600)
                            {
                                string s = "";
                                if (Main.ActivePlayersCount > 1)
                                {
                                    s = Lang.BossChat("Athena2Defeat4");
                                }
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat3") + s + "...", Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 720)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat5"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 840)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat6"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 960)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat7"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] == 1080)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat8"), Color.CornflowerBlue);
                                npc.netUpdate = true;
                            }
                            else if (npc.ai[0] >= 1200)
                            {
                                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Athena2Defeat9"), Color.CornflowerBlue);
                                AAModGlobalNPC.SpawnBoss(Main.player[npc.target], ModContent.NPCType<AthenaFlee>(), false, npc.Center);
                                npc.active = false;
                                npc.netUpdate = true;
                            }
                        }
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.ai[1] == 0)
            {
                if (npc.frameCounter >= 6)
                {
                    npc.frame.Y += frameHeight;
                    npc.frameCounter = 0;
                    if (npc.frame.Y >= frameHeight * 7)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (npc.ai[2] == 0)
                {
                    if (npc.ai[0] < 480)
                    {
                        if (npc.frameCounter >= 15)
                        {
                            npc.frame.Y += frameHeight;
                            npc.frameCounter = 0;
                            if (npc.frame.Y >= frameHeight * 10 || npc.frame.Y < frameHeight * 7)
                            {
                                npc.frame.Y = frameHeight * 7;
                            }
                        }
                    }
                    else if (npc.ai[0] >= 480 && npc.ai[0] < 720)
                    {
                        npc.frame.Y = frameHeight * 10;
                    }
                    else if (npc.ai[0] >= 720)
                    {
                        if (npc.frameCounter >= 15)
                        {
                            npc.frame.Y += frameHeight;
                            npc.frameCounter = 0;
                            if (npc.frame.Y < frameHeight * 11 || npc.frame.Y >= frameHeight * 15)
                            {
                                npc.frame.Y = frameHeight * 11;
                            }
                        }
                    }
                }
                else
                {
                    if (npc.ai[0] < 270)
                    {
                        if (npc.frameCounter >= 15)
                        {
                            npc.frame.Y += frameHeight;
                            npc.frameCounter = 0;
                            if (npc.frame.Y >= frameHeight * 10 || npc.frame.Y < frameHeight * 7)
                            {
                                npc.frame.Y = frameHeight * 7;
                            }
                        }
                    }
                    else if (npc.ai[0] >= 270 && npc.ai[0] < 450)
                    {
                        npc.frame.Y = frameHeight * 10;
                    }
                    else if (npc.ai[0] >= 450)
                    {
                        if (npc.frameCounter >= 15)
                        {
                            npc.frame.Y += frameHeight;
                            npc.frameCounter = 0;
                            if (npc.frame.Y < frameHeight * 11 || npc.frame.Y >= frameHeight * 15)
                            {
                                npc.frame.Y = frameHeight * 11;
                            }
                        }
                    }
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 14f;
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
    }
}