using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class FireProj : ModProjectile
    {
        public int noTileHitCounter = 120;

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.extraUpdates = 2;
            projectile.aiStyle = 0;
        }

        public override void AI()
        {
			if (projectile.direction == 1) 	projectile.rotation += 0.1f;
			else projectile.rotation -= 0.1f;
			
            if (projectile.position.Y > Main.player[projectile.owner].position.Y - 300f)
            {
                projectile.tileCollide = true;
            }
            if (projectile.position.Y < Main.worldSurface * 16.0)
            {
                projectile.tileCollide = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            Vector2 position = projectile.Center + (Vector2.Normalize(projectile.velocity) * 10f);
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0);
                
                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for(int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, 1, ModContent.DustType<Dusts.AkumaDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FireProjBoom"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = mod.GetTexture("Projectiles/Akuma/FireProj1");
			if (projectile.ai[0] == 2f) texture = mod.GetTexture("Projectiles/Akuma/FireProj2");
			if (projectile.ai[0] == 3f) texture = mod.GetTexture("Projectiles/Akuma/FireProj3");
			if (projectile.ai[0] == 4f) texture = mod.GetTexture("Projectiles/Akuma/FireProj4");
			if (projectile.ai[0] == 5f) texture = mod.GetTexture("Projectiles/Akuma/FireProj5");
            spriteBatch.Draw(texture, new Vector2(projectile.Center.X - Main.screenPosition.X, projectile.Center.Y - Main.screenPosition.Y + 2),
                        new Rectangle(0, 0, texture.Width, texture.Height), Color.White, projectile.rotation,
                        new Vector2(projectile.width * 0.5f, projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}