using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;

namespace AAMod.Projectiles
{
    public class SingularityVortex : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Singularity Vortex");
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 100;
            projectile.alpha = 130;
            projectile.scale = .01f;
            projectile.alpha = 255;
            projectile.tileCollide = false;
        }

        private float RingRotation = 0f;

        public override void AI()
        {
            RingRotation += 0.03f;

            if (projectile.alpha > 80)
            {
                projectile.alpha -= 3;
            }
            else
            {
                projectile.alpha = 80;
            }

            if (projectile.scale > 1f && projectile.ai[0] == 0)
            {
                projectile.hostile = true;
                projectile.ai[0] = 1;
                projectile.scale = 1f;
            }
            else
            {
                projectile.hostile = false;
                projectile.scale += .5f;
            }

            if (projectile.ai[0] == 1 && projectile.penetrate > 0)
            {
                projectile.scale = projectile.penetrate / 100f;
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color lightColor)
        {
            Texture2D Tex = Main.projectileTexture[projectile.type];
            Texture2D Vortex = mod.GetTexture("Projectiles/SingularityVortex1");
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            BaseDrawing.DrawTexture(spritebatch, Vortex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, RingRotation, 0, 1, frame, projectile.GetAlpha(GenericUtils.COLOR_GLOWPULSE), true);
            BaseDrawing.DrawTexture(spritebatch, Tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, -RingRotation, 0, 1, frame, projectile.GetAlpha(GenericUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}
