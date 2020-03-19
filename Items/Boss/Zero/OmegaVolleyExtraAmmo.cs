using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Boss.Zero
{
    public class OmegaVolleyExtraAmmo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Omega Shoot");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        
		}

		public override void SetDefaults()
		{
			projectile.width = 20;               
			projectile.height = 20;
			projectile.scale = 0.5f;              
			projectile.aiStyle = -1;             
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.ranged = true;           
			projectile.timeLeft = 600;          
			projectile.alpha = 0;             
			projectile.light = 0f;            
			projectile.ignoreWater = true;          
			projectile.tileCollide = true;          
			projectile.extraUpdates = 1;            
			aiType = ProjectileID.Bullet;           
		}

		private int homingtime = 3;
		private int homingDelay = 3;

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void AI()
        {
			Player projOwner = Main.player[projectile.owner];
           	projectile.direction = projOwner.direction;
           	projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (projectile.spriteDirection == -1)
            {
               projectile.rotation -= MathHelper.ToRadians(180f);
            }

			float num167 = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
			float num168 = projectile.ai[0];
			if (num168 == 0f)
			{
				projectile.ai[0] = num167;
				num168 = num167;
			}
			if(homingtime >= 0 && homingDelay < 0)
			{
				float num169 = projectile.position.X;
				float num170 = projectile.position.Y;
				float num171 = 300f;
				bool flag4 = false;
				int num172 = 0;
				if (projectile.ai[1] == 0f)
				{
					int num;
					for (int num173 = 0; num173 < 200; num173 = num + 1)
					{
						if (Main.npc[num173].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == num173 + 1))
						{
							float num174 = Main.npc[num173].position.X + Main.npc[num173].width / 2;
							float num175 = Main.npc[num173].position.Y + Main.npc[num173].height / 2;
							float num176 = Math.Abs(projectile.position.X + projectile.width / 2 - num174) + Math.Abs(projectile.position.Y + projectile.height / 2 - num175);
							if (num176 < num171 && Collision.CanHit(new Vector2(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2), 1, 1, Main.npc[num173].position, Main.npc[num173].width, Main.npc[num173].height))
							{
								num171 = num176;
								num169 = num174;
								num170 = num175;
								flag4 = true;
								num172 = num173;
							}
						}
						num = num173;
					}
					if (flag4)
					{
						projectile.ai[1] = num172 + 1;
					}
					flag4 = false;
				}
				if (projectile.ai[1] > 0f)
				{
					int num177 = (int)(projectile.ai[1] - 1f);
					if (Main.npc[num177].active && Main.npc[num177].CanBeChasedBy(this, true) && !Main.npc[num177].dontTakeDamage)
					{
						float num178 = Main.npc[num177].position.X + Main.npc[num177].width / 2;
						float num179 = Main.npc[num177].position.Y + Main.npc[num177].height / 2;
						float num180 = Math.Abs(projectile.position.X + projectile.width / 2 - num178) + Math.Abs(projectile.position.Y + projectile.height / 2 - num179);
						if (num180 < 1000f)
						{
							flag4 = true;
							num169 = Main.npc[num177].position.X + Main.npc[num177].width / 2;
							num170 = Main.npc[num177].position.Y + Main.npc[num177].height / 2;
						}
					}
					else
					{
						projectile.ai[1] = 0f;
					}
				}
				if (!projectile.friendly)
				{
					flag4 = false;
				}
				if (flag4)
				{
					float num181 = num168;
					Vector2 vector19 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
					float num182 = num169 - vector19.X;
					float num183 = num170 - vector19.Y;
					float num184 = (float)Math.Sqrt(num182 * num182 + num183 * num183);
					num184 = num181 / num184;
					num182 *= num184;
					num183 *= num184;
					int num185 = 8;
					projectile.velocity.X = (projectile.velocity.X * (num185 - 1) + num182) / num185;
					projectile.velocity.Y = (projectile.velocity.Y * (num185 - 1) + num183) / num185;
				}
				homingtime --;
				homingDelay = -1;
            }
			else if(homingDelay >= 0)
			{
				homingDelay --;
				Vector2 speedkeep = projectile.velocity;
				speedkeep.Normalize();
				projectile.velocity = speedkeep * projectile.ai[0];
			}
			else
			{
				homingtime = 3;
				homingDelay = 10;
				Vector2 speedkeep = projectile.velocity;
				speedkeep.Normalize();
				projectile.velocity = speedkeep * projectile.ai[0];
			}
			return;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			
			damage = (int)(damage * 1.5f);
		}
	}
}
