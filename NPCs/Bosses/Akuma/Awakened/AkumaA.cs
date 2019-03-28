using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using BaseMod;
using System.IO;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    [AutoloadBossHead]
    public class AkumaA : ModNPC
	{
        public bool Panic;
        public bool Loludided;
        private bool weakness = false;
        public int fireTimer = 0;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
			NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 3;
        }

		public override void SetDefaults()
		{
			npc.noTileCollide = true;
            npc.width = 84;
            npc.height = 84;
			npc.aiStyle = -1;
			npc.netAlways = true;
            npc.lifeMax = 190000;
            npc.damage = 100;
            npc.defense = 150;
            npc.value = Item.buyPrice(20, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.NPCKilled, "Sounds/Sounds/AkumaRoar");
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Akuma2");
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
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
        }


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }


        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        private int speed = 8;
        public static int MinionCount = 0;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)npc.ai[2]);
                writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
            }
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                npc.ai[2] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }
        public Texture2D AkumaTex = null;

        public override bool PreAI()
        {
            Player player = Main.player[npc.target];
            if (npc.ai[1] == 1 || npc.ai[2] >= 400)
            {
                AkumaTex = mod.GetTexture("NPCs/Bosses/Akuma/Awakened/AkumaA1");
            }
            else
            {
                AkumaTex = mod.GetTexture("NPCs/Bosses/Akuma/Awakened/AkumaA");
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
            float dist = npc.Distance(player.Center);
            npc.ai[2]++;
            if (npc.ai[2] == 300)
            {
                QuoteSaid = false;
                Roar(roarTimerMax, false);
                internalAI[1] += 1;
            }
            if (npc.ai[2] > 300)
            {
                Attack(npc);
            }
            if (npc.ai[2] >= 400)
            {
                npc.ai[2] = 0;
            }

            if (npc.life > npc.lifeMax / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;

                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
                Main.NewText(AAWorld.downedAkuma ? "Still got it, do you? Ya got fire in your spirit! I like that about you, kid!" : "What?! How have you lasted this long?! Why you little... I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }

            if (dist > 400 & Main.rand.Next(20) == 1 && npc.ai[1] == 0 && npc.ai[2] < 300)
            {
                npc.ai[1] = 1;
            }
            if (npc.ai[1] == 1)
            {
                attackTimer++;
                if ((attackTimer == 20 || attackTimer == 50 || attackTimer == 79) && npc.HasBuff(BuffID.Wet))
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("MireBubbleDust"), 0f, 0f, 90, default(Color), 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].velocity.Y -= 1f;
                    }
                    if (weakness == false)
                    {
                        weakness = true;
                        Main.NewText("ACK..! WATER! I LOATHE WATER!!!", Color.DeepSkyBlue);
                    }
                }
                else
                {
                    Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 20);
                    AAAI.BreatheFire(npc, true, mod.ProjectileType<AkumaABreath>(), 2, 2);
                }
                if (attackTimer >= 80)
                {
                    npc.ai[1] = 0 ;
                    attackTimer = 0;
                    attackFrame = 0;
                    attackCounter = 0;
                }
            }

            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default(Color), 2f);
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
                if (npc.ai[0] == 0)
                {
                    npc.realLife = npc.whoAmI;
                    int latestNPC = npc.whoAmI;
                    int segment = 0;
                    int AkumaALength = 9;
                    for (int i = 0; i < AkumaALength; ++i)
                    {
                        if (segment == 0 || segment == 2 || segment == 3 || segment == 5 || segment == 6 || segment == 8)
                        {
                            latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaABody"), npc.whoAmI, 0, latestNPC);
                            Main.npc[latestNPC].realLife = npc.whoAmI;
                            Main.npc[latestNPC].ai[3] = npc.whoAmI;
                            segment += 1;
                        }
                        if (segment == 1 || segment == 4 || segment == 7)
                        {
                            latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaAArms"), npc.whoAmI, 0, latestNPC);
                            Main.npc[latestNPC].realLife = npc.whoAmI;
                            Main.npc[latestNPC].ai[3] = npc.whoAmI;
                            segment += 1;
                        }
                    }
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaABody"), npc.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = npc.whoAmI;
                    Main.npc[latestNPC].ai[3] = npc.whoAmI;

                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaABody1"), npc.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = npc.whoAmI;
                    Main.npc[latestNPC].ai[3] = npc.whoAmI;

                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaATail"), npc.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = npc.whoAmI;
                    Main.npc[latestNPC].ai[3] = npc.whoAmI;

                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }

            int minTilePosX = (int)(npc.position.X / 16.0) - 1;
            int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 2;
            int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
            int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 2;
            if (minTilePosX < 0)
                minTilePosX = 0;
            if (maxTilePosX > Main.maxTilesX)
                maxTilePosX = Main.maxTilesX;
            if (minTilePosY < 0)
                minTilePosY = 0;
            if (maxTilePosY > Main.maxTilesY)
                maxTilePosY = Main.maxTilesY;

            bool collision = true;
            
            float speed = 10f;
            float acceleration = 0.16f;

            Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

            float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
            float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
            npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
            npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
            npc.TargetClosest(true);
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            float absDirX = Math.Abs(dirX);
            float absDirY = Math.Abs(dirY);
            float newSpeed = speed / length;
            dirX = dirX * (newSpeed * 2);
            dirY = dirY * (newSpeed * 2);
            if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
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
                Main.NewText("Nighttime won't save you from me this time, kid! The day is born anew!", Color.DeepSkyBlue);
                Main.dayTime = true;
                Main.time = 0;
            }

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                if (Loludided == false)
                {
                    Main.NewText("You just got burned, kid.", new Color(180, 41, 32));
                    Loludided = true;
                }
                npc.velocity.Y = npc.velocity.Y + 1f;
                if ((double)npc.position.Y > Main.rockLayer * 16.0)
                {
                    npc.velocity.Y = npc.velocity.Y + 1f;
                    speed = 30f;
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

            if (collision)
            {
                if (npc.localAI[0] != 1)
                    npc.netUpdate = true;
                npc.localAI[0] = 1f;
            }
            if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                npc.netUpdate = true;
            
            return false;
        }
        public override void NPCLoot()
		{
            if (Main.expertMode)
            {

                Main.NewText(AAWorld.downedAkuma ? "Heh, not to shabby this time kid. I'm impressed. Here. Take your prize." : "GRAH..! HOW!? HOW COULD I LOSE TO A MERE MORTAL TERRARIAN?! Hmpf...fine kid, you win, fair and square. Heere's your reward.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                AAWorld.downedAkuma = true;
                //npc.DropLoot(Items.Vanity.Mask.AkumaMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Akuma.AkumaATrophy.type, 1f / 10);
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                npc.DropBossBags();
                if (Main.rand.Next(20) == 0 && AAWorld.PowerDropped == false)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PowerStone"));
                    AAWorld.PowerDropped = true;
                }
                return;
            }
            Main.NewText("Nice. You cheated. Now come fight me in expert mode like a real man.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            return;
        }

        
        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool Quote5;
        public bool QuoteSaid;

        public void Attack(NPC npc)
        {
            Player player = Main.player[npc.target];
            if (internalAI[1] == 1 || internalAI[1] == 7 || internalAI[1] == 15 || internalAI[1] == 18 || internalAI[1] == 21)
            {
                if (!QuoteSaid)
                {
                    Main.NewText((!Quote1) ? "Sky's fallin' again! On your toes!" : "Down comes the flames of fury again!", new Color(45, 46, 70));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (npc.ai[2] == 320 || npc.ai[2] == 340 || npc.ai[2] == 360 || npc.ai[2] == 380)
                {
                    int Fireballs = Main.expertMode ? 10 : 7;
                    for (int Loops = 0; Loops < Fireballs; Loops++)
                    {
                        AkumaAttacks.Dragonfire(npc, mod, true);
                    }
                }
            }

            if ((internalAI[1] == 2 || internalAI[1] == 6 || internalAI[1] == 12 || internalAI[1] == 16 || internalAI[1] == 24))
            {
                if (!QuoteSaid)
                {
                    Main.NewText((!Quote1) ? "You underestimate the artillery of a dragon, kid!" : "Flames don't give in till the end, kid!", new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (npc.ai[2] == 350)
                {
                    int Fireballs = Main.expertMode ? 5 : 3;
                    float spread = 45f * 0.0174f;
                    float baseSpeed = (float)Math.Sqrt((npc.velocity.X * npc.velocity.X) + (npc.velocity.Y * npc.velocity.Y));
                    double startAngle = Math.Atan2(npc.velocity.X, npc.velocity.Y) - .1d;
                    double deltaAngle = spread / 6f;
                    double offsetAngle;
                    for (int i = 0; i < Fireballs; i++)
                    {
                        offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle) * 2, baseSpeed * (float)Math.Cos(offsetAngle) * 2, mod.ProjectileType<AkumaABomb>(), npc.damage / (Main.expertMode ? 2 : 4), 3, Main.myPlayer);
                    }
                }
            }

            if (internalAI[1] == 3 || internalAI[1] == 8 || internalAI[1] == 11 || internalAI[1] == 17 || internalAI[1] == 23)
            {
                int Fireballs = Main.expertMode ? 12 : 14;
                if (!QuoteSaid)
                {
                    Main.NewText((!Quote1) ? "Heads up! Volcano's eruptin' kid!" : "INCOMING!", new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (npc.ai[2] == 330 || npc.ai[2] == 360 || npc.ai[2] == 390)
                {
                    for (int Loops = 0; Loops < Fireballs; Loops++)
                    {
                        AkumaAttacks.Eruption(npc, mod);
                    }
                }
            }

            if (internalAI[1] == 4 || internalAI[1] == 10 || internalAI[1] == 13 || internalAI[1] == 20 || internalAI[1] == 25)
            {
                if (npc.ai[2] == 350)
                {
                    if (NPC.CountNPCS(mod.NPCType<AncientLung>()) < (Main.expertMode ? 3 : 4))
                    {
                        AkumaAttacks.SpawnLung(player, mod, true);
                        MinionCount += 1;
                    }
                }
            }
            
            if (internalAI[1] == 5 || internalAI[1] == 9 || internalAI[1] == 14 || internalAI[1] == 19 || internalAI[1] == 22)
            {
                if (!QuoteSaid)
                {
                    Main.NewText((!Quote1) ? "Hey Kid? Like Fireworks? No? Too Bad!" : "Here comes the grand finale, kid!", new Color(180, 41, 32));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                if (npc.ai[2] == 350)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X * 2, npc.velocity.Y, mod.ProjectileType<AFireProjHostile>(), npc.damage / (Main.expertMode ? 2 : 4), 3, Main.myPlayer);
                }
            }

            if (internalAI[1] > 25)
            {
                internalAI[1] = 0;
            }
        }
        

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.penetrate > 1)
            {
                damage = (int)(damage * .5f);
            }
        }

        public static Texture2D glowTex = null, glowTex1 = null, glowTex2 = null, glowTex3 = null, glowTex4 = null, glowTex5 = null;
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (AkumaTex == null)
            {
                AkumaTex = Main.npcTexture[npc.type];
            }
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/AkumaA_Glow");
                glowTex1 = mod.GetTexture("Glowmasks/AkumaA1_Glow");
                glowTex2 = mod.GetTexture("Glowmasks/AkumaAArms_Glow");
                glowTex3 = mod.GetTexture("Glowmasks/AkumaABody_Glow");
                glowTex4 = mod.GetTexture("Glowmasks/AkumaABody1_Glow");
                glowTex5 = mod.GetTexture("Glowmasks/AkumaATail_Glow");
            }
            Vector2 Drawpos = npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY);
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            if (npc.ai[1] == 1 || npc.ai[2] >= 470 || Main.npc[(int)npc.ai[3]].ai[1] == 1 || Main.npc[(int)npc.ai[3]].ai[2] >= 500)
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            }
            else
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            }

            Texture2D HeadGlow = (npc.ai[1] == 1 || npc.ai[2] >= 500) ? glowTex1 : glowTex;

            Texture2D myGlowTex = (npc.type == mod.NPCType<AkumaA>() ? HeadGlow : npc.type == mod.NPCType<AkumaAArms>() ? glowTex2 : npc.type == mod.NPCType<AkumaABody>() ? glowTex3 : npc.type == mod.NPCType<AkumaABody1>() ? glowTex4 : glowTex5);
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(AkumaTex, Drawpos, npc.frame, npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, npc, npc.GetAlpha(Color.White), true, npc.frame.Size() / 2);
            return false;
        }


        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaDust>();
            if (npc.life <= 0)
            {
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
                int roarSound = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Sounds/AkumaRoar");
                Main.PlaySound(roarSound, npc.Center);
            }
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<AkumaA>()))
            {
                return false;
            }
            return true;
        }
    }
}
