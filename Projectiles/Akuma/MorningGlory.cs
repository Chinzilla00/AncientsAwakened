using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

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
            projectile.aiStyle = -1;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            
        }
        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 45f)
                {
                    float num975 = 0.98f;
                    float num976 = 0.35f;
                    projectile.ai[1] = 45f;
                    projectile.velocity.X = projectile.velocity.X * num975;
                    projectile.velocity.Y = projectile.velocity.Y + num976;
                }
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                int num977 = 15;
                bool flag53 = false;
                projectile.localAI[0] += 1f;
                int num978 = (int)projectile.ai[1];
                if (projectile.localAI[0] >= 60 * num977)
                {
                    flag53 = true;
                }
                else if (num978 < 0 || num978 >= 200)
                {
                    flag53 = true;
                }
                else if (Main.npc[num978].active)
                {
                    projectile.Center = Main.npc[num978].Center - projectile.velocity * 2f;
                    projectile.gfxOffY = Main.npc[num978].gfxOffY;
                }
                else
                {
                    flag53 = true;
                }
                if (flag53)
                {
                    projectile.Kill();
                }
            }
        }

        public bool StuckInEnemy = false;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 60);
            Rectangle myRect = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
            bool flag3 = projectile.Colliding(myRect, target.getRect());
            if (flag3 && !StuckInEnemy && !target.boss)
            {
                int proj = Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType<AkumaExp>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
                Main.projectile[proj].melee = false;
                Main.projectile[proj].ranged = true;
                StuckInEnemy = true;
                projectile.ai[0] = 1f;
                projectile.ai[1] = target.whoAmI;
                projectile.velocity = (target.Center - projectile.Center) * 0.75f;
                projectile.netUpdate = true;
            }
            else if (target.boss)
            {
                Projectile.NewProjectile(projectile.position, projectile.velocity, mod.ProjectileType<AkumaExp>(), projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI);
            }
        }
    }
}