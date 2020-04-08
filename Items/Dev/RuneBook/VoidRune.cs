using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Dev.RuneBook
{
    public class VoidRune : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Rune");
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 20;
            projectile.timeLeft = 18000;
            projectile.timeLeft *= 5;
            projectile.minionSlots = 0f;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.damage = 100;
        }
        int maxlife = 0;
        int target = -1;
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.position.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16, 1f, 0.95f, 0.8f);
            bool flag64 = projectile.type == mod.ProjectileType("VoidRune");
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(mod.BuffType("CCRune"), 3600);
            if (!modPlayer.CCBookEX)
            {
                projectile.active = false;
                return;
            }
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.CCRune = false;
                }
                if (modPlayer.CCRune || modPlayer.CCBookEX)
                {
                    projectile.timeLeft = 2;
                }
            }
            
            float num633 = 700f;
            float num634 = 800f;
            float num635 = 1200f;
            float num636 = 150f;
            float num637 = 0.05f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == mod.ProjectileType("VoidRune");
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
            if (projectile.ai[0] == 2f)
            {
                projectile.ai[1] += 1f;
                projectile.extraUpdates = 1;
                projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
                
                if (projectile.ai[1] > 40f)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.extraUpdates = 0;
                    projectile.numUpdates = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    flag24 = true;
                }
            }
            if (flag24)
            {
                return;
            }
            bool flag25 = false;
            Vector2 vector46 = projectile.position;
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
                    if ((num646 < num633 && Main.npc[num645].life > maxlife) && nPC2.active && nPC2.life > 0 && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                    {
                        maxlife = Main.npc[num645].life;
                        target = num645;
                    }
                }
            }
            if(target > -1)
            {
                vector46 = Main.npc[target].Center;
                flag25 = true;
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
                Vector2 vector = vector46 - projectile.Center - new Vector2(0, 50f + Main.npc[target].height/2);
                float num639 = 7f;
                if (vector.Length() > 200f && num639 < 10f)
                {
                    num639 = 10f;
                }
                if (vector.Length() > 200f)
                {
                    vector.Normalize();
                    vector *= num639;
                    projectile.velocity = (projectile.velocity * 40f + vector) / 41f;
                }
                else if(vector.Length() < 40f && (projectile.velocity.X != 0f || projectile.velocity.Y != 0f))
                {
                    vector.Normalize();
                    vector *= num639;
                    projectile.velocity = (projectile.velocity + vector * 40f) / 41f;
                }
                if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.02f;
                    projectile.velocity.Y = -0.01f;
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
            
            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += 1;
            }
            if (projectile.ai[1] > 300f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }
            if (projectile.ai[0] == 0f)
            {
                float scaleFactor3 = 8f;
				int num658 = mod.ProjectileType("CCRuneNovaRay");
				if (flag25 && projectile.ai[1] == 0)
				{
					projectile.ai[1] += 1f;
					if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
					{
						Vector2 value19 = vector46 - projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X, value19.Y, num658, projectile.damage, 0f, Main.myPlayer, projectile.whoAmI, target);
						projectile.netUpdate = true;
					}
				}
            }
        }
    }
}