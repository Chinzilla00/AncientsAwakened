using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.AH
{
    public class BlazeClaw : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blaze Claw");
			Main.projFrames[projectile.type] = 5;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}


        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 20;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0.5f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
        }

        public override void AI()
        {
            float radius = 700f;
            float num14 = 800f;
            float num15 = 1200f;
            float num16 = 150f;
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.AddBuff(mod.BuffType("ChaosClaw"), 3600);
            if (player.dead)
            {
                modPlayer.ChaosClaw = false;
            }
            if (modPlayer.ChaosClaw)
            {
                projectile.timeLeft = 2;
            }
            for (int whoAmI = 0; whoAmI < 1000; whoAmI++)
            {
                if (whoAmI != projectile.whoAmI && Main.projectile[whoAmI].active && Main.projectile[whoAmI].owner == projectile.owner && Math.Abs(projectile.position.X - Main.projectile[whoAmI].position.X) + Math.Abs(projectile.position.Y - Main.projectile[whoAmI].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[whoAmI].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - 0.05f;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + 0.05f;
                    }
                    if (projectile.position.Y < Main.projectile[whoAmI].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - 0.05f;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + 0.05f;
                    }
                }
            }
            Vector2 position = projectile.position;
            bool foundTarget = false;
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
                NPC target = Main.npc[num645];
                if (target.CanBeChasedBy(projectile, false))
                {
                    float distance = Vector2.Distance(target.Center, projectile.Center);
                    if (((Vector2.Distance(projectile.Center, position) > distance && distance < radius) || !foundTarget) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
                    {
                        radius = distance;
                        position = target.Center;
                        foundTarget = true;
                    }
                }
            }
            float num18 = num14;
            if (foundTarget)
            {
                num18 = num15;
            }
            if (Vector2.Distance(player.Center, projectile.Center) > num18)
            {
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }
            if (foundTarget && projectile.ai[0] == 0f)
            {
                Vector2 difference = position - projectile.Center;
                float num648 = difference.Length();
                difference.Normalize();
                if (num648 > 200f)
                {
                    float scaleFactor2 = 6f;
                    difference *= scaleFactor2;
                    projectile.velocity = (projectile.velocity * 40f + difference) / 41f;
                }
                else
                {
                    float num649 = 4f;
                    difference *= -num649;
                    projectile.velocity = (projectile.velocity * 40f + difference) / 41f;
                }
            }
            else
            {
                bool flag26 = false;
                if (!flag26)
                {
                    flag26 = (projectile.ai[0] == 1f);
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
                if (num651 < num16 && flag26 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                if (num651 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (projectile.height / 2);
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
            if (foundTarget)
            {
                projectile.rotation = (position - projectile.Center).ToRotation() + 3.14159274f;
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 2)
            {
                projectile.frame = 0;
            }
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (projectile.ai[1] > 90f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float speedScale = 7f;
                int shoot = mod.ProjectileType("BlazeBall");
                if (foundTarget && projectile.ai[1] == 0f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, position, 0, 0))
                    {
                        Vector2 speed = position - projectile.Center;
                        speed.Normalize();
                        speed *= speedScale;
                        int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speed.X, speed.Y, shoot, projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num659].timeLeft = 300;
                        projectile.netUpdate = true;
                    }
                }
            }
        }
    }
}