using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles
{
    public class ManaRose : ModProjectile
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Mana Rose");
			Main.projFrames[projectile.type] = 2;
		}	

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.aiStyle = -1;
            projectile.timeLeft = 320;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.magic = true;
        }

		public override void AI()
		{
			BaseAI.AIVilethorn(projectile, 50, 6, 12);
			if (projectile.ai[1] == 12)
			{
				projectile.frame = 0;
			}
			else
			{
				projectile.frame = 1;
			}
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			Rectangle frame = BaseDrawing.GetFrame(projectile.frame, 34, 34, 0, 0);
			BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 2, frame, projectile.GetAlpha(Color.White), true);
			return false;
		}
	}
}