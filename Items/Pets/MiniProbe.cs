using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class MiniProbe : ModProjectile
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mini Probe"); // Automatic from .lang files
			Main.projFrames[projectile.type] = 6;
			Main.projPet[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.SuspiciousTentacle);
            aiType = ProjectileID.SuspiciousTentacle;
            projectile.width = 14;
            projectile.height = 14;
            
        }

        public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.suspiciouslookingTentacle = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.dead)
			{
				modPlayer.MiniProbe = false;
			}
			if (modPlayer.MiniProbe)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}