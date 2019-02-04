using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Pets
{
    public class Raidmini : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raidmini"); // Automatic from .lang files
			Main.projFrames[projectile.type] = 3;
			Main.projPet[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.DD2PetDragon);
			aiType = ProjectileID.DD2PetDragon;
            projectile.width = 66;
            projectile.height = 56;
        }


        public static Texture2D glowTex = null;
        public static Texture2D glowTex1 = null;
        public Color color;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/Raidmini_Glow1");
                glowTex1 = mod.GetTexture("Glowmasks/Raidmini_Glow2");
            }
            color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(projectile.position), BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position));
            BaseDrawing.DrawTexture(spritebatch, Main.projectileTexture[projectile.type], 0, projectile, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, projectile, color);
            BaseDrawing.DrawTexture(spritebatch, glowTex1, 0, projectile, Color.White);
            return false;
        }


        public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.petFlagDD2Dragon = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.dead)
			{
				modPlayer.Raidmini = false;
			}
			if (modPlayer.Raidmini)
			{
				projectile.timeLeft = 2;
			}
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 2)
                {
                    projectile.frame = 0;
                }
            }
        }
	}
}