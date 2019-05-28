using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Serpent

{
    public class BB : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BB");
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.IceDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color));
            }
            target.AddBuff(BuffID.Chilled, 200);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.IceDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color));
            }
            return true;
        }

        public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.FruitcakeChakram);
        }
    }
}
