using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Gunk : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gunk");
        }
        public override void SetDefaults()
        {
            projectile.penetrate = 1;  
            projectile.width = 18;
            projectile.height = 18;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 300;
            projectile.aiStyle = -1;
            projectile.alpha = 70;
        }

        public override void AI()
        {
            if (projectile.ai[0]++ > 60)
            {
                projectile.ai[0] = 0;
                projectile.ai[1] += 1;
                if (projectile.ai[1] > 2)
                {
                    projectile.ai[1] = 2;
                }
            }
            projectile.frame = (int)projectile.ai[1];
            if (projectile.ai[1] == 0)
            {
                projectile.scale = 1 / 4;
            }
            else if (projectile.ai[1] == 0)
            {
                projectile.scale = 1 / 2;
            }
            else
            {
                projectile.scale = 1;
            }
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AcidDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 46, new Color(0, 255, 217), 1.184211f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AcidDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 46, new Color(0, 255, 217), 1.184211f);
                Main.dust[num469].velocity *= 2f;
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int width = Main.projectileTexture[projectile.type].Width;
            int height = Main.projectileTexture[projectile.type].Height;

            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, width, height / 3, 0, 2);

            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, 1f, projectile.rotation, 0, 3, frame, lightColor, true);
            return false;
        }
    }
}
