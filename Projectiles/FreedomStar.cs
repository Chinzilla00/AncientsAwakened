using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class FreedomStar : ModProjectile
    {
        private int chargeLevel = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FreedomStar");
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            projectile.ai[0] += 1f;

            if (projectile.ai[0] >= 60f)
            {
                chargeLevel = 2;
            }

            else if (projectile.ai[0] >= 30f)
            {
                chargeLevel = 1;
            }

            else
            {
                chargeLevel = 0;
            }

            projectile.position = new Vector2();
        }

        
        /*
        public override void Kill(int timeLeft)
        {
            if (projectile.owner == Main.myPlayer)
            {
                switch (chargeLevel)
                {
                    case 0:
                        Projectile.NewProjectile();
                }
            }
        }*/
    }
}
