using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Projectiles.Rajah.Supreme;

namespace AAMod.Items.Armor.Champion.Baron
{
    public class BaronBunny : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baron Bunny");
			Main.projFrames[projectile.type] = 8;
            projectile.minionSlots = 0;
        }
    	
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.minionSlots = 0;
        }

        int MeterF = 0;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.Baron = false;
            }
            if (modPlayer.Baron)
            {
                projectile.timeLeft = 2;
            }

            float shootInterval = 90f;

            if (player.statLife < player.statLifeMax2 * (4 / 5))
            {
                MeterF = 1; shootInterval = 70f;
                projectile.damage = (int)(170 * player.minionDamage);
            }
            else if (player.statLife < player.statLifeMax2 * (3 / 5))
            {
                MeterF = 2; shootInterval = 60f;
                projectile.damage = (int)(190 * player.minionDamage);
            }
            else if (player.statLife < player.statLifeMax2 * (2 / 5))
            {
                MeterF = 3; shootInterval = 40f;
                projectile.damage = (int)(210 * player.minionDamage);
            }
            else if (player.statLife < player.statLifeMax2 * (1 / 5))
            {
                MeterF = 4; shootInterval = 20f;
                projectile.damage = (int)(250 * player.minionDamage);
            }
            else
            {
                MeterF = 5;
                projectile.damage = (int)(150 * player.minionDamage);
            }

            float num633 = 700f;
			float num634 = 800f;
			float num635 = 1200f;
			float num636 = 150f;
			float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = Main.projectile[num638].type == mod.ProjectileType("BaronBunny");
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
					float scaleFactor2 = 6f;
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
				float num650 = 6f;
				if (flag26)
				{
					num650 = 15f;
				}
				Vector2 center2 = projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
				float num651 = vector48.Length();
				if (num651 > 200f && num650 < 8f)
				{
					num650 = 8f;
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
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}

            projectile.frameCounter++;
            if (projectile.frameCounter >= 10)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            if (projectile.frame > 4)
            {
                projectile.frame = 0;
            }
			if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (projectile.ai[1] > shootInterval)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
			if (projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 10f;
				if (flag25 && projectile.ai[1] == 0f)
				{
					projectile.ai[1] += 1f;
                    int num658 = Main.rand.Next(3);
                    switch (num658)
                    {
                        case 0:
                            ModContent.ProjectileType<RabbitBeam>();
                            break;
                        case 1:
                            ModContent.ProjectileType<BaronSpear>();
                            break;
                        case 2:
                            ModContent.ProjectileType<BaronCarrot>();
                            break;

                    }
                    if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
					{
                        if (num658 == ModContent.ProjectileType<BaronCarrot>())
                        {
                            float spread = 45f * 0.0174f;
                            Vector2 dir = Vector2.Normalize(vector46 - projectile.Center);
                            dir *= scaleFactor3;
                            float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                            double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                            double deltaAngle = spread / 6f;
                            for (int i = 0; i < 3; i++)
                            {
                                double offsetAngle = startAngle + (deltaAngle * i);
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), num658, projectile.damage, 5, Main.myPlayer);
                            }
                        }
                        else
                        {
                            Vector2 value19 = vector46 - projectile.Center;
                            value19.Normalize();
                            value19 *= scaleFactor3;
                            int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X, value19.Y, num658, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num659].timeLeft = 300;
                            Main.projectile[num659].hostile = false;
                            Main.projectile[num659].friendly = true;
                            projectile.netUpdate = true;
                        }
                        projectile.netUpdate = true;
                    }
				}
			}

            projectile.rotation = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];

            Texture2D tex = Main.projectileTexture[projectile.type];
            Texture2D Mad = mod.GetTexture("Items/Armor/Champion/Baron/BaronMad");
            Texture2D Eyes = mod.GetTexture("Items/Armor/Champion/Baron/BaronMadEyes");
            Texture2D Meter = mod.GetTexture("Items/Armor/Champion/Baron/BaronBunnyMeter");
            Texture2D MeterGlow = mod.GetTexture("Items/Armor/Champion/Baron/BaronBunnyMeter_Glow");

            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, tex.Width, tex.Height / 8, 0, 0);
            Rectangle MeterFrame = BaseMod.BaseDrawing.GetFrame(MeterF, Meter.Width, Meter.Height / 5, 0, 0);

            if (player.statLife < player.statLifeMax2 * (1 / 5))
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingRainbowDye);
                BaseMod.BaseDrawing.DrawTexture(spriteBatch, MeterGlow, shader, projectile.position - new Vector2(44, 0), projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 5, MeterFrame, Color.White, true);
                BaseMod.BaseDrawing.DrawTexture(spriteBatch, Eyes, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 8, frame, Main.DiscoColor, true);
            }

            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Meter, 0, projectile.position - new Vector2(44, 0), projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 5, MeterFrame, Color.White, true);

            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 8, frame, lightColor, true);

            if (player.statLife < player.statLifeMax2 * (3 / 5))
            {
                BaseMod.BaseDrawing.DrawTexture(spriteBatch, Mad, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 8, frame, lightColor, true);
            }

            if (player.statLife < player.statLifeMax2 * (1 / 5))
            {
                int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingRainbowDye);
                BaseMod.BaseDrawing.DrawTexture(spriteBatch, MeterGlow, shader, projectile.position - new Vector2(44, 0), projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 5, MeterFrame, Color.White, true);
                BaseMod.BaseDrawing.DrawTexture(spriteBatch, Eyes, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 8, frame, Main.DiscoColor, true);
            }


            return false;
        }

    }
}