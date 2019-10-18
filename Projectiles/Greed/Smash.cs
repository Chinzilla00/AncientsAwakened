using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Greed
{
    public abstract class Smash : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 200;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 120;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        public override void AI()
        {
            NPC target = Main.npc[(int)projectile.ai[1]];
            if (projectile.ai[0]++ > 20f)
            {
                projectile.Kill();
                return;
            }
            if (projectile.ai[0] == 1f)
            {
                
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y += knockback * target.knockBackResist;
        }
    }
}
