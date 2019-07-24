using Microsoft.Xna.Framework;
using Terraria;
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
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.scale = 0.9f;
            projectile.melee = true;
            projectile.timeLeft = 300;
            projectile.scale = .1f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
		
        public override void AI()
        {
            projectile.rotation += .05f;
            if (projectile.ai[0] == 0f)
            {
                projectile.scale += .02f;
                if (projectile.scale >= 1)
                {
                    projectile.ai[0] = 1f;
                }
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.scale -= .02f;
                if (projectile.scale <= 0)
                {
                    projectile.active = false;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, -projectile.rotation, projectile.direction, 1, new Rectangle(0, 0, tex.Width, tex.Height), AAColor.Yamata, true);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.direction, 1, new Rectangle(0, 0, tex.Width, tex.Height), AAColor.ZeroShield, true);
            return false;
        }
    }
}
