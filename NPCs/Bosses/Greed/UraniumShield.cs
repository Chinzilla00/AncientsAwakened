using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class UraniumShield : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiation");
		}

		public override void SetDefaults()
		{
			projectile.width = 90;
			projectile.height = 90;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;          
		}

        public override void AI()
        {
            NPC Body = Main.npc[(int)projectile.ai[0]];
            projectile.Center = Body.Center;
            if (Body == null || Body.life <= 0 || (Body.ai[0] != 21 && Body.type != mod.NPCType<GreedMinion>()))
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
