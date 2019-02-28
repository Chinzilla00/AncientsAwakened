using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    public class YamataShot : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Blast");
            Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = 1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.alpha = 20;   
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			aiType = ProjectileID.WoodenArrowFriendly;           
            
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.HydraToxin>(), 600);
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y - 2f, 100, default (Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.YamataADust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y - 4f, 100, default(Color));
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 51, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("Shockwave"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
    }
}
