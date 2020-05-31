
using AAMod.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Terra.Projectiles
{
    public class TerraCrystal : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Crystal");
            Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 42;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.light = 0.4f;
            projectile.ignoreWater = true;
            projectile.minion = true;
            projectile.minionSlots = 0;
        }

		Vector2 PlayerPoint = Vector2.Zero;
        float intAI = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(intAI);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                intAI = reader.ReadFloat();
            }
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (!modPlayer.TerraSu)
			{
				projectile.Kill();
				return;
			}

            switch (modPlayer.CrystalMode)
            {
                case 0: projectile.frame = 0; break;
                case 1: projectile.frame = 1; break;
                case 2: projectile.frame = 2; break;
                default: projectile.frame = 0; break;
            }

            if (modPlayer.CrystalMode != 1)
			{
				PlayerPoint.X = player.Center.X - projectile.width / 2;
				PlayerPoint.Y = player.Center.Y - projectile.height / 2 + player.gfxOffY - 60f;

				MoveToPoint(PlayerPoint);

				if (player.gravDir == -1f)
				{
					projectile.position.Y = projectile.position.Y + 120f;
					projectile.rotation = 3.14f;
				}
				else
				{
					projectile.rotation = 0f;
				}

				if (modPlayer.CrystalMode == 0)
				{
					if (projectile.owner == Main.myPlayer)
					{
						if (intAI != 0f)
						{
                            intAI -= 1f;
							return;
						}
						float num396 = projectile.position.X;
						float num397 = projectile.position.Y;
						float num398 = 700f;
						bool flag11 = false;
						for (int num399 = 0; num399 < 200; num399++)
						{
							if (Main.npc[num399].CanBeChasedBy(this, true))
							{
								float num400 = Main.npc[num399].position.X + Main.npc[num399].width / 2;
								float num401 = Main.npc[num399].position.Y + Main.npc[num399].height / 2;
								float num402 = Math.Abs(projectile.position.X + projectile.width / 2 - num400) + Math.Abs(projectile.position.Y + projectile.height / 2 - num401);
								if (num402 < num398 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num399].position, Main.npc[num399].width, Main.npc[num399].height))
								{
									num398 = num402;
									num396 = num400;
									num397 = num401;
									flag11 = true;
								}
							}
						}
						if (flag11)
						{
							float num403 = 12f;
							Vector2 vector29 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
							float num404 = num396 - vector29.X;
							float num405 = num397 - vector29.Y;
							float num406 = (float)Math.Sqrt(num404 * num404 + num405 * num405);
							num406 = num403 / num406;
							num404 *= num406;
							num405 *= num406;
							int p = Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, num404, num405, ModContent.ProjectileType<TerraSphere>(), Player.crystalLeafDamage, Player.crystalLeafKB, projectile.owner, 0f, 0f);
							Main.projectile[p].melee = false;
							Main.projectile[p].minion = true;
							Main.projectile[p].minionSlots = 0;
                            intAI = 50f;
							return;
						}
					}
				}
                else
                {
                    if (player.statLife < player.statLifeMax2)
                    {
                        if (Main.rand.Next(3) == 0)
                        {
                            Vector2 Vel = (player.Center - player.Center) * 0.05f;
                            int dustID = Dust.NewDust(projectile.Center, 0, 0, DustID.GoldFlame, Vel.X, Vel.Y, 50);
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
			}
			else
			{
                projectile.rotation = projectile.velocity.X * 0.04f;
                if (Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                }
                float num633 = 700f;
                float num634 = 800f;
                float num635 = 1200f;
                float num636 = 150f;
                float num637 = 0.05f;
                for (int num638 = 0; num638 < 1000; num638++)
                {
                    bool flag23 = Main.projectile[num638].type == mod.ProjectileType("TerraCrystal");
                    if (num638 != projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == projectile.owner && flag23 && Math.Abs(projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < projectile.width)
                    {
                        if (projectile.position.X < Main.projectile[num638].position.X)
                        {
                            projectile.velocity.X = projectile.velocity.X - num637;
                        }
                        else
                        {
                            projectile.velocity.X = projectile.velocity.X + num637;
                        }
                        if (projectile.position.Y < Main.projectile[num638].position.Y)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - num637;
                        }
                        else
                        {
                            projectile.velocity.Y = projectile.velocity.Y + num637;
                        }
                    }
                }
                bool flag24 = false;
                if (projectile.ai[0] == 2f)
                {
                    projectile.ai[1] += 1f;
                    projectile.extraUpdates = 1;
                    if (projectile.ai[1] > 40f)
                    {
                        projectile.ai[1] = 1f;
                        projectile.ai[0] = 0f;
                        projectile.extraUpdates = 0;
                        projectile.numUpdates = 0;
                        projectile.netUpdate = true;
                    }
                    else
                    {
                        flag24 = true;
                    }
                }
                if (flag24)
                {
                    return;
                }
                Vector2 vector46 = projectile.position;
                bool flag25 = false;
                if (projectile.ai[0] != 1f)
                {
                    projectile.tileCollide = false;
                }
                if (projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16)))
                {
                    projectile.tileCollide = false;
                }
                if (player.HasMinionAttackTargetNPC)
                {
                    NPC nPC2 = Main.npc[player.MinionAttackTargetNPC];
                    if (nPC2.CanBeChasedBy(projectile, false))
                    {
                        float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                        if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                        {
                            vector46 = nPC2.Center;
                            flag25 = true;
                        }
                    }
                }
                else
                {
                    for (int num645 = 0; num645 < 200; num645++)
                    {
                        NPC nPC2 = Main.npc[num645];
                        if (nPC2.CanBeChasedBy(projectile, false))
                        {
                            float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                            if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                            {
                                num633 = num646;
                                vector46 = nPC2.Center;
                                flag25 = true;
                            }
                        }
                    }
                }
                float num647 = num634;
                if (flag25)
                {
                    num647 = num635;
                }
                if (Vector2.Distance(player.Center, projectile.Center) > num647)
                {
                    projectile.ai[0] = 1f;
                    projectile.tileCollide = false;
                    projectile.netUpdate = true;
                }
                if (flag25 && projectile.ai[0] == 0f)
                {
                    Vector2 vector47 = vector46 - projectile.Center;
                    float num648 = vector47.Length();
                    vector47.Normalize();
                    if (num648 > 200f)
                    {
                        float scaleFactor2 = 8f;
                        vector47 *= scaleFactor2;
                        projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                    }
                    else
                    {
                        float num649 = 4f;
                        vector47 *= -num649;
                        projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                    }
                }
                else
                {
                    bool flag26 = false;
                    if (!flag26)
                    {
                        flag26 = projectile.ai[0] == 1f;
                    }
                    float num650 = 5f; //6
                    if (flag26)
                    {
                        num650 = 12f; //15
                    }
                    Vector2 center2 = projectile.Center;
                    Vector2 vector48 = player.Center - center2 + new Vector2(0f, -30f); //-60
                    float num651 = vector48.Length();
                    if (num651 > 200f && num650 < 6.5f) //200 and 8
                    {
                        num650 = 6.5f; //8
                    }
                    if (num651 < num636 && flag26 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                    {
                        projectile.ai[0] = 0f;
                        projectile.netUpdate = true;
                    }
                    if (num651 > 2000f)
                    {
                        projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width / 2;
                        projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height / 2;
                        projectile.netUpdate = true;
                    }
                    if (num651 > 70f)
                    {
                        vector48.Normalize();
                        vector48 *= num650;
                        projectile.velocity = (projectile.velocity * 40f + vector48) / 41f;
                    }
                    else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        projectile.velocity.X = -0.2f;
                        projectile.velocity.Y = -0.1f;
                    }
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] += Main.rand.Next(1, 4);
                }
                if (projectile.ai[1] > 80f)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
                if (projectile.ai[0] == 0f)
                {
                    float scaleFactor3 = 24f;
                    int num658 = ModContent.ProjectileType<TerraSphere>();
                    if (flag25 && projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
                        {
                            Vector2 value19 = vector46 - projectile.Center;
                            value19.Normalize();
                            value19 *= scaleFactor3;
                            int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X, value19.Y, num658, (int)(80 * player.minionDamage), 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num659].melee = false; 
                            Main.projectile[num659].minion = true;
                            Main.projectile[num659].minionSlots = 0;
                            Main.projectile[num659].timeLeft = 300;
                            projectile.netUpdate = true;
                        }
                    }
                }
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 3, 0, 0);

            BaseDrawing.DrawAura(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, auraPercent, 1.4f, projectile.scale, projectile.rotation, projectile.direction, 3, frame, 0, 0, Color.White);
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 3, frame, projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);

            return false;
        }

        public void MoveToPoint(Vector2 point)
		{
			float moveSpeed = 20f;
			float velMultiplier = 1f;
			Vector2 dist = point - projectile.Center;
			float length = dist == Vector2.Zero ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 200f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 10f)
			{
				moveSpeed *= 0.01f;
			}
			projectile.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
			projectile.velocity *= moveSpeed;
			projectile.velocity *= velMultiplier;
		}
	}
}
