using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Fulgurite
{
    public class FulguriteRing : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.scale = .1f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            projectile.knockBack = 10;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.Center = player.Center;
            projectile.scale = 1 - (projectile.alpha / 255);
            if (projectile.ai[0] == 0)
            {
                projectile.alpha -= 15;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    projectile.ai[0] = 1;
                }
            }
            else
            {
                projectile.alpha += 5;
                if (projectile.alpha >= 255)
                {
                    projectile.active = false;
                }
            }
        }
        

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item94, projectile.position);
        }

        

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }
    }
}