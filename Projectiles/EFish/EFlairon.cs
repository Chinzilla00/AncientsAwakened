using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.EFish
{
    public class EFlairon : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor Flairon");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Flairon);
        }
		
		
    }
}