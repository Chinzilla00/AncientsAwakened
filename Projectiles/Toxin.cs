using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Toxin : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.StarWrath);
            projectile.penetrate = 3;  
            projectile.width = 45;
            projectile.height = 17;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Toxin");
    }
	
	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 1000);
        }
    }
}
