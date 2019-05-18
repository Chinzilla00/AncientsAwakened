using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah
{
    public class BaneT : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bane of the Bunny");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.aiStyle = 1;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            aiType = ProjectileID.BoneJavelin;
        }
    }
}
