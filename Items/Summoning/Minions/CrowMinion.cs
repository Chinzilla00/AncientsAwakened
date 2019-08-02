using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class CrowMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crow");
            Main.projFrames[projectile.type] = 8;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
        }
        public override void SetDefaults()
        {
			projectile.CloneDefaults(317);
			projectile.aiStyle = 54;
			aiType = 317;
            projectile.width = 40;
            projectile.height = 32;
            projectile.timeLeft = 18000;
            projectile.timeLeft *= 5;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.AddBuff(mod.BuffType("CrowMinion"), 3600);
            if (player.dead)
            {
                modPlayer.CrowMinion = false;
            }
            if (modPlayer.CrowMinion)
            {
                projectile.timeLeft = 2;
            }

            projectile.rotation = projectile.velocity.X * 0.05f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
            }
            if (projectile.frame < 4)
            {
                projectile.frame = 4;
            }
            if (projectile.frame > 7)
            {
                projectile.frame = 4;
            }
            if (Math.Abs(projectile.velocity.X) > 0.2)
            {
                projectile.spriteDirection = -projectile.direction;
                return;
            }

            for (int num527 = 0; num527 < 1000; num527++)
            {
                if (num527 != projectile.whoAmI && Main.projectile[num527].active && Main.projectile[num527].owner == projectile.owner && Main.projectile[num527].type == projectile.type && Math.Abs(projectile.position.X - Main.projectile[num527].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num527].position.Y) < (float)projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num527].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - 0.05f;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + 0.05f;
                    }
                    if (projectile.position.Y < Main.projectile[num527].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - 0.05f;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + 0.05f;
                    }
                }
            }
            float num528 = projectile.position.X;
            float num529 = projectile.position.Y;
            float num530 = 900f;
            bool flag19 = false;
            int num531 = 500;
            if (projectile.ai[1] != 0f || projectile.friendly)
            {
                num531 = 1400;
            }
            if (Math.Abs(projectile.Center.X - Main.player[projectile.owner].Center.X) + Math.Abs(projectile.Center.Y - Main.player[projectile.owner].Center.Y) > num531)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.tileCollide = true;
                NPC ownerMinionAttackTargetNPC2 = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(this, false))
                {
                    float num532 = ownerMinionAttackTargetNPC2.position.X + (float)(ownerMinionAttackTargetNPC2.width / 2);
                    float num533 = ownerMinionAttackTargetNPC2.position.Y + (float)(ownerMinionAttackTargetNPC2.height / 2);
                    float num534 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num532) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num533);
                    if (num534 < num530 && Collision.CanHit(projectile.position, projectile.width, projectile.height, ownerMinionAttackTargetNPC2.position, ownerMinionAttackTargetNPC2.width, ownerMinionAttackTargetNPC2.height))
                    {
                        num530 = num534;
                        num528 = num532;
                        num529 = num533;
                        flag19 = true;
                    }
                }
                if (!flag19)
                {
                    for (int num535 = 0; num535 < 200; num535++)
                    {
                        if (Main.npc[num535].CanBeChasedBy(this, false))
                        {
                            float num536 = Main.npc[num535].position.X + (float)(Main.npc[num535].width / 2);
                            float num537 = Main.npc[num535].position.Y + (float)(Main.npc[num535].height / 2);
                            float num538 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num536) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num537);
                            if (num538 < num530 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num535].position, Main.npc[num535].width, Main.npc[num535].height))
                            {
                                num530 = num538;
                                num528 = num536;
                                num529 = num537;
                                flag19 = true;
                            }
                        }
                    }
                }
            }
            else
            {
                projectile.tileCollide = false;
            }
            if (!flag19)
            {
                projectile.friendly = true;
                float num539 = 8f;
                if (projectile.ai[0] == 1f)
                {
                    num539 = 12f;
                }
                Vector2 vector38 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num540 = Main.player[projectile.owner].Center.X - vector38.X;
                float num541 = Main.player[projectile.owner].Center.Y - vector38.Y - 60f;
                float num542 = (float)Math.Sqrt((double)(num540 * num540 + num541 * num541));
                if (num542 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                }
                if (num542 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.width / 2);
                }
                if (num542 > 70f)
                {
                    num542 = num539 / num542;
                    num540 *= num542;
                    num541 *= num542;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num540) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num541) / 21f;
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
                projectile.friendly = false;
                projectile.rotation = projectile.velocity.X * 0.05f;
                projectile.frameCounter++;
                if (projectile.frameCounter >= 4)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
                if (Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                    return;
                }
            }
            else
            {
                if (projectile.ai[1] == -1f)
                {
                    projectile.ai[1] = 17f;
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f;
                }
                if (projectile.ai[1] == 0f)
                {
                    projectile.friendly = true;
                    float num543 = 8f;
                    Vector2 vector39 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num544 = num528 - vector39.X;
                    float num545 = num529 - vector39.Y;
                    float num546 = (float)Math.Sqrt((double)(num544 * num544 + num545 * num545));
                    if (num546 < 100f)
                    {
                        num543 = 14f;
                    }
                    num546 = num543 / num546;
                    num544 *= num546;
                    num545 *= num546;
                    projectile.velocity.X = (projectile.velocity.X * 14f + num544) / 15f;
                    projectile.velocity.Y = (projectile.velocity.Y * 14f + num545) / 15f;
                }
                else
                {
                    projectile.friendly = false;
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 10f)
                    {
                        projectile.velocity *= 1.15f;
                    }
                }
            }
        }
        
    }
}