using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{

    public class ShenA : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon Awakened; Unyieldng Chaos Incarnate");
            Main.npcFrameCount[npc.type] = 5;
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.height = 200;
            npc.width = 444;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.knockBackResist = 0f;
            npc.damage = 230;
            npc.defense = 230;
            npc.lifeMax = 1600000;
            npc.value = Item.buyPrice(40, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.alpha = 255;
            npc.DeathSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/ShenA");
            musicPriority = MusicPriority.BossHigh;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax);
        }

        public bool spawnalpha = false;

        public override void AI()
        {
            if (npc.alpha > 0 && spawnalpha == false)
            {
                npc.alpha -= 5;
            }
            if (npc.alpha <= 0)
            {
                npc.alpha = 0;
                spawnalpha = true;
            }
            bool expertMode = Main.expertMode;
            float expertDamage = expertMode ? (0.50f * Main.damageMultiplier) : 1f;
            bool ninthHealth = npc.life <= npc.lifeMax * 0.9;
            bool eighthHealth = npc.life <= npc.lifeMax * 0.8;
            bool seventhHealth = npc.life <= npc.lifeMax * 0.7;
            bool sixthHealth = npc.life <= npc.lifeMax * 0.6;
            bool fifthHealth = npc.life <= npc.lifeMax * 0.5;
            bool fourthHealth = npc.life <= npc.lifeMax * 0.4;
            bool thirdHealth = npc.life <= npc.lifeMax * 0.3;
            bool secondHealth = npc.life <= npc.lifeMax * 0.2;
            bool firstHealth = npc.life <= npc.lifeMax * 0.1;
            int flareCount = 10;
            bool Charge = npc.ai[3] < 10f;
            float teleportLocation = 0f;
            int teleChoice = Main.rand.Next(2);

            int aiChangeRate = expertMode ? 50 : 60;
            float npcVelocity = expertMode ? 0.5f : 0.4f;
            float scaleFactor = expertMode ? 11f : 10.8f;
            float playerRunAcceleration = Main.player[npc.target].velocity.Y == 0f ? Math.Abs(Main.player[npc.target].moveSpeed * 0.5f) : (Main.player[npc.target].runAcceleration * 1f);
            if (playerRunAcceleration <= 1f)
            {
                playerRunAcceleration = 1f;
            }
            int chargeTime = expertMode ? 26 : 27;
            float chargeSpeed = playerRunAcceleration * (expertMode ? 27f : 26.5f);
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
            float num1463 = 6.28318548f / (num1461 / 2);
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
            float npcRotation = (float)Math.Atan2(player.Center.Y - vectorCenter.Y, player.Center.X - vectorCenter.X);
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
                if (npcRotation - npc.rotation > 3.1415926535897931)
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
                if (npc.rotation - npcRotation > 3.1415926535897931)
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
                npc.dontTakeDamage = false;
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
                if (npc.ai[2] == num1457 - 30)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1464)
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
                    npc.ai[1] = 300 * Math.Sign((vectorCenter - player.Center).X);
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
                if (npc.ai[2] >= aiChangeRate)
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
                    if (fifthHealth) //checks if can initiate phase 2
                    {
                        num1472 = 4;
                    }
                    if (num1472 == 1)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
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
                    Vector2 vector171 = Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f;
                    vector171 = vector171.RotatedBy((num1474 - (num1473 / 2 - 1)) * 3.1415926535897931 / (float)num1473, default(Vector2)) + vectorCenter;
                    Vector2 value18 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);

                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= chargeTime)
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
                    npc.ai[1] = 300 * Math.Sign((vectorCenter - player.Center).X);
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
                if (npc.ai[2] % num1455 == 0f) //fire flare bombs from mouth
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60); //changed
                    if (Main.netMode != 1)
                    {
                        if (NPC.CountNPCS(mod.NPCType("DetonatingFlare")) < flareCount)
                        {
                            Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (npc.width + 20) / 2f + vectorCenter;
                            float speedX = Main.rand.Next(15, 23);
                            int detFlare = NPC.NewNPC((int)vector6.X, (int)vector6.Y - 100, mod.NPCType("DetonatingFlare"), 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[detFlare].localAI[1] = Main.rand.Next(5, 9);
                            Main.npc[detFlare].localAI[2] = speedX / 100;
                        }
                        int damage = expertMode ? 150 : 164;
                        int randomTime = Main.rand.Next(500, 1001);
                        Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (npc.width + 20) / 2f + vectorCenter;
                        int projectile = Projectile.NewProjectile((int)vector173.X, (int)vector173.Y - 100, Main.rand.Next(-200, 201) * 0.13f, Main.rand.Next(-200, 201) * 0.13f, mod.ProjectileType("DiscordianInferno"), damage, 0f, Main.myPlayer, 0f, 0f); //changed
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
                if (npc.ai[2] >= num1454)
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
                if (npc.ai[2] == num1457 - 30)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (Main.netMode != 1 && npc.ai[2] == num1457 - 30)
                {
                    int randomTime = Main.rand.Next(200, 400);
                    int randomTime2 = Main.rand.Next(100, 300);
                    Vector2 vector174 = npc.rotation.ToRotationVector2() * (Vector2.UnitX * npc.direction) * (npc.width + 20) / 2f + vectorCenter;
                    int projectile = Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("DiscordianBreath"), 0, 0f, Main.myPlayer, 1f, npc.target + 1); //changed
                    int projectile2 = Projectile.NewProjectile(vector174.X, vector174.Y, -(float)npc.direction * 2, 8f, mod.ProjectileType("DiscordianBreath"), 0, 0f, Main.myPlayer, 0f, 0f); //changed
                    Main.projectile[projectile].timeLeft = randomTime;
                    Main.projectile[projectile2].timeLeft = randomTime2;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1457)
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
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == num1458 - 60)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1458)
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
                    npc.ai[1] = 300 * Math.Sign((vectorCenter - player.Center).X);
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
                if (npc.ai[2] >= aiChangeRate)
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
                    if (seventhHealth) //checks if can initiate phase 3
                    {
                        num1478 = 4;
                    }
                    if (num1478 == 1)
                    {
                        npc.ai[0] = 6f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
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
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
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
                    Vector2 vector176 = Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f;
                    vector176 = vector176.RotatedBy((num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (float)num1479, default(Vector2)) + vectorCenter;
                    Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);

                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= chargeTime)
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
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (npc.ai[2] % num1462 == 0f)
                {
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60); //changed
                    if (Main.netMode != 1)
                    {
                        if (NPC.CountNPCS(mod.NPCType("DetonatingFlare2")) < flareCount)
                        {
                            Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (npc.width + 20) / 2f + vectorCenter;
                            int detFlare = NPC.NewNPC((int)vector6.X, (int)vector6.Y - 100, mod.NPCType("DetonatingFlare2"), 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[detFlare].localAI[3] = Main.rand.Next(3, 9);
                        }
                        int damage = expertMode ? 85 : 90;
                        Vector2 vector173 = Vector2.Normalize(player.Center - vectorCenter) * (npc.width + 20) / 2f + vectorCenter;

                    }
                }
                npc.velocity = npc.velocity.RotatedBy(-(double)num1463 * (float)npc.direction, default(Vector2));
                npc.rotation -= num1463 * npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1461)
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
                if (npc.ai[2] == num1457 - 30)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (Main.netMode != 1 && npc.ai[2] == num1457 - 30)
                {
                    Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("DiscordianInferno"), 0, 0f, Main.myPlayer, 1f, npc.target + 1); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1457)
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
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == num1459 - 60)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1459)
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
                    npc.ai[1] = 300 * Math.Sign((vectorCenter - player.Center).X);
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
                if (npc.ai[2] >= aiChangeRate)
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
                    if (secondHealth) //checks if can initiate phase 4
                    {
                        num1478 = 4;
                    }
                    if (num1478 == 1)
                    {
                        npc.ai[0] = 11f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vectorCenter) * chargeSpeed;
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
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
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
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
                    Vector2 vector176 = Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f;
                    vector176 = vector176.RotatedBy((num1480 - (num1479 / 2 - 1)) * 3.1415926535897931 / (float)num1479, default(Vector2)) + vectorCenter;
                    Vector2 value21 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);

                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= chargeTime)
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
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                if (npc.ai[2] == 0f)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (npc.ai[2] % num1462 == 0f)
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
                            Vector2 vector6 = Vector2.Normalize(player.Center - vectorCenter) * (npc.width + 20) / 2f + vectorCenter;
                            float speedX = Main.rand.Next(10, 16);
                            int detFlare = NPC.NewNPC((int)vector6.X, (int)vector6.Y - 100, randomSpawn, 0, 0f, 0f, 0f, 0f, 255);
                            Main.npc[detFlare].localAI[1] = Main.rand.Next(5, 11);
                            Main.npc[detFlare].localAI[2] = speedX / 100;
                            Main.npc[detFlare].localAI[3] = Main.rand.Next(3, 11);
                        }
                        int damage = expertMode ? 90 : 100;
                        Vector2 vector = Vector2.Normalize(player.Center - vectorCenter) * (npc.width + 20) / 2f + vectorCenter;

                    }
                }
                npc.velocity = npc.velocity.RotatedBy(-(double)num1463 * (float)npc.direction, default(Vector2));
                npc.rotation -= num1463 * npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1461)
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
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, 0f, 0.02f);
                if (npc.ai[2] == num1457 - 30)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92); //changed
                }
                if (Main.netMode != 1 && npc.ai[2] == num1457 - 30)
                {
                    Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("DiscordianInferno"), 0, 0f, Main.myPlayer, 1f, npc.target + 1); //changed
                    int randomTime = Main.rand.Next(200, 400);
                    int randomTime2 = Main.rand.Next(100, 300);
                    Vector2 vector174 = npc.rotation.ToRotationVector2() * (Vector2.UnitX * npc.direction) * (npc.width + 20) / 2f + vectorCenter;
                    int projectile = Projectile.NewProjectile(vectorCenter.X, vectorCenter.Y, 0f, 0f, mod.ProjectileType("DiscordianBreath"), 0, 0f, Main.myPlayer, 1f, npc.target + 1); //changed
                    int projectile2 = Projectile.NewProjectile(vector174.X, vector174.Y, -(float)npc.direction * 2, 8f, mod.ProjectileType("DiscordianBreath"), 0, 0f, Main.myPlayer, 0f, 0f); //changed
                    Main.projectile[projectile].timeLeft = randomTime;
                    Main.projectile[projectile2].timeLeft = randomTime2;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1457)
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
                npc.dontTakeDamage = false;
                npc.chaseable = false;
                if (npc.ai[2] < num1459 - 90)
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
                if (npc.ai[2] == num1459 - 60)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92, 1f, 0f);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1459)
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
                    npc.ai[1] = 360 * Math.Sign((vectorCenter - player.Center).X);
                }
                Vector2 value7 = player.Center + new Vector2(npc.ai[1], 0) - vectorCenter; //teleport distance
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
                if (npc.ai[2] >= aiChangeRate)
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
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
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
                    Vector2 vector11 = Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f;
                    vector11 = vector11.RotatedBy((m - (num34 / 2 - 1)) * 3.1415926535897931 / (float)num34, default(Vector2)) + vectorCenter;
                    Vector2 value8 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);

                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= chargeTime)
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
                npc.dontTakeDamage = false;
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
                if (npc.ai[2] == num1460 / 2)
                {
                    Main.PlaySound(29, (int)vectorCenter.X, (int)vectorCenter.Y, 92, 1f, 0f);
                }
                if (Main.netMode != 1 && npc.ai[2] == num1460 / 2)
                {
                    if (npc.ai[1] == 0f)
                    {
                        npc.ai[1] = 300 * Math.Sign((vectorCenter - player.Center).X);
                    }
                    Vector2 center = player.Center + new Vector2(-npc.ai[1], 0); //teleport distance
                    vectorCenter = (npc.Center = center);
                    int num36 = Math.Sign(player.Center.X - vectorCenter.X);
                    npc.rotation -= num1463 * npc.direction;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1460)
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
                npc.velocity = npc.velocity.RotatedBy(-(double)num1463 * (float)npc.direction, default(Vector2));
                npc.rotation -= num1463 * npc.direction;
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num1461)
                {
                    npc.ai[0] = 15f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] += 1f;
                    npc.netUpdate = true;
                }
            }
            npc.frameCounter++;
            if (npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 364;
                if (npc.frame.Y > (364 * 4))
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
            if (player.Center.X > npc.Center.X) // so it faces the player
            {
                npc.spriteDirection = -1;
            }
            else
            {
                npc.spriteDirection = 1;
            }
        }

        public bool Health9 = false;
        public bool Health8 = false;
        public bool Health7 = false;
        public bool Health6 = false;
        public bool Health5 = false;
        public bool Health4 = false;
        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;
        public bool HealthOneHalf = false;

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= npc.lifeMax * 0.9f && !Health9)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Face it, child..! You’ll never defeat the living embodiment of disarray itself..!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health9 = true;
            }
            if (npc.life <= npc.lifeMax * 0.8f && !Health8)
            {
                if (Main.netMode != 1) BaseUtility.Chat("You’re still going? How amusing…", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health8 = true;
            }
            if (npc.life <= npc.lifeMax * 0.7f && !Health7)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Putting up a fight when you know Death is inevitable...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health7 = true;
            }
            if (npc.life <= npc.lifeMax * 0.6f && !Health6)
            {
                if (Main.netMode != 1) BaseUtility.Chat("Now stop making this hard! Stand still and take it like a man!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health6 = true;
            }
            if (npc.life <= npc.lifeMax * 0.5f && !Health5)
            {
                if (Main.netMode != 1) BaseUtility.Chat("This is getting real obnoxious chasing you around, you know..!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health5 = true;
            }
            if (npc.life <= npc.lifeMax * 0.4f && !Health4)
            {
                if (Main.netMode != 1) BaseUtility.Chat("DIE ALREADY YOU INSIGNIFICANT LITTLE WORM!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health4 = true;
            }
            if (npc.life <= npc.lifeMax * 0.3f && !Health3)
            {
                if (Main.netMode != 1) BaseUtility.Chat("WHAT?! HOW HAVE YOU-- ENOUGH! YOU WILL KNOW WHAT IT MEANS TO FEEL UNYIELDING CHAOS!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health3 = true;
            }
            if (npc.life <= npc.lifeMax * 0.2f && !Health2)
            {
                if (Main.netMode != 1) BaseUtility.Chat("NO! I WILL NOT LOSE! NOT TO YOU!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health2 = true;
            }
            if (npc.life <= npc.lifeMax * 0.1f && !Health1)
            {
                if (Main.netMode != 1) BaseUtility.Chat("GRAAAAAAAAAH!!!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                Health1 = true;
            }
            if (npc.life <= npc.lifeMax * 0.05f && !Health1)
            {
                if (Main.netMode != 1) BaseUtility.Chat("I AM SHEN DORAGON! BRINGER OF DEATH AND DISASTER, AND I WILL NOT BE OUTDONE BY A HAIRLESS APE! PREPARE FOR YOUR DEMISE!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                HealthOneHalf = true;
            }
            if (Health3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/LastStand");
            }
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                npc.width = 400;
                npc.height = 350;
                npc.position.X = npc.position.X - npc.width / 2;
                npc.position.Y = npc.position.Y - npc.height / 2;
                for (int num621 = 0; num621 < 60; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 90; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 3f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].velocity *= 2f;
                }
            }
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/Bosses/Shen/ShenA_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(sb, glowTex, 0, npc, AAColor.Shen2);
            BaseDrawing.DrawAfterimage(sb, glowTex, 0, npc, 0.8f, 1f, 4, false, 0f, 0f, AAColor.Shen2);
            return false;
        }
    }
    
}
