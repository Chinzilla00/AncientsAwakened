using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using BaseMod;

namespace AAMod.NPCs.Bosses.Serpent
{
    [AutoloadBossHead]
    public class Serpent : ModNPC
	{
        public bool flies = false;
        public float speed = 30f;
        public float turnSpeed = 0.25f;
        bool TailSpawned = false;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Subzero Serpent");
			NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 4;

        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
        }

        public override void SetDefaults()
		{
            npc.damage = 30;
            npc.npcSlots = 7f;
            npc.width = 32;
            npc.height = 76;
            npc.defense = 20;
            npc.lifeMax = 6000;
            npc.aiStyle = 6;
            aiType = -1;
            animationType = 10;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.value = Item.buyPrice(0, 7, 0, 0);
            npc.alpha = 255;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
            bossBag = mod.ItemType("SerpentBag");
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Boss6");
            npc.scale = 1.15f;
        }
        private bool fireAttack;
        private int attackTimer;
        private bool tongue;

        public override void AI()
        {
            int FrameHeight = 88;
            if (Main.rand.Next(10) == 0 && tongue == false)
            {
                tongue = true;
            }
            if (tongue == true)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += FrameHeight;
                    if (npc.frame.Y > (FrameHeight * 3))
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                        tongue = false;
                    }
                }
            }
            Player player = Main.player[npc.target];
            float dist = npc.Distance(player.Center);
            if (dist > 300 & Main.rand.Next(20) == 1 && fireAttack == false)
            {
                fireAttack = true;
            }
            if (fireAttack == true)
            {
                attackTimer++;
                if ((attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79) && !npc.HasBuff(103))
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        if (Main.netMode != 1)
                        {
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
                            Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, npc.velocity.X * 2f, npc.velocity.Y * 2f, mod.ProjectileType("SnowBreath"), npc.damage, 0, Main.myPlayer);
                        }
                    }
                }
                
                if (attackTimer >= 80)
                {
                    fireAttack = false;
                    attackTimer = 0;
                }
            }
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("SnowDust"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            bool expertMode = Main.expertMode;
            float speedMult = expertMode ? 1.3f : 1.4f;
            float life = (float)npc.life;
            float totalLife = (float)npc.lifeMax;
            speed = 15f * (speedMult - (life / totalLife));
            turnSpeed = 0.15f * (speedMult - (life / totalLife));
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (npc.target < 0 || npc.target == 255 || player.dead)
            {
                npc.TargetClosest(true);
            }
            npc.velocity.Length();
            npc.alpha -= 42;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            if (!TailSpawned)
            {
                npc.realLife = npc.whoAmI;
                int latestNPC = npc.whoAmI;
                int segment = 0;
                int SerpentLength = 3;
                for (int i = 0; i < SerpentLength; ++i)
                {
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaBody"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaArms"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    segment += 1;
                }
                latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaTail"), npc.whoAmI, 0, latestNPC);
                Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                TailSpawned = true;
            }
            int num180 = (int)(npc.position.X / 16f) - 1;
            int num181 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
            int num182 = (int)(npc.position.Y / 16f) - 1;
            int num183 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            if (!flies)
            {
                for (int num952 = num180; num952 < num181; num952++)
                {
                    for (int num953 = num182; num953 < num183; num953++)
                    {
                        if (Main.tile[num952, num953] != null && ((Main.tile[num952, num953].nactive() && (Main.tileSolid[(int)Main.tile[num952, num953].type] || (Main.tileSolidTop[(int)Main.tile[num952, num953].type] && Main.tile[num952, num953].frameY == 0))) || Main.tile[num952, num953].liquid > 64))
                        {
                            Vector2 vector105;
                            vector105.X = (float)(num952 * 16);
                            vector105.Y = (float)(num953 * 16);
                            if (npc.position.X + (float)npc.width > vector105.X && npc.position.X < vector105.X + 16f && npc.position.Y + (float)npc.height > vector105.Y && npc.position.Y < vector105.Y + 16f)
                            {
                                flies = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flies)
            {
                npc.localAI[1] = 1f;
                Rectangle rectangle12 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int num954 = 1000;
                bool flag95 = true;
                if (npc.position.Y > player.position.Y)
                {
                    for (int num955 = 0; num955 < 255; num955++)
                    {
                        if (Main.player[num955].active)
                        {
                            Rectangle rectangle13 = new Rectangle((int)Main.player[num955].position.X - num954, (int)Main.player[num955].position.Y - num954, num954 * 2, num954 * 2);
                            if (rectangle12.Intersects(rectangle13))
                            {
                                flag95 = false;
                                break;
                            }
                        }
                    }
                    if (flag95)
                    {
                        flies = true;
                    }
                }
            }
            if (player.dead)
            {
                flies = false;
                npc.velocity.Y = npc.velocity.Y + 1f;
                if ((double)npc.position.Y > Main.worldSurface * 16.0)
                {
                    npc.velocity.Y = npc.velocity.Y + 1f;
                }
                if ((double)npc.position.Y > Main.rockLayer * 16.0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == npc.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }
            float num188 = speed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num191 = player.position.X + (float)(player.width / 2);
            float num192 = player.position.Y + (float)(player.height / 2);
            num191 = (float)((int)(num191 / 16f) * 16);
            num192 = (float)((int)(num192 / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
            if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    num191 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector18.X;
                    num192 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                npc.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                int num194 = npc.width;
                num193 = (num193 - (float)num194) / num193;
                num191 *= num193;
                num192 *= num193;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num191;
                npc.position.Y = npc.position.Y + num192;
            }
            else
            {
                if (!flies)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + (turnSpeed * 0.75f);
                    if (npc.velocity.Y > num188)
                    {
                        npc.velocity.Y = num188;
                    }
                    if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.4)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - num189 * 1.1f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num189 * 1.1f;
                        }
                    }
                    else if (npc.velocity.Y == num188)
                    {
                        if (npc.velocity.X < num191)
                        {
                            npc.velocity.X = npc.velocity.X + num189;
                        }
                        else if (npc.velocity.X > num191)
                        {
                            npc.velocity.X = npc.velocity.X - num189;
                        }
                    }
                    else if (npc.velocity.Y > 4f)
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num189 * 0.9f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num189 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (!flies && npc.behindTiles && npc.soundDelay == 0)
                    {
                        float num195 = num193 / 40f;
                        if (num195 < 10f)
                        {
                            num195 = 10f;
                        }
                        if (num195 > 20f)
                        {
                            num195 = 20f;
                        }
                        npc.soundDelay = (int)num195;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
                    }
                    num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                    float num196 = System.Math.Abs(num191);
                    float num197 = System.Math.Abs(num192);
                    float num198 = num188 / num193;
                    num191 *= num198;
                    num192 *= num198;
                    bool flag21 = false;
                    if (!flag21)
                    {
                        if ((npc.velocity.X > 0f && num191 > 0f) || (npc.velocity.X < 0f && num191 < 0f) || (npc.velocity.Y > 0f && num192 > 0f) || (npc.velocity.Y < 0f && num192 < 0f))
                        {
                            if (npc.velocity.X < num191)
                            {
                                npc.velocity.X = npc.velocity.X + num189;
                            }
                            else
                            {
                                if (npc.velocity.X > num191)
                                {
                                    npc.velocity.X = npc.velocity.X - num189;
                                }
                            }
                            if (npc.velocity.Y < num192)
                            {
                                npc.velocity.Y = npc.velocity.Y + num189;
                            }
                            else
                            {
                                if (npc.velocity.Y > num192)
                                {
                                    npc.velocity.Y = npc.velocity.Y - num189;
                                }
                            }
                            if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((npc.velocity.X > 0f && num191 < 0f) || (npc.velocity.X < 0f && num191 > 0f)))
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y + num189 * 2f;
                                }
                                else
                                {
                                    npc.velocity.Y = npc.velocity.Y - num189 * 2f;
                                }
                            }
                            if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((npc.velocity.Y > 0f && num192 < 0f) || (npc.velocity.Y < 0f && num192 > 0f)))
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X + num189 * 2f;
                                }
                                else
                                {
                                    npc.velocity.X = npc.velocity.X - num189 * 2f;
                                }
                            }
                        }
                        else
                        {
                            if (num196 > num197)
                            {
                                if (npc.velocity.X < num191)
                                {
                                    npc.velocity.X = npc.velocity.X + num189 * 1.1f;
                                }
                                else if (npc.velocity.X > num191)
                                {
                                    npc.velocity.X = npc.velocity.X - num189 * 1.1f;
                                }
                                if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
                                {
                                    if (npc.velocity.Y > 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + num189;
                                    }
                                    else
                                    {
                                        npc.velocity.Y = npc.velocity.Y - num189;
                                    }
                                }
                            }
                            else
                            {
                                if (npc.velocity.Y < num192)
                                {
                                    npc.velocity.Y = npc.velocity.Y + num189 * 1.1f;
                                }
                                else if (npc.velocity.Y > num192)
                                {
                                    npc.velocity.Y = npc.velocity.Y - num189 * 1.1f;
                                }
                                if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
                                {
                                    if (npc.velocity.X > 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X + num189;
                                    }
                                    else
                                    {
                                        npc.velocity.X = npc.velocity.X - num189;
                                    }
                                }
                            }
                        }
                    }
                }
                npc.rotation = (float)System.Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
                if (flies)
                {
                    if (npc.localAI[0] != 1f)
                    {
                        npc.netUpdate = true;
                    }
                    npc.localAI[0] = 1f;
                }
                else
                {
                    if (npc.localAI[0] != 0f)
                    {
                        npc.netUpdate = true;
                    }
                    npc.localAI[0] = 0f;
                }
                if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
                {
                    npc.netUpdate = true;
                    return;
                }
            }
        }

        public override void NPCLoot()
		{
            if (!Main.expertMode)
            {
                AAWorld.downedSerpent = true;
                npc.DropLoot(mod.ItemType("SnowMana"), 10, 15);
                string[] lootTable = { "BlizardBuster", "SerpentSpike", "Icepick", "SerpentSting", "Sickle", "SickleShot", "SnakeStaff", "SubzeroSlasher" };
                int loot = Main.rand.Next(lootTable.Length);
                if (Main.rand.Next(9) == 0)
                {
                    npc.DropLoot(mod.ItemType("SnowflakeShuriken"), 90, 120); 
                }
                else
                {
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                }
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.HealingPotion;   //boss drops
                AAWorld.downedSerpent = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {

                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.SnowDust>();
                int dust2 = mod.DustType<Dusts.SnowDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }
    }

    public class SerpentBody : Serpent
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Serpent/SerpentBody"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Subzero Serpent");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.npcSlots = 5f;
            npc.width = 24; //324
            npc.height = 30;
            npc.dontCountMe = true;
            npc.scale = 1.15f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {

            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 20;
                npc.height = 40;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.SnowDust>();
                int dust2 = mod.DustType<Dusts.SnowDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }



        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    // NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("SnowDustLight"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 42;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }


            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }

    public class SerpentTail : Serpent
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Serpent/SerpentTail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Subzero Serpent");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.npcSlots = 5f;
            npc.width = 24; //324
            npc.height = 30;
            npc.dontCountMe = true;
            npc.scale = 1.15f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {

                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.width = 44;
                npc.height = 78;
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.SnowDust>();
                int dust2 = mod.DustType<Dusts.SnowDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    // NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("SnowDustLight"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 42;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }


            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }

}
