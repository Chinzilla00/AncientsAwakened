using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Dusts;
using Microsoft.Xna.Framework.Graphics;
using System;
using AAMod.CrossMod;

namespace AAMod.Projectiles.Greed.WKG
{
    public class OreChunk : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
			projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ore");
		}

        public override void AI()
        {
            OreEffect();
            if (projectile.velocity.X > 0)
            {
                projectile.direction = 1;
            }
            else
            {
                projectile.direction = -1;
            }
            projectile.rotation += .2f * projectile.direction;

            for (int m = projectile.oldPos.Length - 1; m > 0; m--)
            {
                projectile.oldPos[m] = projectile.oldPos[m - 1];
            }
            projectile.oldPos[0] = projectile.position;

            int k = (int)projectile.ai[1];
            if(k == ItemID.SilverOre)
            {
                bool flag = false;
                Vector2 velocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true, true, 1);;
                if (velocity != projectile.velocity)
				{
					flag = true;
				}
                if (flag && ProjectileLoader.OnTileCollide(projectile, projectile.velocity))
			    {
                    projectile.velocity = - projectile.velocity;
                    projectile.penetrate--;
                }
            }
            else if(k == ItemID.TungstenOre)
            {
                projectile.penetrate = -1;
                projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().CanImpale = true;
                projectile.GetGlobalProjectile<Buffs.ImplaingProjectile>().damagePerImpaler = 30;
                if (projectile.ai[0] == 1f)
                {
                    projectile.rotation = 0;
                    projectile.tileCollide = false;
                    int num6 = 15;
                    bool flag = false;
                    bool flag2 = false;
                    float[] localAI = projectile.localAI;
                    int num7 = 0;
                    float num8 = localAI[num7];
                    localAI[num7] = num8 + 1f;
                    if (projectile.localAI[0] % 30f == 0f)
                    {
                        flag2 = true;
                    }
                    int num9 = (int)projectile.localAI[1];
                    if (projectile.localAI[0] >= 60 * num6)
                    {
                        flag = true;
                    }
                    else if (num9 < 0 || num9 >= 200)
                    {
                        flag = true;
                    }
                    else if (Main.npc[num9].active && !Main.npc[num9].dontTakeDamage)
                    {
                        projectile.Center = Main.npc[num9].Center - projectile.velocity * 2f;
                        projectile.gfxOffY = Main.npc[num9].gfxOffY;
                        projectile.alpha = Main.npc[num9].alpha;
                        if (flag2)
                        {
                            Main.npc[num9].HitEffect(0, 1.0);
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        projectile.Kill();
                    }
                }
            }
            else if(k == mod.ItemType("Abyssium"))
            {
                if(projectile.ai[0]++ > 800)
                {
                    projectile.Kill();
                }
                if(projectile.ai[0] % 30 == 15)
                {
                    for(int shoot = 0; shoot < 6; shoot ++)
                    {
                        Vector2 vector17 = projectile.velocity;
                        vector17.Normalize();
                        vector17 *= Main.rand.Next(70, 91) * 0.1f;
                        vector17.X += Main.rand.Next(-30, 31) * 0.04f;
                        vector17.Y += Main.rand.Next(-30, 31) * 0.03f;
                        NewProjectile(projectile.position.X, projectile.position.Y, vector17.X, vector17.Y, 523, projectile.damage, 0, Main.myPlayer, Main.rand.Next(20), 0f);
                    }
                }
            }
            else if(k == ItemID.Hellstone)
            {
                if(projectile.ai[0]++ > 800)
                {
                    projectile.Kill();
                }
                if(projectile.ai[0] % 20 == 10)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        Vector2 vector109 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f + 30f);
                        float num824 = projectile.position.X - vector109.X;
                        float num825 = projectile.position.Y - vector109.Y;
                        num824 += Main.rand.Next(-20, 51);
                        num825 += Main.rand.Next(20, 51);
                        num825 *= 0.2f;
                        float num826 = (float)Math.Sqrt(num824 * num824 + num825 * num825);
                        num824 *= num826;
                        num825 *= num826;
                        num824 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        num825 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        int p = NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[p].ranged = true;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].friendly = true;
                    }
                }
            }
            else if(k == ItemID.CobaltOre)
            {
                bool flag = false;
                Vector2 velocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true, true, 1);;
                if (velocity != projectile.velocity)
				{
					flag = true;
				}
                if (flag && ProjectileLoader.OnTileCollide(projectile, projectile.velocity))
			    {
                    projectile.velocity = - projectile.velocity;
                }
            }
            else if(k == ItemID.AdamantiteOre)
            {
                bool flag = false;
                if(projectile.velocity == Vector2.Zero) projectile.Kill();
                else if(projectile.velocity.Length() < 8f) projectile.velocity = Vector2.Normalize(projectile.velocity) * 8f;
                Vector2 velocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true, true, 1);;
                if (velocity != projectile.velocity)
				{
					flag = true;
				}
                if (flag && ProjectileLoader.OnTileCollide(projectile, projectile.velocity))
			    {
                    if(velocity.Y != projectile.velocity.Y) projectile.velocity.Y = 0;
                    if(velocity.X != projectile.velocity.X) projectile.velocity.X = 0;
                }
            }
            else if(k == mod.ItemType("DarkmatterOre"))
            {
                int num5 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width * 3, projectile.height * 3, ModContent.DustType<DarkmatterDust>() , 0f, 0f, 200, default, 0.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.75f;
                Main.dust[num5].fadeIn = 1.3f;
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= Main.rand.Next(50, 100) * 0.04f;
                Main.dust[num5].velocity = vector;
                vector.Normalize();
                vector *= 34f;
                Main.dust[num5].position = projectile.Center - vector;

                if(projectile.ai[0]++ > 800)
                {
                    projectile.Kill();
                }

                for (int i = 0; i < 20; i++)
                {
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 200);
                    offset.Y += (float)(Math.Cos(angle) * 200);
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center - projectile.velocity + offset, 0, 0,  ModContent.DustType<DarkmatterDust>(), 0, 0, 100, default, 1f)];
                    dust.velocity = projectile.velocity;
                    dust.noGravity = true;
                }

                if(projectile.ai[0] % 20 == 10)
                {
                    for(int n = 0; n < 200; n++)
                    {
                        if(!Main.npc[n].townNPC && !Main.npc[n].dontTakeDamage && (Main.npc[n].position - projectile.position).Length() < 200)
                        {
                            Main.player[projectile.owner].ApplyDamageToNPC(Main.npc[n], projectile.damage / 10, 0, 1, false);
                        }
                    }
                }
            }
            else if(k == mod.ItemType("DaybreakIncineriteOre"))
            {
                if(projectile.ai[0] == 1f)
                {
                    if(projectile.localAI[0]++ >= 15f)
                    {
                        projectile.localAI[0] = 0f;
                        NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), projectile.damage, projectile.knockBack * 3, Main.myPlayer, 0, 0);
                    }
                    else if(projectile.localAI[0] <= 0f)
                    {
                        projectile.localAI[0] = 0f;
                    }
                }
            }
            else if(k == mod.ItemType("RadiumOre"))
            {
                projectile.ai[0] ++;
                if(projectile.ai[0] > 600)
                {
                    projectile.ai[0] = 600;
                }
                else
                {
                    projectile.damage += 4;
                }
                projectile.velocity += Vector2.Normalize(projectile.velocity) * 0.03f;
            }
            else if(k == mod.ItemType("EventideAbyssiumOre"))
            {
                if(projectile.localAI[0] == 1)
                {
                    const int homingDelay = 20;
                    const float desiredFlySpeedInPixelsPerFrame = 60;
                    const float amountOfFramesToLerpBy = 20;

                    projectile.ai[0]++;
                    if (projectile.ai[0] > homingDelay)
                    {
                        projectile.ai[0] = homingDelay;

                        int foundTarget = HomeOnTarget();
                        if (foundTarget != -1)
                        {
                            NPC n = Main.npc[foundTarget];
                            Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                            projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                        }
                    }
                }
                else if(projectile.localAI[0] >= 2)
                {
                    projectile.ai[0]++;
                    if (projectile.ai[0] > 20)
                    {
                        projectile.localAI[0] = 1;
                    }
                }
            }
            else if(k == mod.ItemType("Apocalyptite"))
            {
                if((projectile.ai[0] ++) % 40 == 20 && projectile.localAI[0] < 3)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        Vector2 vector82 = new Vector2(projectile.velocity.X, projectile.velocity.Y);
                        float ai = Main.rand.Next(100);
                        Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(3.1415f * 2));
                        Vector2 vector84 = Vector2.Normalize(vector83.RotatedByRandom(0.8)) * 14f;
                        int id = NewProjectile(projectile.position.X + projectile.velocity.X, projectile.position.Y  + projectile.velocity.Y, vector84.X * 2, vector84.Y * 2, ModContent.ProjectileType<Zero.ZeroTaze>(), (int) (projectile.damage * .02f), 0f, Main.myPlayer, vector83.ToRotation(), ai);
                        Main.projectile[id].timeLeft = 30;
                    }
                    projectile.localAI[0] ++;
                }
                if(projectile.ai[0] > 800)
                {
                    projectile.Kill();
                }
            }
            else if(ModLoader.GetMod("CalamityMod") != null)
            {
                if (projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "ChaoticOre").item.type)
                {
                    if(projectile.ai[0]++ > 800)
                    {
                        projectile.Kill();
                    }
                    if (Main.rand.Next(30) == 0)
                    {
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "LavaChunk").projectile.type;
                        int p = NewProjectile(projectile.Center.X + projectile.velocity.X, projectile.Center.Y + projectile.velocity.Y, 0f, 0.1f, projtype, projectile.damage, 2f, projectile.owner, 0f, 0f);
                        Main.projectile[p].ranged = true;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].friendly = true;
                    }
                }
                else if(projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "AstralOre").item.type)
                {
                    if(projectile.ai[0]++ > 800)
                    {
                        projectile.Kill();
                    }
                    if (Main.rand.Next(40) == 0)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            float num13 = projectile.position.X + Main.rand.Next(-400, 400);
                            float num14 = projectile.position.Y - Main.rand.Next(500, 800);
                            Vector2 vector2 = new Vector2(num13, num14);
                            float num15 = projectile.position.X + projectile.width / 2 - vector2.X;
                            float num16 = projectile.position.Y + projectile.height / 2 - vector2.Y;
                            num15 += Main.rand.Next(-100, 101);
                            float num17 = 25f;
                            int num18 = Main.rand.Next(3);
                            if (num18 == 0)
                            {
                                num18 = ModSupport.GetModProjectile("CalamityMod", "AstralStar").projectile.type;
                            }
                            else if (num18 == 1)
                            {
                                num18 = 92;
                            }
                            else
                            {
                                num18 = 12;
                            }
                            float num19 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
                            num19 = num17 / num19;
                            num15 *= num19;
                            num16 *= num19;
                            int num20 = NewProjectile(num13, num14, num15, num16, num18, projectile.damage, 5f, projectile.owner, 0f, 0f);
                            Main.projectile[num20].ranged = true;
                        }
                    }
                }
            }
            else if(ModLoader.GetMod("Redemption") != null)
            {
                
            }
            else if(projectile.ai[1] > 3930 && ItemLoader.GetItem((int) projectile.ai[1]).mod != null)
			{
                try
                {
                    ItemLoader.GetItem((int) projectile.ai[1]).mod.Call(new object[]
                    {
                        "AAOreCannonOreAI",
                        projectile.ai[1]
                    });
                }
                catch
                {
                    return;
                }
			}
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.itemTexture[(int)projectile.ai[1]].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < 3; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((3 - k) / (float)3);
				spriteBatch.Draw(Main.itemTexture[(int)projectile.ai[1]], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}

            if (projectile.ai[1] == ItemID.DemoniteOre || projectile.ai[1] == mod.ItemType("Abyssium") || projectile.ai[1] == ItemID.LunarOre || projectile.ai[1] == mod.ItemType("EventideAbyssiumOre"))
            {
                spriteBatch.Draw(Main.itemTexture[(int)projectile.ai[1]], projectile.position, null, lightColor, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            else if(projectile.ai[1] > 3930 && ItemLoader.GetItem((int) projectile.ai[1]).mod != null)
			{
                try
                {
                    ItemLoader.GetItem((int) projectile.ai[1]).mod.Call(new object[]
                    {
                        "AAOreCannonOreDraw",
                        projectile.ai[1]
                    });
                }
                catch
                {
                    return false;
                }
			}
            
            /*
            Rectangle frame = BaseDrawing.GetFrame(1, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);

            if (projectile.ai[1] == ItemID.DemoniteOre || projectile.ai[1] == mod.ItemType("Abyssium") || projectile.ai[1] == ItemID.LunarOre || projectile.ai[1] == mod.ItemType("EventideAbyssiumOre"))
            {
                BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.oldPos, 1, projectile.rotation, projectile.direction, 1, frame, .8f, 1, 4, true, 0, 0, lightColor);
            }
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            */
            return false;
        }

        public override void Kill(int timeLeft)
        {
            int DustType = DType();
            for (int num468 = 0; num468 < 5; num468++)
            {
                float VelX = -projectile.velocity.X * 0.2f;
                float VelY = -projectile.velocity.Y * 0.2f;
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustType, VelX, VelY);
            }
            if (projectile.ai[1] == ItemID.Meteorite)
            {
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default, 2.1f);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if (projectile.ai[1] == mod.ItemType("Abyssium"))
            {
                for(int shoot = 0; shoot < 3; shoot ++)
                {
                    Vector2 vector17 = projectile.velocity;
                    vector17.Normalize();
                    vector17 *= Main.rand.Next(70, 91) * 0.1f;
                    vector17.X += Main.rand.Next(-30, 31) * 0.04f;
                    vector17.Y += Main.rand.Next(-30, 31) * 0.03f;
                    int id = NewProjectile(projectile.position.X, projectile.position.Y, vector17.X, vector17.Y, 523, projectile.damage, 0, Main.myPlayer, Main.rand.Next(20), 0f);
                    Main.projectile[id].tileCollide = false;
                }
            }
            else if (projectile.ai[1] == ItemID.ChlorophyteOre)
            {
                for (int s = 0; s < 3; s++)
                {
                    NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<OreSpores>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, s);
                }
            }
            else if (projectile.ai[1] == ItemID.LunarOre)
            {
                NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, 0);
            }
            else if (projectile.ai[1] == mod.ItemType("DaybreakIncineriteOre"))
            {
                NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), projectile.damage, projectile.knockBack * 3, Main.myPlayer, 0, 0);
            }
            else if (projectile.ai[1] == mod.ItemType("Apocalyptite"))
            {
                for (int v = 0; v < 4; v++)
                {
                    int x = Main.rand.Next(-6, 6);
                    int y = -Main.rand.Next(3, 5);
                    int p = NewProjectile(projectile.position, new Vector2(x, y), ModContent.ProjectileType<AFrag>(), projectile.damage, 0, projectile.owner, 0, Main.rand.Next(23));
                    Main.projectile[p].Center = projectile.Center;
                }
            }
            else if(ModLoader.GetMod("CalamityMod") != null)
            {
                if(projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "CryonicOre").item.type)
                {
                    Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 27, 1f, 0f);
					float num36 = 0.783f;
					double num37 = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - num36 / 2f;
					double num38 = num36 / 8f;
					for (int num40 = 0; num40 < 8; num40++)
                    {
                        float num41 = Main.rand.Next(1, 7);
                        float num42 = Main.rand.Next(1, 7);
                        double num43 = num37 + num38 * (num40 + num40 * num40) / 2.0 + 32f * num40;
                        int num44 = NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(num43) * 5.0), (float)(Math.Cos(num43) * 5.0) + num41, 90, projectile.damage, 1f, projectile.owner, 0f, 0f);
                        int num45 = NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-(float)Math.Sin(num43) * 5.0), (float)(-(float)Math.Cos(num43) * 5.0) + num42, 90, projectile.damage, 1f, projectile.owner, 0f, 0f);
                        Main.projectile[num44].ranged = true;
                        Main.projectile[num45].ranged = true;
                    }
                    return;
                }
                else if (projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "ChaoticOre").item.type)
                {
                    Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 74, 1f, 0f);
                    int projtype = ModSupport.GetModProjectile("CalamityMod", "ChaosBlaze").projectile.type;
					int p = NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, projtype, projectile.damage / 3, 1f, projectile.owner, 0f, 0f);
                    Main.projectile[p].ranged = true;
					return;
                }
                else if(projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "CharredOre").item.type)
                {
					Vector2 vector5 = new Vector2(projectile.position.X, projectile.position.Y);
                    int num40 = ModSupport.GetModProjectile("CalamityMod", "BrimstoneHellblast").projectile.type;
                    float num35 = projectile.velocity.X;
                    float num37 = projectile.velocity.Y;
                    for (int m = 0; m < 6; m++)
                    {
                        Vector2 vector6 = Vector2.Normalize(new Vector2(num35 + Main.rand.Next(-4, 4), num37 + Main.rand.Next(-4, 4))) * Main.rand.Next(6, 12);
                        int num41 = NewProjectile(vector5.X, vector5.Y, vector6.X, vector6.Y, num40, projectile.damage, 0f, projectile.owner, 1f, 0f);
                        Main.projectile[num41].timeLeft = 300;
                        Main.projectile[num41].tileCollide = false;
                        Main.projectile[num41].hostile = false;
                        Main.projectile[num41].friendly = true;
                        Main.projectile[num41].ranged = true;
                    }
                    int num42 = 12;
                    float num43 = MathHelper.ToRadians(30f);
                    double num44 = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - num43 / 2f;
                    double num45 = num43 / num42;
                    float num46 = 6f;
                    for (int n = 0; n < 6; n++)
                    {
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "BrimstoneBarrage").projectile.type;
                        double num47 = num44 + num45 * (n + n * n) / 2.0 + 32f * n + 0.5f * Main.rand.NextDouble();
                        int id1 = NewProjectile(vector5.X, vector5.Y, (float)(Math.Sin(num47) * num46), (float)(Math.Cos(num47) * num46), projtype, projectile.damage, 0f, projectile.owner, 1f, 0f);
                        int id2 = NewProjectile(vector5.X, vector5.Y, (float)(-(float)Math.Sin(num47) * (double)num46), (float)(-(float)Math.Cos(num47) * (double)num46), projtype, projectile.damage, 0f, projectile.owner, 1f, 0f);
                        Main.projectile[id1].hostile = false;
                        Main.projectile[id1].friendly = true;
                        Main.projectile[id1].ranged = true;
                        Main.projectile[id2].hostile = false;
                        Main.projectile[id2].friendly = true;
                        Main.projectile[id2].ranged = true;
                    }
                    return;
                }
                else if(projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "PerennialOre").item.type)
                {
                    int projtype = ModSupport.GetModProjectile("CalamityMod", "ReaverBlast").projectile.type;
                    int id = NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, projtype, projectile.damage, 0f, projectile.owner, 0f, 0f);
                    Main.projectile[id].ranged = true;
                    return;
                }
                else if(projectile.ai[1] == ModSupport.GetModItem("CalamityMod", "UelibloomOre").item.type)
                {
                    int num21 = Main.rand.Next(2, 4);
					for (int i = 0; i < num21; i++)
					{
						Vector2 vector3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						while (vector3.X == 0f && vector3.Y == 0f)
						{
							vector3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						}
						vector3.Normalize();
						vector3 *= Main.rand.Next(70, 101) * 0.1f;
						int num22 = NewProjectile(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2, vector3.X, vector3.Y, 206, projectile.damage / 2, 0f, projectile.owner, 0f, 0f);
						Main.projectile[num22].magic = false;
                        Main.projectile[num22].ranged = true;
						Main.projectile[num22].netUpdate = true;
					}
                }
            }
            else if(projectile.ai[1] > 3930 && ItemLoader.GetItem((int)projectile.ai[1]).mod != null)
			{
                try
                {
                    ItemLoader.GetItem((int)projectile.ai[1]).mod.Call(new object[]
                    {
                        "AAOreCannonOreKill",
                        projectile.ai[1]
                    });
                }
                catch
                {
                    return;
                }
			}
            else
            {
                return;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            int k = (int)projectile.ai[1];
            if(k == ItemID.CopperOre)
            {
                damage = (int)(damage * 1.1f);
            }
            else if(k == ItemID.IronOre)
            {
               target.AddBuff(BuffID.BrokenArmor, 180);
            }
            else if(k == ItemID.LeadOre)
            {
                target.AddBuff(BuffID.Weak, 180);
            }
            if(k == ItemID.TungstenOre)
            {
                target.AddBuff(mod.BuffType("Impaled"), 900);
                Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                if (projectile.owner == Main.myPlayer)
                {
                    for (int i = 0; i < 200; i++)
                    {
                        if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && ((projectile.friendly && (!Main.npc[i].friendly || projectile.type == 318 || (Main.npc[i].type == NPCID.Guide && projectile.owner < 255 && Main.player[projectile.owner].killGuide) || (Main.npc[i].type == NPCID.Clothier && projectile.owner < 255 && Main.player[projectile.owner].killClothier))) || (projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (projectile.owner < 0 || Main.npc[i].immune[projectile.owner] == 0 || projectile.maxPenetrate == 1) && (Main.npc[i].noTileCollide || !projectile.ownerHitCheck || projectile.CanHit(Main.npc[i])))
                        {
                            bool flag;
                            if (Main.npc[i].type == NPCID.SolarCrawltipedeTail)
                            {
                                Rectangle rect = Main.npc[i].getRect();
                                int num = 8;
                                rect.X -= num;
                                rect.Y -= num;
                                rect.Width += num * 2;
                                rect.Height += num * 2;
                                flag = projectile.Colliding(rectangle, rect);
                            }
                            else
                            {
                                flag = projectile.Colliding(rectangle, Main.npc[i].getRect());
                            }
                            if (flag)
                            {
                                if (Main.npc[i].reflectingProjectiles && projectile.CanReflect())
                                {
                                    Main.npc[i].ReflectProjectile(projectile.whoAmI);
                                    return;
                                }
                                projectile.ai[0] = 1f;
                                projectile.localAI[1] = i;
                                projectile.velocity = (Main.npc[i].Center - projectile.Center) * 0.75f;
                                projectile.netUpdate = true;
                                projectile.StatusNPC(i);
                                projectile.damage = 0;
                                projectile.timeLeft = 1200;
                            }
                        }
                    }
                }
            }
            else if(k == ItemID.GoldOre || k == ItemID.PlatinumOre)
            {
                target.AddBuff(BuffID.Midas, 180);
                if(k == ItemID.GoldOre)
                {
                    damage += (int)(target.defense * (Main.expertMode? 0.75f : 0.5f));
                }
                if(k == ItemID.PlatinumOre && Main.rand.Next(5) == 0)
                {
                    int itemcreat = 0;
                    itemcreat = Item.NewItem((int)target.position.X, (int)target.position.Y, 16, 16, ItemID.SilverCoin, Main.rand.Next(15, 20), false, 0, false, false);
                    if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            else if(k == ItemID.DemoniteOre)
            {
                damage += 50;
                if (Main.rand.Next(5) == 0)
                {
                    target.AddBuff(BuffID.ShadowFlame, 180);
                }
            }
            else if(k == ItemID.CrimtaneOre)
            {
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= (float)(damage * 0.02);
                NewProjectile(target.position.X, target.position.Y, 0f, 0f, 305, 0, 0f, projectile.owner, projectile.owner, (float)(damage * 0.02));
                if (Main.rand.Next(5) == 0)
                {
                    target.AddBuff(BuffID.Confused, 180);
                }
            }
            else if(k == mod.ItemType("Incinerite"))
            {
                target.AddBuff(BuffID.OnFire, 240);
                if (Main.rand.Next(5) == 0)
                {
                    for(int shoot = 0; shoot < 3; shoot ++)
                    {
                        Vector2 vector109 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f + 30f);
                        float num824 = projectile.position.X - vector109.X;
                        float num825 = projectile.position.Y - vector109.Y;
                        num824 += Main.rand.Next(-20, 51);
                        num825 += Main.rand.Next(20, 51);
                        num825 *= 0.2f;
                        float num826 = (float)Math.Sqrt(num824 * num824 + num825 * num825);
                        num824 *= num826;
                        num825 *= num826;
                        num824 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        num825 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                        int p = NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[p].ranged = true;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].friendly = true;
                    }
                }
            }
            else if(k == mod.ItemType("Abyssium"))
            {
                target.AddBuff(BuffID.Venom, 180);
            }
            else if(k == mod.ItemType("DynaskullOre"))
            {
                if(projectile.ai[0] != 1f)
                {
                    Vector2 shoot = Vector2.Zero;
                    int projType = projectile.type;
                    for(int shootid = 0; shootid < 16; shootid++)
                    {
                        shoot = new Vector2((float)Math.Sin(shootid * 0.125f * Math.PI), (float)Math.Cos(shootid * 0.125f * Math.PI));
                        shoot *= 10f;
                        int p = NewProjectile(projectile.position.X, projectile.position.Y, shoot.X, shoot.Y, projType, damage/2, 5, Main.myPlayer, 0, mod.ItemType("DynaskullOre"));
                        Main.projectile[p].ai[0] = 1f;
                        Main.projectile[p].scale /= 2;
                        Main.projectile[p].width /= 2;
                        Main.projectile[p].height /= 2;
                    }
                }
            }
            else if(k == ItemID.Hellstone)
            {
                target.AddBuff(BuffID.OnFire, 1200);
                for(int shoot = 0; shoot < 7; shoot ++)
                {
                    Vector2 vector109 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f + 30f);
                    float num824 = projectile.position.X - vector109.X;
                    float num825 = projectile.position.Y - vector109.Y;
                    num824 += Main.rand.Next(-20, 51);
                    num825 += Main.rand.Next(20, 51);
                    num825 *= 0.2f;
                    float num826 = (float)Math.Sqrt(num824 * num824 + num825 * num825);
                    num824 *= num826;
                    num825 *= num826;
                    num824 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                    num825 *= 1f + Main.rand.Next(-30, 31) * 0.01f;
                    int p = NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), damage, 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[p].ranged = true;
                    Main.projectile[p].hostile = false;
                    Main.projectile[p].friendly = true;
                }
            }
            else if(k == ItemID.CobaltOre)
            {
                if(projectile.tileCollide)
                {
                    projectile.velocity = - projectile.velocity;
                }
            }
            else if(k == ItemID.PalladiumOre)
            {
                if(projectile.damage / 2 > 100f)
                NewProjectile(projectile.position.X, projectile.position.Y, -projectile.velocity.X, -projectile.velocity.Y, mod.ProjectileType("OreChunk"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, ItemID.PalladiumOre);
            }
            else if(k == ItemID.MythrilOre || k == ItemID.OrichalcumOre)
            {
                if(k == ItemID.MythrilOre)
                {
                    target.AddBuff(BuffID.CursedInferno, 600);
                }
                else if(k == ItemID.OrichalcumOre)
                {
                    target.AddBuff(BuffID.Ichor, 600);
                }

                for(int i = 0; i < 200; i++)
                {
                    if((Main.npc[i].Center - target.Center).Length() < 200f && !Main.npc[i].friendly && !Main.npc[i].townNPC && !Main.npc[i].dontTakeDamage && Main.npc[i] != target)
                    {
                        projectile.velocity = target.DirectionTo(Main.npc[i].Center) * projectile.velocity.Length();
                        break;
                    }
                }
            }
            else if(k == ItemID.AdamantiteOre)
            {
                projectile.scale = (float)(projectile.scale / 1.3);
                projectile.width = (int)(projectile.width / 1.3);
                projectile.height = (int)(projectile.height / 1.3);
                projectile.damage = (int)(projectile.damage / 1.3);
            }
            else if(k == mod.ItemType("HallowedOre"))
            {
                //target.AddBuff(BuffID.Slow, 180);
                Player player = Main.player[projectile.owner];
                if(projectile.ai[0] < 2f)
                {
                    int p = NewProjectile(player.Center.X, player.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("OreChunk"), projectile.damage, projectile.knockBack, projectile.owner, ++projectile.ai[0], mod.ItemType("HallowedOre"));
                }
            }
            else if(k == ItemID.ChlorophyteOre)
            {
                for(int shootid = 0; shootid < 4; shootid++)
                {
                    NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * Main.rand.Next(-3, 3) * 0.1f, projectile.velocity.Y * Main.rand.Next(-3, 3) * 0.1f, 228, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                }
                target.AddBuff(BuffID.Poisoned, 240);
                target.AddBuff(BuffID.Venom, 240);
            }
            else if(k == ItemID.LunarOre)
            {
                if(projectile.damage / 2 > 100f)
                {
                    Vector2 vector = projectile.velocity.RotatedBy(Math.PI /2);
                    vector = Vector2.Normalize(vector);
                    for(int newone = -1; newone <= 1; newone += 2)
                    {
                        int p = NewProjectile(projectile.Center.X + vector.X * 40f * newone, projectile.Center.Y + vector.Y * 40f * newone, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("OreChunk"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, ItemID.LunarOre);
                        Main.projectile[p].scale /= 2;
                        Main.projectile[p].width /= 2;
                        Main.projectile[p].height /= 2;
                        Main.projectile[p].ai[0] = 1f;
                    }
                }
                if(projectile.ai[0] != 1f) NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), (int)(projectile.damage / 2.5), projectile.knockBack, projectile.owner, 0, 0);
            }
            else if(k == mod.ItemType("SkyCrystal"))
            {
                int num90 = 3;
                if (Main.rand.Next(3) == 0)
                {
                    num90 ++;
                }
                for (int num91 = 0; num91 < num90; num91++)
                {
                    Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f + Main.rand.Next(201) * -(float)projectile.direction + (projectile.Center.X - projectile.position.X), projectile.Center.Y - 600f);
                    vector2.X = (vector2.X * 10f + projectile.Center.X) / 11f + Main.rand.Next(-100, 101);
                    vector2.Y -= 150 * num91;
                    float num82 = projectile.Center.X - vector2.X;
                    float num83 = projectile.Center.Y - vector2.Y;
                    if (num83 < 0f)
                    {
                        num83 *= -1f;
                    }
                    if (num83 < 20f)
                    {
                        num83 = 20f;
                    }
                    float num92 = num82 + Main.rand.Next(-40, 41) * 0.03f;
                    float speedY2 = num83 + Main.rand.Next(-40, 41) * 0.03f;
                    num92 *= Main.rand.Next(75, 150) * 0.01f;
                    vector2.X += Main.rand.Next(-50, 51);
                    Vector2 speedfinal = Vector2.Normalize(new Vector2(num92, speedY2)) * projectile.velocity.Length();
                    NewProjectile(vector2.X, vector2.Y, speedfinal.X, speedfinal.Y, mod.ProjectileType("SeraphFeather"), projectile.damage, 0, projectile.owner, 0f, 1f);
                }
            }
            else if(k == mod.ItemType("CovetiteOre"))
            {
                for(int i = 0; i < 12; i++)
                {
                    NewProjectile(projectile.position.X + 30f, projectile.position.Y + 30f, Main.rand.Next(-3, 4), Main.rand.Next(-3, 10), ModContent.ProjectileType<Gold>(), projectile.damage / 2, 1, projectile.owner, 0, 1);
                }
            }
            else if(k == mod.ItemType("DarkmatterOre"))
            {
                target.AddBuff(ModContent.BuffType<Buffs.Electrified>(), 180);
            }
            else if(k == mod.ItemType("DaybreakIncineriteOre"))
            {
                target.AddBuff(BuffID.Daybreak, 400);
                NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("DaybreakBlast"), (int)(projectile.damage / 2.5), projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[0] = 1f;
            }
            else if(k == mod.ItemType("EventideAbyssiumOre"))
            {
                target.AddBuff(ModContent.BuffType<Buffs.Moonraze>(), 400);

                projectile.localAI[0] ++;

                if(projectile.velocity.Length() < 10f) projectile.velocity = 10 * Vector2.Normalize(projectile.velocity);
            }
            else if(ModLoader.GetMod("CalamityMod") != null)
            {
                if(k == ModSupport.GetModItem("CalamityMod", "AerialiteOre").item.type)
                {
                    for (int i = 0; i < 4; i++)
					{
						float num = target.position.X + Main.rand.Next(-400, 400);
						float num2 = target.position.Y - Main.rand.Next(500, 800);
						Vector2 vector = new Vector2(num, num2);
						float num3 = target.position.X + target.width / 2 - vector.X;
						float num4 = target.position.Y + target.height / 2 - vector.Y;
						num3 += Main.rand.Next(-100, 101);
						float num5 = 20;
						float num6 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
						num6 = num5 / num6;
						num3 *= num6;
						num4 *= num6;
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "StickyFeatherAero").projectile.type;
						NewProjectile(num, num2, num3, num4, projtype, projectile.damage, 1f, projectile.owner, 0f, 0f);
					}
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CryonicOre").item.type)
                {
                    target.AddBuff(24, 240, false);
                    target.AddBuff(44, 240, false);
                    int bufftype = ModSupport.GetModBuff("CalamityMod", "GlacialState").Type;
                    target.AddBuff(bufftype, 120, false);
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "AstralOre").item.type)
                {
                    int bufftype = ModSupport.GetModBuff("CalamityMod", "AstralInfectionDebuff").Type;
                    target.AddBuff(bufftype, 360, false);
                    for (int j = 0; j < 6; j++)
					{
						float num13 = target.position.X + Main.rand.Next(-400, 400);
						float num14 = target.position.Y - Main.rand.Next(500, 800);
						Vector2 vector2 = new Vector2(num13, num14);
						float num15 = target.position.X + target.width / 2 - vector2.X;
						float num16 = target.position.Y + target.height / 2 - vector2.Y;
						num15 += Main.rand.Next(-100, 101);
						float num17 = 25f;
						int num18 = Main.rand.Next(3);
						if (num18 == 0)
						{
							num18 = ModSupport.GetModProjectile("CalamityMod", "AstralStar").projectile.type;
						}
						else if (num18 == 1)
						{
							num18 = 92;
						}
						else
						{
							num18 = 12;
						}
						float num19 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
						num19 = num17 / num19;
						num15 *= num19;
						num16 *= num19;
						int num20 = NewProjectile(num13, num14, num15, num16, num18, projectile.damage, 5f, projectile.owner, 0f, 0f);
						Main.projectile[num20].ranged = true;
                        Main.projectile[num20].noDropItem = true;
					}
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "ChaoticOre").item.type)
                {
                    target.AddBuff(24, 720, false);
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CharredOre").item.type)
                {
                    int bufftype = ModSupport.GetModBuff("CalamityMod", "BrimstoneFlames").Type;
                    target.AddBuff(bufftype, 720, false);
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "PerennialOre").item.type)
                {
                    Main.PlaySound(SoundID.NPCHit, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
					float num46 = 0.783f;
					double num47 = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - num46 / 2f;
					double num48 = num46 / 8f;
                    for (int num50 = 0; num50 < 4; num50++)
                    {
                        float x2 = Utils.NextBool(Main.rand, 2) ? (projectile.Center.X + 100f) : (projectile.Center.X - 100f);
                        Vector2 vector5 = new Vector2(x2, projectile.Center.Y + Main.rand.Next(-100, 101));
                        double num51 = num47 + num48 * (num50 + num50 * num50) / 2.0 + 32f * num50;
                        int num52 = NewProjectile(vector5.X, vector5.Y, (float)(Math.Sin(num51) * 5.0), (float)(Math.Cos(num51) * 5.0), 567, projectile.damage, 2f, projectile.owner, 0f, 0f);
                        Main.projectile[num52].ranged = true;
                        Main.projectile[num52].usesLocalNPCImmunity = true;
                        Main.projectile[num52].localNPCHitCooldown = 60;
                        int num53 = NewProjectile(vector5.X, vector5.Y, (float)(-(float)Math.Sin(num51) * 5.0), (float)(-(float)Math.Cos(num51) * 5.0), 568, projectile.damage, 2f, projectile.owner, 0f, 0f);
                        Main.projectile[num53].ranged = true;
                        Main.projectile[num53].usesLocalNPCImmunity = true;
                        Main.projectile[num53].localNPCHitCooldown = 60;
                    }
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "UelibloomOre").item.type)
                {
                    int num3 = 9 + Main.rand.Next(3);
                    for (int i = 0; i < num3; i++)
                    {
                        float num4 = 0.025f * i;
                        float num5 = projectile.velocity.X + Main.rand.Next(-25, 26) * num4;
                        float num6 = projectile.velocity.Y + Main.rand.Next(-25, 26) * num4;
                        float num7 = projectile.velocity.Length();
                        num7 = 14f / num7;
                        num5 *= num7;
                        num6 *= num7;
                        int id = NewProjectile(Main.player[projectile.owner].position.X, Main.player[projectile.owner].position.Y, num5, num6, 206, projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 0f);
                        Main.projectile[id].ranged = true;
                    }
                    if(!target.SpawnedFromStatue && (target.damage > 5 || target.boss) && target.lifeMax > 100 && Main.rand.Next(5) == 0)
                    {
                        int itemcreat = 0;
                        itemcreat = Item.NewItem((int)target.position.X, (int)target.position.Y, 16, 16, 58, 1, false, 0, false, false);
                        if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                        }
                        if(Main.bloodMoon)
                        {
                            int droptype = ModSupport.GetModItem("CalamityMod", "BloodOrb").item.type;
                            itemcreat = Item.NewItem((int)target.position.X, (int)target.position.Y, 16, 16, droptype, 1, false, 0, false, false);
                            if (Main.netMode == NetmodeID.MultiplayerClient && itemcreat > 0)
                            {
                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
                            }
                        }
                    }
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "ExodiumClusterOre").item.type)
                {
                    int bufftype1 = ModSupport.GetModBuff("CalamityMod", "Horror").Type;
                    int bufftype2 = ModSupport.GetModBuff("CalamityMod", "MarkedforDeath").Type;
                    target.AddBuff(bufftype1, 240, false);
                    target.AddBuff(bufftype2, 240, false);
                    if(!target.immortal)
                    {
                        int rangedLevel = (int)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "rangedLevel", false, false);
                        if(rangedLevel < 12500)
                        {
                            rangedLevel += 2;
                            CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "rangedLevel", rangedLevel, false, false);
                        }
                    }
                    bool revenge = (bool)ModSupport.GetModWorldConditions("CalamityMod", "CalamityWorld", "revenge", false, true);
                    if(revenge)
                    {
                        bool Death = (bool)ModSupport.GetModWorldConditions("CalamityMod", "CalamityWorld", "death", false, true);
                        int stress = (int)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "stress", false, false);
                        bool rageMode = (bool)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "rageMode", false, false);
                        int adrenaline = (int)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "adrenaline", false, false);
                        bool adrenalineMode = (bool)ModSupport.GetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "adrenalineMode", false, false);
                        if(stress < 10000 && !rageMode)
                        {
                            stress += Death? 350 : 150;
                            CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "stress", stress, false, false);
                        }
                        if(adrenaline < 10000 && !adrenalineMode)
                        {
                            adrenaline += Death? 350 : 150;
                            CrossMod.ModSupport.SetModPlayerConditions("CalamityMod", Main.player[projectile.owner], "CalamityPlayer", "adrenaline", adrenaline, false, false);
                        }
                    }
                    return;
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "AuricOre").item.type)
                {
                    float num2 = Main.rand.Next(22, 30);
                    int num6 = 4;
                    for (int i = 0; i < num6; i++)
                    {
                        Vector2 vector = projectile.Center;
                        vector.X = (vector.X + projectile.Center.X) / 2f;
                        vector.Y -= 100 * i;
                        float num3 = projectile.position.X - vector.X;
                        float num4 = projectile.position.X - vector.Y;
                        float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                        num5 = num2 / num5;
                        num3 *= num5;
                        num4 *= num5;
                        float num7 = num3 + Main.rand.Next(-360, 361) * 0.02f;
                        float num8 = num4 + Main.rand.Next(-360, 361) * 0.02f;
                        int projtype = ModSupport.GetModProjectile("CalamityMod", "ElementBall").projectile.type;
                        NewProjectile(vector.X, vector.Y, num7, num8, projtype, projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, Main.rand.Next(3));
                    }
                }
            }
            else
            {
                return;
            }
        }

        public void OreEffect()
        {
            int k = (int)projectile.ai[1];
            Item item = new Item();
            if(k > 0)
            {
                item.SetDefaults(k, false);
            }
            if(k == ItemID.DemoniteOre || k == mod.ItemType("Abyssium") || k == mod.ItemType("RadiumOre"))
            {
                projectile.extraUpdates = 1;
            }
            else if(k == ItemID.Hellstone || k == mod.ItemType("Incinerite"))
            {
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if(k == ItemID.LunarOre)
            {
                projectile.extraUpdates = 2;
            }
            else if(k == mod.ItemType("EventideAbyssiumOre"))
            {
                projectile.extraUpdates = 2;
                projectile.tileCollide = false;
                for (int num291 = 0; num291 < 5; num291++)
                {
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Moonraze>(), 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if(ModLoader.GetMod("CalamityMod") != null)
            {
                if(k == ModSupport.GetModItem("CalamityMod", "AerialiteOre").item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.t_Slime, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CryonicOre").item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.BlueCrystalShard, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "AstralOre").item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int dustType = ModSupport.GetModDust("Calamity", "AstralChunkDust").Type;
                        int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "ChaoticOre").item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "CharredOre").item.type)
                {
                    for (int num291 = 0; num291 < 5; num291++)
                    {
                        int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 235, 0f, -1f, 90, default, 3f);
                        Main.dust[num292].velocity *= 2f;
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "PerennialOre").item.type)
                {
                    for (int num291 = 0; num291 < 3; num291++)
                    {
                        int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 74, projectile.velocity.X * 0.2f + projectile.direction * 3, projectile.velocity.Y * 0.2f, 100, default, 0.75f);
                        Main.dust[num292].noGravity = true;
                    };
                }
                else if(k == ModSupport.GetModItem("CalamityMod", "UelibloomOre").item.type)
                {
                    for (int num291 = 0; num291 < 2; num291++)
                    {
                        int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 157, 0f, -1f, 90, default, 3f);
                        Main.dust[num292].noGravity = true;
                    };
                }
            }
            else if(k > 3930 && Config.LuckyOre[k] > 650 && item.modItem.mod != AAMod.instance)
            {
                int dustid = DustID.Copper;
                switch (WorldGen.genRand.Next(10))
                {
                    case 0:
                        dustid = DustID.Copper; break;
                    case 1:
                        dustid = DustID.Tin; break;
                    case 2:
                        dustid = DustID.Iron; break;
                    case 3:
                        dustid = DustID.Lead; break;
                    case 4:
                        dustid = DustID.Silver; break;
                    case 5:
                        dustid = DustID.Tungsten; break;
                    case 6:
                        dustid = DustID.Gold; break;
                    case 7:
                        dustid = DustID.Platinum; break;
                    case 8:
                        dustid = DustID.t_Meteor; break;
                    case 9:
                        dustid = DustID.Fire; break;
                }
                for (int num291 = 0; num291 < 3; num291++)
                {
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustid, 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else
            {
                return;
            }
        }

        public int Damage()
        {
            int orevalue = 0;
            if(Config.LuckyOre.TryGetValue((int)projectile.ai[1], out orevalue))
            {
                return (int)Math.Exp(orevalue * 0.67/100);
            }
            else if((int)projectile.ai[1] == ItemID.Hellstone)
            {
                return (int)Math.Exp(500 * 0.67/100);
            }
            else
            {
                return (int)Math.Exp(100 * 0.67/100);
            }
            /* 
            switch ((int)projectile.ai[1])
            {
                case 0:
                    return 8;
                case 1:
                    return 9;
                case 2:
                    return 10;
                case 3:
                case 4:
                    return 11;
                case 5:
                    return 12;
                case 6:
                    return 13;
                case 7:
                    return 15;
                case 8:
                    return 21;
                case 9:
                    return 19;
                case 10:
                    return 22;
                case 11:
                    return 14;
                case 12:
                    return 26;
                case 13:
                    return 36;
                case 14:
                    return 39;
                case 15:
                    return 41;
                case 16:
                    return 44;
                case 17:
                    return 47;
                case 18:
                    return 50;
                case 19:
                    return 52;
                case 20:
                    return 57;
                case 21:
                    return 75;
                case 22:
                    return 110;
                case 23:
                    return 130;
                case 24:
                    return 170;
                case 25:
                    return 160;
                case 26:
                    return 130;
                case 27:
                    return 150;
                default:
                    goto case 0;
            }
            */
        }

        public int DType()
        {
            int k = (int)projectile.ai[1];
            if(k == ItemID.CopperOre)
            {
                return DustID.Copper;
            }
            else if(k == ItemID.TinOre)
            {
                return DustID.Tin;
            }
            else if(k == ItemID.IronOre)
            {
                return DustID.Iron;
            }
            else if(k == ItemID.LeadOre)
            {
                return DustID.Lead;
            }
            else if(k == ItemID.SilverOre)
            {
                return DustID.Silver;
            }
            else if(k == ItemID.TungstenOre)
            {
                return DustID.Tungsten;
            }
            else if(k == ItemID.GoldOre)
            {
                return DustID.Gold;
            }
            else if(k == ItemID.PlatinumOre)
            {
                return DustID.Platinum;
            }
            else if(k == ItemID.Meteorite)
            {
                return DustID.t_Meteor;
            }
            else if (k == ItemID.DemoniteOre)
            {
                return 14;
            }
            else if (k == ItemID.CrimtaneOre)
            {
                return 117;
            }
            else if (k == mod.ItemType("Abyssium"))
            {
                return ModContent.DustType<AbyssiumDust>();
            }
            else if (k == mod.ItemType("Incinerite"))
            {
                return ModContent.DustType<IncineriteDust>();
            }
            else if (k == ItemID.Hellstone)
            {
                return DustID.Fire;
            }
            else if (k == ItemID.CobaltOre)
            {
                return 48;
            }
            else if (k == ItemID.PalladiumOre)
            {
                return 144;
            }
            else if (k == ItemID.MythrilOre)
            {
                return 49;
            }
            else if (k == ItemID.OrichalcumOre)
            {
                return 145;
            }
            else if (k == ItemID.AdamantiteOre)
            {
                return 50;
            }
            else if (k == ItemID.TitaniumOre)
            {
                return 146;
            }
            else if (k == mod.ItemType("HallowedOre"))
            {
                return DustID.Gold;
            }
            else if (k == ItemID.ChlorophyteOre)
            {
                return 128;
            }
            else if (k == ItemID.LunarOre)
            {
                return ModContent.DustType<LuminiteDust>();
            }
            else if (k == mod.ItemType("DarkmatterOre"))
            {
                return ModContent.DustType<DarkmatterDust>();
            }
            else if (k == mod.ItemType("RadiumOre"))
            {
                return ModContent.DustType<RadiumDust>();
            }
            else if (k == mod.ItemType("DaybreakIncineriteOre"))
            {
                return ModContent.DustType<DaybreakIncineriteDust>();
            }
            else if (k == mod.ItemType("EventideAbyssiumOre"))
            {
                return ModContent.DustType<YamataDust>();
            }
            else if (k == mod.ItemType("Apocalyptite"))
            {
                return ModContent.DustType<VoidDust>();
            }
            else if (Config.LuckyOre[k] <= 300)
            {
                return DustID.Copper;
            }
            else if (Config.LuckyOre[k] <= 700)
            {
                return DustID.Gold;
            }
            else
            {
                switch (WorldGen.genRand.Next(18))
                {
                    case 0:
                        return DustID.Copper;
                    case 1:
                        return DustID.Tin;
                    case 2:
                        return DustID.Iron;
                    case 3:
                        return DustID.Lead;
                    case 4:
                        return DustID.Silver;
                    case 5:
                        return DustID.Tungsten;
                    case 6:
                        return DustID.Gold;
                    case 7:
                        return DustID.Platinum;
                    case 8:
                        return DustID.t_Meteor;
                    case 9:
                        return ModContent.DustType<LuminiteDust>();
                    case 10:
                        return ModContent.DustType<DarkmatterDust>();
                    case 11:
                        return ModContent.DustType<RadiumDust>();
                    case 12:
                        return ModContent.DustType<DaybreakIncineriteDust>();
                    case 13:
                        return ModContent.DustType<YamataDust>();
                    case 14:
                        return ModContent.DustType<VoidDust>();
                    case 15:
                        return ModContent.DustType<IncineriteDust>();
                    case 16:
                        return ModContent.DustType<AbyssiumDust>();
                    case 17:
                        return DustID.Fire;
                }
            }

            switch ((int)projectile.ai[1])
            {
                case 0:
                    return DustID.Copper;
                case 1:
                    return DustID.Tin;
                case 2:
                    return DustID.Iron;
                case 3:
                    return DustID.Lead;
                case 4:
                    return DustID.Silver;
                case 5:
                    return DustID.Tungsten;
                case 6:
                    return DustID.Gold;
                case 7:
                    return DustID.Platinum;
                case 8:
                    return DustID.t_Meteor;
                case 9:
                    return 14;
                case 10:
                    return 117;
                case 11:
                    return ModContent.DustType<IncineriteDust>();
                case 12:
                    return ModContent.DustType<AbyssiumDust>();
                case 13:
                    return DustID.Fire;
                case 14:
                    return 48;
                case 15:
                    return 144;
                case 16:
                    return 49;
                case 17:
                    return 145;
                case 18:
                    return 50;
                case 19:
                    return 146;
                case 20:
                    return DustID.Gold;
                case 21:
                    return 128;
                case 22:
                    return ModContent.DustType<LuminiteDust>();
                case 23:
                    return ModContent.DustType<DarkmatterDust>();
                case 24:
                    return ModContent.DustType<RadiumDust>();
                case 25:
                    return ModContent.DustType<DaybreakIncineriteDust>();
                case 26:
                    return ModContent.DustType<YamataDust>();
                case 27:
                    return ModContent.DustType<VoidDust>();
                default:
                    goto case 0;
            }

        }

        private int NewProjectile(float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int proj = Projectile.NewProjectile(X, Y, SpeedX, SpeedY, Type, Damage, KnockBack, Owner, ai0, ai1);
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].melee = false;
            Main.projectile[proj].ranged = true;
            Main.projectile[proj].magic = false;
            Main.projectile[proj].minion = false;
            Main.projectile[proj].thrown = false;
            Main.projectile[proj].sentry = false;
            return proj;
        }

        private int NewProjectile(Vector2 position, Vector2 velocity, int Type, int Damage, float KnockBack, int Owner = 255, float ai0 = 0f, float ai1 = 0f)
		{
            int proj = Projectile.NewProjectile(position, velocity, Type, Damage, KnockBack, Owner, ai0, ai1);
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].melee = false;
            Main.projectile[proj].ranged = true;
            Main.projectile[proj].magic = false;
            Main.projectile[proj].minion = false;
            Main.projectile[proj].thrown = false;
            Main.projectile[proj].sentry = false;
            return proj;
        }
        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance)
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
    }
}
