using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class SolarSun : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solar");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.melee = true;
        }

        public override void AI()
        {
            Projectile yoyo = Main.projectile[(int)projectile.ai[0]];
            projectile.Center = yoyo.Center;
            projectile.rotation -= (float)projectile.direction * 6.28318548f / 120f;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {

            return false;
        }
    }
}