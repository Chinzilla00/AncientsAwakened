using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
{
    public class AcidProj : ModProjectile
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid");     
            Main.projFrames[projectile.type] = 5;     
		}

		public override void SetDefaults()
		{
			projectile.width = 14;               
			projectile.height = 14;              
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
            target.AddBuff(BuffID.Venom, 600);
        }

        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AcidDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AcidDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, new Color(86, 191, 188));
                Main.dust[num469].velocity *= 2f;
            }
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("AcidBoom"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
    }
}
