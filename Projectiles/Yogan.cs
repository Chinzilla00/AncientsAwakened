using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Yogan : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BlueMoon);
            projectile.penetrate = -1;  
            projectile.width = 42;
            projectile.height = 40;
			projectile.tileCollide = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            ProjectileDraw.DrawChain(projectile.whoAmI, Main.player[projectile.owner].MountedCenter,
                "AAMod/Projectiles/Yogan_Chain");
            ProjectileDraw.DrawAroundOrigin(projectile.whoAmI, lightColor);
            return false;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 300);
			target.AddBuff(BuffID.Frostburn, 300);
		}
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacier Breaker");
        }


    }
}
