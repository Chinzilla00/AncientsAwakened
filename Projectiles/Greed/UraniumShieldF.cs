using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Greed
{
    public class UraniumShieldF : ModProjectile
	{
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiation");
		}

		public override void SetDefaults()
		{
			projectile.width = 90;
			projectile.height = 90;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;          
		}

        public override void AI()
        {
            Projectile Body = Main.projectile[(int)projectile.ai[0]];
            projectile.Center = Body.Center;
            if (Body == null || !Body.active || (Body.ai[0] != 21 && Body.type != mod.ProjectileType<GreedMinion>()))
            {
                projectile.active = false;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D shield = mod.GetTexture("NPCs/Bosses/Greed/UraniumShield");
            BaseDrawing.DrawTexture(spriteBatch, shield, 0, projectile.position, shield.Width, shield.Height, 1f, 0, 0, 1, new Rectangle(0, 0, shield.Width, shield.Height), AAColor.Uranium, true);
            return false;
        }
    }
}
