using System;
using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class Rift : VoidStarPF
    {
        public override void SetDefaults()
		{
            base.SetDefaults();
            projectile.magic = false;
            projectile.melee = true;
            projectile.extraUpdates = 2;
        }
    }
}
