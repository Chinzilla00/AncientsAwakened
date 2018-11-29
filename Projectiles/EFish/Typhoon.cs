using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.EFish
{
    public class Typhoon : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultiblade Typhoon");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Typhoon);
        }
		
		
    }
}