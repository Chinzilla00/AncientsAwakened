using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Projectiles.Akuma
{
    public class MorningGlory : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Morning Glory");
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
			projectile.timeLeft = 180;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 1;
        }
		
        private const int alphaReduction = 25;
		
        public override void AI()
        {	
			if (projectile.ai[1] != -1f) projectile.rotation =
				projectile.velocity.ToRotation() + (float)Math.PI / 2 + (float)Math.PI / 4;
			if (projectile.ai[1] == -1f) projectile.rotation =
				projectile.velocity.ToRotation() + (float)Math.PI / 2;
			
			if (projectile.ai[1] != -1f) projectile.ai[0]++;
			
			if (projectile.ai[0] == 1f || projectile.ai[0] == 3f)
			{
				int numberProjectiles = 2;
				float rotation = MathHelper.ToRadians(1);
				if (projectile.ai[0] == 3f) rotation = MathHelper.ToRadians(2);
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
					int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<MorningGlory>(),  projectile.damage, projectile.knockBack, projectile.owner, 0, -1f);
					Main.projectile[proj].usesLocalNPCImmunity = true;
					Main.projectile[proj].localNPCHitCooldown = 10;
					Main.projectile[proj].penetrate = -1;
					Main.projectile[proj].rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2 + (float)Math.PI / 4;
				}
			}

			if (projectile.ai[1] != -1f)
			{
				if (projectile.alpha > 0)
				{
					projectile.alpha -= alphaReduction;
				}
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
			}
			if (projectile.ai[1] == -1f)
			{
				projectile.alpha += 2;
				if (projectile.alpha >= 255)
				{
					projectile.Kill();
				}
			}
			
			for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 0);
                
                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }
		
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Texture2D texture = Main.projectileTexture[projectile.type];
			if (projectile.ai[1] == -1f) texture = mod.GetTexture("Projectiles/Akuma/MorningGloryPhantom");
            spriteBatch.Draw(texture, new Vector2(projectile.Center.X - Main.screenPosition.X, projectile.Center.Y - Main.screenPosition.Y + 2),
                        new Rectangle(0, 0, texture.Width, texture.Height), Color.White, projectile.rotation,
                        new Vector2(projectile.width * 0.5f, projectile.height * 0.5f), 1f, SpriteEffects.None, 0f);
            return false;
        }

        public override void Kill(int i)
        {
			Main.PlaySound(SoundID.Item14, projectile.position);
			Projectile.NewProjectile(projectile.position, projectile.velocity, ModContent.ProjectileType<AkumaExp>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
        }
	}
}
 