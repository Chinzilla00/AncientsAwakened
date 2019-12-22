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
using AAMod.NPCs.Bosses.Rajah.Supreme;

namespace AAMod.NPCs.Bosses.Rajah
{
    [AutoloadBossHead]
    public class Rajah : ModNPC
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public int damage = 0;

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
            npc.value = Item.sellPrice(0, 1, 10, 0);
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
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
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
            if (Main.netMode == NetmodeID.MultiplayerClient)
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
        private Texture2D SupremeGlow;
        private Texture2D SupremeEyes;
        private Texture2D ArmTex;
        public int WeaponFrame = 0;
        public Vector2 MovePoint;
        public bool SelectPoint = false;

        /*
         * npc.ai[0] = Jump Timer
         * npc.ai[1] = Ground Minion Alternation
         * npc.ai[2] = Weapon Change timer
         * npc.ai[3] = Weapon type
         */

        public int roarTimer = 0;
        public int roarTimerMax = 240;
        public bool Roaring => roarTimer > 0;

        public void Roar(int timer)
        {
            roarTimer = timer;
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), npc.Center);
        }

        public Vector2 WeaponPos;
        public Vector2 StaffPos;

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (isSupreme)
            {
                damage *= .5f;
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

        private bool SayLine = false;

        public override void AI()
        {
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }
            AAModGlobalNPC.Rajah = npc.whoAmI;
            WeaponPos = new Vector2(npc.Center.X + (npc.direction == 1 ? -78 : 78), npc.Center.Y - 9);
            StaffPos = new Vector2(npc.Center.X + (npc.direction == 1 ? 78 : -78), npc.Center.Y - 9);
            if (Roaring) roarTimer--;
            
            if (Main.netMode != 1 && npc.type == ModContent.NPCType<SupremeRajah>() && isSupreme == false)
            {
                isSupreme = true;
                npc.netUpdate = true;
            }

            if (isSupreme && npc.life <= npc.lifeMax / 7 && !SayLine && Main.netMode != 1)
            {
                SayLine = true;
                string Name;

                int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                if (bunnyKills >= 100 && !AAWorld.downedRajahsRevenge)
                {
                    Name = "MUDERER";
                }
                else
                {
                    if (Main.netMode != 0)
                    {
                        Name = "Terrarians";
                    }
                    else if (!AAWorld.downedRajahsRevenge)
                    {
                        Name = "Terrarian";
                    }
                    else
                    {
                        Name = Main.LocalPlayer.name;
                    }
                }
                if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Rajah5") + Name.ToUpper() + Lang.BossChat("Rajah6"), 107, 137, 179);
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand");
            }

            Player player = Main.player[npc.target];
            if (npc.target >= 0 && Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    if (isSupreme)
                    {
                        if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Rajah7"), 107, 137, 179);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.position, npc.velocity, ModContent.ProjectileType<SupremeRajahBookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    else
                    {
                        if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Rajah2"), 107, 137, 179);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.position, npc.velocity, ModContent.ProjectileType<RajahBookIt>(), damage, 0, Main.myPlayer);
                        }
                    }
                    npc.active = false;
                    npc.noTileCollide = true;
                    npc.netUpdate = true;
                    return;
                }
            }

            if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 10000)
            {
                npc.TargetClosest(true);
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 10000)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Rajah3"), 107, 137, 179);
                    if (Main.netMode != 1)
                    {
                        if (isSupreme)
                        {
                            Projectile.NewProjectile(npc.position, npc.velocity, ModContent.ProjectileType<SupremeRajahBookIt>(), damage, 0, Main.myPlayer); //Originally 100 damage
                        }
                        else
                        {
                            Projectile.NewProjectile(npc.position, npc.velocity, ModContent.ProjectileType<RajahBookIt>(), damage, 0, Main.myPlayer);
                        }
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
            if (player.Center.Y < npc.position.Y - 30f || TileBelowEmpty() || 
                !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) ||
                Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 2000 || isDashing)
            {
                npc.noGravity = true;
                FlyAI();
            }
            else
            {
                npc.noTileCollide = false;
                npc.noGravity = false;
                isDashing = false;
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
            else if (npc.ai[3] == 0 && npc.ai[2] >= ChangeRate())
            {
                if (Main.rand.Next(5) == 0)
                {
                    Roar(roarTimerMax);
                }
                if (Main.netMode != 1)
                {
                    internalAI[3] = 0;
                    npc.ai[2] = 0;
                    if (ModSupport.GetMod("ThoriumMod") != null && Main.rand.Next(7) == 0)
                    {
                        npc.ai[3] = 7;
                    }
                    else
                    {
                        if (isSupreme)
                        {
                            npc.ai[3] = Main.rand.Next(7);
                        }
                        else
                        {
                            npc.ai[3] = Main.rand.Next(4);
                        }
                    }
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
                            if (NPC.CountNPCS(ModContent.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon1>()) < 5)
                            {
                                Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
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
                                if (NPC.CountNPCS(ModContent.NPCType<RabbitcopterSoldier>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon1>()) < 5)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon1>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            else if (npc.ai[1] == 1)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<BunnyBrawler>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon2>()) < 5)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon2>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon2>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                }
                            }
                            else if (npc.ai[1] == 2)
                            {
                                if (NPC.CountNPCS(ModContent.NPCType<BunnyBattler>()) + AAGlobalProjectile.CountProjectiles(ModContent.ProjectileType<BunnySummon3>()) < 8)
                                {
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));

                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));

                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
                                    
                                    Projectile.NewProjectile(StaffPos, Vector2.Zero, ModContent.ProjectileType<BunnySummon3>(), 0, 0, Main.myPlayer, Main.rand.Next((int)npc.Center.X - 500, (int)npc.Center.X + 500), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50));
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
                        int Rocket = isSupreme ? ModContent.ProjectileType<RajahRocketEXR>() : ModContent.ProjectileType<RajahRocket>();
                        Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, Rocket, damage, 5, Main.myPlayer);
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[3] == 2) //Royal Scepter
                {
                    int carrots = isSupreme ? 5 : 3;
                    int carrotType = isSupreme ? mod.ProjectileType("CarrotEXR") : mod.ProjectileType("CarrotHostile");
                    float spread = 45f * 0.0174f;
                    Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                    dir *= ProjSpeed();
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / carrots * 2;
                    if (internalAI[3] > 40)
                    {
                        internalAI[3] = 0;
                        for (int i = 0; i < carrots; i++)
                        {
                            double offsetAngle = startAngle + (deltaAngle * i);
                            Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), carrotType, damage, 5, Main.myPlayer, 0);
                        }
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[3] == 3) //Javelin
                {
                    int Javelin = isSupreme ? ModContent.ProjectileType<BaneTEXR>() : ModContent.ProjectileType<BaneR>();
                    if (internalAI[3] == (isSupreme ? 40 : 60))
                    {
                        Vector2 dir = Vector2.Normalize(player.position - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, Javelin, damage, 5, Main.myPlayer);
                    }
                    if (internalAI[3] > (isSupreme ? 60 : 90))
                    {
                        internalAI[3] = 0;
                    }
                    npc.netUpdate = true;
                }
                else if (npc.ai[3] == 4) //Excalihare
                {
                    if (internalAI[3] > 40)
                    {
                        internalAI[3] = 0;
                        Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                        dir *= ProjSpeed();
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, ModContent.ProjectileType<ExcalihareR>(), damage, 5, Main.myPlayer);
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[3] == 5) //Fluffy Fury
                {
                    int Arrows = Main.rand.Next(2, 4);
                    float spread = 45f * 0.0174f;
                    Vector2 dir = Vector2.Normalize(player.Center - WeaponPos);
                    dir *= ProjSpeed();
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                    double deltaAngle = spread / (Arrows * 2);
                    if (internalAI[3] > 50)
                    {
                        internalAI[3] = 0;
                        for (int i = 0; i < Arrows; i++)
                        {
                            double offsetAngle = startAngle + (deltaAngle * i);
                            Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("CarrowR"), damage, 5, Main.myPlayer);
                        }
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[3] == 6) //Rabbits Wrath
                {
                    if (internalAI[3] > 5)
                    {
                        internalAI[3] = 0;
                        Vector2 vector12 = new Vector2(player.Center.X, player.Center.Y);
                        float num75 = 14f;
                        for (int num120 = 0; num120 < 3; num120++)
                        {
                            Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                            vector2.Y -= 120 * num120;
                            Vector2 vector13 = vector12 - vector2;
                            if (vector13.Y < 0f)
                            {
                                vector13.Y *= -1f;
                            }
                            if (vector13.Y < 20f)
                            {
                                vector13.Y = 20f;
                            }
                            vector13.Normalize();
                            vector13 *= num75;
                            float num82 = vector13.X;
                            float num83 = vector13.Y;
                            float speedX5 = num82;
                            float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                            int p = Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, ModContent.ProjectileType<CarrotEXR>(), damage, 6, Main.myPlayer, 0, 0);
                            Main.projectile[p].tileCollide = false;
                        }
                        npc.netUpdate = true;
                    }
                }
                else if (npc.ai[3] == 7) //Carrot Farmer
                {
                    if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<CarrotFarmerR>()))
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<CarrotFarmerR>(), damage, 3f, Main.myPlayer, npc.whoAmI);
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
                }
            }
            else
            {
                if (npc.life == npc.lifeMax / 7)
                {
                    npc.damage = (int)(npc.defDamage * 1.5f);
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
            if (npc.ai[3] == 1) //Bunzooka
            {
                return "NPCs/Bosses/Rajah/RajahArmsB";
            }
            else if (npc.ai[3] == 2) //Scepter
            {
                return "NPCs/Bosses/Rajah/RajahArmsR";
            }
            else if (npc.ai[3] == 3 && internalAI[3] <= (isSupreme ? 40 : 60)) //Javelin
            {
                return "NPCs/Bosses/Rajah/RajahArmsS";
            }
            else if (npc.ai[3] == 4) //Excalihare
            {
                return "NPCs/Bosses/Rajah/Supreme/Excalihare";
            }
            else if (npc.ai[3] == 5) //Fluffy Fury
            {
                return "NPCs/Bosses/Rajah/Supreme/FluffyFury";
            }
            else if (npc.ai[3] == 6) //Rabbits Wrath
            {
                return "NPCs/Bosses/Rajah/Supreme/RabbitsWrath";
            }
            else
            {
                return "BlankTex";
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
                            int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + npc.height), npc.width + 20, 4, 31, 0f, 0f, 100);
                            Main.dust[num624].velocity *= 0.2f;
                        }
                        int num625 = Gore.NewGore(new Vector2(num622 - 20, npc.position.Y + npc.height - 8f), default, Main.rand.Next(61, 64), 1f);
                        Main.gore[num625].velocity *= 0.4f;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                    if (npc.position.X < Main.player[npc.target].position.X && npc.position.X + npc.width > Main.player[npc.target].position.X + Main.player[npc.target].width)
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

        bool isDashing = false;
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
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 1000)
                {
                    speed = 50f;
                    isDashing = true;
                }
                else
                {
                    speed = 16f;
                    isDashing = false;
                }
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

        public int ChangeRate()
        {
            if (npc.type == ModContent.NPCType<SupremeRajah>())
            {
                return 120;
            }
            return 240;
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
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RajahTrophy"));
            }
            if (isSupreme)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SupremeRajahHelmet1"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SupremeRajahHelmet2"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/SupremeRajahHelmet3"), 1f);
                if (!AAWorld.downedRajahsRevenge)
                {
                    int n = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, ModContent.NPCType<SupremeRajahDefeat>());
                    Main.npc[n].Center = npc.Center;
                }
                else
                {
                    string Name;
                    if (Main.netMode != 0)
                    {
                        Name = "Terrarians";
                    }
                    else
                    {
                        Name = Main.LocalPlayer.name;
                    }
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Rajah8") + Name + Lang.BossChat("Rajah9"), 107, 137, 179, true);
                    int p = Projectile.NewProjectile(npc.position, npc.velocity, ModContent.ProjectileType<SupremeRajahLeave>(), 100, 0, Main.myPlayer);
                    Main.projectile[p].Center = npc.Center;
                }
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
                else
                {
                    npc.DropLoot(ModContent.ItemType<RajahPelt>(), Main.rand.Next(15, 31));
                    string[] lootTable = { "Excalihare", "FluffyFury", "RabbitsWrath" };
                    int loot = Main.rand.Next(lootTable.Length);
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                }
            }
            else
            {
                int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                if (bunnyKills >= 100)
                {
                    if (Main.netMode != 1) BaseUtility.Chat(Lang.BossChat("Rajah4"), 107, 137, 179, true);
                }
                Projectile.NewProjectile(npc.position, npc.velocity, ModContent.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
                if (!Main.expertMode)
                {
                    if (Main.rand.Next(7) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RajahMask"));
                    }
                    npc.DropLoot(ModContent.ItemType<RajahPelt>(), Main.rand.Next(10, 26));
                    string[] lootTableA = { "BaneOfTheBunny", "Bunnyzooka", "RoyalScepter", "Punisher", "RabbitcopterEars" };
                    int lootA = Main.rand.Next(lootTableA.Length);
                    npc.DropLoot(mod.ItemType(lootTableA[lootA]));
                }
                else
                {
                    npc.DropBossBags();
                }
            }
            AAWorld.downedRajah = true;
            npc.value = 0f;
            npc.boss = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (isSupreme)
            {
                potionType = ModContent.ItemType<Items.Potions.TheBigOne>();
                return;
            }
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * .6f); 
        }

        public void RajahTexture()
        {
            string IsRoaring = Roaring ? "Roar" : "";
            string Supreme = isSupreme ? "Supreme/Supreme" : "";
            if (internalAI[1] == 0)
            {
                RajahTex = mod.GetTexture("NPCs/Bosses/Rajah/" + Supreme + "Rajah" + IsRoaring + "_Fly");
                Glow = mod.GetTexture("Glowmasks/Rajah" + IsRoaring + "_Fly_Glow");
                SupremeGlow = mod.GetTexture("Glowmasks/SupremeRajah" + IsRoaring + "_Fly_Glow");
                SupremeEyes = mod.GetTexture("Glowmasks/SupremeRajah" + IsRoaring + "_Fly_Eyes");
            }
            else
            {
                RajahTex = mod.GetTexture("NPCs/Bosses/Rajah/" + Supreme + "Rajah" + IsRoaring);
                Glow = mod.GetTexture("Glowmasks/Rajah" + IsRoaring + "_Glow");
                SupremeGlow = mod.GetTexture("Glowmasks/SupremeRajah" + IsRoaring + "_Glow");
                SupremeEyes = mod.GetTexture("Glowmasks/SupremeRajah" + IsRoaring + "_Eyes");
            }
        }
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            bool RageMode = !isSupreme && npc.life < npc.lifeMax / 7;
            bool SupremeRageMode = isSupreme && npc.life < npc.lifeMax / 7;
            RajahTexture();
            if (isSupreme && isDashing)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, RajahTex, 0, npc, 1f, 1f, 10, false, 0f, 0f, Main.DiscoColor);
            }
            if (RageMode)
            {
                Color RageColor = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Firebrick, drawColor, Color.Firebrick);
                BaseDrawing.DrawAura(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, 0f, npc.direction, 8, npc.frame, 0f, -5f, RageColor);
            }
            else if (SupremeRageMode)
            {
                BaseDrawing.DrawAura(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, 0f, npc.direction, 8, npc.frame, 0f, -5f, Main.DiscoColor);
            }
            if (npc.ai[3] != 0 && npc.ai[3] < 6) //If holding a weapon
            {
                ArmTex = mod.GetTexture(WeaponTexture());
                Rectangle WeaponRectangle = new Rectangle(0, WeaponFrame, 300, 220);
                BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, WeaponRectangle, drawColor, true);
            }
            BaseDrawing.DrawTexture(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            if (npc.ai[3] == 6) //If Rabbits Wrath
            {
                ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/Supreme/RabbitsWrath");
                Rectangle WeaponRectangle = new Rectangle(0, WeaponFrame, 300, 220);
                BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, WeaponRectangle, drawColor, true);
            }
            if (RageMode)
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
                BaseDrawing.DrawTexture(spriteBatch, Glow, shader, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Color.White, true);
            }
            if (SupremeRageMode)
            {
                BaseDrawing.DrawTexture(spriteBatch, Glow, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Main.DiscoColor, true);
                BaseDrawing.DrawAura(spriteBatch, Glow, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, 0f, npc.direction, 8, npc.frame, 0f, -5f, Main.DiscoColor);
                BaseDrawing.DrawTexture(spriteBatch, SupremeGlow, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Main.DiscoColor, true);
                BaseDrawing.DrawAura(spriteBatch, SupremeGlow, 0, npc.position, npc.width, npc.height, auraPercent, 1f, 1f, 0f, npc.direction, 8, npc.frame, 0f, -5f, Main.DiscoColor);
                return false;
            }
            else if (isSupreme)
            {
                BaseDrawing.DrawTexture(spriteBatch, SupremeEyes, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, Main.DiscoColor, true);
            }
            return false;
        }

        public override string BossHeadTexture => "AAMod/NPCs/Bosses/Rajah/Rajah_Head_Boss";

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 30f;
            if (moveSpeed == 0f || npc.Center == point) return;
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }
    }

    [AutoloadBossHead]
    public class Rajah2 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 80;
            npc.defense = 60;
            npc.lifeMax = 80000;
        }
    }

    [AutoloadBossHead]
    public class Rajah3 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 80;
            npc.defense = 70;
            npc.lifeMax = 100000;
            npc.life = 100000;
        }
    }

    [AutoloadBossHead]
    public class Rajah4 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 100;
            npc.defense = 90;
            npc.lifeMax = 200000;
            npc.life = 200000;
        }
    }

    [AutoloadBossHead]
    public class Rajah5 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 130;
            npc.defense = 100;
            npc.lifeMax = 300000;
            npc.life = 300000;
        }
    }

    [AutoloadBossHead]
    public class Rajah6 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 180;
            npc.defense = 150;
            npc.lifeMax = 500000;
            npc.life = 500000;
        }
    }

    [AutoloadBossHead]
    public class Rajah7 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 210;
            npc.defense = 170;
            npc.lifeMax = 700000;
            npc.life = 700000;
        }
    }

    [AutoloadBossHead]
    public class Rajah8 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 250;
            npc.defense = 180;
            npc.lifeMax = 900000;
            npc.life = 900000;
        }
    }

    [AutoloadBossHead]
    public class Rajah9 : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Rajah";
        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 290;
            npc.defense = 230;
            npc.lifeMax = 1000000;
            npc.life = 1000000;
        }
    }

    [AutoloadBossHead]
    public class SupremeRajah : Rajah
    {
        public override string Texture => "AAMod/NPCs/Bosses/Rajah/Supreme/SupremeRajah";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit; Champion of the Innocent");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.damage = 310;
            npc.defense = 0;
            npc.lifeMax = 2600000;
            npc.life = 2600000;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/SupremeRajah");
            bossBag = mod.ItemType("RajahCache");
            isSupreme = true;
            npc.value = Item.sellPrice(3, 0, 0, 0);
        }
        public override string BossHeadTexture => "AAMod/NPCs/Bosses/Rajah/SupremeRajah_Head_Boss";
    }
}