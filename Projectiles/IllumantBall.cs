using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class IllumantBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BlueMoon);
            projectile.penetrate = -1;  
            projectile.width = 38;
            projectile.height = 38;
			projectile.tileCollide = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            ProjectileDraw.DrawChain(projectile.whoAmI, Main.player[projectile.owner].MountedCenter,
                "AAMod/Projectiles/IllumantBall_Chain");
            ProjectileDraw.DrawAroundOrigin(projectile.whoAmI, lightColor);
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IllumantBall");
        }


    }
}
