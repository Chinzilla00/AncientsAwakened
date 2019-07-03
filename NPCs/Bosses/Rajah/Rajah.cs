
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System.IO;
using AAMod.Items.Boss.Rajah;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.Rajah
{
    [AutoloadBossHead]
    public class Rajah : ModNPC
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 130;
            npc.height = 220;
            npc.aiStyle = -1;
            npc.damage = 130;
            npc.defense = 90;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 1000f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/Rajah");
            npc.value = 10000f;
            npc.boss = true;
            npc.netAlways = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RajahTheme");
            bossBag = mod.ItemType("RajahBag");
        }

        public bool isSupreme = false;
        public float[] internalAI = new float[6];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(isSupreme);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat(); //SpaceOctopus AI stuff
                internalAI[1] = reader.ReadFloat(); //Is Flying
                internalAI[2] = reader.ReadFloat(); //Is Jumping
                internalAI[3] = reader.ReadFloat(); //Minion/Rocket Timer
                isSupreme = reader.ReadBool();
            }
        }

        private Texture2D RajahTex;
        private Texture2D Glow;
        private Texture2D ArmTex;
        public int WeaponFrame = 0;

        /*
         * npc.ai[0] = Jump Timer
         * npc.ai[1] = Ground Minion Alternation
         * npc.ai[2] = Weapon Change timer
         * npc.ai[3] = Weapon type
         */

        public int roarTimer = 0;
        public int roarTimerMax = 240;
        public bool Roaring
        {
            get
            {
                return roarTimer > 0;
            }
        }

        public void Roar(int timer)
        {
            roarTimer = timer;
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
        }

        public Vector2 WeaponPos;
        public Vector2 StaffPos;

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true && NPC.killCount[NPCID.Bunny] >= 1000)
            {
                if (damage > npc.lifeMax / 8)
                {
                    Main.NewText("JUSTICE CANNOT BE CHEATED", 107, 137, 179);
                    damage = 0;
                }

                npc.damage = 9999;
                npc.defense = 99999;

                return false;
            }

            return true;
        }

        public float ProjSpeed()
        {
            if (isSupreme)
            {
                return 16f;
            }
            else if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more damage is done
            {
                return 10f;
            }
            if (npc.life < (npc.lifeMax * .7f))
            {
                return 11f;
            }
            if (npc.life < (npc.lifeMax * .65f))
            {
                return 12f;
            }
            if (npc.life < (npc.lifeMax * .4f))
            {
                return 13f;
            }
            if (npc.life < (npc.lifeMax * .25f))
            {
                return 14f;
            }
            if (npc.life < (npc.lifeMax * .1f))
            {
                return 16f;
            }
            return 9f;
        }


        public override void AI()
        {
            AAModGlobalNPC.Rajah = npc.whoAmI;
            WeaponPos = new Vector2(npc.Center.X + (npc.direction == 1 ? -78 : 78), npc.Center.Y - 9);
            StaffPos = new Vector2(npc.Center.X + (npc.direction == 1 ? 78 : -78), npc.Center.Y - 9);
            if (Roaring) roarTimer--;

            if (Main.netMode != 1 && npc.type == mod.NPCType<SupremeRajah>())
            {
                isSupreme = true;
                npc.netUpdate = true;
            }

            Player player = Main.player[npc.target];
            if (npc.target >= 0 && Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    Main.NewText("Justice has been served...", 107, 137, 179);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
                    }
                    npc.active = false;
                    npc.noTileCollide = true;
                    npc.netUpdate = true;
                    return;
                }
            }

            if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 3000)
            {
                npc.TargetClosest(true);
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 3000)
                {
                    Main.NewText("Coward.", 107, 137, 179);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
                    }
                    npc.active = false;
                    npc.noTileCollide = true;
                    npc.netUpdate = true;
                    return;
                }
            }


            if (player.Center.X < npc.Center.X)
            {
                npc.direction = 1;
            }
            else
            {
                npc.direction = -1;
            }

            if (player.Center.Y < npc.position.Y - 30f || TileBelowEmpty() || !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                npc.noGravity = true;
                FlyAI();
            }
            else
            {
                npc.noTileCollide = false;
                npc.noGravity = false;
                JumpAI();
            }

            if (npc.target <= 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }

            if (Main.netMode != 1)
            {
                npc.ai[2]++;
                internalAI[3]++;
            }
            if (npc.ai[2] >= 500)
            {
                internalAI[3] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                npc.netUpdate = true;
            }
            else if (npc.ai[3] == 0 && npc.ai[2] >= 240)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Roar(roarTimerMax);
                }
                internalAI[3] = 0;
                npc.ai[2] = 0;
                if (AAMod.thoriumLoaded)
                {
                    npc.ai[3] = Main.rand.Next(5);
                }
                else
                {
                    npc.ai[3] = Main.rand.Next(4);
                }
                npc.netUpdate = true;
            }

            if (Main.netMode != 1)
            {
                if (npc.ai[3] == 0) //Minion Phase
                {
                    if (internalAI[3] >= 80)
                    {
                        internalAI[3] = 0;
                        if (internalAI[1] == 0)
                        {
                            if (NPC.CountNPCS(mod.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon1>()) < 5)
                            {
                                Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                            }
                            npc.netUpdate = true;
                        }
                        else
                        {
                            if (npc.ai[1] > 2)
                            {
                                npc.ai[1] = 0;
                            }
                            if (npc.ai[1] == 0)
                            {
                                if (NPC.CountNPCS(mod.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon1>()) < 5)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            else if (npc.ai[1] == 1)
                            {
                                if (NPC.CountNPCS(mod.NPCType<BunnyBrawler>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon2>()) < 5)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon2>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            else if (npc.ai[1] == 2)
                            {
                                if (NPC.CountNPCS(mod.NPCType<BunnyBattler>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon3>()) < 8)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));

                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));

                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                    
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            npc.ai[1] += 1;
                            npc.netUpdate = true;
                        }
                    }
                }
                else if (npc.ai[3] == 1) //Bunzooka
                {
                    if (internalAI[3] > 40)
                    {
                        internalAI[3] = 0;
                        Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, mod.ProjectileType<RajahRocket>(), npc.damage / 5, 5, Main.myPlayer);
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[3] == 2) //Royal Scepter
                {
                    float spread = 45f * 0.0174f;
                    Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                    dir *= ProjSpeed();
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    if (internalAI[3] > 40)
                    {
                        internalAI[3] = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            double offsetAngle = startAngle + (deltaAngle * i);
                            Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("CarrotHostile"), npc.damage / 3, 5, Main.myPlayer);
                        }
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[3] == 3) //Javelin
                {
                    if (internalAI[3] == 60)
                    {
                        Vector2 dir = Vector2.Normalize(player.position - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, mod.ProjectileType<BaneR>(), npc.damage / 5, 5, Main.myPlayer);
                    }
                    if (internalAI[3] > 90)
                    {
                        internalAI[3] = 0;
                    }
                    npc.netUpdate = true;
                }
                else if (npc.ai[3] == 4) //Carrot Farmer
                {
                    if (!AAGlobalProjectile.AnyProjectiless(mod.ProjectileType<CarrotFarmerR>()))
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType<CarrotFarmerR>(), npc.damage / 5, 3f, Main.myPlayer, npc.whoAmI);
                        npc.netUpdate = true;
                    }
                }
            }
            
            if (Main.expertMode)
            {
                if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more damage is done
                {
                    npc.damage = (int)(npc.defDamage * 1.1f);
                }
                if (npc.life < (npc.lifeMax * .7f))
                {
                    npc.damage = (int)(npc.defDamage * 1.3f);
                }
                if (npc.life < (npc.lifeMax * .65f))
                {
                    npc.damage = (int)(npc.defDamage * 1.5f);
                }
                if (npc.life < (npc.lifeMax * .4f))
                {
                    npc.damage = (int)(npc.defDamage * 1.7f);
                }
                if (npc.life < (npc.lifeMax * .25f))
                {
                    npc.damage = (int)(npc.defDamage * 1.9f);
                }
                if (npc.life < (npc.lifeMax / 7))
                {
                    npc.damage = (int)(npc.defDamage * 2.2f);
                    npc.defense = (int)(npc.defense * 1.5f);
                }
            }
            else
            {
                if (npc.life == npc.lifeMax / 7)
                {
                    npc.damage = (int)(npc.defDamage * 1.5f);
                    npc.defense = (int)(npc.defense * 1.5f);
                }
            }

            npc.rotation = 0;
        }

        public bool TileBelowEmpty()
        {
            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + npc.height) / 16f);

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

        public string WeaponTexture()
        {
            if (npc.ai[3] == 0 || npc.ai[3] == 4) //No Weapon or Carrot Farmer
            {
                return "BlankTex";
            }
            else if (npc.ai[3] == 1) //Bunzooka
            {
                return "NPCs/Bosses/Rajah/RajahArmsB";
            }
            else if (npc.ai[3] == 2) //Scepter
            {
                return "NPCs/Bosses/Rajah/RajahArmsR";
            }
            else //Javelin
            {
                if (internalAI[3] > 60)
                {
                    return "BlankTex";
                }
                return "NPCs/Bosses/Rajah/RajahArmsS";
            }
        }

        public void JumpAI()
        {
            internalAI[1] = 1;
            if (npc.ai[0] == 0f)
            {
                npc.noTileCollide = false;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    internalAI[2] += 1f;
                    if (internalAI[2] > 0f)
                    {
                        if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more frequent the jumps
                        {
                            internalAI[2] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .7f))
                        {
                            internalAI[2] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .65f))
                        {
                            internalAI[2] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .4f))
                        {
                            internalAI[2] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .25f))
                        {
                            internalAI[2] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .1f))
                        {
                            internalAI[2] += 2;
                        }
                    }
                    if (internalAI[2] >= 250f)
                    {
                        internalAI[2] = -20f;
                    }
                    else if (internalAI[2] == -1f)
                    {
                        npc.TargetClosest(true);
                        npc.velocity.X = 6 * npc.direction;
                        npc.velocity.Y = -12.1f;
                        npc.ai[0] = 1f;
                        internalAI[2] = 0f;
                        npc.netUpdate = true;
                    }
                }
            }
            else if (npc.ai[0] == 1f)
            {
                if (npc.velocity.Y == 0f)
                {
                    Main.PlaySound(SoundID.Item14, npc.position);
                    npc.ai[0] = 0f;
                    for (int num622 = (int)npc.position.X - 20; num622 < (int)npc.position.X + npc.width + 40; num622 += 20)
                    {
                        for (int num623 = 0; num623 < 4; num623++)
                        {
                            int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + (float)npc.height), npc.width + 20, 4, 31, 0f, 0f, 100);
                            Main.dust[num624].velocity *= 0.2f;
                        }
                        int num625 = Gore.NewGore(new Vector2((float)(num622 - 20), npc.position.Y + (float)npc.height - 8f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num625].velocity *= 0.4f;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                    if (npc.position.X < Main.player[npc.target].position.X && npc.position.X + (float)npc.width > Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
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

        public void FlyAI()
        {
            if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
            {
                npc.noTileCollide = true;
            }
            else
            {
                npc.noTileCollide = false;
            }
            float speed = 10f;
            if (isSupreme)
            {
                speed = 16f;
            }
            else if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more damage is done
            {
                speed = 11f;
            }
            else if (npc.life < (npc.lifeMax * .7f))
            {
                speed = 12f;
            }
            else if (npc.life < (npc.lifeMax * .65f))
            {
                speed = 13f;
            }
            else if (npc.life < (npc.lifeMax * .4f))
            {
                speed = 14f;
            }
            else if (npc.life < (npc.lifeMax * .25f))
            {
                speed = 15f;
            }
            else if (npc.life < (npc.lifeMax * .1f))
            {
                speed = 16f;
            }
            BaseAI.AISpaceOctopus(npc, ref internalAI, .25f, speed, 300, 0, null);
            internalAI[1] = 0;
        }

        public override void FindFrame(int frameHeight)
        {
            if (internalAI[1] == 0)
            {
                WeaponFrame = frameHeight * 5;
                if (npc.frameCounter++ > 3)
                {
                    npc.frame.Y += frameHeight;
                    npc.frameCounter = 0;
                    if (npc.frame.Y > frameHeight * 7)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
            else
            {
                WeaponFrame = npc.frame.Y;
                if (npc.ai[0] == 0f)
                {
                    if (internalAI[2] < -17f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                    else if (internalAI[2] < -14f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight;
                    }
                    else if (internalAI[2] < -11f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 2;
                    }
                    else if (internalAI[2] < -8f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 3;
                    }
                    else if (internalAI[2] < -5f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 4;
                    }
                    else if (internalAI[2] < -2f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 5;
                    }
                    else
                    {
                        if (npc.frameCounter++ > 7.5f)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y += frameHeight;
                            if (npc.frame.Y > frameHeight * 2)
                            {
                                npc.frame.Y = 0;
                            }
                        }
                    }
                }
                else if (npc.ai[0] == 1f)
                {
                    if (npc.velocity.Y != 0f)
                    {
                        npc.frame.Y = frameHeight * 5;
                    }
                    else
                    {
                        npc.frameCounter++;
                        if  (npc.frame.Y > 3)
                        {
                            if (npc.frameCounter > 0)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y = frameHeight * 6;
                            }
                            else if (npc.frameCounter > 4)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y = frameHeight * 7;
                            }
                            else if (npc.frameCounter > 8)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y = 0;
                            }
                        }
                        else
                        {
                            if (npc.frameCounter > 7.5f)
                            {
                        npc.frameCounter = 0;
                                npc.frame.Y += frameHeight;
                                if (npc.frame.Y > frameHeight * 2)
                                {
                                    npc.frame.Y = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType<RajahPelt>(), Main.rand.Next(10, 26));
                string[] lootTableA = { "BaneOfTheBunny", "Bunnyzooka", "RoyalScepter", "Punisher", "RabbitcopterEars"};
                int lootA = Main.rand.Next(lootTableA.Length);
                npc.DropLoot(mod.ItemType(lootTableA[lootA]));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RajahTrophy"));
            }
            BaseUtility.Chat("You win this time, murderer...but I will avenge those you've mercilicely slain...", 107, 137, 179, true);
            Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
            npc.value = 0f;
            npc.boss = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
            AAWorld.downedRajah = true;
            if (npc.type == mod.NPCType<SupremeRajah>())
            {
                AAWorld.downedRajahsRevenge = true;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.3f);  //boss damage increase in expermode
        }

        public void RajahTexture()
        {
            string IsRoaring = Roaring ? "Roar" : "";
            if (internalAI[1] == 0)
            {
                RajahTex = mod.GetTexture("NPCs/Bosses/Rajah/Rajah" + IsRoaring + "_Fly");
                Glow = mod.GetTexture("Glowmasks/Rajah" + IsRoaring + "_Fly_Glow");
            }
            else
            {
                RajahTex = mod.GetTexture("NPCs/Bosses/Rajah/Rajah" + IsRoaring);
                Glow = mod.GetTexture("Glowmasks/Rajah" + IsRoaring + "_Glow");
            }
        }
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            bool RageMode = !isSupreme && npc.life < npc.lifeMax / 7;
            bool SupremeRageMode = isSupreme && npc.life < npc.lifeMax / 7;
            if (RageMode)
            {
                Color RageColor = BaseUtility.MultiLerpColor((Main.player[Main.myPlayer].miscCounter % 100) / 100f, Color.Firebrick, drawColor, Color.Firebrick);
                BaseDrawing.DrawAura(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, 0f, npc.direction, 8, npc.frame, 0f, -5f, RageColor);
            }
            else if (SupremeRageMode)
            {
                BaseDrawing.DrawAura(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, 0f, npc.direction, 8, npc.frame, 0f, -5f, Main.DiscoColor);
            }
            if (npc.ai[3] != 0) //No Weapon
            {
                ArmTex = mod.GetTexture(WeaponTexture());
                Rectangle WeaponRectangle = new Rectangle(0, WeaponFrame, 300, 220);
                BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, WeaponRectangle, drawColor, true);
            }
            RajahTexture();
            BaseDrawing.DrawTexture(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            if (isSupreme)
            {
                BaseDrawing.DrawTexture(spriteBatch, Glow, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Main.DiscoColor, true);
                BaseDrawing.DrawAura(spriteBatch, Glow, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, 0f, npc.direction, 8, npc.frame, 0f, -5f, Main.DiscoColor);
            }
            if (RageMode)
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
                BaseDrawing.DrawTexture(spriteBatch, Glow, shader, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Color.White, true);
            }
            return false;
        }

        public override string BossHeadTexture
        {
            get
            {
                return "AAMod/NPCs/Bosses/Rajah/Rajah_Head_Boss";
            }
        }
    }

    [AutoloadBossHead]
    public class Rajah2 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 160;
            npc.defense = 130;
            npc.lifeMax = 80000;
        }
    }

    [AutoloadBossHead]
    public class Rajah3 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 180;
            npc.defense = 150;
            npc.lifeMax = 100000;
            npc.life = 100000;
        }
    }

    [AutoloadBossHead]
    public class Rajah4 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 200;
            npc.defense = 180;
            npc.lifeMax = 200000;
            npc.life = 200000;
        }
    }

    [AutoloadBossHead]
    public class Rajah5 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 250;
            npc.defense = 210;
            npc.lifeMax = 300000;
            npc.life = 300000;
        }
    }

    [AutoloadBossHead]
    public class Rajah6 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 300;
            npc.defense = 230;
            npc.lifeMax = 500000;
            npc.life = 500000;
        }
    }

    [AutoloadBossHead]
    public class Rajah7 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 340;
            npc.defense = 250;
            npc.lifeMax = 700000;
            npc.life = 700000;
        }
    }

    [AutoloadBossHead]
    public class Rajah8 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 370;
            npc.defense = 270;
            npc.lifeMax = 900000;
            npc.life = 900000;
        }
    }

    [AutoloadBossHead]
    public class Rajah9 : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 400;
            npc.defense = 290;
            npc.lifeMax = 1000000;
            npc.life = 1000000;
        }
    }

    [AutoloadBossHead]
    public class SupremeRajah : Rajah
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Rajah/Rajah"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit; Champion of the Innocent");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 450;
            npc.defense = 350;
            npc.lifeMax = 4000000;
            npc.life = 4000000;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SupremeRajah");
        }
    }
}