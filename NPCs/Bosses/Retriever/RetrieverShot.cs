using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Retriever
{
    public class RetrieverShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 42;
            projectile.damage = 10;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.scale *= 0.7f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] += 0.1f;
            projectile.velocity *= 0.75f;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return projHitbox.Intersects(targetHitbox);
        }

		public bool spawnSound = false;
        public override void AI()
        {
			if(Main.netMode != 2 && !spawnSound)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 33);				
				spawnSound = true;
			}
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;

            if (Main.rand.Next(1) == 0)
            {
                int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.FulguriteDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }

        
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Violet;
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5) 
            {
                projectile.frame++; 
                projectile.frameCounter = 0;
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }
    }
}