using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class PonyBoomEX : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ponysplosion");
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = 50;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
        }

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Item20, projectile.position);
                projectile.localAI[0] += 1f;
            }
            bool flag15 = false;
            bool flag16 = false;
            if (projectile.velocity.X < 0f && projectile.position.X < projectile.ai[0])
            {
                flag15 = true;
            }
            if (projectile.velocity.X > 0f && projectile.position.X > projectile.ai[0])
            {
                flag15 = true;
            }
            if (projectile.velocity.Y < 0f && projectile.position.Y < projectile.ai[1])
            {
                flag16 = true;
            }
            if (projectile.velocity.Y > 0f && projectile.position.Y > projectile.ai[1])
            {
                flag16 = true;
            }
            if (flag15 && flag16)
            {
                projectile.Kill();
            }
            for (int num457 = 0; num457 < 10; num457++)
            {
                int num458 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 100, Main.DiscoColor, 1.2f);
                Main.dust[num458].noGravity = true;
                Main.dust[num458].velocity *= 0.5f;
                Main.dust[num458].velocity += projectile.velocity * 0.1f;
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType<PonyBoomEX2>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }

    }
}
