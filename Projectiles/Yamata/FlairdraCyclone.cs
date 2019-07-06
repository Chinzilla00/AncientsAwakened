using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class FlairdraCyclone : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cyclone");
		}
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 90;
            projectile.melee = true;
            projectile.noEnchantments = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            projectile.rotation += 0.1f;
            if (projectile.ai[0] == 0f)
            {
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
		
		public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
			//projectile.tileCollide = false;
			//projectile.timeLeft = 20;
			projectile.ai[0] = 1f;
			return false;
		}
    }
}