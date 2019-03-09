using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    internal class EaterProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("CursedFlame");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.CursedFlameFriendly);
            projectile.magic = false;
            projectile.minion = true;
        }

    }
}