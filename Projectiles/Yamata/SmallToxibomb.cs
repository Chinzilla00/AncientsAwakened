using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class SmallToxibomb : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Bomb");     
            Main.projFrames[projectile.type] = 4;     
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void SetDefaults()
		{
			projectile.width = 14;               
			projectile.height = 14;              
			projectile.aiStyle = 1;             
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.magic = true;           
			projectile.penetrate = 1;           
			projectile.timeLeft = 600;          
			projectile.alpha = 20;              
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
            projectile.aiStyle = 0;
            
		}

        public override void AI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;

                }
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.rotation += projectile.velocity.X * 0.1f;
                float num689 = 500f;
                int num690 = -1;
                for (int num691 = 0; num691 < 200; num691++)
                {
                    NPC nPC5 = Main.npc[num691];
                    if (nPC5.CanBeChasedBy(this, false) && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC5.position, nPC5.width, nPC5.height))
                    {
                        float num692 = (nPC5.Center - projectile.Center).Length();
                        if (num692 < num689)
                        {
                            num690 = num691;
                            num689 = num692;
                        }
                    }
                }
                projectile.ai[0] = num690 + 1;
                if (projectile.ai[0] == 0f)
                {
                    projectile.ai[0] = -15f;
                }
                if (projectile.ai[0] > 0f)
                {
                    float scaleFactor5 = Main.rand.Next(35, 75) / 30f;
                    projectile.velocity = (projectile.velocity * 20f + Vector2.Normalize(Main.npc[(int)projectile.ai[0] - 1].Center - projectile.Center + new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101))) * scaleFactor5) / 21f;
                    projectile.netUpdate = true;
                }
            }
            else if (projectile.ai[0] > 0f)
            {
                Vector2 value23 = Vector2.Normalize(Main.npc[(int)projectile.ai[0] - 1].Center - projectile.Center);
                projectile.velocity = (projectile.velocity * 40f + value23 * 12f) / 41f;
            }
            else
            {
                projectile.ai[0] += 1f;
                projectile.alpha -= 25;
                if (projectile.alpha < 50)
                {
                    projectile.alpha = 50;
                }
                projectile.velocity *= 0.95f;
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = Main.rand.Next(80, 121) / 100f;
                projectile.netUpdate = true;
            }
            projectile.scale = projectile.ai[1];
            return;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.width, projectile.height), projectile.width, projectile.height, ModContent.DustType<Dusts.YamataDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.YamataDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("ToxiboomSmall"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
    }
}
