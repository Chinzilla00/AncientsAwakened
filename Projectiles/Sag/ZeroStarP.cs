using Terraria;
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
            projectile.aiStyle = -1;
            projectile.width = 32;
	        projectile.height = 32;
	        projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
	        projectile.penetrate = -1;
        }
        public float[] internalAI = new float[1];
        public float[] shootAI = new float[1];

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 20f, 30, 20f, 0.6f, true);
            int Target = BaseAI.GetNPC(projectile.Center, -1, 500);
            if (Target != -1 && !Main.npc[Target].friendly)
            {
                NPC target = Main.npc[Target];
                BaseAI.ShootPeriodic(projectile, target.position, 14, 14, ModContent.ProjectileType<Darkray>(), ref internalAI[0], 30, projectile.damage, 7, true);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 8;
            height = 8;
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            BaseDrawing.DrawTexture(spriteBatch, mod.GetTexture("Projectiles/Sag/ZeroStarP"), 0, projectile, lightColor, true);
            BaseDrawing.DrawTexture(spriteBatch, Glow, 0, projectile, AAColor.COLOR_WHITEFADE1, true);
            return false;
        }
    }
}