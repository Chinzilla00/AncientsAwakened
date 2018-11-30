using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
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
