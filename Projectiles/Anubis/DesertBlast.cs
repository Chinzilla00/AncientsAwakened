using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Projectiles.Anubis
{
    class DesertBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Blast");
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.alpha = 20;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 1f / 255f, (255 - projectile.alpha) * 0.7f / 255f, (255 - projectile.alpha) * 0f / 255f);
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            for (int num339 = 0; num339 < 16; num339++)
            {
                Dust dust1;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.JudgementDust>(), 0, 0, 0, Color.White, 1f)];
                dust1.noGravity = true;
            }
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 27);
            for (int num506 = 0; num506 < 15; num506++)
            {
                Dust dust1;
                Vector2 position = projectile.position;
                dust1 = Main.dust[Dust.NewDust(projectile.Center, projectile.width, projectile.height, ModContent.DustType<Dusts.JudgementDust>(), 0, 0, 0, Color.White, 1f)];
                dust1.noGravity = true;
            }
			for (int h = 0; h < 3; h++)
			{
				Vector2 vel = new Vector2(0, -1);
				float rand = Main.rand.NextFloat() * 6.283f;
				vel = vel.RotatedBy(rand);
				vel *= 8f;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vel.X, vel.Y, mod.ProjectileType("FragBlast"), projectile.damage, 0, Main.myPlayer);
			}
        }
    }
}