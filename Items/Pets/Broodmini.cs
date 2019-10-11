using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEM
    /// </summary>
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
			projectile.CloneDefaults(ProjectileID.DD2PetDragon);
			aiType = ProjectileID.DD2PetDragon;
            projectile.width = 66;
            projectile.height = 56;
            
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
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
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