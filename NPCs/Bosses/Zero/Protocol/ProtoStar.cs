using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    public class ProtoStar : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 72;
            projectile.height = 72;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.scale = .1f;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.rand.Next(7) == 0)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Unstable>(), 180);
            }
        }

        public override void AI()
        {
            projectile.rotation += .2f;
            if (projectile.ai[0] == 0)
            {
                projectile.scale += .1f;
                if (projectile.scale >= 1)
                {
                    projectile.ai[0] = 1;
                }
            }
            else
            {
                projectile.scale -= .1f;
                if (projectile.scale <= 0)
                {
                    projectile.Kill();
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            int damage = projectile.damage / 3;
            Main.PlaySound(SoundID.Item73, (int)projectile.position.X, (int)projectile.position.Y);
            int a = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(0f, -12f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[a].Center = projectile.Center;
            int b = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(0f, 12f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[b].Center = projectile.Center;
            int c = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(-12f, 0f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[c].Center = projectile.Center;
            int d = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(12f, 0f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[d].Center = projectile.Center;
            int e = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(8f, 8f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[e].Center = projectile.Center;
            int f = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(8f, -8f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[f].Center = projectile.Center;
            int g = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(-8f, 8f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[g].Center = projectile.Center;
            int h = Projectile.NewProjectile(new Vector2(projectile.Center.X, projectile.Center.Y), new Vector2(-8f, -8f), mod.ProjectileType("ProtoBlast"), damage, 3);
            Main.projectile[h].Center = projectile.Center;
        }
    }
}