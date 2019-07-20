using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace AAMod.Projectiles.Rajah.Supreme

{
    public class RabbitRocketEX : RabbitRocket3
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rocket");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            int p = Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), mod.ProjectileType<RabbitBoomEX>(), projectile.damage, projectile.knockBack, projectile.owner);
            Main.projectile[p].magic = false;
            Main.projectile[p].ranged = true;
            Main.projectile[p].Center = projectile.Center;
            float spread = 12f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 6;
            for (int i = 0; i < 3; i++)
            {
                double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 3f) * 5, (float)(Math.Cos(offsetAngle) * 3f) * 5, mod.ProjectileType("CarrotEX"), projectile.damage / 6, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.projectile[proj].melee = false;
                Main.projectile[proj].magic = false;
                Main.projectile[proj].ranged = true;
                 proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 3f) * 5, (float)(-Math.Cos(offsetAngle) * 3f) * 5, mod.ProjectileType("CarrotEX"), projectile.damage / 6, projectile.knockBack, projectile.owner, 0f, 0f);
                Main.projectile[proj].melee = false;
                Main.projectile[proj].magic = false;
                Main.projectile[proj].ranged = true;
            }
        }
    }
}
