using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Shen
{
    public class MeteorBlue : ModProjectile
    {
        public int noTileHitCounter = 120;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 3;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 150);
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.extraUpdates = 2;
            projectile.aiStyle = 0;
        }

        public bool EnemyHit = false;
        public bool TileHit = false;

        public override void AI()
        {
            if (projectile.position.Y > Main.player[projectile.owner].position.Y - 300f)
            {
                projectile.tileCollide = true;
            }
            if ((double)projectile.position.Y < Main.worldSurface * 16.0)
            {
                projectile.tileCollide = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            Vector2 position = projectile.Center + (Vector2.Normalize(projectile.velocity) * 10f);
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), 0f, 0f, 0, default(Color), 1f);
                
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            TileHit = true;
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            EnemyHit = true;
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void Kill(int timeLeft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, 1, mod.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color));
                Main.dust[num469].velocity *= 2f;
            }
            if (TileHit)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 30, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("MeteorStrikeBlue"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
            if (EnemyHit)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 30, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("MeteorBoomBlue"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }

        

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}