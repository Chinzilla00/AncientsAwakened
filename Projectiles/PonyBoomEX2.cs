using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class PonyBoomEX2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ponysplosion");
        }

        public override void SetDefaults()
        {
            projectile.width = 130;
            projectile.height = 130;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Item20, projectile.position);
                projectile.localAI[0] += 1f;
            }
            projectile.ai[0] += 1f;
            if (projectile.type == 296)
            {
                projectile.ai[0] += 3f;
            }
            float num461 = 25f;
            if (projectile.ai[0] > 180f)
            {
                num461 -= (projectile.ai[0] - 180f) / 2f;
            }
            if (num461 <= 0f)
            {
                num461 = 0f;
                projectile.Kill();
            }
            if (projectile.type == 296)
            {
                num461 *= 0.7f;
            }
            int num462 = 0;
            while (num462 < num461)
            {
                float num463 = Main.rand.Next(-10, 11);
                float num464 = Main.rand.Next(-10, 11);
                float num465 = Main.rand.Next(3, 9);
                float num466 = (float)System.Math.Sqrt(num463 * num463 + num464 * num464);
                num466 = num465 / num466;
                num463 *= num466;
                num464 *= num466;
                int num467 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, Main.DiscoColor, 1.5f);
                Main.dust[num467].noGravity = true;
                Main.dust[num467].position.X = projectile.Center.X;
                Main.dust[num467].position.Y = projectile.Center.Y;
                Dust expr_14B5B_cp_0 = Main.dust[num467];
                expr_14B5B_cp_0.position.X = expr_14B5B_cp_0.position.X + Main.rand.Next(-10, 11);
                Dust expr_14B85_cp_0 = Main.dust[num467];
                expr_14B85_cp_0.position.Y = expr_14B85_cp_0.position.Y + Main.rand.Next(-10, 11);
                Main.dust[num467].velocity.X = num463;
                Main.dust[num467].velocity.Y = num464;
                num462++;
            }
        }
    }
}
