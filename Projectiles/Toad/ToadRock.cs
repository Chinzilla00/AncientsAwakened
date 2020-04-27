using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Toad
{
    public class ToadRock : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            projectile.penetrate = 1;  
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 300;
            projectile.magic = true;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Rock");
		}

        public override bool PreKill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<Toadsplosion>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 0);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.ShroomDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, ModContent.DustType<Dusts.ShroomDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseUtility.RotateVector(default, new Vector2(9f, 0f), m / (float)pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
                Main.dust[dustID].noGravity = true;
            }
            return true;
        }
    }
}
