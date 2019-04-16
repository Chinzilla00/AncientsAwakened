using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Raider
{

    [AutoloadBossHead]
    public class Raider : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Raider Ultima");
            Main.npcFrameCount[npc.type] = 8;

        }

        public override void SetDefaults()
        {
            npc.aiStyle = 0;
            npc.width = 202;
            npc.height = 196;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = true;
            npc.damage = 70;
            npc.defense = 30;
            npc.lifeMax = 30000;
            npc.value = Item.buyPrice(0, 10, 50, 0);
            npc.buffImmune[BuffID.Ichor] = true;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.netAlways = true;
            npc.friendly = false;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            bossBag = mod.ItemType("RaiderBag");
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore4"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore5"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore6"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGore7"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGoreJaw"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/RaiderGoreHorn"), 1f);
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RaiderTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofFright, Main.rand.Next(20, 40));
            }
            else
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SoulofFright, Main.rand.Next(25, 40));
                if (Main.rand.Next(10) == 0)
                {
                    //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BroodMask"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RaidEgg"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FulguriteBar"), Main.rand.Next(30, 64));

            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
            AAWorld.downedRaider = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 0.8f);  //boss damage increase in expermode
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore4"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BroodGore3"), 1f);
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.Electrified, Main.rand.Next(100, 180));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }

        public int projectileInterval = 300; //how long until you fire projectiles
        private int projectileTimer = 0;
        public const float AISTATE_RUNAWAY = -1f; //run awaaaaay
        public const float AISTATE_FLYABOVEPLAYER = 0f; //fly above the player
        public const float AISTATE_FLYBACKTOPLAYER = 1f; //uses this to fly back to the player after laying eggs, no idea why it's the second state lol
        public const float AISTATE_FLYTOPLAYER = 2f; //fly at the player slowly
        public const float AISTATE_CHARGEATPLAYER = 3f; //charge the player from the side
        public const float AISTATE_SPAWNEGGS = 4f; //spawn eggs
        public int ProjectileChoice = 0;

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 192;
                bool isCharging = (npc.ai[0] >= AISTATE_CHARGEATPLAYER && npc.ai[0] < AISTATE_SPAWNEGGS); //all ai states between charges
                if (isCharging && npc.frame.Y >= 192 * 8)
                {
                    npc.frame.Y = 192 * 4;
                }
                else
                if (!isCharging && npc.frame.Y >= 192 * 4)
                {
                    npc.frame.Y = 192 * 0;
                }
            }
        }


        public static Texture2D glowTex = null;
        public static Texture2D glowTex1 = null;
        public Color color;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/Raider_Glow1");
                glowTex1 = mod.GetTexture("Glowmasks/Raider_Glow2");
            }
            color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(npc.position), BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position), Color.Violet, BaseDrawing.GetLightColor(npc.position));
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, npc, Color.White);
            return false;
        }

        public override void AI()
        {
            if (Main.netMode != 1 && npc.ai[0] == AISTATE_FLYABOVEPLAYER) //only fire bombs when (attempting to) fly above the player
            {
                projectileTimer++;
                if (projectileTimer >= projectileInterval && projectileTimer % 10 == 0)
                {
                    if (projectileTimer > (projectileInterval + 50))
                        projectileTimer = 0;
                    Vector2 dir = new Vector2(npc.velocity.X * 3f + (2f * npc.direction), npc.velocity.Y * 0.5f + 1f);
                    Vector2 firePos = new Vector2(npc.Center.X + (71 * npc.direction), npc.Center.Y - 30f);
                    firePos = BaseMod.BaseUtility.RotateVector(npc.Center, firePos, npc.rotation); //+ (npc.direction == -1 ? (float)Math.PI : 0f)));
                    int projID = 0;
                    int npcID = 0;
                    if (ProjectileChoice == 1 || ProjectileChoice == 3 || ProjectileChoice == 4 || ProjectileChoice == 6 || ProjectileChoice == 8 || ProjectileChoice == 10)
                    {
                        int RocketNumber = 5;

                        if (Main.expertMode)
                        {
                            RocketNumber = 7;
                        }
                        for (int Rockets = 0; Rockets < RocketNumber; Rockets++)
                        {
                            npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<RaidRocket>(), 0);
                        }
                        Main.npc[npcID].netUpdate = true;
                    }
                    else
                    {
                        projID = Projectile.NewProjectile(firePos, dir, mod.ProjectileType("Raidsphere"), npc.damage / 2, 1, 255);
                        Main.projectile[projID].netUpdate = true;
                    }
                }
            }
            int numberOfMinions = 9; //max number of eggs/broodminis to spawn
            bool DespawnAttempt = false;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.knockBackResist = 0.2f * Main.expertKnockBack;
            npc.damage = npc.defDamage;
            if (Main.player[npc.target] == null || Main.dayTime)
            {
                DespawnAttempt = true;
            }
            else if (npc.target < 0 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
                Vector2 vector204 = Main.player[npc.target].Center - npc.Center;
                if (Main.player[npc.target].dead || vector204.Length() > 3000f)
                {
                    DespawnAttempt = true;
                }
            }
            if (Main.netMode != 1 && DespawnAttempt)
            {
                if (npc.ai[0] != AISTATE_RUNAWAY)
                    npc.netUpdate = true;
                npc.ai[0] = AISTATE_RUNAWAY;
            }

            if (npc.ai[0] == AISTATE_RUNAWAY)
            {
                npc.noTileCollide = true;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                if (npc.timeLeft < 10)
                    npc.timeLeft = 10;
                npc.velocity.X *= 0.9f;
                npc.velocity.Y -= 0.1f;
                if (npc.velocity.Y > 15f) npc.velocity.Y = 15f;
                npc.rotation = 0f;
                if (npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
            }
            else
            if (npc.ai[0] == AISTATE_FLYABOVEPLAYER)
            {
                npc.TargetClosest(true);
                if (npc.Center.X < Main.player[npc.target].Center.X - 2f)
                {
                    npc.direction = 1;
                }
                if (npc.Center.X > Main.player[npc.target].Center.X + 2f)
                {
                    npc.direction = -1;
                }
                npc.spriteDirection = npc.direction;
                if (npc.collideX)
                {
                    npc.velocity.X = npc.velocity.X * (-npc.oldVelocity.X * 0.6f);
                    if (npc.velocity.X > 5f)
                    {
                        npc.velocity.X = 5f;
                    }
                    if (npc.velocity.X < -5f)
                    {
                        npc.velocity.X = -5f;
                    }
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.velocity.Y * (-npc.oldVelocity.Y * 0.6f);
                    if (npc.velocity.Y > 5f)
                    {
                        npc.velocity.Y = 5f;
                    }
                    if (npc.velocity.Y < -5f)
                    {
                        npc.velocity.Y = -5f;
                    }
                }
                Vector2 value51 = Main.player[npc.target].Center - npc.Center;
                value51.Y -= 200f;
                if (value51.Length() > 800f)
                {
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                }
                else if (value51.Length() > 80f)
                {
                    float scaleFactor15 = 6f;
                    float num1306 = 30f;
                    value51.Normalize();
                    value51 *= scaleFactor15;
                    npc.velocity = ((npc.velocity * (num1306 - 1f)) + value51) / num1306;
                }
                else if (npc.velocity.Length() > 2f)
                {
                    npc.velocity *= 0.99f;
                }
                else if (npc.velocity.Length() < 1f)
                {
                    npc.velocity *= 1.11f;
                }
                npc.ai[1] += 1f;
                if (npc.ai[1] >= 120f && Main.netMode != 1)
                {
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    npc.netUpdate = true;
                    while (npc.ai[0] == 0f)
                    {
                        int num1307 = Main.rand.Next(3);
                        if (num1307 == 0 && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                        {
                            npc.ai[0] = 2f;
                        }
                        else if (num1307 == 1)
                        {
                            npc.ai[0] = 3f;
                        }
                        else if (num1307 == 2 && NPC.CountNPCS(mod.NPCType("RaidEgg")) + NPC.CountNPCS(mod.NPCType("Raidmini")) < numberOfMinions)
                        {
                            npc.ai[0] = AISTATE_SPAWNEGGS;
                        }
                    }
                    return;
                }
            }
            else
            if (npc.ai[0] == AISTATE_FLYBACKTOPLAYER)
            {
                npc.collideX = false;
                npc.collideY = false;
                npc.noTileCollide = true;
                npc.knockBackResist = 0f;
                if (npc.target < 0 || !Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                }
                if (npc.velocity.X < 0f)
                {
                    npc.direction = -1;
                }
                else if (npc.velocity.X > 0f)
                {
                    npc.direction = 1;
                }
                npc.spriteDirection = npc.direction;
                Vector2 value52 = Main.player[npc.target].Center - npc.Center;
                if (value52.Length() < 300f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                }
                float scaleFactor16 = 7f + (value52.Length() / 100f);
                float num1308 = 25f;
                value52.Normalize();
                value52 *= scaleFactor16;
                npc.velocity = ((npc.velocity * (num1308 - 1f)) + value52) / num1308;
                return;
            }
            else
            if (npc.ai[0] == AISTATE_FLYTOPLAYER)
            {
                npc.damage = (int)((double)npc.defDamage * 0.5);
                npc.knockBackResist = 0f;
                if (npc.target < 0 || !Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                }
                if (Main.player[npc.target].Center.X - 10f < npc.Center.X)
                {
                    npc.direction = -1;
                }
                else if (Main.player[npc.target].Center.X + 10f > npc.Center.X)
                {
                    npc.direction = 1;
                }
                npc.spriteDirection = npc.direction;
                if (npc.collideX)
                {
                    npc.velocity.X = npc.velocity.X * (-npc.oldVelocity.X * 0.5f);
                    if (npc.velocity.X > 5f)
                    {
                        npc.velocity.X = 5f;
                    }
                    if (npc.velocity.X < -5f)
                    {
                        npc.velocity.X = -5f;
                    }
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.velocity.Y * (-npc.oldVelocity.Y * 0.5f);
                    if (npc.velocity.Y > 5f)
                    {
                        npc.velocity.Y = 5f;
                    }
                    if (npc.velocity.Y < -5f)
                    {
                        npc.velocity.Y = -5f;
                    }
                }
                Vector2 value53 = Main.player[npc.target].Center - npc.Center;
                value53.Y -= 20f;
                npc.ai[2] += 0.0222222228f;
                if (Main.expertMode)
                {
                    npc.ai[2] += 0.0166666675f;
                }
                float scaleFactor17 = 4f + npc.ai[2] + (value53.Length() / 120f);
                float num1309 = 20f;
                value53.Normalize();
                value53 *= scaleFactor17;
                npc.velocity = ((npc.velocity * (num1309 - 1f)) + value53) / num1309;
                npc.ai[1] += 1f;
                if (npc.ai[1] > 240f || !Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 0f;
                    return;
                }
            }
            else
            if (npc.ai[0] == AISTATE_CHARGEATPLAYER)
            {
                npc.knockBackResist = 0f;
                npc.noTileCollide = true;
                if (npc.velocity.X < 0f)
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
                npc.spriteDirection = npc.direction;
                Vector2 value54 = Main.player[npc.target].Center - npc.Center;
                value54.Y -= 12f;
                if (npc.Center.X > Main.player[npc.target].Center.X)
                {
                    value54.X += 400f;
                }
                else
                {
                    value54.X -= 400f;
                }
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) > 350f && Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) < 20f)
                {
                    npc.ai[0] = 3.1f;
                    npc.ai[1] = 0f;
                }
                npc.ai[1] += 0.0333333351f;
                float scaleFactor18 = 8f + npc.ai[1];
                float num1310 = 4f;
                value54.Normalize();
                value54 *= scaleFactor18;
                npc.velocity = ((npc.velocity * (num1310 - 1f)) + value54) / num1310;
                return;
            }
            else
            if (npc.ai[0] == 3.1f) //sub charge
            {
                npc.knockBackResist = 0f;
                npc.noTileCollide = true;
                Vector2 vector206 = Main.player[npc.target].Center - npc.Center;
                vector206.Y -= 12f;
                float scaleFactor19 = 16f;
                float num1311 = 8f;
                vector206.Normalize();
                vector206 *= scaleFactor19;
                npc.velocity = ((npc.velocity * (num1311 - 1f)) + vector206) / num1311;
                if (npc.velocity.X < 0f)
                {
                    npc.direction = -1;
                }
                else
                {
                    npc.direction = 1;
                }
                npc.spriteDirection = npc.direction;
                npc.ai[1] += 1f;
                if (npc.ai[1] > 10f)
                {
                    npc.velocity = vector206;
                    if (npc.velocity.X < 0f)
                    {
                        npc.direction = -1;
                    }
                    else
                    {
                        npc.direction = 1;
                    }
                    npc.ai[0] = 3.2f;
                    npc.ai[1] = 0f;
                    npc.ai[1] = (float)npc.direction;
                    return;
                }
            }
            else
            if (npc.ai[0] == 3.2f) //sub charge
            {
                npc.damage = (int)((double)npc.defDamage * 1.3);
                npc.collideX = false;
                npc.collideY = false;
                npc.knockBackResist = 0f;
                npc.noTileCollide = true;
                npc.ai[2] += 0.0333333351f;
                npc.velocity.X = (16f + npc.ai[2]) * npc.ai[1];
                if ((npc.ai[1] > 0f && npc.Center.X > Main.player[npc.target].Center.X + 260f) || (npc.ai[1] < 0f && npc.Center.X < Main.player[npc.target].Center.X - 260f))
                {
                    if (!Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                    }
                    else if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) > 800f)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                    }
                }
                return;
            }
            else
            if (npc.ai[0] == AISTATE_SPAWNEGGS)
            {
                npc.ai[0] = 0f;
                npc.TargetClosest(true);
                if (Main.netMode != 1)
                {
                    npc.ai[1] = -1f;
                    npc.ai[2] = -1f;
                    for (int num1312 = 0; num1312 < 1000; num1312++)
                    {
                        int num1313 = (int)Main.player[npc.target].Center.X / 16;
                        int num1314 = (int)Main.player[npc.target].Center.Y / 16;
                        int num1315 = 30 + (num1312 / 50);
                        int num1316 = 20 + (num1312 / 75);
                        num1313 += Main.rand.Next(-num1315, num1315 + 1);
                        num1314 += Main.rand.Next(-num1316, num1316 + 1);
                        if (!WorldGen.SolidTile(num1313, num1314))
                        {
                            while (!WorldGen.SolidTile(num1313, num1314) && (double)num1314 < Main.worldSurface)
                            {
                                num1314++;
                            }
                            if ((new Vector2((float)((num1313 * 16) + 8), (float)((num1314 * 16) + 8)) - Main.player[npc.target].Center).Length() < 600f)
                            {
                                npc.ai[0] = 4.1f;
                                npc.ai[1] = (float)num1313;
                                npc.ai[2] = (float)num1314;
                                break;
                            }
                        }
                    }
                }
                npc.netUpdate = true;
                return;
            }
            else
            if (npc.ai[0] == 4.1f) //sub spawning eggs
            {
                if (npc.velocity.X < -2f)
                {
                    npc.direction = -1;
                }
                else if (npc.velocity.X > 2f)
                {
                    npc.direction = 1;
                }
                npc.spriteDirection = npc.direction;
                npc.noTileCollide = true;
                int num1317 = (int)npc.ai[1];
                int num1318 = (int)npc.ai[2];
                float x2 = (float)((num1317 * 16) + 8);
                float y2 = (float)((num1318 * 16) - 20);
                Vector2 vector207 = new Vector2(x2, y2);
                vector207 -= npc.Center;
                float num1319 = 6f + (vector207.Length() / 150f);
                if (num1319 > 10f)
                {
                    num1319 = 10f;
                }
                float num1320 = 10f;
                if (vector207.Length() < 10f)
                {
                    npc.ai[0] = 4.2f;
                }
                vector207.Normalize();
                vector207 *= num1319;
                npc.velocity = ((npc.velocity * (num1320 - 1f)) + vector207) / num1320;
                return;
            }
            else
            if (npc.ai[0] == 4.2f) //sub spawning eggs
            {
                npc.knockBackResist = 0f;
                npc.noTileCollide = true;
                int num1321 = (int)npc.ai[1];
                int num1322 = (int)npc.ai[2];
                float x3 = (float)((num1321 * 16) + 8);
                float y3 = (float)((num1322 * 16) - 20);
                Vector2 vector208 = new Vector2(x3, y3);
                vector208 -= npc.Center;
                float num1323 = 4f;
                float num1324 = 2f;
                if (Main.netMode != 1 && vector208.Length() < 4f)
                {
                    int num1325 = 70;
                    if (Main.expertMode)
                    {
                        num1325 = (int)((double)num1325 * 0.75);
                    }
                    npc.ai[3] += 1f;
                    if (npc.ai[3] == (float)num1325)
                    {
                        NPC.NewNPC((num1321 * 16) + 8, num1322 * 16, mod.NPCType("RaidEgg"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                    }
                    else if (npc.ai[3] == (float)(num1325 * 2))
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 0f;
                        if (NPC.CountNPCS(mod.NPCType("RaidEgg")) + NPC.CountNPCS(mod.NPCType("Raidmini")) < numberOfMinions && Main.rand.Next(3) != 0)
                        {
                            npc.ai[0] = 4f;
                        }
                        else if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                        {
                            npc.ai[0] = 1f;
                        }
                    }
                }
                if (vector208.Length() > num1323)
                {
                    vector208.Normalize();
                    vector208 *= num1323;
                }
                npc.velocity = ((npc.velocity * (num1324 - 1f)) + vector208) / num1324;
                return;
            }
        }
    }
}