using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class Sharkron : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sharkron"); // Automatic from .lang files
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.ZephyrFish);
			aiType = ProjectileID.ZephyrFish;
            projectile.width = 66;
            projectile.height = 56;
            
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.zephyrfish = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.dead)
			{
				modPlayer.Sharkron = false;
			}
			if (modPlayer.Sharkron)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}