using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Demon
{
	public class ImpMinion : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imp Servant");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.FlyingImp);
            projectile.minionSlots = 0;
        }
    }
}