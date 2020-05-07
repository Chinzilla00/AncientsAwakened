using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Core.Projectiles
{
    public class RockChunk : ModProjectile
    {
        public override string Texture => "AAMod/NPCs/Bosses/Core/Projectiles/RockChunk0";
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (projectile.velocity.X > 0)
            {
                projectile.direction = 1;
            }
            else
            {
                projectile.direction = -1;
            }
            if (projectile.velocity.X != 0)
            {
                projectile.rotation += .2f * projectile.direction;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                float VelX = -projectile.velocity.X * 0.2f;
                float VelY = -projectile.velocity.Y * 0.2f;
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.Stone, VelX, VelY);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = mod.GetTexture("NPCs/Bosses/Core/Projectiles/RockChunk" + projectile.ai[1]);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile, lightColor, true);

            return false;
        }
    }
}
