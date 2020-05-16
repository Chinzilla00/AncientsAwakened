
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles
{
    public class OrderDiscP : ModProjectile
    {
		public static int defense = 0;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(106);
			projectile.melee = false;
            projectile.ranged = true;
            projectile.penetrate = -1;  
            projectile.width = 22;
            projectile.height = 32;
			projectile.aiStyle = 3;
			aiType = 106;
        }

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Order Disc");
		}
		
		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 211,
				projectile.velocity.X * .5f, projectile.velocity.Y * .5f, 200, Scale: 1.1f);
				dust.velocity += projectile.velocity * 0.4f;
				dust.velocity *= 0.3f;
			}
		}
		
		public override void ModifyHitNPC (NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			defense = target.defense;
			target.defense = 0;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;
			target.defense = defense;
		}

        public override bool OnTileCollide(Vector2 velocityChange)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1);
            }
            BaseAI.TileCollideBoomerang(projectile, ref velocityChange, true);
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, Color.White, true);
            return false;
        }
    }
}
