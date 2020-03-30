using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class ChaosConstruct : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Construct");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
    	
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 1;
            projectile.timeLeft = 18000 * 5;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.minion = true;
        }
        public override void AI()
        {
            bool flag64 = projectile.type == mod.ProjectileType("ChaosConstruct");
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(mod.BuffType("ChaosConstruct"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.ChaosConstruct = false;
                }
                if (modPlayer.ChaosConstruct)
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
				bool flag23 = Main.projectile[num638].type == mod.ProjectileType("ChaosConstruct");
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
			if (player.HasMinionAttackTargetNPC)
			{
				NPC nPC2 = Main.npc[player.MinionAttackTargetNPC];
                if (nPC2.CanBeChasedBy(projectile, false))
				{
					float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
					if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						vector46 = nPC2.Center;
						flag25 = true;
					}
				}
			}
			else
			{
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

			projectile.rotation += projectile.velocity.X * .01f;

			if (projectile.ai[1] > 0f)
			{
				projectile.ai[1] += Main.rand.Next(1, 4);
			}
			if (projectile.ai[1] > 160f)
			{
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 8f;
				if (flag25 && projectile.ai[1] == 0f)
				{
					projectile.ai[1] += 1f;
					if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
					{
						Vector2 value19 = vector46 - projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X, value19.Y, ModContent.ProjectileType<ChaosConstructShot>(), (int)(projectile.damage * 0.8f), 0f, Main.myPlayer, 0f, Main.rand.Next(2));
						projectile.netUpdate = true;
					}
				}
			}
        }

		public float rot = 0;

		public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
		{
			Texture2D Tex = Main.projectileTexture[projectile.type];
			Texture2D Glow = mod.GetTexture("Items/Summoning/Minions/ChaosConstruct_Eye");

			Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 4, 0, 0);

			BaseDrawing.DrawTexture(spritebatch, Glow, 0, projectile.position, projectile.width, projectile.height, projectile.scale, 0, 0, 4, frame, Color.White, true);
			BaseDrawing.DrawTexture(spritebatch, Tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, lightColor, true);
			return false;
		}
	}
}