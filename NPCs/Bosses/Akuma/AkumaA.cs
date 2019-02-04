using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using BaseMod;

namespace AAMod.NPCs.Bosses.Akuma
{

    [AutoloadBossHead]
    public class AkumaA : ModNPC
	{
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaA"; } }

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
            npc.height = 120;
			npc.aiStyle = -1;
			npc.netAlways = true;
            npc.lifeMax = 150000;
            if (npc.life > npc.lifeMax / 3)
            {
                npc.damage = 125;
                npc.defense = 125;
            }
            if (npc.life <= npc.lifeMax / 3)
            {
                npc.damage = 150;
                npc.defense = 100;
            }
            npc.value = Item.buyPrice(20, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Akuma2");
            musicPriority = MusicPriority.BossHigh;
            bossBag = mod.ItemType("AkumaBag");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.buffImmune[103] = false;
            npc.alpha = 255;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = npc.lifeMax;
        }

        private bool fireAttack;
        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        public override bool PreAI()
		{
            if (fireAttack == true)
            {
                attackCounter++;
                if (attackCounter > 10)
                {
                    attackFrame++;
                    attackCounter = 0;
                }
                if (attackFrame >= 3)
                {
                    attackFrame = 2;
                }
            }

            npc.frameCounter = 0;
            Main.dayTime = true;
            Main.time = 24000;
            Player player = Main.player[npc.target];
			float dist = npc.Distance(player.Center);
            fireTimer++;
            if (dist > 400 && fireTimer >= 240 && fireAttack == false)
            {
                fireAttack = true;

                fireTimer = 0;
            }
            if (fireAttack == true)
            {
                attackTimer++;
                if (Main.rand.Next(5) == 0)
                {
                    if (attackTimer == 20 && !npc.HasBuff(103))
                    {
                        Main.PlaySound(SoundID.Item34, npc.position);
                        int proj2 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y + Main.rand.Next(-20, 20), npc.velocity.X * 1.6f, npc.velocity.Y * 1.6f, mod.ProjectileType("AFireProjHostile"), 20, 0, Main.myPlayer);
                        Main.projectile[proj2].damage = npc.damage / 3;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                    if (attackTimer == 20 && npc.HasBuff(103))
                    {
                        for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                        {
                            int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("MireBubbleDust"), 0f, 0f, 100, default(Color), 2f);
                            Main.dust[num935].noGravity = true;
                            Main.dust[num935].velocity.Y -= 1f;
                        }
                        if (weakness == false)
                        {
                            weakness = true;
                            Main.NewText("CAUGH! WATER! I HATE WATER!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                        }
                    }
                    if (attackTimer >= 80)
                    {
                        fireAttack = false;
                    }
                }
                else
                {
                    if ((attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79) && !npc.HasBuff(103))
                    {
                        Main.PlaySound(SoundID.Item34, npc.position);
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
                                Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, npc.velocity.X * 2f, npc.velocity.Y * 2f, mod.ProjectileType("AkumaABreath"), npc.damage, 0, Main.myPlayer);
                            }
                        }
                    }
                    if ((attackTimer == 30 || attackTimer == 60 || attackTimer == 79) && npc.HasBuff(103))
                    {
                        for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                        {
                            int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("MireBubbleDust"), npc.damage, 0f, 75, default(Color), 2f);
                            Main.dust[num935].noGravity = true;
                            Main.dust[num935].velocity.Y -= 1f;
                        }
                        if (weakness == false)
                        {
                            weakness = true;
                            Main.NewText("CAUGH! WATER! I HATE WATER!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                        }
                    }
                    if (attackTimer >= 80)
                    {
                        fireAttack = false;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
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
                            Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                            Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                            segment += 1;
                        }
                        if (segment == 1 || segment == 4 || segment == 7)
                        {
                            latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaAArms"), npc.whoAmI, 0, latestNPC);
                            Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                            Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                            segment += 1;
                        }
                    }

                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaATail"), npc.whoAmI, 0, latestNPC);
					Main.npc[(int)latestNPC].realLife = npc.whoAmI;
					Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

					npc.ai[0] = 1;
					npc.netUpdate = true;
				}
			}
            if (npc.life <= npc.lifeMax / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
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

			for (int i = minTilePosX; i < maxTilePosX; ++i)
			{
				for (int j = minTilePosY; j < maxTilePosY; ++j)
				{
					if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
					{
						Vector2 vector2;
						vector2.X = (float)(i * 16);
						vector2.Y = (float)(j * 16);
						if (npc.position.X + npc.width > vector2.X && npc.position.X < vector2.X + 16.0 && (npc.position.Y + npc.height > (double)vector2.Y && npc.position.Y < vector2.Y + 16.0))
						{
							collision = true;
							if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
								WorldGen.KillTile(i, j, true, true, false);
						}
					}
				}
			}
            float speedval = 0f;
            if (npc.life > npc.lifeMax / 3 && npc.type == mod.NPCType<AkumaA>())
            {
                speedval = 9f;
            }
            if (npc.life <= npc.lifeMax / 3 && npc.type == mod.NPCType<AkumaA>())
            {
                speedval = 11f;
            }
            float speed = speedval;
            float acceleration = 0.18f;

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

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                if (Loludided == false)
                {
                    Main.NewText("You just got burned, kid.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
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

            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;

            }
            else
            {
                npc.spriteDirection = -1;
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

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D attackAni = mod.GetTexture("NPCs/Bosses/Akuma/AkumaA");
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (fireAttack == false)
            {
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (fireAttack == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = attackAni.Height / 3;
                int y6 = num214 * attackFrame;
                Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, attackAni.Width, num214)), drawColor, npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaADust>();
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
            if (npc.life > npc.lifeMax / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little... I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit! I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }


        }

        public const string HeadTex = "AAMod/NPCs/Boss/Akuma/AkumaAHead_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(HeadTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void NPCLoot()
		{
            if (Main.expertMode)
            {
                //npc.DropLoot(Items.Vanity.Mask.AkumaMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Akuma.AkumaTrophy.type, 1f / 10);
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                npc.DropBossBags();
                
            }
            return;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            

            if (!AAWorld.downedAkuma && Main.expertMode)
            {
                Main.NewText("Gah! How could this happen?! Even in my full form?! Fine, take your reward. You earned it.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                BaseUtility.Chat("The volcanoes of the inferno are finally quelled...", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);

            }
            if (AAWorld.downedAkuma && Main.expertMode)
            {
                Main.NewText("Snuffed out again. You have my respect, kid. Here.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

            }
            if (!Main.expertMode)
            {
                Main.NewText("Nice hacks, kid. Now come back and fight me like a real man in expert mode. Then I’ll give you your prize.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

            }

            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
                AAWorld.downedAkuma = true;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                Main.NewText("Wuss.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
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
    }


    [AutoloadBossHead]
    public class AkumaAArms : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaAArms"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
             npc.width = 84;
            npc.height = 84;
            npc.dontCountMe = true;
            npc.alpha = 255;
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

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 10;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

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
            if (npc.life <= npc.lifeMax / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
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

        public const string BodyTex = "AAMod/NPCs/Boss/Akuma/AkumaABody_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(BodyTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            
            if (npc.life <= 0)
            {
                int dust1 = mod.DustType<Dusts.AkumaADust>();
                int dust2 = mod.DustType<Dusts.AkumaADust>();
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
            if (npc.life > npc.lifeMax / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little...I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit; I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
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
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }
    
    [AutoloadBossHead]
    public class AkumaABody : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaABody"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
             npc.width = 84;
            npc.height = 84;
            npc.dontCountMe = true;
            npc.alpha = 255;
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

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 10;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

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
            if (npc.life <= npc.lifeMax  / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
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

        public const string BodyTex = "AAMod/NPCs/Boss/Akuma/AkumaABody_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(BodyTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaADust>();
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
            if (npc.life > npc.lifeMax  / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little...I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit; I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
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
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }


    [AutoloadBossHead]
    public class AkumaATail : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaATail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 78;
            npc.height = 78;
            npc.dontCountMe = true;
            npc.alpha = 255;
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
            if (npc.life <= npc.lifeMax  / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
            }

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 10;
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

        public const string BodyTex = "AAMod/NPCs/Boss/Akuma/AkumaATail_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(BodyTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaADust>();
            if (npc.life <= 0)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;

            }
            if (npc.life > npc.lifeMax  / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little...I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit; I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
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
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }

}
