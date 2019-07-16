using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah.Supreme
{
	public class RoyalRabbit : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Rabbit");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 30;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.minionSlots = 1;
        }

        public override void AI()
        {
			bool flag64 = projectile.type == mod.ProjectileType("RoyalRabbit");
			Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (!player.active)
            {
                projectile.active = false;
                return;
            }
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.RabbitcopterR = false;
                }
                if (modPlayer.RabbitcopterR)
                {
                    projectile.timeLeft = 2;
                }
            }
            float num9 = projectile.width;
            float num8 = 0.1f;
            num9 *= 2f;
            for (int j = 0; j < 1000; j++)
            {
                if (j != projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == projectile.owner && Main.projectile[j].type == projectile.type && Math.Abs(projectile.position.X - Main.projectile[j].position.X) + Math.Abs(projectile.position.Y - Main.projectile[j].position.Y) < num9)
                {
                    if (projectile.position.X < Main.projectile[j].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - num8;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num8;
                    }
                    if (projectile.position.Y < Main.projectile[j].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num8;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num8;
                    }
                }
            }
            Vector2 vector = projectile.position;
            float num10 = 400f;
            bool flag = false;
            int num11 = -1;
            projectile.tileCollide = false;
            NPC ownerMinionAttackTargetNPC2 = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(this, false))
            {
                float num14 = Vector2.Distance(ownerMinionAttackTargetNPC2.Center, projectile.Center);
                if (((Vector2.Distance(projectile.Center, vector) > num14 && num14 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC2.position, ownerMinionAttackTargetNPC2.width, ownerMinionAttackTargetNPC2.height))
                {
                    num10 = num14;
                    vector = ownerMinionAttackTargetNPC2.Center;
                    flag = true;
                    num11 = ownerMinionAttackTargetNPC2.whoAmI;
                }
            }
            if (!flag)
            {
                for (int l = 0; l < 200; l++)
                {
                    NPC nPC2 = Main.npc[l];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num15 = Vector2.Distance(nPC2.Center, projectile.Center);
                        if (((Vector2.Distance(projectile.Center, vector) > num15 && num15 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                        {
                            num10 = num15;
                            vector = nPC2.Center;
                            flag = true;
                            num11 = l;
                        }
                    }
                }
            }
            int num16 = 500;
            if (flag)
            {
                num16 = 1000;
            }
            float num17 = Vector2.Distance(player.Center, projectile.Center);
            if (num17 > num16)
            {
                projectile.ai[0] = 1f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
            }
            if (flag && projectile.ai[0] == 0f)
            {
                Vector2 vector4 = vector - projectile.Center;
                float num18 = vector4.Length();
                vector4.Normalize();
                if (num18 < 150f)
                {
                    float num21 = 4f;
                    vector4 *= -num21;
                    projectile.velocity.X = (projectile.velocity.X * 40f + vector4.X) / 41f;
                    projectile.velocity.Y = (projectile.velocity.Y * 40f + vector4.Y) / 41f;
                }
                else
                {
                    projectile.velocity *= 0.97f;
                }
            }
            else
            {
                if (!Collision.CanHitLine(projectile.Center, 1, 1, Main.player[projectile.owner].Center, 1, 1))
                {
                    projectile.ai[0] = 1f;
                }
                float num22 = 6f;
                if (projectile.ai[0] == 1f)
                {
                    num22 = 15f;
                }
                Vector2 center2 = projectile.Center;
                Vector2 vector6 = player.Center - center2 + new Vector2(0f, -60f);
                projectile.ai[1] = 3600f;
                projectile.netUpdate = true;
                vector6 = player.Center - center2;
                int num23 = 1;
                for (int m = 0; m < projectile.whoAmI; m++)
                {
                    if (Main.projectile[m].active && Main.projectile[m].owner == projectile.owner && Main.projectile[m].type == projectile.type)
                    {
                        num23++;
                    }
                }
                vector6.X -= 10 * Main.player[projectile.owner].direction;
                vector6.X -= num23 * 40 * Main.player[projectile.owner].direction;
                vector6.Y -= 10f;
                float num24 = vector6.Length();
                if (num24 > 200f && num22 < 9f)
                {
                    num22 = 9f;
                }
                num22 = (int)(num22 * 0.75);
                if (num24 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (num24 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width / 2;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.width / 2;
                }
                if (num24 > 10f)
                {
                    vector6.Normalize();
                    if (num24 < 50f)
                    {
                        num22 /= 2f;
                    }
                    vector6 *= num22;
                    projectile.velocity = (projectile.velocity * 20f + vector6) / 21f;
                }
                else
                {
                    projectile.direction = Main.player[projectile.owner].direction;
                    projectile.velocity *= 0.9f;
                }
            }
            projectile.rotation = projectile.velocity.X * 0.05f;
            if (projectile.velocity.X > 0f)
            {
                projectile.spriteDirection = (projectile.direction = -1);
            }
            else if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = (projectile.direction = 1);
            }
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += 1f;
                if (Main.rand.Next(3) == 0)
                {
                    projectile.ai[1] += 1f;
                }
            }
            if (projectile.ai[1] > Main.rand.Next(30, 60))
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float scaleFactor4 = 11f;
                int num29 = mod.ProjectileType<RabbitBeam>();
                
                if (flag)
                {
                    if ((vector - projectile.Center).X > 0f)
                    {
                        projectile.spriteDirection = (projectile.direction = -1);
                    }
                    else if ((vector - projectile.Center).X < 0f)
                    {
                        projectile.spriteDirection = (projectile.direction = 1);
                    }
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        if (Main.myPlayer == projectile.owner)
                        {
                            Vector2 value4 = vector - projectile.Center;
                            value4.Normalize();
                            value4 *= scaleFactor4;
                            int num33 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value4.X*2, value4.Y*2, num29, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num33].timeLeft = 300;
                            Main.projectile[num33].netUpdate = true;
                            projectile.netUpdate = true;
                        }
                    }
                }
            }
			if (++projectile.frameCounter > 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override bool PreAI()
        {
            if (Main.rand.Next(6) == 0)
            {
                int num25 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f, 100, Main.DiscoColor, 2f);
                Main.dust[num25].velocity *= 0.3f;
                Main.dust[num25].noGravity = true;
                Main.dust[num25].noLight = true;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 4, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 4, frame, lightColor, false);
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Glowmasks/RoyalRabbit_Glow"), 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 4, frame, Main.DiscoColor, false);
            return false;
        }
    }


    internal class RabbitBeam : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rabbit Bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 100;
            projectile.aiStyle = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }

        public override void AI()
        {
            for (int num443 = 0; num443 < 2; num443++)
            {
                Vector2 vector31 = projectile.position;
                vector31 -= projectile.velocity * (num443 * 0.25f);
                projectile.alpha = 255;
                int num444 = Dust.NewDust(vector31, 1, 1, DustID.Shadowflame, 0f, 0f, 0, Main.DiscoColor);
                Main.dust[num444].position = vector31;
                Dust expr_13D2C_cp_0 = Main.dust[num444];
                expr_13D2C_cp_0.position.X += (projectile.width / 2);
                Dust expr_13D50_cp_0 = Main.dust[num444];
                expr_13D50_cp_0.position.Y += (projectile.height / 2);
                Main.dust[num444].scale = Main.rand.Next(70, 110) * 0.013f;
                Main.dust[num444].velocity *= 0.2f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            float num13 = 10f + 3f * 4f;
            Vector2 value6 = new Vector2(1.05f, 1f);
            Vector2 spinningpoint = new Vector2(0f, -3f - 3f).RotatedByRandom(3.1415927410125732);
            for (float num14 = 0f; num14 < num13; num14 += 1f)
            {
                int num15 = Dust.NewDust(projectile.Center, 0, 0, 66, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num15].position = projectile.Center;
                Main.dust[num15].velocity = spinningpoint.RotatedBy(6.28318548f * num14 / num13) * value6 * (0.8f + Main.rand.NextFloat() * 0.4f);
                Main.dust[num15].color = Main.hslToRgb(num14 / num13, 1f, 0.5f);
                Main.dust[num15].noGravity = true;
                Main.dust[num15].scale = 1f + 3 / 3f;
            }
            target.AddBuff(mod.BuffType<Buffs.InfinityOverload>(), 200);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num585 = 0; num585 < 20; num585++)
            {
                int num586 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f, 100, Main.DiscoColor, 2f);
                Main.dust[num586].noGravity = true;
                Main.dust[num586].velocity *= 4f;
            }
        }
    }
}