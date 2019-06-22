using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Rajah
{
    [AutoloadBossHead]
    public class Rajah : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 130;
            npc.height = 214;
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

        public bool SetLife = false;
        public bool isSupreme = false;
        public float[] internalAI = new float[5];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(SetLife);
                writer.Write(isSupreme);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat(); //SpaceOctopus AI stuff
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat(); //Minion/Rocket Timer
                internalAI[3] = reader.ReadFloat(); //Ground Minion Alternation
                internalAI[4] = reader.ReadFloat(); //Is Flying
                SetLife = reader.ReadBool(); //Set Life
                isSupreme = reader.ReadBool(); //Is supreme
            }
        }

        private Texture2D RajahTex;
        private Texture2D Glow;
        private Texture2D ArmTex;
        public int WeaponFrame = 0;

        /*
         * npc.ai[0] = Jump Timer
         * npc.ai[1] = Jumping
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
        Projectile CarrotFarmer;

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
            if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more damage is done
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
            if (npc.ai[3] == .1f)
            {
                npc.ai[3] = 0f;
                isSupreme = true;
            }
            WeaponPos = new Vector2(npc.Center.X + (npc.direction == 1 ? -78 : 78), npc.Center.Y - 9);
            StaffPos = new Vector2(npc.Center.X + (npc.direction == 1 ? 78 : -78), npc.Center.Y - 9);
            if (Roaring) roarTimer--;

            if (Main.netMode != 1 && !SetLife)
            {
                npc.life = npc.lifeMax;
                SetLife = true;
                npc.netUpdate = true;
            }

            Player player = Main.player[npc.target];
            if (npc.target >= 0 && Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    Main.NewText("Justice has been served...", 107, 137, 179);
                    Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
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
                    Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
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
                if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.noTileCollide = true;
                }
                else
                {
                    npc.noTileCollide = false;
                }
                npc.noGravity = true;
                FlyAI();
            }
            else
            {
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
                internalAI[2]++;
            }
            if (npc.ai[2] >= 500)
            {
                internalAI[2] = 0;
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
                internalAI[2] = 0;
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
                    if (internalAI[2] >= 80)
                    {
                        internalAI[2] = 0;
                        if (internalAI[4] == 0)
                        {
                            if (NPC.CountNPCS(mod.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon1>()) < 5)
                            {
                                Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                            }
                            npc.netUpdate = true;
                        }
                        else
                        {
                            if (internalAI[3] > 2)
                            {
                                internalAI[3] = 0;
                            }
                            if (internalAI[3] == 0)
                            {
                                if (NPC.CountNPCS(mod.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon1>()) < 5)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            else if (internalAI[3] == 1)
                            {
                                if (NPC.CountNPCS(mod.NPCType<BunnyBrawler>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon2>()) < 5)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon2>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            else if (internalAI[3] == 2)
                            {
                                if (NPC.CountNPCS(mod.NPCType<BunnyBattler>()) + AAGlobalProjectile.CountProjectiles(mod.ProjectileType<BunnySummon3>()) < 8)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));

                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));

                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                    
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, mod.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            internalAI[3] += 1;
                            npc.netUpdate = true;
                        }
                    }
                }
                else if (npc.ai[3] == 1) //Bunzooka
                {
                    if (internalAI[2] > 40)
                    {
                        internalAI[2] = 0;
                        Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, mod.ProjectileType<RajahRocket>(), npc.damage / 3, 5, Main.myPlayer);
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
                    if (internalAI[2] > 40)
                    {
                        internalAI[2] = 0;
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
                    if (internalAI[2] == 60)
                    {
                        Vector2 dir = Vector2.Normalize(player.position - WeaponPos);
                        dir *= ProjSpeed(); ;
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y + 4, mod.ProjectileType<BaneR>(), npc.damage / 3, 5, Main.myPlayer);
                    }
                    if (internalAI[2] > 90)
                    {
                        internalAI[2] = 0;
                    }
                    npc.netUpdate = true;
                }
                else if (npc.ai[3] == 4) //Carrot Farmer
                {
                    if (!AAGlobalProjectile.AnyProjectiless(mod.ProjectileType<CarrotFarmerR>()))
                    {
                        CarrotFarmer = Main.projectile[Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType<CarrotFarmerR>(), npc.damage / 3, 3f, Main.myPlayer, npc.whoAmI)];
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
                if (npc.life < (npc.lifeMax * .1f))
                {
                    npc.damage = npc.defDamage * 2;
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
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[(int)Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
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
                if (internalAI[2] > 60)
                {
                    return "BlankTex";
                }
                return "NPCs/Bosses/Rajah/RajahArmsS";
            }
        }

        public void JumpAI()
        {
            internalAI[4] = 1;
            if (npc.ai[0] == 0f)
            {
                npc.noTileCollide = false;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] > 0f)
                    {
                        if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more frequent the jumps
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .7f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .65f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .4f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .25f))
                        {
                            npc.ai[1] += 2;
                        }
                        if (npc.life < (npc.lifeMax * .1f))
                        {
                            npc.ai[1] += 2;
                        }
                    }
                    if (npc.ai[1] >= 250f)
                    {
                        npc.ai[1] = -20f;
                    }
                    else if (npc.ai[1] == -1f)
                    {
                        npc.TargetClosest(true);
                        npc.velocity.X = 6 * npc.direction;
                        npc.velocity.Y = -12.1f;
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
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
                            int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + (float)npc.height), npc.width + 20, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
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
            float speed = 7f;
            if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more damage is done
            {
                speed = 10f;
            }
            if (npc.life < (npc.lifeMax * .7f))
            {
                speed = 10.5f;
            }
            if (npc.life < (npc.lifeMax * .65f))
            {
                speed = 11f;
            }
            if (npc.life < (npc.lifeMax * .4f))
            {
                speed = 11.5f;
            }
            if (npc.life < (npc.lifeMax * .25f))
            {
                speed = 12f;
            }
            if (npc.life < (npc.lifeMax * .1f))
            {
                speed = 12.5f;
            }
            BaseAI.AISpaceOctopus(npc, ref internalAI, .25f, speed, 300, 0, null);
            internalAI[4] = 0;
        }

        

        public override void FindFrame(int frameHeight)
        {
            if (internalAI[4] == 0)
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
                    if (npc.ai[1] < -17f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                    else if (npc.ai[1] < -14f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight;
                    }
                    else if (npc.ai[1] < -11f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 2;
                    }
                    else if (npc.ai[1] < -8f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 3;
                    }
                    else if (npc.ai[1] < -5f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 4;
                    }
                    else if (npc.ai[1] < -2f)
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
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.3f);  //boss damage increase in expermode
        }

        public void RajahTexture()
        {
            string IsRoaring = Roaring ? "Roar" : "";
            if (internalAI[4] == 0)
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

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[3] != 0) //No Weapon
            {
                ArmTex = mod.GetTexture(WeaponTexture());
                Rectangle WeaponRectangle = new Rectangle(0, WeaponFrame, 300, 220);
                BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, WeaponRectangle, drawColor, true);
            }
            RajahTexture();
            BaseDrawing.DrawTexture(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);

            if (isSupreme)
            {
                BaseDrawing.DrawTexture(spriteBatch, Glow, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Color.White, true);
                BaseDrawing.DrawTexture(spriteBatch, Glow, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Main.DiscoColor, true);
                BaseDrawing.DrawAfterimage(spriteBatch, Glow, 0, npc, 0.3f, 1f, 8, false, 0f, 0f, Main.DiscoColor);
            }
            return false;
        }
    }
}