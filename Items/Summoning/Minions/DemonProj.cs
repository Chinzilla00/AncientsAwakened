using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    internal class DemonProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.DemonScythe);
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.magic = false;
            projectile.minion = true;
        }

    }
}