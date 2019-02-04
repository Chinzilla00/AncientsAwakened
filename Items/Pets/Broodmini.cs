using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class Broodmini : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broodmini"); // Automatic from .lang files
			Main.projFrames[projectile.type] = 3;
			Main.projPet[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.DD2PetGhost);
			aiType = ProjectileID.DD2PetDragon;
            projectile.width = 66;
            projectile.height = 56;
            
        }

        public static Texture2D glowTex = null;
        public Color color;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/Broodmini_Glow");
            }
            color = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, BaseDrawing.GetLightColor(projectile.position), BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position), Color.Violet, BaseDrawing.GetLightColor(projectile.position));
            BaseDrawing.DrawTexture(spritebatch, Main.projectileTexture[projectile.type], 0, projectile, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, projectile, color);
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
				modPlayer.Broodmini = false;
			}
			if (modPlayer.Broodmini)
			{
				projectile.timeLeft = 2;
			}
        }
	}
}