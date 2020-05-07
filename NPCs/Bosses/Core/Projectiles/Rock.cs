using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Core.Projectiles
{
    public class Rock : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            projectile.width = 32;
            projectile.height = 32;
			projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (projectile.velocity.X > 0)
            {
                projectile.direction = 1;
            }
            else
            {
                projectile.direction = -1;
            }
            if (projectile.velocity.X != 0)
            {
                projectile.rotation += .2f * projectile.direction;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                float VelX = -projectile.velocity.X * 0.2f;
                float VelY = -projectile.velocity.Y * 0.2f;
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.Stone, VelX, VelY);
            }

            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int i = 0; i < Main.rand.Next(5, 10); i++)
            {
                int x = Main.rand.Next(-6, 6);
                int y = -Main.rand.Next(3, 5);
                int p = Projectile.NewProjectile(projectile.position, new Vector2(x, y), ModContent.ProjectileType<RockChunk>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0, Main.rand.Next(23));
                Main.projectile[p].Center = projectile.Center - new Vector2(0, 25);
            }
        }
    }

    public class RockFall : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetDefaults()
        {
            projectile.width = 250;
            projectile.height = 2;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            projectile.ai[0]++;

            for (int d = 0; d < 25; d++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 250, 2, Main.rand.Next(3) == 0 ? DustID.Dirt : DustID.Stone);
            }

            if (projectile.ai[0] % 45 == 0)
            {
                Projectile.NewProjectile(new Vector2(projectile.position.X + Main.rand.Next(250), projectile.position.Y), Vector2.Zero, ModContent.ProjectileType<Rock>(), 50 / 4, 0, Main.myPlayer);
            }

            if (Main.npc[(int)projectile.ai[0]].ai[1] != 2)
            {
                projectile.active = false;
            }
        }
    }
}
