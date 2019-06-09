using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Mjolnir : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.extraUpdates = 2;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Mjolnir");
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
                if (projectile.ai[1] >= 20f)
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
                Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num43 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector2.X;
                float num44 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector2.Y;
                float num45 = (float)Math.Sqrt((double)(num43 * num43 + num44 * num44));
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
            projectile.rotation += 0.4f * (float)projectile.direction;
            if (projectile.ai[0] == 0f)
            {
                Vector2 velocity = projectile.velocity;
                velocity.Normalize();
                projectile.rotation = (float)Math.Atan2((double)velocity.Y, (double)velocity.X) + 1.57f;
                return;
            }
            Vector2 vector4 = projectile.Center - Main.player[projectile.owner].Center;
            vector4.Normalize();
            projectile.rotation = (float)Math.Atan2((double)vector4.Y, (double)vector4.X) + 1.57f;
            return;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] = 1f;
            projectile.velocity.X = -oldVelocity.X;
            projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }

        public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            target.AddBuff(mod.BuffType<Buffs.Electrified>(), 200);
		}
    }
}
