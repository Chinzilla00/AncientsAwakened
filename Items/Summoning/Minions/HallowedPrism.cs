using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Summoning.Minions
{
    public class HallowedPrism : Minion2
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Prism");
            Main.projFrames[projectile.type] = 5;

        } 

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.minionSlots = 1f;
            projectile.ignoreWater = true;
        }
        

        public override void AI()
        {
            if (projectile.ai[0] == 2f)
            {
                projectile.ai[1] -= 1f;
                projectile.tileCollide = false;
                if (projectile.ai[1] > 3f)
                {
                    int num = Dust.NewDust(projectile.Center, 0, 0, 220 + Main.rand.Next(2), projectile.velocity.X, projectile.velocity.Y, 100, AAColor.Hallow, 1f);
                    Main.dust[num].scale = 0.5f + (float)Main.rand.NextDouble() * 0.3f;
                    Main.dust[num].velocity /= 2.5f;
                    Main.dust[num].noGravity = true;
                    Main.dust[num].noLight = true;
                    Main.dust[num].frame.Y = 80;
                }
                if (projectile.ai[1] != 0f)
                {
                    return;
                }
                projectile.ai[1] = 30f;
                projectile.ai[0] = 0f;
                projectile.velocity /= 5f;
                projectile.velocity.Y = 0f;
                projectile.extraUpdates = 0;
                projectile.numUpdates = 0;
                projectile.netUpdate = true;
                projectile.extraUpdates = 0;
                projectile.numUpdates = 0;
            }
            if (projectile.extraUpdates > 1)
            {
                projectile.extraUpdates = 0;
            }
            if (projectile.numUpdates > 1)
            {
                projectile.numUpdates = 0;
            }
            if (projectile.localAI[0] > 0f)
            {
                projectile.localAI[0] -= 1f;
            }
            float num8 = 0.05f;
            float num9 = (float)projectile.width;
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
            float num10 = 300f;
            bool flag = false;
            int num11 = -1;
            projectile.tileCollide = true;
            Vector2 center = Main.player[projectile.owner].Center;
            Vector2 value = new Vector2(0.5f);
            if (projectile.type == 423)
            {
                value.Y = 0f;
            }
            NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
            {
                Vector2 vector2 = ownerMinionAttackTargetNPC.position + ownerMinionAttackTargetNPC.Size * value;
                float num12 = Vector2.Distance(vector2, center);
                if (((Vector2.Distance(center, vector) > num12 && num12 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                {
                    num10 = num12;
                    vector = vector2;
                    flag = true;
                    num11 = ownerMinionAttackTargetNPC.whoAmI;
                }
            }
            if (!flag)
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC nPC = Main.npc[k];
                    if (nPC.CanBeChasedBy(this, false))
                    {
                        Vector2 vector3 = nPC.position + nPC.Size * value;
                        float num13 = Vector2.Distance(vector3, center);
                        if (((Vector2.Distance(center, vector) > num13 && num13 < num10) || !flag) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                        {
                            num10 = num13;
                            vector = vector3;
                            flag = true;
                            num11 = k;
                        }
                    }
                }
            }
            int num16 = 500;
            if (flag)
            {
                num16 = 1200;
            }
            Player player = Main.player[projectile.owner];
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
                vector4 = vector - Vector2.UnitY * 80f;
                int num19 = (int)vector4.Y / 16;
                if (num19 < 0)
                {
                    num19 = 0;
                }
                Tile tile = Main.tile[(int)vector4.X / 16, num19];
                if (tile != null && tile.active() && Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
                {
                    vector4 += Vector2.UnitY * 16f;
                    tile = Main.tile[(int)vector4.X / 16, (int)vector4.Y / 16];
                    if (tile != null && tile.active() && Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
                    {
                        vector4 += Vector2.UnitY * 16f;
                    }
                }
                vector4 -= projectile.Center;
                num18 = vector4.Length();
                vector4.Normalize();
                if (num18 > 300f && num18 <= 800f && projectile.localAI[0] == 0f)
                {
                    projectile.ai[0] = 2f;
                    projectile.ai[1] = (float)((int)(num18 / 10f));
                    projectile.extraUpdates = (int)projectile.ai[1];
                    projectile.velocity = vector4 * 10f;
                    projectile.localAI[0] = 60f;
                    return;
                }
                if (num18 > 200f)
                {
                    float scaleFactor2 = 6f;
                    vector4 *= scaleFactor2;
                    projectile.velocity.X = (projectile.velocity.X * 40f + vector4.X) / 41f;
                    projectile.velocity.Y = (projectile.velocity.Y * 40f + vector4.Y) / 41f;
                }
                if (num18 > 70f && num18 < 130f)
                {
                    float scaleFactor3 = 7f;
                    if (num18 < 100f)
                    {
                        scaleFactor3 = -3f;
                    }
                    vector4 *= scaleFactor3;
                    projectile.velocity = (projectile.velocity * 20f + vector4) / 21f;
                    if (Math.Abs(vector4.X) > Math.Abs(vector4.Y))
                    {
                        projectile.velocity.X = (projectile.velocity.X * 10f + vector4.X) / 11f;
                    }
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
                float num24 = vector6.Length();
                if (num24 > 200f && num22 < 9f)
                {
                    num22 = 9f;
                }
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
                if (num24 > 70f)
                {
                    vector6.Normalize();
                    vector6 *= num22;
                    projectile.velocity = (projectile.velocity * 20f + vector6) / 21f;
                }
                else
                {
                    if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        projectile.velocity.X = -0.15f;
                        projectile.velocity.Y = -0.05f;
                    }
                    projectile.velocity *= 1.01f;
                }
            }
            projectile.rotation = projectile.velocity.X * 0.05f;
            projectile.frameCounter++;
            int num28 = 3;
            if (projectile.frameCounter >= 4 * num28)
            {
                projectile.frameCounter = 0;
            }
            projectile.frame = projectile.frameCounter / num28;
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
                if (Main.rand.Next(3) != 0)
                {
                    projectile.ai[1] += 1f;
                }
            }
            if (projectile.ai[1] > 30f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float scaleFactor4 = 4f;
                int num29 = mod.ProjectileType<Prismshot>();
                if (flag)
                {
                    if (Math.Abs((vector - projectile.Center).ToRotation() - 1.57079637f) > 0.7853982f)
                    {
                        projectile.velocity += Vector2.Normalize(vector - projectile.Center - Vector2.UnitY * 80f);
                        return;
                    }
                    if ((vector - projectile.Center).Length() > 400f)
                    {
                        return;
                    }
                    if (projectile.ai[1] == 0f)
                    {
                        projectile.ai[1] += 1f;
                        if (Main.myPlayer == projectile.owner)
                        {
                            Vector2 value2 = vector - projectile.Center;
                            value2.Normalize();
                            value2 *= scaleFactor4;
                            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value2.X, value2.Y, num29, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            projectile.netUpdate = true;
                            return;
                        }
                    }
                }
            }
        }

        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = (AAPlayer)player.GetModPlayer(mod, "AAPlayer");
            if (player.dead)
            {
                modPlayer.HallowedPrism = false;
            }
            if (modPlayer.BabyPhoenix)
            {
                projectile.timeLeft = 2;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1000);
        }
    }
}