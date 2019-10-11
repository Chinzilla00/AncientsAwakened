using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev.Minions
{
    public class Squirrel1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirrel");
            Main.projFrames[projectile.type] = 18;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 44;
            projectile.height = 36;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.minionSlots = 1f;
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
        
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (!player.active)
            {
                projectile.active = false;
                return;
            }
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = true;
            if (projectile.lavaWet)
            {
                projectile.ai[0] = 1f;
                projectile.ai[1] = 0f;
            }
            if (player.dead)
            {
                modPlayer.Squirrel = false;
            }
            if (modPlayer.Squirrel)
            {
                projectile.timeLeft = Main.rand.Next(2, 10);
            }
            int num = 10;
            int num2 = 40 * (projectile.minionPos + 1) * player.direction;
            if (player.position.X + player.width / 2 < projectile.position.X + projectile.width / 2 - num + num2)
            {
                flag = true;
            }
            else if (player.position.X + player.width / 2 > projectile.position.X + projectile.width / 2 + num + num2)
            {
                flag2 = true;
            }
            else if (player.position.X + player.width / 2 < projectile.position.X + projectile.width / 2 - num)
            {
                flag = true;
            }
            else if (player.position.X + player.width / 2 > projectile.position.X + projectile.width / 2 + num)
            {
                flag2 = true;
            }
            if (projectile.ai[1] == 0f)
            {
				projectile.tileCollide = true;
                int num36 = 500;
                num36 += 40 * projectile.minionPos;
                if (projectile.localAI[0] > 0f)
                {
                    num36 += 500;
                }
                if (player.rocketDelay2 > 0)
                {
                    projectile.ai[0] = 1f;
                }
                Vector2 vector6 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num37 = player.position.X + player.width / 2 - vector6.X;
                float num38 = player.position.Y + player.height / 2 - vector6.Y;
                float num39 = (float)Math.Sqrt(num37 * num37 + num38 * num38);
                if (num39 > 2000f)
                {
                    projectile.position.X = player.position.X + player.width / 2 - projectile.width / 2;
                    projectile.position.Y = player.position.Y + player.height / 2 - projectile.height / 2;
                }
                else if (num39 > num36 || (Math.Abs(num38) > 300f && (projectile.localAI[0] <= 0f)))
                {
                    if (num38 > 0f && projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = 0f;
                    }
                    if (num38 < 0f && projectile.velocity.Y > 0f)
                    {
                        projectile.velocity.Y = 0f;
                    }
                    projectile.ai[0] = 1f;
                }
            }
            if (projectile.ai[0] != 0f)
            {
                int num41 = 100;
                projectile.tileCollide = false;
                Vector2 vector7 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num42 = player.position.X + player.width / 2 - vector7.X;
                num42 -= 40 * player.direction;
                float num43 = 700f;
                if (flag5)
                {
                    num43 += 100f;
                }
                bool flag6 = false;
                int num44 = -1;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].CanBeChasedBy(this, false))
                    {
                        float num45 = Main.npc[j].position.X + Main.npc[j].width / 2;
                        float num46 = Main.npc[j].position.Y + Main.npc[j].height / 2;
                        float num47 = Math.Abs(player.position.X + player.width / 2 - num45) + Math.Abs(player.position.Y + player.height / 2 - num46);
                        if (num47 < num43)
                        {
                            if (Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
                            {
                                num44 = j;
                            }
                            flag6 = true;
                            break;
                        }
                    }
                }
                if (!flag6)
                {
                    num42 -= 40 * projectile.minionPos * player.direction;
                }
                if (flag6 && num44 >= 0)
                {
                    projectile.ai[0] = 0f;
                }
                float num48 = player.position.Y + player.height / 2 - vector7.Y;
                float num49 = (float)Math.Sqrt(num42 * num42 + num48 * num48);
                float num40 = 0.4f;
                float num50 = 12f;
                if (num50 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
                {
                    num50 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
                }
                if (num49 < num41 && player.velocity.Y == 0f && projectile.position.Y + projectile.height <= player.position.Y + player.height && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    if (projectile.velocity.Y < -6f)
                    {
                        projectile.velocity.Y = -6f;
                    }
                }
                if (num49 < 60f)
                {
                    num42 = projectile.velocity.X;
                    num48 = projectile.velocity.Y;
                }
                else
                {
                    num49 = num50 / num49;
                    num42 *= num49;
                    num48 *= num49;
                }
                if (projectile.velocity.X < num42)
                {
                    projectile.velocity.X = projectile.velocity.X + num40;
                    if (projectile.velocity.X < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num40 * 1.5f;
                    }
                }
                if (projectile.velocity.X > num42)
                {
                    projectile.velocity.X = projectile.velocity.X - num40;
                    if (projectile.velocity.X > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - num40 * 1.5f;
                    }
                }
                if (projectile.velocity.Y < num48)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num40;
                    if (projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num40 * 1.5f;
                    }
                }
                if (projectile.velocity.Y > num48)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num40;
                    if (projectile.velocity.Y > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num40 * 1.5f;
                    }
                }
                if (projectile.frame < 12)
                {
                    projectile.frame = Main.rand.Next(12, 18);
                    projectile.frameCounter = 0;
                }
                if (projectile.velocity.X > 0.5)
                {
                    projectile.spriteDirection = -1;
                }
                else if (projectile.velocity.X < -0.5)
                {
                    projectile.spriteDirection = 1;
                }
                if (projectile.spriteDirection == -1)
                {
                    projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
                }
                else
                {
                    projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 3.14f;
                }
                return;
            }
            else
            {
                float num57 = 40 * projectile.minionPos;
                int num58 = 30;
                int num59 = 60;
                projectile.localAI[0] -= 1f;
                if (projectile.localAI[0] < 0f)
                {
                    projectile.localAI[0] = 0f;
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f;
                }
                else
                {
                    float num60 = projectile.position.X;
                    float num61 = projectile.position.Y;
                    float num62 = 100000f;
                    float num63 = num62;
                    int num64 = -1;
                    NPC ownerMinionAttackTargetNPC = projectile.OwnerMinionAttackTargetNPC;
                    if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
                    {
                        float num65 = ownerMinionAttackTargetNPC.position.X + ownerMinionAttackTargetNPC.width / 2;
                        float num66 = ownerMinionAttackTargetNPC.position.Y + ownerMinionAttackTargetNPC.height / 2;
                        float num67 = Math.Abs(projectile.position.X + projectile.width / 2 - num65) + Math.Abs(projectile.position.Y + projectile.height / 2 - num66);
                        if (num67 < num62)
                        {
                            if (num64 == -1 && num67 <= num63)
                            {
                                num63 = num67;
                                num60 = num65;
                                num61 = num66;
                            }
                            if (Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                            {
                                num62 = num67;
                                num60 = num65;
                                num61 = num66;
                                num64 = ownerMinionAttackTargetNPC.whoAmI;
                            }
                        }
                    }
                    if (num64 == -1)
                    {
                        for (int l = 0; l < 200; l++)
                        {
                            if (Main.npc[l].CanBeChasedBy(this, false))
                            {
                                float num68 = Main.npc[l].position.X + Main.npc[l].width / 2;
                                float num69 = Main.npc[l].position.Y + Main.npc[l].height / 2;
                                float num70 = Math.Abs(projectile.position.X + projectile.width / 2 - num68) + Math.Abs(projectile.position.Y + projectile.height / 2 - num69);
                                if (num70 < num62)
                                {
                                    if (num64 == -1 && num70 <= num63)
                                    {
                                        num63 = num70;
                                        num60 = num68;
                                        num61 = num69;
                                    }
                                    if (Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[l].position, Main.npc[l].width, Main.npc[l].height))
                                    {
                                        num62 = num70;
                                        num60 = num68;
                                        num61 = num69;
                                        num64 = l;
                                    }
                                }
                            }
                        }
                    }
                    if (num64 == -1 && num63 < num62)
                    {
                        num62 = num63;
                    }
                    float num71 = 400f;
                    if (projectile.position.Y > Main.worldSurface * 16.0)
                    {
                        num71 = 200f;
                    }
                    if (num62 < num71 + num57 && num64 == -1)
                    {
                        float num72 = num60 - (projectile.position.X + projectile.width / 2);
                        if (num72 < -5f)
                        {
                            flag = true;
                            flag2 = false;
                        }
                        else if (num72 > 5f)
                        {
                            flag2 = true;
                            flag = false;
                        }
                    }
                    else if (num64 >= 0 && num62 < 800f + num57)
                    {
                        projectile.localAI[0] = num59;
                        float num73 = num60 - (projectile.position.X + projectile.width / 2);
                        if (num73 > 300f || num73 < -300f)
                        {
                            if (num73 < -50f)
                            {
                                flag = true;
                                flag2 = false;
                            }
                            else if (num73 > 50f)
                            {
                                flag2 = true;
                                flag = false;
                            }
                        }
                        else if (projectile.owner == Main.myPlayer)
                        {
                            projectile.ai[1] = num58;
                            float num74 = 12f;
                            Vector2 vector8 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height / 2 - 8f);
                            float num75 = num60 - vector8.X + Main.rand.Next(-20, 21);
                            float num76 = Math.Abs(num75) * 0.1f;
                            num76 = num76 * Main.rand.Next(0, 100) * 0.001f;
                            float num77 = num61 - vector8.Y + Main.rand.Next(-20, 21) - num76;
                            float num78 = (float)Math.Sqrt(num75 * num75 + num77 * num77);
                            num78 = num74 / num78;
                            num75 *= num78;
                            num77 *= num78;
                            int num79 = projectile.damage;
                            int num80 = Terraria.ModLoader.ModContent.ProjectileType<Acorn>();
                            int num81 = Projectile.NewProjectile(vector8.X, vector8.Y, num75 * 2, num77 * 2, num80, num79, projectile.knockBack, Main.myPlayer, 0f, num64);
                            Main.projectile[num81].timeLeft = 300;
                            if (num75 < 0f)
                            {
                                projectile.direction = -1;
                            }
                            if (num75 > 0f)
                            {
                                projectile.direction = 1;
                            }
                            projectile.netUpdate = true;
                        }
                    }
                }
                if (projectile.localAI[0] == 0f)
                {
                    projectile.direction = player.direction;
                }
                projectile.rotation = 0f;
                float num104 = 6f;
                float num103 = 0.2f;
                if (num104 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
                {
                    num104 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
                    num103 = 0.3f;
                }
                if (flag)
                {
                    if (projectile.velocity.X > -3.5)
                    {
                        projectile.velocity.X = projectile.velocity.X - num103;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X - num103 * 0.25f;
                    }
                }
                else if (flag2)
                {
                    if (projectile.velocity.X < 3.5)
                    {
                        projectile.velocity.X = projectile.velocity.X + num103;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num103 * 0.25f;
                    }
                }
                else
                {
                    projectile.velocity.X = projectile.velocity.X * 0.9f;
                    if (projectile.velocity.X >= -num103 && projectile.velocity.X <= num103)
                    {
                        projectile.velocity.X = 0f;
                    }
                }
                if (flag || flag2)
                {
                    int num105 = (int)(projectile.position.X + projectile.width / 2) / 16;
                    int j2 = (int)(projectile.position.Y + projectile.height / 2) / 16;
                    if (flag)
                    {
                        num105--;
                    }
                    if (flag2)
                    {
                        num105++;
                    }
                    num105 += (int)projectile.velocity.X;
                    if (WorldGen.SolidTile(num105, j2))
                    {
                        flag4 = true;
                    }
                }
                if (player.position.Y + player.height - 8f > projectile.position.Y + projectile.height)
                {
                    flag3 = true;
                }
                Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY, 1, false, 0);
                if (projectile.velocity.Y == 0f)
                {
                    if (!flag3 && (projectile.velocity.X < 0f || projectile.velocity.X > 0f))
                    {
                        int num106 = (int)(projectile.position.X + projectile.width / 2) / 16;
                        int j3 = (int)(projectile.position.Y + projectile.height / 2) / 16 + 1;
                        if (flag)
                        {
                            num106--;
                        }
                        if (flag2)
                        {
                            num106++;
                        }
                        WorldGen.SolidTile(num106, j3);
                    }
                    if (flag4)
                    {
                        int num107 = (int)(projectile.position.X + projectile.width / 2) / 16;
                        int num108 = (int)(projectile.position.Y + projectile.height) / 16 + 1;
                        if (WorldGen.SolidTile(num107, num108) || Main.tile[num107, num108].halfBrick() || Main.tile[num107, num108].slope() > 0)
                        {
                            try
                            {
                                num107 = (int)(projectile.position.X + projectile.width / 2) / 16;
                                num108 = (int)(projectile.position.Y + projectile.height / 2) / 16;
                                if (flag)
                                {
                                    num107--;
                                }
                                if (flag2)
                                {
                                    num107++;
                                }
                                num107 += (int)projectile.velocity.X;
                                if (!WorldGen.SolidTile(num107, num108 - 1) && !WorldGen.SolidTile(num107, num108 - 2))
                                {
                                    projectile.velocity.Y = -5.1f;
                                }
                                else if (!WorldGen.SolidTile(num107, num108 - 2))
                                {
                                    projectile.velocity.Y = -7.1f;
                                }
                                else if (WorldGen.SolidTile(num107, num108 - 5))
                                {
                                    projectile.velocity.Y = -11.1f;
                                }
                                else if (WorldGen.SolidTile(num107, num108 - 4))
                                {
                                    projectile.velocity.Y = -10.1f;
                                }
                                else
                                {
                                    projectile.velocity.Y = -9.1f;
                                }
                            }
                            catch
                            {
                                projectile.velocity.Y = -9.1f;
                            }
                        }
                    }
                }
                if (projectile.velocity.X > num104)
                {
                    projectile.velocity.X = num104;
                }
                if (projectile.velocity.X < -num104)
                {
                    projectile.velocity.X = -num104;
                }
                if (projectile.velocity.X < 0f)
                {
                    projectile.direction = -1;
                }
                if (projectile.velocity.X > 0f)
                {
                    projectile.direction = 1;
                }
                if (projectile.velocity.X > num103 && flag2)
                {
                    projectile.direction = 1;
                }
                if (projectile.velocity.X < -num103 && flag)
                {
                    projectile.direction = -1;
                }
                if (projectile.direction == -1)
                {
                    projectile.spriteDirection = 1;
                }
                if (projectile.direction == 1)
                {
                    projectile.spriteDirection = -1;
                }
                if (projectile.ai[1] > 0f)
                {
                    if (projectile.localAI[1] == 0f)
                    {
                        projectile.localAI[1] = 1f;
                        projectile.frame = 1;
                    }
                    if (projectile.frame != 0)
                    {
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 4)
                        {
                            projectile.frame++;
                            projectile.frameCounter = 0;
                        }
                        if (projectile.frame == 4)
                        {
                            projectile.frame = 0;
                        }
                    }
                }
                else if (projectile.velocity.Y == 0f)
                {
                    projectile.localAI[1] = 0f;
                    if (projectile.velocity.X == 0f)
                    {
                        projectile.frame = 0;
                        projectile.frameCounter = 0;
                    }
                    else if (projectile.velocity.X < -0.8 || projectile.velocity.X > 0.8)
                    {
                        projectile.frameCounter += (int)Math.Abs(projectile.velocity.X);
                        projectile.frameCounter++;
                        if (projectile.frameCounter > 6)
                        {
                            projectile.frame++;
                            projectile.frameCounter = 0;
                        }
                        if (projectile.frame < 5)
                        {
                            projectile.frame = 5;
                        }
                        if (projectile.frame >= 11)
                        {
                            projectile.frame = 5;
                        }
                    }
                    else
                    {
                        projectile.frame = 0;
                        projectile.frameCounter = 0;
                    }
                }
                else if (projectile.velocity.Y < 0f)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = 4;
                }
                else if (projectile.velocity.Y > 0f)
                {
                    projectile.frameCounter = 0;
                    projectile.frame = 4;
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.4f;
                if (projectile.velocity.Y > 10f)
                {
                    projectile.velocity.Y = 10f;
                }
                return;
            }
        }
    }
}
