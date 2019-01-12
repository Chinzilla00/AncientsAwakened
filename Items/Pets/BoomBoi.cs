using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class BoomBoi : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boomer"); // Automatic from .lang files
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
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.dead)
			{
				modPlayer.BoomBoi = false;
			}
			if (modPlayer.BoomBoi)
			{
				projectile.timeLeft = 2;
			}
        }
	}
}