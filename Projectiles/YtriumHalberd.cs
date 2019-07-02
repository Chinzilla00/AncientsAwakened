using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class YtriumHalberd : ModProjectile
	{
		public static Color lightColor = new Color(82, 138, 206);
		public static Vector2[] spearPos = new Vector2[]{ new Vector2(0, 0), new Vector2(50, -25), new Vector2(100, -50), new Vector2(100, 0), new Vector2(100, 50), new Vector2(50, 25), new Vector2(30, 0), new Vector2(150, 0), new Vector2(150, 0), new Vector2(30, 0) };
	
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Ytrium Halberd");
		}	

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.timeLeft = 600;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
        }


        public override void AI()
        {
            Main.player[projectile.owner].direction = projectile.direction;
            Main.player[projectile.owner].heldProj = projectile.whoAmI;
            Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
            projectile.position.X = Main.player[projectile.owner].position.X + (Main.player[projectile.owner].width / 2) - (projectile.width / 2);
            projectile.position.Y = Main.player[projectile.owner].position.Y + (Main.player[projectile.owner].height / 2) - (projectile.height / 2);
            projectile.position += projectile.velocity * projectile.ai[0];
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, Main.rand.Next(2) == 0 ? mod.DustType<Dusts.AkumaDust>() : mod.DustType<Dusts.YamataAuraDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 3f;
                projectile.netUpdate = true;
            }
            if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
            {
                projectile.ai[0] -= 2.4f;
            }
            else
            {
                projectile.ai[0] += 0.95f;
            }

            if (Main.player[projectile.owner].itemAnimation == 0)
            {
                projectile.Kill();
            }

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 10;
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			BaseMod.BaseDrawing.DrawProjectileSpear(sb, Main.projectileTexture[projectile.type], 0, projectile, null, 0f, 0f);
			return false;
		}
	}
}