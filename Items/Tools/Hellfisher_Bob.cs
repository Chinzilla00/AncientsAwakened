using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Hellfisher_Bob : ModProjectile
    {
        

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BobberHotline);
        }
    }
}
