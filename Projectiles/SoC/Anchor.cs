using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.SoC
{
    public class Anchor : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reality Anchor");
		}
        public override void SetDefaults()
        {

            projectile.width = 34;
            projectile.height = 34;
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        public int BoomTimer = 0;
        public bool Boom = true;

        public override void AI()
        {
            if (projectile.velocity.Y > 0)
            {
                BoomTimer++;
            }
            else
            {
                BoomTimer = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;

                if (projectile.ai[1] >= 10f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + 0.5f;
                    if (projectile.velocity.Y < 0f)
                    {

                        projectile.velocity.Y = projectile.velocity.Y + 0.35f;
                    }
                    projectile.velocity.X = projectile.velocity.X * 0.95f;
                    if (projectile.velocity.Y > 16f)
                    {
                        projectile.velocity.Y = 16f;
                    }
                    if (Vector2.Distance(projectile.Center, Main.player[projectile.owner].Center) > 800f)
                    {
                        projectile.ai[0] = 1f;
                    }
                }
                else if (projectile.ai[1] >= 30f)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                projectile.tileCollide = false;
                float num41 = 16f;
                float num42 = 4f;
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
                Vector2 vector3 = new Vector2(num43, num44) - projectile.velocity;
                if (vector3 != Vector2.Zero)
                {
                    Vector2 value = vector3;
                    value.Normalize();
                    projectile.velocity += value * Math.Min(num42, vector3.Length());
                }
                else
                {
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


        public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            //target.AddBuff(BuffID.Daybreak, 600);
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }
		
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
            if (BoomTimer > 180 && Boom)
            {
                Boom = false;

                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 30, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("RealityBurstHuge"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[0] = 1f;
                return false;
            }
            if (BoomTimer > 120 && Boom)
            {
                Boom = false;

                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 20, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("RealityBurstLarge"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[0] = 1f;
                return false;
            }
            if (BoomTimer > 60 && Boom)
            {
                Boom = false;

                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 10, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("RealityBurstMed"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[0] = 1f;
                return false;
            }
            else
            {
                Boom = false;

                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 5, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("RealityBurstSmall"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.ai[0] = 1f;
                return false;
            }
        }
		
 
        // chain voodoo
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			
            Texture2D texture = ModLoader.GetTexture("AAMod/Projectiles/SoC/Anchor_Chain");
 
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}