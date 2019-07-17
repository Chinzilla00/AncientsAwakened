using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.TrueDemon
{
    internal class ShadowflameBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Bolt");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 100;
            projectile.aiStyle = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            return false;
        }

        public override void AI()
        {
            for (int num443 = 0; num443 < 2; num443++)
            {
                Vector2 vector31 = projectile.position;
                vector31 -= projectile.velocity * (num443 * 0.25f);
                projectile.alpha = 255;
                int num444 = Dust.NewDust(vector31, 1, 1, DustID.Shadowflame, 0f, 0f, 0);
                Main.dust[num444].position = vector31;
                Dust expr_13D2C_cp_0 = Main.dust[num444];
                expr_13D2C_cp_0.position.X += (projectile.width / 2);
                Dust expr_13D50_cp_0 = Main.dust[num444];
                expr_13D50_cp_0.position.Y += (projectile.height / 2);
                Main.dust[num444].scale = Main.rand.Next(70, 110) * 0.013f;
                Main.dust[num444].velocity *= 0.2f;
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num585 = 0; num585 < 20; num585++)
            {
                int num586 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f, 100, default, 2f);
                Main.dust[num586].noGravity = true;
                Main.dust[num586].velocity *= 4f;
            }
        }
    }
}