using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Witch
{
    public class FlameSoul : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Soul");
			Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0.25f;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.Orange.R, Color.Orange.R, Color.Orange.R, 128);
        }

        public override void AI()
        {
        	float num633 = 700f;
			float num634 = 800f;
			float num635 = 1200f;
			float num636 = 150f;
			bool flag64 = projectile.type == mod.ProjectileType("FlameSoul");
			Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.FlameSoul = false;
				}
				if (modPlayer.FlameSoul)
				{
					projectile.timeLeft = 2;
				}
			}

            projectile.damage = FireDamage(player);

            float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = (Main.projectile[num638].type == mod.ProjectileType("FlameSoul"));
				if (num638 != projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == projectile.owner && flag23 && Math.Abs(projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < (float)projectile.width)
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
				projectile.rotation = projectile.velocity.X / 15;
				projectile.frameCounter++;
				if (projectile.frameCounter > 1)
				{
					projectile.frame++;
					projectile.frameCounter = 0;
				}
				if (projectile.frame > 3)
				{
					projectile.frame = 0;
				}
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
			for (int num645 = 0; num645 < 200; num645++)
			{
				NPC nPC2 = Main.npc[num645];
				if (nPC2.CanBeChasedBy(projectile, false))
				{
					float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
					if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						num633 = num646;
						vector46 = nPC2.Center;
						flag25 = true;
					}
				}
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
				Vector2 vector47 = vector46 - projectile.Center;
				float num648 = vector47.Length();
				vector47.Normalize();
				if (num648 > 200f)
				{
					float scaleFactor2 = 8f;
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
				if (num651 < num636 && flag26 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
				{
					projectile.ai[0] = 0f;
					projectile.netUpdate = true;
				}
				if (num651 > 2000f)
				{
					projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
					projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2);
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
				projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (projectile.ai[1] > 40f)
			{
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 0f)
			{
				if (projectile.ai[1] == 0f && flag25 && num633 < 500f)
				{
					projectile.ai[1] += 1f;
					if (Main.myPlayer == projectile.owner)
					{
						projectile.ai[0] = 2f;
						Vector2 value20 = vector46 - projectile.Center;
						value20.Normalize();
						projectile.velocity = value20 * 8f;
						projectile.netUpdate = true;
						return;
					}
				}
			}
        }

        public int FireDamage(Player player)
        {
            if (player.statLife > (player.statLifeMax * .9f))
            {
                 return projectile.damage = 60 + 0;
            }
            if (player.statLife > (player.statLifeMax * .8f))
            {
                return projectile.damage = 60 + 5;
            }
            if (player.statLife > (player.statLifeMax * .7f))
            {
                return projectile.damage = 60 + 10;
            }
            if (player.statLife > (player.statLifeMax * .6f))
            {
                return projectile.damage = 60 + 15;
            }
            if (player.statLife > (player.statLifeMax * .5f))
            {
                return projectile.damage = 60 + 20;
            }
            if (player.statLife > (player.statLifeMax * .4f))
            {
                return projectile.damage = 60 + 25;
            }
            if (player.statLife > (player.statLifeMax * .3f))
            {
                return projectile.damage = 60 + 30;
            }
            if (player.statLife > (player.statLifeMax * .2f))
            {
                return projectile.damage = 60 + 35;
            }
            if (player.statLife > (player.statLifeMax * .1f))
            {
                return projectile.damage = 60 + 40;
            }
            return projectile.damage = 60 + 60;
        }
    }
}