using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Magma : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(664);
            projectile.hostile = true;
            projectile.friendly = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magma");
        }
	
	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1000);
        }
    }
}
