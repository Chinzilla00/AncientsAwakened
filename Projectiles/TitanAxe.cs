using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    // to investigate: Projectile.Damage, (8843)
    class TitanAxe : ModProjectile
	{
		public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.extraUpdates = 2;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Titan Axe");
		}
		
		public override void AI()
        {
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(SoundID.Item7, projectile.position);
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 35f)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                projectile.tileCollide = false;
                float num41 = 15f;
                float num42 = 3f;
                Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num43 = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - vector2.X;
                float num44 = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - vector2.Y;
                float num45 = (float)Math.Sqrt(num43 * num43 + num44 * num44);
                if (num45 > 3000f)
                {
                    projectile.Kill();
                }
                num45 = num41 / num45;
                num43 *= num45;
                num44 *= num45;
                if (projectile.velocity.X < num43)
                {
                    projectile.velocity.X = projectile.velocity.X + num42;
                    if (projectile.velocity.X < 0f && num43 > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num42;
                    }
                }
                else if (projectile.velocity.X > num43)
                {
                    projectile.velocity.X = projectile.velocity.X - num42;
                    if (projectile.velocity.X > 0f && num43 < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - num42;
                    }
                }
                if (projectile.velocity.Y < num44)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num42;
                    if (projectile.velocity.Y < 0f && num44 > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num42;
                    }
                }
                else if (projectile.velocity.Y > num44)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num42;
                    if (projectile.velocity.Y > 0f && num44 < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num42;
                    }
                }
                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value2 = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                    if (rectangle.Intersects(value2))
                    {
                        projectile.Kill();
                    }
                }
            }
			
            projectile.rotation = projectile.velocity.ToRotation();
        }
		
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
            projectile.ai[0] = 1f;
            projectile.velocity.X = -oldVelocity.X;
            projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
	}
}
