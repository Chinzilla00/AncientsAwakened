using Microsoft.Xna.Framework;
using Terraria;
using BaseMod;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Dusts;
using Microsoft.Xna.Framework.Graphics;
using System;

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
            else if(k == mod.ItemType("DaybreakIncineriteOre"))
            {
                if(projectile.ai[0] == 1f)
                {
                    if(projectile.localAI[0] >= 30f)
                    {
                        Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), projectile.damage, projectile.knockBack * 3, Main.myPlayer, 0, 0);
                    }
                    else if(projectile.localAI[0] <= 0f)
                    {
                        projectile.localAI[0] = 0f;
                    }
                    else
                    {
                        projectile.localAI[0] ++;
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
                projectile.velocity += Vector2.Normalize(projectile.velocity);
                projectile.damage += (int)(projectile.ai[0] / 2);
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
                else if(projectile.localAI[0] == 2)
                {
                    projectile.ai[0]++;
                    if (projectile.ai[0] > 20)
                    {
                        projectile.localAI[0] = 1;
                    }
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
            else if (projectile.ai[1] == ItemID.ChlorophyteOre)
            {
                for (int s = 0; s < 3; s++)
                {
                    Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<OreSpores>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, s);
                }
            }
            else if (projectile.ai[1] == ItemID.LunarOre)
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, 0);
            }
            else if (projectile.ai[1] == mod.ItemType("DaybreakIncineriteOre"))
            {
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<DaybreakBlast>(), projectile.damage, projectile.knockBack * 3, Main.myPlayer, 0, 0);
            }
            else if (projectile.ai[1] == mod.ItemType("Apocalyptite"))
            {
                for (int v = 0; v < 4; v++)
                {
                    int x = Main.rand.Next(-6, 6);
                    int y = -Main.rand.Next(3, 5);
                    int p = Projectile.NewProjectile(projectile.position, new Vector2(x, y), ModContent.ProjectileType<AFrag>(), projectile.damage, 0, Main.myPlayer, 0, Main.rand.Next(23));
                    Main.projectile[p].Center = projectile.Center;
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
                        if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && ((projectile.friendly && (!Main.npc[i].friendly || projectile.type == 318 || (Main.npc[i].type == 22 && projectile.owner < 255 && Main.player[projectile.owner].killGuide) || (Main.npc[i].type == 54 && projectile.owner < 255 && Main.player[projectile.owner].killClothier))) || (projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (projectile.owner < 0 || Main.npc[i].immune[projectile.owner] == 0 || projectile.maxPenetrate == 1) && (Main.npc[i].noTileCollide || !projectile.ownerHitCheck || projectile.CanHit(Main.npc[i])))
                        {
                            bool flag;
                            if (Main.npc[i].type == 414)
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
                if(k == ItemID.PlatinumOre && Main.rand.Next(5) == 0)
                {
                    int itemcreat = 0;
                    itemcreat = Item.NewItem((int)target.position.X, (int)target.position.Y, 16, 16, ItemID.SilverCoin, Main.rand.Next(5, 10), false, 0, false, false);
                    if (Main.netMode == 1 && itemcreat > 0)
                    {
                        NetMessage.SendData(21, -1, -1, null, itemcreat, 1f, 0f, 0f, 0, 0, 0);
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
                Projectile.NewProjectile(target.position.X, target.position.Y, 0f, 0f, 305, 0, 0f, projectile.owner, projectile.owner, (float)(damage * 0.02));
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
                        Vector2 vector109 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f + 30f);
                        float num824 = projectile.position.X - vector109.X;
                        float num825 = projectile.position.Y - vector109.Y;
                        num824 += (float)Main.rand.Next(-20, 51);
                        num825 += (float)Main.rand.Next(20, 51);
                        num825 *= 0.2f;
                        float num826 = (float)Math.Sqrt((double)(num824 * num824 + num825 * num825));
                        num824 *= num826;
                        num825 *= num826;
                        num824 *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
                        num825 *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
                        int p = Projectile.NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[p].ranged = true;
                        Main.projectile[p].hostile = false;
                        Main.projectile[p].friendly = true;
                    }
                }
            }
            else if(k == mod.ItemType("Abyssium"))
            {
                target.AddBuff(BuffID.Venom, 180);
                if (Main.rand.Next(5) == 0)
                {
                    for(int shoot = 0; shoot < 3; shoot ++)
                    {
                        float num82 = (float)Main.mouseX + Main.screenPosition.X;
						float num83 = (float)Main.mouseY + Main.screenPosition.Y;
                        Vector2 vector17 = new Vector2(num82, num83);
                        vector17.X += (float)Main.rand.Next(-30, 31) * 0.04f;
                        vector17.Y += (float)Main.rand.Next(-30, 31) * 0.03f;
                        vector17.Normalize();
                        vector17 *= (float)Main.rand.Next(70, 91) * 0.1f;
                        vector17.X += (float)Main.rand.Next(-30, 31) * 0.04f;
                        vector17.Y += (float)Main.rand.Next(-30, 31) * 0.03f;
                        Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vector17.X, vector17.Y, 523, damage, 0, Main.myPlayer, (float)Main.rand.Next(20), 0f);
                    }
                }
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
                        int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, shoot.X, shoot.Y, projType, damage/2, 5, Main.myPlayer, 0, mod.ItemType("DynaskullOre"));
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
                    Vector2 vector109 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f + 30f);
                    float num824 = projectile.position.X - vector109.X;
                    float num825 = projectile.position.Y - vector109.Y;
                    num824 += (float)Main.rand.Next(-20, 51);
                    num825 += (float)Main.rand.Next(20, 51);
                    num825 *= 0.2f;
                    float num826 = (float)Math.Sqrt((double)(num824 * num824 + num825 * num825));
                    num824 *= num826;
                    num825 *= num826;
                    num824 *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
                    num825 *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
                    int p = Projectile.NewProjectile(vector109.X, vector109.Y, num824, num825, Main.rand.Next(326, 329), damage, 0f, Main.myPlayer, 0f, 0f);
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
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -projectile.velocity.X, -projectile.velocity.Y, mod.ProjectileType("OreChunk"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, ItemID.PalladiumOre);
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
                    if((Main.npc[i].Center - target.Center).Length() < 400f && !Main.npc[i].friendly && !Main.npc[i].townNPC && !Main.npc[i].dontTakeDamage && Main.npc[i] != target)
                    {
                        projectile.velocity = target.DirectionTo(Main.npc[i].Center) * projectile.velocity.Length();
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
                if(projectile.ai[0] != 0)
                {
                    int p = Projectile.NewProjectile(player.Center.X, player.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("OreChunk"), projectile.damage, projectile.knockBack, projectile.owner, 0f, mod.ItemType("HallowedOre"));
                    Main.projectile[p].ai[0] = 1f;
                }
            }
            else if(k == ItemID.ChlorophyteOre)
            {
                for(int shootid = 0; shootid < 4; shootid++)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X * Main.rand.Next(-3, 3) * 0.1f, projectile.velocity.Y * Main.rand.Next(-3, 3) * 0.1f, 228, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
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
                        int p = Projectile.NewProjectile(projectile.Center.X + vector.X * 40f, projectile.Center.Y + vector.Y * 40f, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("OreChunk"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, ItemID.LunarOre);
                        Main.projectile[p].scale /= 2;
                        Main.projectile[p].width /= 2;
                        Main.projectile[p].height /= 2;
                        Main.projectile[p].ai[0] = 1f;
                    }
                }
                if(projectile.ai[0] != 1f) Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<LuminiteBlast>(), (int)(projectile.damage / 2.5), projectile.knockBack, Main.myPlayer, 0, 0);
            }
            else if(k == mod.ItemType("DarkmatterOre"))
            {
                target.AddBuff(ModContent.BuffType<Buffs.Electrified>(), 180);
            }
            else if(k == mod.ItemType("DaybreakIncineriteOre"))
            {
                target.AddBuff(BuffID.Daybreak, 400);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FireProjBoom"), (int)(projectile.damage / 2.5), projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[0] = 1f;
            }
            else if(k == mod.ItemType("EventideAbyssiumOre"))
            {
                target.AddBuff(ModContent.BuffType<Buffs.Moonraze>(), 400);

                projectile.localAI[0] ++;

                if(projectile.velocity.Length() < 10f) projectile.velocity = 10 * Vector2.Normalize(projectile.velocity);
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
            item.SetDefaults(k, false);
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
                    int num292 = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.Moonraze>(), 0f, 0f, 100);
                    Main.dust[num292].velocity *= 2f;
                    Main.dust[num292].noGravity = true;
                };
            }
            else if(k >= 3930 && Config.LuckyOre[k] > 650 && item.modItem.mod != ModLoader.GetMod("AAMod"))
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
