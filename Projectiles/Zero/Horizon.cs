using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class Horizon : ModProjectile
	{
        public override void SetDefaults()
		{
            
            projectile.width = 64;
            projectile.height = 64;
            projectile.alpha = 100;
            projectile.light = 0.2f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 2;
            projectile.tileCollide = false;
            projectile.scale = 0.9f;
            projectile.melee = true;
            projectile.timeLeft = 300;
            projectile.scale = .1f;
        }
        float RotValue = .1f;
        public override void AI()
        {
            
            if (projectile.ai[0] == 0)
            {
                projectile.scale *= 1.1f;
                projectile.rotation += RotValue;
                if (projectile.scale >= 1)
                {
                    RotValue = -.1f;
                    projectile.ai[0] = 1f;
                }
            }
            if (projectile.ai[0] == 1)
            {
                projectile.scale *= .9f;
                projectile.rotation -= RotValue;
                if (projectile.scale >= 0)
                {
                    projectile.active = false;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, -RotValue, -1, 1, new Rectangle(0, 0, tex.Width, tex.Height), AAColor.Yamata, true);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, RotValue, projectile.direction, 1, new Rectangle(0, 0, tex.Width, tex.Height), AAColor.ZeroShield, true);
            return false;
        }
    }
}
