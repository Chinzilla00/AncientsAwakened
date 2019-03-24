using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dev.Moon
{
	public class MoonBeeMinion : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Wasp");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 34;
            projectile.height = 26;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0;
        }
        
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<AAPlayer>(mod);
            if (player.dead)
            {
                modPlayer.MoonBee = false;
            }
            if (modPlayer.MoonBee)
            {
                projectile.timeLeft = 2;
            }
            float num8 = 0.05f;
            float num9 = (float)projectile.width;
            num8 = 0.1f;
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
            if (num17 > (float)num16)
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
                vector6.X -= (float)(10 * Main.player[projectile.owner].direction);
                vector6.X -= (float)(num23 * 40 * Main.player[projectile.owner].direction);
                vector6.Y -= 10f;
                float num24 = vector6.Length();
                if (num24 > 200f && num22 < 9f)
                {
                    num22 = 9f;
                }
                num22 = (float)((int)((double)num22 * 0.75));
                if (num24 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (num24 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.width / 2);
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
            if (projectile.ai[1] > (float)Main.rand.Next(180, 900))
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float scaleFactor4 = 11f;
                int num29 = mod.ProjectileType<MoonProjectile>();
                
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
                            int num33 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value4.X, value4.Y, num29, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num33].timeLeft = 300;
                            Main.projectile[num33].netUpdate = true;
                            projectile.netUpdate = true;
                        }
                    }
                }
            }
        }

        public override bool PreAI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }
    }
}