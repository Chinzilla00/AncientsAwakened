using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles.Shen
{
    public class ChaosSlayerSwordEX : ModProjectile
    {
		public int swordType = 0;
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blade of Unyielding Chaos");
		}

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.alpha = 0;
            projectile.tileCollide = false;
			projectile.extraUpdates = 1;
        }

		public float vectorOffset = 0f;
		public bool offsetLeft = false;
		public Vector2 originalVelocity = Vector2.Zero;

        public override void AI()
        {
            int dustType = (swordType == 0 ? mod.DustType<Dusts.DiscordLight>() : swordType == 1 ? mod.DustType<Dusts.AkumaDustLight>() : mod.DustType<Dusts.YamataDustLight>());

            int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
			Main.dust[dustID].velocity *= 0f;
			Main.dust[dustID].noLight = false;
			Main.dust[dustID].noGravity = true;
			if(swordType != 0)
			{
				dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1) - projectile.velocity, 2, 2, dustType, 0f, 0f, 100, Color.White, 1.2f);
				Main.dust[dustID].velocity *= 0f;
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}

			if(originalVelocity == Vector2.Zero)
			{
				originalVelocity = projectile.velocity;
			}
			if(swordType != 0)
			{
				if(offsetLeft)
				{
					vectorOffset -= 0.04f;
					if(vectorOffset <= -1f)
					{
						vectorOffset = -1f;
						offsetLeft = false;
					}
				}else
				{
					vectorOffset += 0.04f;
					if(vectorOffset >= 1f)
					{
						vectorOffset = 1f;
						offsetLeft = true;
					}
				}
				float velRot = BaseUtility.RotationTo(projectile.Center, projectile.Center + originalVelocity);
				projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(projectile.velocity.Length(), 0f), velRot + (vectorOffset * 0.5f));
			}
			projectile.rotation = BaseUtility.RotationTo(projectile.Center, projectile.Center + projectile.velocity) + 1.57f - (float)MathHelper.PiOver4;
			projectile.spriteDirection = 1;
        }

        public override void Kill(int timeLeft)
        {
			int dustType = (swordType == 0 ? mod.DustType<Dusts.Discord>() : swordType == 1 ? mod.DustType<Dusts.AkumaDustLight>() : mod.DustType<Dusts.YamataDustLight>());
            int boomType = (swordType == 0 ? mod.ProjectileType<MeteorBoom>() : swordType == 1 ? mod.ProjectileType<MeteorBoomBlue>() : mod.ProjectileType<MeteorBoomRed>());
            int pieCut = 20;
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), ((float)m / (float)pieCut) * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			for(int m = 0; m < pieCut; m++)
			{
				int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), ((float)m / (float)pieCut) * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}

            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, boomType, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.PlaySound(SoundID.Item62, (int)projectile.position.X, (int)projectile.position.Y);
        }
        

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 150);
		}		
    }
}