﻿using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

using System.IO;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    [AutoloadBossHead]
    public class AkumaA : ModNPC
    {
        public override bool CloneNewInstances => (ModSupport.GetMod("AAModEXAI") != null ? true : false);

        public override ModNPC Clone()
		{
            if(ModSupport.GetMod("AAModEXAI") != null)
            {
                return ModSupport.GetModNPC("AAModEXAI", "AkumaA");
            }
			return (ModNPC)MemberwiseClone();
		}
        public bool Loludided;
        public int fireTimer = 0;
        public int damage = 0;
        private bool weakness;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oni Akuma");
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.width = 80;
            npc.height = 80;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.damage = 150;
            npc.defense = 90;
            npc.lifeMax = 500000;
            npc.value = Item.sellPrice(0, 40, 0, 0);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/AkumaRoar");
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Akuma2");
            musicPriority = MusicPriority.BossHigh;
            bossBag = mod.ItemType("AkumaBag");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.buffImmune[103] = false;
            npc.alpha = 255;
            musicPriority = MusicPriority.BossHigh;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            else
            {
                potionType = 0;
            }
        }
        public static int MinionCount = 0;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }
        public Texture2D AkumaTex = null;

        public bool spawnAshe = false;

        bool title = false;

        public override bool PreAI()
        {
            if (!title)
            {
                AAMod.ShowTitle(npc, 8);
                title = true;
            }
            Player player = Main.player[npc.target];
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }

            npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 146;
            }
            if (npc.frame.Y > 146 * 2)
            {
                npc.frame.Y = 0;
            }

            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }

            if (Main.netMode != 1)
            {
                if (npc.localAI[2] == 0)
                {
                    npc.realLife = npc.whoAmI;
                    int latestNPC = npc.whoAmI;
                    int[] Frame = { 1, 2, 0, 1, 2, 2, 1, 2, 2, 0, 1, 2, 2, 1, 2, 2, 0, 1, 2, 3, 4};
                    for (int i = 0; i < Frame.Length; ++i)
                    {
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaABody"), npc.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = npc.whoAmI;
                        Main.npc[latestNPC].ai[3] = npc.whoAmI;
                        Main.npc[latestNPC].netUpdate = true;
                        Main.npc[latestNPC].ai[2] = Frame[i];
                    }
                    npc.localAI[2] = 1;
                    npc.netUpdate2 = true;
                }
            }

            bool collision = true;

            Vector2 targetPos;
            switch ((int)npc.ai[0])
            {
                case 0: //chase while breathing fire, original code
                    if (!npc.HasPlayerTarget)
                        npc.TargetClosest(true);
                    targetPos = Main.player[npc.target].Center;
                    MovementWorm(targetPos, 15f, 0.13f); //original movement
                    Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 20);
                    AAAI.BreatheFire(npc, true, ModContent.ProjectileType<AkumaABreath>(), 2, 4);
                    if (npc.HasBuff(BuffID.Wet))
                    {
                        fireTimer++;

                        if (fireTimer % 20 == 0)
                        {
                            for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                            {
                                int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("MireBubbleDust"), 0f, 0f, 90, default, 2f);
                                Main.dust[num935].noGravity = true;
                                Main.dust[num935].velocity.Y -= 1f;
                            }
                            if (weakness == false)
                            {
                                weakness = true;
                                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("Akuma1"), Color.DeepSkyBlue);
                            }
                        }
                    }
                    else
                    {
                        AAAI.BreatheFire(npc, true, ModContent.ProjectileType<AkumaBreath>(), 2, 4);
                    }
                    if (++npc.ai[1] > 240)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 1: //chase harder, shoot fragballs
                    targetPos = player.Center;
                    MovementWorm(targetPos, 16f, 0.26f);
                    if (++npc.ai[2] > 60)
                    {
                        npc.ai[2] = 0;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, 20f * Vector2.Normalize(npc.velocity), ModContent.ProjectileType<AkumaAFireballFrag>(), npc.damage / 4, 0f, Main.myPlayer);
                    }
                    if (++npc.ai[1] > 300)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 2: //fly up for overhead meteor rain dash
                    targetPos = player.Center;
                    targetPos.X += 800 * (npc.Center.X < player.Center.X ? -1 : 1);
                    targetPos.Y -= 400;
                    MovementWorm(targetPos, 20f, 0.6f);
                    if (++npc.ai[1] > 240 || npc.Distance(targetPos) < 100) //initiate dash
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = npc.Center.X < player.Center.X ? 1 : -1; //remember which side to end up on
                        npc.velocity = 20f * Vector2.UnitX * npc.ai[2];
                        npc.velocity.Y /= 5f;
                        npc.netUpdate = true;
                    }
                    break;

                case 3: //meteor rain
                    targetPos = new Vector2(player.Center.X + npc.ai[2] * 1000, npc.Center.Y);
                    MovementWorm(targetPos, 30f, 0.26f); //accelerate horizontally
                    if (++npc.ai[3] > 40)
                    {
                        npc.ai[3] = 0;
                        if (Main.netMode != 1)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == npc.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = 4f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-1f, 1f);
                                        Projectile.NewProjectile(Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaRock>(), Main.npc[i].damage / 4, 0f, Main.myPlayer);
                                    }
                                }
                        }
                    }
                    if (++npc.ai[1] > 240 || (npc.ai[2] > 0 ? npc.Center.X > player.Center.X + 700 : npc.Center.X < player.Center.X - 700))
                    {
                        if (Main.netMode != 1)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == npc.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = 4f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-1f, 1f);
                                        Projectile.NewProjectile(Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaRock>(), Main.npc[i].damage / 4, 0f, Main.myPlayer);
                                    }
                                }
                        }
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                        npc.velocity.Normalize();
                        npc.velocity *= 15f;
                        npc.velocity = npc.velocity.RotatedBy(npc.velocity.X > 0 ? Math.PI / 2 : -Math.PI / 2);
                    }
                    break;

                case 4: //turn around, chase player for a bit
                    targetPos = player.Center;
                    MovementWorm(targetPos, 15f, 0.13f);
                    if (++npc.ai[1] > 120)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                        if (Main.netMode != 1) //fire deathray
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(npc.velocity), ModContent.ProjectileType<AkumaADeathraySmall>(), npc.damage / 4, 0f, Main.myPlayer, 0, npc.whoAmI);
                    }
                    break;

                case 5: //currently firing deathray, weaker acceleration
                    targetPos = player.Center;
                    MovementWorm(targetPos, 15f, 0.08f);
                    if (++npc.ai[1] > 240)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 6: //fire lasers from all segments, slower now
                    targetPos = player.Center;
                    MovementWorm(targetPos, 10f, 0.26f);
                    if (npc.ai[1] == 120 - 60 && Main.netMode != 1)
                    {
                        bool fire = true;
                        for (int i = 0; i < Main.maxNPCs; i++)
                            if (Main.npc[i].active && Main.npc[i].realLife == npc.whoAmI)
                            {
                                fire = !fire;
                                if (fire)
                                {
                                    Projectile.NewProjectile(Main.npc[i].Center, Main.npc[i].rotation.ToRotationVector2(), ModContent.ProjectileType<AkumaADeathraySmall>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, (float)Math.PI / 2, Main.npc[i].whoAmI);
                                    Projectile.NewProjectile(Main.npc[i].Center, (Main.npc[i].rotation + (float)Math.PI).ToRotationVector2(), ModContent.ProjectileType<AkumaADeathraySmall>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, (float)-Math.PI / 2, Main.npc[i].whoAmI);
                                }
                            }
                    }
                    if (++npc.ai[2] > 140)
                    {
                        npc.ai[2] = 0;
                    }
                    if (++npc.ai[1] > 120 + 180)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 7: //go under and prepare for dash
                    targetPos = player.Center;
                    targetPos.X += 700 * (npc.Center.X < player.Center.X ? -1 : 1);
                    targetPos.Y += 400;
                    MovementWorm(targetPos, 20f, 0.6f);
                    if (++npc.ai[1] > 240 || npc.Distance(targetPos) < 100) //initiate dash
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = npc.Center.X < player.Center.X ? 1 : -1; //remember which side to end up on
                        npc.velocity.X = 25f * npc.ai[2];
                        npc.velocity.Y /= 5f;
                        npc.netUpdate = true;
                    }
                    break;

                case 8: //wait till past player
                    if (++npc.ai[1] > 240 || (npc.ai[2] > 0 ? npc.Center.X > player.Center.X : npc.Center.X < player.Center.X))
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 9: //eruption
                    npc.velocity *= 0.9875f;
                    if (++npc.ai[2] == 30)
                    {
                        if (Main.netMode != 1)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == npc.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = -5f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-.5f, .5f);
                                        vel *= 1.5f;
                                        Projectile.NewProjectile(Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaAMeteor>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, 0f, 1f);
                                    }
                                }
                        }
                    }
                    if (++npc.ai[1] > 120)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                        if (Main.netMode != 1)
                        {
                            bool fire = true;
                            for (int i = 0; i < Main.maxNPCs; i++)
                                if (Main.npc[i].active && Main.npc[i].realLife == npc.whoAmI)
                                {
                                    fire = !fire;
                                    if (fire)
                                    {
                                        Vector2 vel = -5f * Vector2.UnitY;
                                        vel.X += Main.rand.NextFloat(-1f, 1f);
                                        vel.Y += Main.rand.NextFloat(-.5f, .5f);
                                        vel *= 1.5f;
                                        Projectile.NewProjectile(Main.npc[i].Center, vel, ModContent.ProjectileType<AkumaAMeteor>(), Main.npc[i].damage / 4, 0f, Main.myPlayer, 0f, 1f);
                                    }
                                }
                        }
                    }
                    break;

                case 10: //lakitu and chase player
                    targetPos = player.Center;
                    MovementWorm(targetPos, 17f, 0.3f);
                    if (npc.ai[2] == 0)
                    {
                        npc.ai[2] = 1;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<AsheA>(), npc.damage / 4, 0f, Main.myPlayer, npc.target); 
                        if (!spawnAshe)
                        {
                            spawnAshe = true;
                            if (AAWorld.downedAkuma)
                            {
                                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("AkumaA1"), Color.DeepSkyBlue);
                                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("AkumaA2"), new Color(102, 20, 48));
                            }
                            else
                            {
                                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("AkumaA3"), new Color(102, 20, 48));
                                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("AkumaA4"), Color.DeepSkyBlue);
                            }
                        }
                    }
                    if (++npc.ai[1] > 300)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                default:
                    npc.ai[0] = 0;
                    goto case 0;
            }

            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;

            }
            else
            {
                npc.spriteDirection = -1;
            }

            if (!Main.dayTime)
            {
                if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("AkumaA8"), Color.DeepSkyBlue);
                Main.dayTime = true;
                Main.time = 0;
            }

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                if (Loludided == false)
                {
                    if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("AkumaA9"), new Color(180, 41, 32));
                    Loludided = true;
                }
                npc.velocity.Y = npc.velocity.Y + 1f;
                if (npc.position.Y > Main.rockLayer * 16.0)
                {
                    npc.velocity.Y = npc.velocity.Y + 1f;
                }
                if (npc.position.Y > Main.rockLayer * 16.0)
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

            if (collision)
            {
                if (npc.localAI[0] != 1)
                    npc.netUpdate = true;
                npc.localAI[0] = 1f;
            }
            if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0) && !npc.justHit)
                npc.netUpdate = true;

            return false;
        }

        public void MovementWorm(Vector2 target, float speed, float acceleration)
        {
            Vector2 npcCenter = npc.Center;// new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            //float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            //float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

            float targetRoundedPosX = target.X;// (int)(targetXPos / 16.0) * 16;
            float targetRoundedPosY = target.Y;// (int)(targetYPos / 16.0) * 16;
            //npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
            //npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
            npc.TargetClosest(true);
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            float absDirX = Math.Abs(dirX);
            float absDirY = Math.Abs(dirY);
            float newSpeed = speed / length;
            dirX *= newSpeed;
            dirY *= newSpeed;
            if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0)
            {
                if (npc.velocity.X < dirX)
                    npc.velocity.X = npc.velocity.X + acceleration;
                else if (npc.velocity.X > dirX)
                    npc.velocity.X = npc.velocity.X - acceleration;
                if (npc.velocity.Y < dirY)
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                else if (npc.velocity.Y > dirY)
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                    else
                        npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                }
                if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X = npc.velocity.X + acceleration * 2f;
                    else
                        npc.velocity.X = npc.velocity.X - acceleration * 2f;
                }
            }
            else if (absDirX > absDirY)
            {
                if (npc.velocity.X < dirX)
                    npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                else if (npc.velocity.X > dirX)
                    npc.velocity.X = npc.velocity.X - acceleration * 1.1f;

                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                    else
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                }
            }
            else
            {
                if (npc.velocity.Y < dirY)
                    npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                else if (npc.velocity.Y > dirY)
                    npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;

                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                if (!AAWorld.downedAkuma)
                {
                    Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("DraconianRune"));
                }
                if (Main.netMode != 1) AAMod.Chat(AAWorld.downedAkuma ? Lang.BossChat("AkumaA10") : Lang.BossChat("AkumaA11"), Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                AAWorld.downedAkuma = true;
                if (Main.rand.Next(50) == 0 && AAWorld.downedShen)
                {
                    Item.NewItem((int)npc.Center.X, (int)npc.Center.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AkumaATrophy"));
                }
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AkumaAMask"));
                }
                npc.DropBossBags();
                return;
            }
            if (Main.netMode != 1) AAMod.Chat(Lang.BossChat("AkumaA12"), Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            return;
        }

        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool Quote5;
        public bool QuoteSaid;

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.penetrate > 1)
            {
                damage = (int)(damage * .5f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            AkumaTex = Main.npcTexture[npc.type];
            if (npc.type == ModContent.NPCType<AkumaA>())
            {
                if (npc.ai[0] == 0 || npc.ai[0] == 1 || npc.ai[0] == 5 || npc.ai[0] == 9)
                {
                    AkumaTex = mod.GetTexture("NPCs/Bosses/Akuma/Awakened/AkumaA1");
                }
                else
                {
                    AkumaTex = mod.GetTexture("NPCs/Bosses/Akuma/Awakened/AkumaA");
                }
            }

            Texture2D glowTex = mod.GetTexture("Glowmasks/AkumaA_Glow");
            Texture2D glowTex1 = mod.GetTexture("Glowmasks/AkumaA1_Glow");
            Texture2D glowTex2 = mod.GetTexture("Glowmasks/AkumaABody_Glow");
            
            int shader;
            if (npc.ai[1] == 1 || npc.ai[2] >= 470 || Main.npc[(int)npc.ai[3]].ai[1] == 1 || Main.npc[(int)npc.ai[3]].ai[2] >= 500)
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            }
            else
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            }

            Texture2D HeadGlow = (npc.ai[0] == 0 || npc.ai[0] == 4) ? glowTex1 : glowTex;

            Texture2D myGlowTex = npc.type == ModContent.NPCType<AkumaA>() ? HeadGlow : glowTex2;
            BaseDrawing.DrawTexture(spriteBatch, AkumaTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 3, npc.frame, npc.GetAlpha(drawColor), true);
            BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 3, npc.frame, npc.GetAlpha(Color.White), true);
            return false;
        }


        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = ModContent.DustType<Dusts.AkumaADust>();
            int dust2 = ModContent.DustType<Dusts.AkumaDust>();
            if (npc.life <= 0)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }


        public int roarTimer = 0;
        public int roarTimerMax = 120;
        public bool Roaring
        {
            get
            {
                return roarTimer > 0;
            }
        }

        public void Roar(int timer, bool fireSound)
        {
            roarTimer = timer;
            if (fireSound)
            {
                Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60);
            }
            else
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/AkumaRoar"), npc.Center);
            }
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                return false;
            }
            return true;
        }
    }

    [AutoloadBossHead]
    public class AkumaABody : AkumaA
    {
        public override string Texture => "AAMod/NPCs/Bosses/Akuma/Awakened/AkumaABody";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oni Akuma");
            Main.npcFrameCount[npc.type] = 5;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.boss = false;
            npc.width = 40;
            npc.height = 40;
            npc.dontCountMe = true;
            npc.chaseable = false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = (directionVector.X > 0f) ? 1 : -1;
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;
            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }


            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[3]].type != mod.NPCType("AkumaA"))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + Main.npc[(int)npc.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + Main.npc[(int)npc.ai[1]].height / 2 - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }

                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            return false;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage *= .1f;
            return true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * (int)npc.ai[2];
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<AkumaA>()))
            {
                return false;
            }
            npc.active = false;
            return true;
        }
    }
}
