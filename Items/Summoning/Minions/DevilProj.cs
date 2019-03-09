using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    internal class DevilProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trident");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.UnholyTridentFriendly);
            projectile.magic = false;
            projectile.minion = true;
        }

    }
}