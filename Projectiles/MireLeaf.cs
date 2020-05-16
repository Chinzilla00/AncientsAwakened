using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class MireLeaf : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Leaf");
        }
        public override void SetDefaults()
        {
            projectile.arrow = true;
            projectile.width = 34;
            projectile.height = 22;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.melee = true;
            aiType = ProjectileID.WoodenArrowFriendly;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.1f, 0.3f);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 180f)
            {
                projectile.ai[0] = 0f;
                projectile.netUpdate = true;
                int dustIndex = Dust.NewDust(projectile.Center, projectile.width, projectile.height, 41);
                Main.dust[dustIndex].velocity *= 0.3f;
            }
        }

        public override void Kill(int i)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1);

            for (int m = 0; m < 12; m++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.BogleafDust>(), projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, Microsoft.Xna.Framework.Color.White, 1.2f);
            }
        }
    }
}

