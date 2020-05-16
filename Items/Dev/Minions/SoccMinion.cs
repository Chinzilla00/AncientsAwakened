using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev.Minions
{
    public class SoccMinion : ModProjectile
    {

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sock Puppet");
			Main.projFrames[projectile.type] = 11;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 60;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 1;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
        }

        public int FrameTimer = 0;

        public override void AI()
        {
            bool flag64 = projectile.type == mod.ProjectileType("SoccMinion");
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(mod.BuffType("Socc"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.Socc = false;
                }
                if (modPlayer.Socc)
                {
                    projectile.timeLeft = 2;
                }
            }

            float num633 = 700f;
            float num634 = 800f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == mod.ProjectileType("SoccMinion");
                if (num638 != projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == projectile.owner && flag23 && Math.Abs(projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num638].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - 0.02f;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + 0.02f;
                    }
                    if (projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - 0.02f;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + 0.02f;
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
            bool AttackFrame = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                AttackFrame = true;
                if (Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                {
                    num633 = Vector2.Distance(projectile.Center, vector46);
                    vector46 = npc.Center;
                    flag25 = true;
                }
            }
            else for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    AttackFrame = true;
                    float num646 = Vector2.Distance(npc.Center, projectile.Center);
                    if ((num646 < num633 || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                    {
                        num633 = num646;
                        vector46 = npc.Center;
                        flag25 = true;
                    }
                }
            }
            float num647 = num634;
            if (flag25)
            {
                num647 = 1200f;
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
                if (num651 < 150f && flag26 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
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
            projectile.rotation = projectile.velocity.X * 0.04f;

            if(flag25)
            {
                projectile.spriteDirection = ((vector46 - projectile.Center).X > 0? 1: -1);
            }
            else
            {
                projectile.spriteDirection =(projectile.velocity.X > 0? 1: -1);
            }

            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (projectile.ai[1] > 20)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                if (flag25 && projectile.ai[1] == 0f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
                    {
                        Vector2 value19 = vector46 - projectile.Center;
                        value19.Normalize();
                        value19 *= 9f;
                        int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X, value19.Y, ProjectileID.SaucerLaser, projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
                        Main.projectile[proj].extraUpdates = 1;
                        Main.projectile[proj].hostile = false;
                        Main.projectile[proj].friendly = true;
                        Main.projectile[proj].magic = false;
                        Main.projectile[proj].minion = true;
                        Main.projectile[proj].netUpdate = true;
                        projectile.netUpdate = true;
                    }
                }
            }


            if (!AttackFrame)
            {
                if (projectile.frameCounter++ >= 10)
                {
                    projectile.frameCounter = 0;
                    if (++projectile.frame > 5)
                    {
                        projectile.frame = 0;
                    }
                }
            }
            else
            {
                if (projectile.frameCounter++ >= 5)
                {
                    projectile.frameCounter = 0;
                    if (projectile.frame < 6 || projectile.frame > 10)
                    {
                        projectile.frame = 6;
                    }
                }
                
            }
        }
    }
}