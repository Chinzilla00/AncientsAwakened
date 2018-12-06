using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    /*
    #region
    public class ShenDoragonRed : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon; Discordian Doomsayer");
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.height = 144;
            npc.width = 84;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.knockBackResist = 0f;
            if (!Main.expertMode && !AAWorld.downedAkuma)
            {
                npc.damage = 170;
                npc.defense = 200;
                npc.lifeMax = 1200000;
                npc.value = Item.buyPrice(0, 55, 0, 0);
            }
            if (!Main.expertMode && AAWorld.downedAkuma)
            {
                npc.damage = 180;
                npc.defense = 210;
                npc.lifeMax = 1400000;
                npc.value = Item.buyPrice(0, 55, 0, 0);
            }
            if (Main.expertMode && !AAWorld.downedAkumaA)
            {
                npc.damage = 180;
                npc.defense = 200;
                npc.lifeMax = 1300000;
                npc.value = Item.buyPrice(0, 0, 0, 0);
            }
            if (Main.expertMode && AAWorld.downedAkumaA)
            {
                npc.damage = 200;
                npc.defense = 230;
                npc.lifeMax = 150000;
                npc.value = Item.buyPrice(0, 0, 0, 0);
            }
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.DeathSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Shen");
            musicPriority = MusicPriority.BossHigh;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            bool expertMode = Main.expertMode;
            float expertDamage = expertMode ? (0.50f * Main.damageMultiplier) : 1f;
            bool ninthHealth = (double)npc.life <= (double)npc.lifeMax * 0.9;
            bool eighthHealth = (double)npc.life <= (double)npc.lifeMax * 0.8;
            bool seventhHealth = (double)npc.life <= (double)npc.lifeMax * 0.7;
            bool sixthHealth = (double)npc.life <= (double)npc.lifeMax * 0.6;
            bool fifthHealth = (double)npc.life <= (double)npc.lifeMax * 0.5;
            bool fourthHealth = (double)npc.life <= (double)npc.lifeMax * 0.4;
            bool thirdHealth = (double)npc.life <= (double)npc.lifeMax * 0.3;
            bool secondHealth = (double)npc.life <= (double)npc.lifeMax * 0.2;
            bool firstHealth = (double)npc.life <= (double)npc.lifeMax * 0.1;
            int flareCount = 10;
            bool Charge = npc.ai[3] < 10f;
            float teleportLocation = 0f;
            int teleChoice = Main.rand.Next(2);
            if (phase3Check)
            {
                flareProjectiles = magicBoost ? 4 : 3;
            }
            else if (phase2Check)
            {
                flareProjectiles = magicBoost ? 3 : 2;
            }
            else
            {
                flareProjectiles = magicBoost ? 2 : 1;
            }
            if (gigaFlareStart && flareTimer == 0)
            {
                flareTimer = summonerRage ? 720 : 900;
            }
            if (flareTimer > 0)
            {
                flareTimer--;
                if (flareTimer == 0)
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        if (Main.netMode != 1)
                        {
                            Vector2 value9 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float spread = 45f * 0.0174f;
                            double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - spread / 2;
                            double deltaAngle = spread / 8f;
                            double offsetAngle;
                            int damage = expertMode ? 80 : 90;
                            int j;
                            for (j = 0; j < flareProjectiles; j++)
                            {
                                offsetAngle = (startAngle + deltaAngle * (j + j * j) / 2f) + 32f * j;
                                Projectile.NewProjectile(value9.X, value9.Y, (float)Main.rand.Next(-200, 201) * 0.125f, (float)Main.rand.Next(-200, 201) * 0.125f, mod.ProjectileType("GigaFlare"), damage, 0f, Main.myPlayer, 1f, (float)(npc.target + 1));
                                Projectile.NewProjectile(value9.X, value9.Y, (float)Main.rand.Next(-200, 201) * 0.125f, (float)Main.rand.Next(-200, 201) * 0.125f, mod.ProjectileType("GigaFlare"), damage, 0f, Main.myPlayer, 1f, (float)(npc.target + 1));
                            }
                        }
                    }
                }
            }
            if (phase3Check)
            {
                skyFlareProjectiles = magicBoost ? 4 : 3;
            }
            else if (phase2Check)
            {
                skyFlareProjectiles = magicBoost ? 3 : 2;
            }
            else if (gigaFlareStart)
            {
                skyFlareProjectiles = 2;
            }
            else
            {
                skyFlareProjectiles = 1;
            }
            if (skyFlareStart && skyFlareCountdown == 0)
            {
                skyFlareCountdown = summonerRage ? 520 : 600;
            }
            if (skyFlareCountdown > 0)
            {
                skyFlareCountdown--;
                if (skyFlareCountdown == 0)
                {
                    if (Main.netMode != 1)
                    {
                        for (int playerIndex = 0; playerIndex < 255; playerIndex++)
                        {
                            if (Main.player[playerIndex].active)
                            {
                                Player player2 = Main.player[playerIndex];
                                int speed = Main.rand.Next(3, 11);
                                float spawnX = Main.rand.Next(1000) - 500 + player2.Center.X;
                                float spawnY = -1000 + player2.Center.Y;
                                Vector2 baseSpawn = new Vector2(spawnX, spawnY);
                                Vector2 baseVelocity = player2.Center - baseSpawn;
                                baseVelocity.Normalize();
                                baseVelocity = baseVelocity * speed;
                                int damage = expertMode ? 65 : 70;
                                for (int k = 0; k < skyFlareProjectiles; k++)
                                {
                                    int randomTime = Main.rand.Next(100, 300);
                                    Vector2 spawn = baseSpawn;
                                    spawn.X = spawn.X + k * 90 - (skyFlareProjectiles * 15);
                                    Vector2 velocity = baseVelocity;
                                    velocity = baseVelocity.RotatedBy(MathHelper.ToRadians(-skyFlareAngleSpread / 2 + (skyFlareAngleSpread * k / (float)skyFlareProjectiles)));
                                    velocity.X = velocity.X + 3 * Main.rand.NextFloat() - 1.5f;
                                    int projectile = Projectile.NewProjectile(spawn.X, spawn.Y, velocity.X, velocity.Y, mod.ProjectileType("SkyFlare"), damage, 10f, Main.myPlayer, 0f, 0f);
                                    Main.projectile[projectile].timeLeft = randomTime;
                                }
                            }
                        }
                    }
                }
            }
            if (phase4Change)
            {
                npc.damage = (int)((float)npc.defDamage * 1.05f * expertDamage);
                npc.defense = meleeAggro ? 130 : 140;
            }
            else if (phase3Change)
            {
                npc.damage = (int)((float)npc.defDamage * 1.05f * expertDamage);
                npc.defense = meleeAggro ? 170 : 180;
            }
            else if (phase2Change)
            {
                npc.damage = (int)((float)npc.defDamage * 1.1f * expertDamage);
                npc.defense = meleeAggro ? 210 : 220;
            }
            else
            {
                npc.damage = npc.defDamage;
                npc.defense = meleeAggro ? 250 : 260;
            }
            int aiChangeRate = expertMode ? 36 : 38;
            float npcVelocity = expertMode ? 0.7f : 0.69f;
            float scaleFactor = expertMode ? 11f : 10.8f;
            if (phase4Change)
            {
                npcVelocity = 0.95f;
                scaleFactor = 14f;
                aiChangeRate = 25;
            }
            else if (phase3Change)
            {
                npcVelocity = 0.9f;
                scaleFactor = 13f;
                aiChangeRate = 25;
            }
            else if (phase2Change && isCharging)
            {
                npcVelocity = (expertMode ? 0.8f : 0.78f);
                scaleFactor = (expertMode ? 12.2f : 12f);
                aiChangeRate = (expertMode ? 36 : 38);
            }
            else if (isCharging && !phase2Change && !phase3Change && !phase4Change)
            {
                aiChangeRate = 25;
            }
            float playerRunAcceleration = Main.player[npc.target].velocity.Y == 0f ? Math.Abs(Main.player[npc.target].moveSpeed * 0.5f) : (Main.player[npc.target].runAcceleration * 1f);
            if (playerRunAcceleration <= 1f)
            {
                playerRunAcceleration = 1f;
            }
            int chargeTime = expertMode ? 26 : 27;
            float chargeSpeed = playerRunAcceleration * (expertMode ? 27f : 26.5f); //17 and 16
            if (phase4Change) //phase 4
            {
                chargeTime = 21;
                chargeSpeed = playerRunAcceleration * 35f; //27
            }
            else if (phase3Change) //phase 3
            {
                chargeTime = 23;
                chargeSpeed = playerRunAcceleration * 30.5f; //27
            }
            else if (isCharging && phase2Change) //phase 2
            {
                chargeTime = (expertMode ? 25 : 26);
                if (expertMode)
                {
                    chargeSpeed = playerRunAcceleration * 28f; //21
                }
            }
            if (rangedSpeed)
            {
                chargeSpeed += 1f;
            }
            int num1454 = 80;
            int num1455 = 4;
            float num1456 = 0.3f;
            float scaleFactor11 = 5f;
            int num1457 = 90;
            int num1458 = 180;
            int num1459 = 180;
            int num1460 = 30;
            int num1461 = 120;
            int num1462 = 4;
            float scaleFactor13 = 20f;
            float num1463 = 6.28318548f / (float)(num1461 / 2);
            int num1464 = 75;
            Vector2 vectorCenter = npc.Center;
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
                player = Main.player[npc.target];
                npc.netUpdate = true;
            }
            if (player.dead)
            {
                npc.velocity.Y = npc.velocity.Y - 0.4f;
                if (npc.timeLeft > 150)
                {
                    npc.timeLeft = 150;
                }
                if (npc.ai[0] > 4f)
                {
                    npc.ai[0] = 5f;
                }
                else
                {
                    npc.ai[0] = 0f;
                }
                npc.ai[2] = 0f;
            }
            else if (npc.timeLeft > 1800)
            {
                npc.timeLeft = 1800;
            }
            if (!isJungle)
            {
                enrageTimer--;
                if (enrageTimer <= 0)
                {
                    aiChangeRate = 15;
                    npc.defense = npc.defDefense * 50;
                    chargeSpeed += 7f;
                }
            }
            else
            {
                enrageTimer = 420;
            }
            if (npc.localAI[0] == 0f)
            {
                npc.localAI[0] = 1f;
                npc.alpha = 255;
                npc.rotation = 0f; //checked
                if (Main.netMode != 1)
                {
                    npc.ai[0] = -1f;
                    npc.netUpdate = true;
                }
            }
            float npcRotation = (float)Math.Atan2((double)(player.Center.Y - vectorCenter.Y), (double)(player.Center.X - vectorCenter.X));
            if (npc.spriteDirection == 1) //changed
            {
                npcRotation += 3.14159274f;
            }
            if (npcRotation < 0f)
            {
                npcRotation += 6.28318548f;
            }
            if (npcRotation > 6.28318548f)
            {
                npcRotation -= 6.28318548f;
            }
            if (npc.ai[0] == -1f)
            {
                npcRotation = 0f;
            }
            if (npc.ai[0] == 3f)
            {
                npcRotation = 0f;
            }
            if (npc.ai[0] == 4f)
            {
                npcRotation = 0f;
            }
            if (npc.ai[0] == 8f)
            {
                npcRotation = 0f;
            }
            if (npc.ai[0] == 9f)
            {
                npcRotation = 0f;
            }
            if (npc.ai[0] == 13f)
            {
                npcRotation = 0f;
            }
            float npcRotationSpeed = 0.04f;
            if (npc.ai[0] == 1f || npc.ai[0] == 6f || npc.ai[0] == 11f)
            {
                npcRotationSpeed = 0f;
            }
            if (npc.ai[0] == 7f || npc.ai[0] == 12f)
            {
                npcRotationSpeed = 0f;
            }
            if (npc.ai[0] == 3f)
            {
                npcRotationSpeed = 0.01f;
            }
            if (npc.ai[0] == 4f)
            {
                npcRotationSpeed = 0.01f;
            }
            if (npc.ai[0] == 8f || npc.ai[0] == 13f)
            {
                npcRotationSpeed = 0.01f;
            }
            if (npc.rotation < npcRotation)
            {
                if ((double)(npcRotation - npc.rotation) > 3.1415926535897931)
                {
                    npc.rotation -= npcRotationSpeed;
                }
                else
                {
                    npc.rotation += npcRotationSpeed;
                }
            }
            if (npc.rotation > npcRotation)
            {
                if ((double)(npc.rotation - npcRotation) > 3.1415926535897931)
                {
                    npc.rotation += npcRotationSpeed;
                }
                else
                {
                    npc.rotation -= npcRotationSpeed;
                }
            }
            if (npc.rotation > npcRotation - npcRotationSpeed && npc.rotation < npcRotation + npcRotationSpeed)
            {
                npc.rotation = npcRotation;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.28318548f;
            }
            if (npc.rotation > 6.28318548f)
            {
                npc.rotation -= 6.28318548f;
            }
            if (npc.rotation > npcRotation - npcRotationSpeed && npc.rotation < npcRotation + npcRotationSpeed)
            {
                npc.rotation = npcRotation;
            }
            if (npc.ai[0] != -1f && npc.ai[0] < 9f)
            {
                bool colliding = Collision.SolidCollision(npc.position, npc.width, npc.height);
                if (colliding)
                {
                    npc.alpha += 15;
                }
                else
                {
                    npc.alpha -= 15;
                }
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
                if (npc.alpha > 150)
                {
                    npc.alpha = 150;
                }
            }
            if (npc.ai[0] == -1f) //initial spawn effects
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                int num1467 = Math.Sign(player.Center.X - vectorCenter.X);
                if (num1467 != 0) //perhaps issues?  probably not
                {
                    npc.direction = num1467;
                    npc.spriteDirection = npc.direction; //end issues
                }
                if (npc.ai[2] > 20f)
                {
                    npc.velocity.Y = -2f;
                    npc.alpha -= 5;
                    bool colliding = Collision.SolidCollision(npc.position, npc.width, npc.height);
                    if (colliding)
                    {
                        npc.alpha += 15;
                    }
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    if (npc.alpha > 150)
                    {
                        npc.alpha = 150;
                    }
                }
                if (npc.ai[2] == (float)(num1457 - 30))
                {
                    int num1468 = 36;
                    for (int num1469 = 0; num1469 < num1468; num1469++)
                    {
                        Vector2 vector169 = Vector2.Normalize(npc.velocity) * new Vector2((float)npc.width / 2f, (float)npc.height) * 0.75f * 0.5f;
                        vector169 = vector169.RotatedBy((double)((float)(num1469 - (num1468 / 2 - 1)) * 6.28318548f / (float)num1468), default(Vector2)) + npc.Center;
                        Vector2 value16 = vector169 - npc.Center;
                        int num1470 = Dust.NewDust(vector169 + value16, 0, 0, 244, value16.X * 2f, value16.Y * 2f, 100, default(Color), 1.4f); //changed
                        Main.dust[num1470].noGravity = true;
                        Main.dust[num1470].noLight = true;
                        Main.dust[num1470].velocity = Vector2.Normalize(value16) * 3f;
                    }
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1464)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 0f && !player.dead)
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
                }
                Vector2 value17 = player.Center + new Vector2(npc.ai[1], -200f) - vectorCenter;
                Vector2 vector170 = Vector2.Normalize(value17 - npc.velocity) * scaleFactor;
                if (npc.velocity.X < vector170.X)
                {
                    npc.velocity.X = npc.velocity.X + npcVelocity;
                    if (npc.velocity.X < 0f && vector170.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + npcVelocity;
                    }
                }
                else if (npc.velocity.X > vector170.X)
                {
                    npc.velocity.X = npc.velocity.X - npcVelocity;
                    if (npc.velocity.X > 0f && vector170.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - npcVelocity;
                    }
                }
                if (npc.velocity.Y < vector170.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    if (npc.velocity.Y < 0f && vector170.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    }
                }
                else if (npc.velocity.Y > vector170.Y)
                {
                    npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    if (npc.velocity.Y > 0f && vector170.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    }
                }
                int num1471 = Math.Sign(player.Center.X - vectorCenter.X);
                if (num1471 != 0) //perhpas issues?
                {
                    if (npc.ai[2] == 0f && num1471 != npc.direction)
                    {
                        npc.rotation = 3.14159274f;
                    }
                    npc.direction = num1471;
                    if (num1471 != 0)
                    {
                        npc.direction = num1471;
                        npc.rotation = 0f;
                        npc.spriteDirection = -npc.direction; //end issues
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)aiChangeRate)
                {
                    int num1472 = 0;
                    switch ((int)npc.ai[3]) //switch for attack modes
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            num1472 = 1;
                            break;
                        case 10:
                            npc.ai[3] = 1f;
                            num1472 = 2;
                            break;
                        case 11:
                            npc.ai[3] = 0f;
                            num1472 = 3;
                            break;
                    }
                    if (phase2Check) //checks if can initiate phase 2
                    {
                        num1472 = 4;
                    }
                    if (num1472 == 1)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num1471 != 0) //charging stuff.  possible issues
                        {
                            npc.direction = num1471;
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction; //end issues
                        }
                    }
                    else if (num1472 == 2)
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num1472 == 3)
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num1472 == 4)
                    {
                        npc.ai[0] = 4f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 1f) //charge attack
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                int num1473 = 7;
                for (int num1474 = 0; num1474 < num1473; num1474++)
                {
                    Vector2 vector171 = Vector2.Normalize(npc.velocity) * new Vector2((float)(npc.width + 50) / 2f, (float)npc.height) * 0.75f;
                    vector171 = vector171.RotatedBy((double)(num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1473), default(Vector2)) + vectorCenter;
                    Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num1475 = Dust.NewDust(vector171 + value18, 0, 0, 244, value18.X * 2f, value18.Y * 2f, 100, default(Color), 1.4f); //changed
                    Main.dust[num1475].noGravity = true;
                    Main.dust[num1475].noLight = true;
                    Main.dust[num1475].velocity /= 4f;
                    Main.dust[num1475].velocity -= npc.velocity;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)chargeTime)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 2f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 2f) //fireball attack
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
                }
                Vector2 value19 = player.Center + new Vector2(npc.ai[1], -200f) - vectorCenter;
                Vector2 vector172 = Vector2.Normalize(value19 - npc.velocity) * scaleFactor11;
                if (npc.velocity.X < vector172.X)
                {
                    npc.velocity.X = npc.velocity.X + num1456;
                    if (npc.velocity.X < 0f && vector172.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + num1456;
                    }
                }
                else if (npc.velocity.X > vector172.X)
                {
                    npc.velocity.X = npc.velocity.X - num1456;
                    if (npc.velocity.X > 0f && vector172.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - num1456;
                    }
                }
                if (npc.velocity.Y < vector172.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + num1456;
                    if (npc.velocity.Y < 0f && vector172.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + num1456;
                    }
                }
                else if (npc.velocity.Y > vector172.Y)
                {
                    npc.velocity.Y = npc.velocity.Y - num1456;
                    if (npc.velocity.Y > 0f && vector172.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - num1456;
                    }
                }
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (npc.ai[2] % (float)num1455 == 0f) //fire flare bombs from mouth
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60); //changed
                    if (Main.netMode != 1)
                    {
                        if (NPC.CountNPCS(mod.NPCType("DetonatingFlare")) < flareCount)
                        {
                            Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (float)(npc.width + 20) / 2f + vectorCenter;
                            float speedX = (float)Main.rand.Next(15, 23);
                            int detFlare = NPC.NewNPC((int)vector6.X, (int)vector6.Y - 100, mod.NPCType("DetonatingFlare"), 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[detFlare].localAI[1] = (float)Main.rand.Next(5, 9);
                            Main.npc[detFlare].localAI[2] = speedX / 100;
                        }
                        int damage = expertMode ? 150 : 164;
                        int randomTime = Main.rand.Next(500, 1001);
                        Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(npc.width + 20) / 2f + vectorCenter;
                        int projectile = Projectile.NewProjectile((int)vector173.X, (int)vector173.Y - 100, (float)Main.rand.Next(-200, 201) * 0.13f, (float)Main.rand.Next(-200, 201) * 0.13f, mod.ProjectileType("FlareBomb"), damage, 0f, Main.myPlayer, 0f, 0f); //changed
                        Main.projectile[projectile].timeLeft = randomTime;
                    }
                }
                int num1476 = Math.Sign(player.Center.X - vectorCenter.X);
                Vector2 dir2 = npc.position - Main.player[npc.target].position;
                if (num1476 != 0) //perhaps issues?
                {
                    npc.direction = num1476;
                    if (npc.spriteDirection != -npc.direction)
                    {
                        npc.rotation += 6.28318548f;
                        if (npc.rotation > 6.28318548f)
                        {
                            npc.rotation = 0f;
                            if (dir2.X < 0)
                            {
                                npc.direction = -1;
                            }
                            else
                            {
                                npc.direction = 1;
                            }
                        }
                    }
                    npc.spriteDirection = -npc.direction; //end issues
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1454)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 3f) //Fire small flares
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num1457 - 30))
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (Main.netMode != 1 && npc.ai[2] == (float)(num1457 - 30))
                {
                    int randomTime = Main.rand.Next(200, 400);
                    int randomTime2 = Main.rand.Next(100, 300);
                    Vector2 vector174 = npc.rotation.ToRotationVector2() * (Vector2.UnitX * (float)npc.direction) * (float)(npc.width + 20) / 2f + vectorCenter;
                    int projectile = Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("Flare"), 0, 0f, Main.myPlayer, 1f, (float)(npc.target + 1)); //changed
                    int projectile2 = Projectile.NewProjectile(vector174.X, vector174.Y, (float)(-(float)npc.direction * 2), 8f, mod.ProjectileType("Flare"), 0, 0f, Main.myPlayer, 0f, 0f); //changed
                    Main.projectile[projectile].timeLeft = randomTime;
                    Main.projectile[projectile2].timeLeft = randomTime2;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1457)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 4f) //enter phase 2
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num1458 - 60))
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1458)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 5f && !player.dead) //phase 2
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
                }
                Vector2 value20 = player.Center + new Vector2(npc.ai[1], -200f) - vectorCenter;
                Vector2 vector175 = Vector2.Normalize(value20 - npc.velocity) * scaleFactor;
                if (npc.velocity.X < vector175.X)
                {
                    npc.velocity.X = npc.velocity.X + npcVelocity;
                    if (npc.velocity.X < 0f && vector175.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + npcVelocity;
                    }
                }
                else if (npc.velocity.X > vector175.X)
                {
                    npc.velocity.X = npc.velocity.X - npcVelocity;
                    if (npc.velocity.X > 0f && vector175.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - npcVelocity;
                    }
                }
                if (npc.velocity.Y < vector175.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    if (npc.velocity.Y < 0f && vector175.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    }
                }
                else if (npc.velocity.Y > vector175.Y)
                {
                    npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    if (npc.velocity.Y > 0f && vector175.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    }
                }
                int num1477 = Math.Sign(player.Center.X - vectorCenter.X);
                if (num1477 != 0) //perhaps an issue lies here
                {
                    if (npc.ai[2] == 0f && num1477 != npc.direction)
                    {
                        npc.rotation = 3.14159274f;
                    }
                    npc.direction = num1477;
                    if (num1477 != 0)
                    {
                        npc.direction = num1477;
                        npc.rotation = 0f;
                        npc.spriteDirection = -npc.direction; //end issue
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)aiChangeRate)
                {
                    int num1478 = 0;
                    switch ((int)npc.ai[3]) //switch between attack modes
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            num1478 = 1;
                            break;
                        case 6:
                            npc.ai[3] = 1f;
                            num1478 = 2;
                            break;
                        case 7:
                            npc.ai[3] = 0f;
                            num1478 = 3;
                            break;
                    }
                    if (phase3Check) //checks if can initiate phase 3
                    {
                        num1478 = 4;
                    }
                    if (num1478 == 1)
                    {
                        npc.ai[0] = 6f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num1477 != 0)
                        {
                            npc.direction = num1477; //perhaps an issue lies here
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction; //end issue
                        }
                    }
                    else if (num1478 == 2)
                    {
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num1477 != 0)
                        {
                            npc.direction = num1477; //perhaps an issue lies here
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction; //end issue
                        }
                        npc.ai[0] = 7f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num1478 == 3)
                    {
                        npc.ai[0] = 8f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num1478 == 4)
                    {
                        npc.ai[0] = 9f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 6f) //charge
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                int num1479 = 7;
                for (int num1480 = 0; num1480 < num1479; num1480++)
                {
                    Vector2 vector176 = Vector2.Normalize(npc.velocity) * new Vector2((float)(npc.width + 50) / 2f, (float)npc.height) * 0.75f;
                    vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
                    Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
                    Main.dust[num1481].noGravity = true;
                    Main.dust[num1481].noLight = true;
                    Main.dust[num1481].velocity /= 4f;
                    Main.dust[num1481].velocity -= npc.velocity;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)chargeTime)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 2f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 7f) //Flare summon
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (npc.ai[2] % (float)num1462 == 0f)
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60); //changed
                    if (Main.netMode != 1)
                    {
                        if (NPC.CountNPCS(mod.NPCType("DetonatingFlare2")) < flareCount)
                        {
                            Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (float)(npc.width + 20) / 2f + vectorCenter;
                            int detFlare = NPC.NewNPC((int)vector6.X, (int)vector6.Y - 100, mod.NPCType("DetonatingFlare2"), 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[detFlare].localAI[3] = (float)Main.rand.Next(3, 9);
                        }
                        int damage = expertMode ? 85 : 90;
                        Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (float)(npc.width + 20) / 2f + vectorCenter;
                        int projectile = Projectile.NewProjectile((int)vector173.X, (int)vector173.Y - 100, (float)Main.rand.Next(-500, 501) * 0.13f, (float)Main.rand.Next(-30, 31) * 0.13f, mod.ProjectileType("FlareDust"), damage, 0f, Main.myPlayer, 0f, 0f); //changed
                        Main.projectile[projectile].timeLeft = 600;
                    }
                }
                npc.velocity = npc.velocity.RotatedBy((double)(-(double)num1463 * (float)npc.direction), default(Vector2));
                npc.rotation -= num1463 * (float)npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1461)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 8f) //stop and fire big flare
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num1457 - 30))
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (Main.netMode != 1 && npc.ai[2] == (float)(num1457 - 30))
                {
                    Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("BigFlare"), 0, 0f, Main.myPlayer, 1f, (float)(npc.target + 1)); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1457)
                {
                    npc.ai[0] = 5f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 9f) //start phase 3
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num1459 - 60))
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1459)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 10f && !player.dead) //phase 3, new part of AI
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
                }
                Vector2 value20 = player.Center + new Vector2(npc.ai[1], -200f) - vectorCenter;
                Vector2 vector175 = Vector2.Normalize(value20 - npc.velocity) * scaleFactor;
                if (npc.velocity.X < vector175.X)
                {
                    npc.velocity.X = npc.velocity.X + npcVelocity;
                    if (npc.velocity.X < 0f && vector175.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + npcVelocity;
                    }
                }
                else if (npc.velocity.X > vector175.X)
                {
                    npc.velocity.X = npc.velocity.X - npcVelocity;
                    if (npc.velocity.X > 0f && vector175.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - npcVelocity;
                    }
                }
                if (npc.velocity.Y < vector175.Y)
                {
                    npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    if (npc.velocity.Y < 0f && vector175.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + npcVelocity;
                    }
                }
                else if (npc.velocity.Y > vector175.Y)
                {
                    npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    if (npc.velocity.Y > 0f && vector175.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y - npcVelocity;
                    }
                }
                int num1477 = Math.Sign(player.Center.X - vectorCenter.X);
                if (num1477 != 0)
                {
                    if (npc.ai[2] == 0f && num1477 != npc.direction) //perhaps an issue lies here
                    {
                        npc.rotation = 3.14159274f;
                    }
                    npc.direction = num1477;
                    if (num1477 != 0)
                    {
                        npc.direction = num1477;
                        npc.rotation = 0f;
                        npc.spriteDirection = -npc.direction; //end issue
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)aiChangeRate)
                {
                    int num1478 = 0;
                    switch ((int)npc.ai[3])
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            num1478 = 1;
                            break;
                        case 6:
                            npc.ai[3] = 1f;
                            num1478 = 2;
                            break;
                        case 7:
                            npc.ai[3] = 0f;
                            num1478 = 3;
                            break;
                    }
                    if (phase4Check) //checks if can initiate phase 4
                    {
                        num1478 = 4;
                    }
                    if (num1478 == 1)
                    {
                        npc.ai[0] = 11f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num1477 != 0)
                        {
                            npc.direction = num1477; //perhaps an issue lies here
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction; //end issue
                        }
                    }
                    else if (num1478 == 2)
                    {
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * scaleFactor13;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num1477 != 0)
                        {
                            npc.direction = num1477; //perhaps an issue lies here
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction; //end issue
                        }
                        npc.ai[0] = 12f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num1478 == 3)
                    {
                        npc.ai[0] = 13f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num1478 == 4)
                    {
                        npc.ai[0] = 14f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 11f) //charge
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                int num1479 = 7;
                for (int num1480 = 0; num1480 < num1479; num1480++)
                {
                    Vector2 vector176 = Vector2.Normalize(npc.velocity) * new Vector2((float)(npc.width + 50) / 2f, (float)npc.height) * 0.75f;
                    vector176 = vector176.RotatedBy((double)(num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (double)((float)num1479), default(Vector2)) + vectorCenter;
                    Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num1481 = Dust.NewDust(vector176 + value21, 0, 0, 244, value21.X * 2f, value21.Y * 2f, 100, default(Color), 1.4f); //changed
                    Main.dust[num1481].noGravity = true;
                    Main.dust[num1481].noLight = true;
                    Main.dust[num1481].velocity /= 4f;
                    Main.dust[num1481].velocity -= npc.velocity;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)chargeTime)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 2f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 12f) //flare circle of doom
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (npc.ai[2] % (float)num1462 == 0f)
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60); //changed
                    if (Main.netMode != 1)
                    {
                        if (NPC.CountNPCS(mod.NPCType("DetonatingFlare2")) < flareCount && NPC.CountNPCS(mod.NPCType("DetonatingFlare")) < flareCount)
                        {
                            int randomSpawn = Main.rand.Next(2);
                            if (randomSpawn == 0)
                            {
                                randomSpawn = mod.NPCType("DetonatingFlare");
                            }
                            else
                            {
                                randomSpawn = mod.NPCType("DetonatingFlare2");
                            }
                            Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (float)(npc.width + 20) / 2f + vectorCenter;
                            float speedX = (float)Main.rand.Next(10, 16);
                            int detFlare = NPC.NewNPC((int)vector6.X, (int)vector6.Y - 100, randomSpawn, 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[detFlare].localAI[1] = (float)Main.rand.Next(5, 11);
                            Main.npc[detFlare].localAI[2] = speedX / 100;
                            Main.npc[detFlare].localAI[3] = (float)Main.rand.Next(3, 11);
                        }
                        int damage = expertMode ? 90 : 100;
                        Vector2 vector = Vector2.Normalize(player.Center - vectorCenter) * (float)(npc.width + 20) / 2f + vectorCenter;
                        int projectile1 = Projectile.NewProjectile((int)vector.X, (int)vector.Y - 100, (float)Main.rand.Next(-501, 501) * 0.13f, (float)Main.rand.Next(-31, 31) * 0.13f, mod.ProjectileType("FlareDust"), damage, 0f, Main.myPlayer, 0f, 0f); //changed
                        Main.projectile[projectile1].timeLeft = 600;
                        int projectile2 = Projectile.NewProjectile((int)vector.X, (int)vector.Y - 100, (float)Main.rand.Next(-31, 31) * 0.13f, (float)Main.rand.Next(-251, 251) * 0.13f, mod.ProjectileType("FlareDust"), damage, 0f, Main.myPlayer, 0f, 0f); //changed
                        Main.projectile[projectile2].timeLeft = 420;
                    }
                }
                npc.velocity = npc.velocity.RotatedBy((double)(-(double)num1463 * (float)npc.direction), default(Vector2));
                npc.rotation -= num1463 * (float)npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1461)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 13f) //dual tornado blast
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num1457 - 30))
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (Main.netMode != 1 && npc.ai[2] == (float)(num1457 - 30))
                {
                    Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("BigFlare"), 0, 0f, Main.myPlayer, 1f, (float)(npc.target + 1)); //changed
                    int randomTime = Main.rand.Next(200, 400);
                    int randomTime2 = Main.rand.Next(100, 300);
                    Vector2 vector174 = npc.rotation.ToRotationVector2() * (Vector2.UnitX * (float)npc.direction) * (float)(npc.width + 20) / 2f + vectorCenter;
                    int projectile = Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("Flare"), 0, 0f, Main.myPlayer, 1f, (float)(npc.target + 1)); //changed
                    int projectile2 = Projectile.NewProjectile(vector174.X, vector174.Y, (float)(-(float)npc.direction * 2), 8f, mod.ProjectileType("Flare"), 0, 0f, Main.myPlayer, 0f, 0f); //changed
                    Main.projectile[projectile].timeLeft = randomTime;
                    Main.projectile[projectile2].timeLeft = randomTime2;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1457)
                {
                    npc.ai[0] = 10f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 14f) //phase 4 would be ai 9
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                if (npc.ai[2] < (float)(num1459 - 90))
                {
                    bool colliding = Collision.SolidCollision(npc.position, npc.width, npc.height);
                    if (colliding)
                    {
                        npc.alpha += 15;
                    }
                    else
                    {
                        npc.alpha -= 15;
                    }
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    if (npc.alpha > 150)
                    {
                        npc.alpha = 150;
                    }
                }
                else if (npc.alpha < 255)
                {
                    npc.alpha += 4;
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                    }
                }
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num1459 - 60))
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92, 1f, 0f);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1459)
                {
                    npc.ai[0] = 15f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 15f && !player.dead) //teleport above or below player would be ai 10
            {
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                if (npc.alpha < 255)
                {
                    npc.alpha += 25;
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                    }
                }
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = (float)(360 * Math.Sign((vectorCenter - player.Center).X));
                }
                Vector2 value7 = player.Center + new Vector2(npc.ai[1], teleportLocation) - vectorCenter; //teleport distance
                Vector2 desiredVelocity = Vector2.Normalize(value7 - npc.velocity) * scaleFactor;
                npc.SimpleFlyMovement(desiredVelocity, npcVelocity);
                int num32 = Math.Sign(player.Center.X - vectorCenter.X);
                if (num32 != 0)
                {
                    if (npc.ai[2] == 0f && num32 != npc.direction)
                    {
                        npc.rotation = 3.14159274f;
                    }
                    npc.direction = num32;
                    if (num32 != 0)
                    {
                        npc.direction = num32;
                        npc.rotation = 0f;
                        npc.spriteDirection = -npc.direction; //end issue
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)aiChangeRate)
                {
                    int num33 = 0;
                    switch ((int)npc.ai[3])
                    {
                        case 0:
                        case 2:
                        case 3:
                        case 5:
                        case 6:
                        case 7:
                            num33 = 1;
                            break;
                        case 1:
                        case 4:
                        case 8:
                            num33 = 2;
                            break;
                    }
                    if (num33 == 1)
                    {
                        npc.ai[0] = 16f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
                        npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
                        if (num32 != 0)
                        {
                            npc.direction = num32; //perhaps an issue lies here
                            if (npc.spriteDirection == 1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = -npc.direction; //end issue
                        }
                    }
                    else if (num33 == 2)
                    {
                        npc.ai[0] = 17f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    else if (num33 == 3)
                    {
                        npc.ai[0] = 18f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 16f) //charge npc would be ai 11
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                npc.alpha -= 25;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
                int num34 = 7;
                for (int m = 0; m < num34; m++)
                {
                    Vector2 vector11 = Vector2.Normalize(npc.velocity) * new Vector2((float)(npc.width + 50) / 2f, (float)npc.height) * 0.75f;
                    vector11 = vector11.RotatedBy((double)(m - (num34 / 2 - 1)) * 3.1415926535897931 / (double)((float)num34), default(Vector2)) + vectorCenter;
                    Vector2 value8 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
                    int num35 = Dust.NewDust(vector11 + value8, 0, 0, 244, value8.X * 2f, value8.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[num35].noGravity = true;
                    Main.dust[num35].noLight = true;
                    Main.dust[num35].velocity /= 4f;
                    Main.dust[num35].velocity -= npc.velocity;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)chargeTime)
                {
                    npc.ai[0] = 15f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 17f) //teleport npc would be ai 12
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                if (npc.alpha < 255)
                {
                    npc.alpha += 17;
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                    }
                }
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == (float)(num1460 / 2))
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92, 1f, 0f);
                }
                if (Main.netMode != 1 && npc.ai[2] == (float)(num1460 / 2))
                {
                    if (npc.ai[1] == 0f)
                    {
                        npc.ai[1] = (float)(300 * Math.Sign((vectorCenter - player.Center).X));
                    }
                    Vector2 center = player.Center + new Vector2(-npc.ai[1], teleportLocation); //teleport distance
                    vectorCenter = (npc.Center = center);
                    int num36 = Math.Sign(player.Center.X - vectorCenter.X);
                    npc.rotation -= num1463 * (float)npc.direction;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1460)
                {
                    npc.ai[0] = 15f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    if (npc.ai[3] >= 9f)
                    {
                        npc.ai[3] = 0f;
                    }
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 18f) //neutral npc would be ai 13
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92, 1f, 0f);
                }
                npc.velocity = npc.velocity.RotatedBy((double)(-(double)num1463 * (float)npc.direction), default(Vector2));
                npc.rotation -= num1463 * (float)npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= (float)num1461)
                {
                    npc.ai[0] = 15f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    npc.netUpdate = true;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Microsoft.Xna.Framework.Color color9 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
            float num66 = 0f;
            Vector2 vector11 = new Vector2((float)(Main.npcTexture[npc.type].Width / 2), (float)(Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2));
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Microsoft.Xna.Framework.Rectangle frame6 = npc.frame;
            Microsoft.Xna.Framework.Color alpha15 = npc.GetAlpha(color9);
            float num212 = 1f - (float)npc.life / (float)npc.lifeMax;
            num212 *= num212;
            alpha15.R = (byte)((float)alpha15.R * num212);
            alpha15.G = (byte)((float)alpha15.G * num212);
            alpha15.B = (byte)((float)alpha15.B * num212);
            alpha15.A = (byte)((float)alpha15.A * num212);
            for (int num213 = 0; num213 < 4; num213++)
            {
                Vector2 position9 = npc.position;
                float num214 = Math.Abs(npc.Center.X - Main.player[Main.myPlayer].Center.X);
                float num215 = Math.Abs(npc.Center.Y - Main.player[Main.myPlayer].Center.Y);
                if (num213 == 0 || num213 == 2)
                {
                    position9.X = Main.player[Main.myPlayer].Center.X + num214;
                }
                else
                {
                    position9.X = Main.player[Main.myPlayer].Center.X - num214;
                }
                position9.X -= (float)(npc.width / 2);
                if (num213 == 0 || num213 == 1)
                {
                    position9.Y = Main.player[Main.myPlayer].Center.Y + num215;
                }
                else
                {
                    position9.Y = Main.player[Main.myPlayer].Center.Y - num215;
                }
                position9.Y -= (float)(npc.height / 2);
                Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(position9.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + vector11.X * npc.scale, position9.Y - Main.screenPosition.Y + (float)npc.height - (float)Main.npcTexture[npc.type].Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + vector11.Y * npc.scale + num66 + npc.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha15, npc.rotation, vector11, npc.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.position.X - Main.screenPosition.X + (float)(npc.width / 2) - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + vector11.X * npc.scale, npc.position.Y - Main.screenPosition.Y + (float)npc.height - (float)Main.npcTexture[npc.type].Height * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + vector11.Y * npc.scale + num66 + npc.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), npc.GetAlpha(color9), npc.rotation, vector11, npc.scale, spriteEffects, 0f);
            return false;
        }

    }
    #endregion
    */
}