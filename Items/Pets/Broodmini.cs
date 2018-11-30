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
			aiType = ProjectileID.DD2PetGhost;
            projectile.width = 66;
            projectile.height = 56;
            
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.petFlagDD2Ghost = false; // Relic from aiType
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