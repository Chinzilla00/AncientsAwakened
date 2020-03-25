using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using BaseMod;

namespace AAMod.Projectiles.Sag
{
    class ZeroStarP : ModProjectile
	{
        public override void SetDefaults()
        {
            projectile.aiStyle = 3;
            projectile.width = 5;
	        projectile.height = 5;
	        projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
	        projectile.penetrate = -1;
	        projectile.timeLeft = 150;
        }
        public float[] internalAI = new float[1];
        public float[] shootAI = new float[4];

        public override void AI()
        {
            const int aislotHomingCooldown = 0;
            const int homingDelay = 20;

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay; 
            }
        }

 
        public override void PostAI()
        {
            int Target = BaseAI.GetNPC(projectile.Center, -1, 500);
            if (Target != -1)
            {
                NPC target = Main.npc[Target];
                BaseAI.ShootPeriodic(projectile, target.position, 14, 14, ModContent.ProjectileType<Darkray>(), ref internalAI[0], 20, projectile.damage, 4, true);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Projectiles/Sag/ZeroStarP"), 0, projectile, lightColor, true);
            BaseDrawing.DrawTexture(spriteBatch, Glow, 0, projectile, AAColor.Oblivion, true);
            return false;
        }
    }
}