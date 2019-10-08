using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class MushMace : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushmace");
		}
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.penetrate = -1; 
            projectile.melee = true;
            projectile.knockBack = 0;
        }
		
		public override void AI()
		{
            if (projectile.timeLeft == 120)
            {
                projectile.ai[0] = 1f;
            }

            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }

            Main.player[projectile.owner].itemAnimation = 10;
            Main.player[projectile.owner].itemTime = 10;

            if (projectile.alpha == 0)
            {
                if (projectile.position.X + (projectile.width / 2) > Main.player[projectile.owner].position.X + (Main.player[projectile.owner].width / 2))
                {
                    Main.player[projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[projectile.owner].ChangeDir(-1);
                }
            }
            Vector2 vector14 = new Vector2(projectile.position.X + (projectile.width * 0.5f), projectile.position.Y + (projectile.height * 0.5f));
            float num166 = Main.player[projectile.owner].position.X + (Main.player[projectile.owner].width / 2) - vector14.X;
            float num167 = Main.player[projectile.owner].position.Y + (Main.player[projectile.owner].height / 2) - vector14.Y;
            float num168 = (float)Math.Sqrt((num166 * num166) + (num167 * num167));
            if (projectile.ai[0] == 0f)
            {
                if (num168 > 400f)
                {
                    projectile.ai[0] = 1f;
                }
                else if (num168 > 500f)
                {
                    projectile.ai[0] = 1f;
                }
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
                projectile.ai[1] += 1f;
                if (projectile.ai[1] > 5f)
                {
                    projectile.alpha = 0;
                }
                if (projectile.ai[1] > 8f)
                {
                    projectile.ai[1] = 8f;
                }
                if (projectile.ai[1] >= 10f)
                {
                    projectile.ai[1] = 15f;
                    projectile.velocity.Y = projectile.velocity.Y + 0.3f;
                }
                if (projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = -1;
                }
                else
                {
                    projectile.spriteDirection = 1;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                projectile.tileCollide = false;
                projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 30f;

                if (num168 < 50f)
                {
                    projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                projectile.velocity.X = num166;
                projectile.velocity.Y = num167;
                if (projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = 1;
                }
                else
                {
                    projectile.spriteDirection = -1;
                }
            }
        }
		
		public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            projectile.ai[0] = 1;
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 16;
            height = 16;
            return true;
        }
		
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
			projectile.ai[0] = 1f;
			return false;
		}
		
 
        // chain voodoo
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        { 
            BaseMod.BaseDrawing.DrawChain(spriteBatch, mod.GetTexture("Chains/MushMaceChain"), 0, projectile.Center, Main.player[projectile.owner].Center);
            return true;
        }
    }
}