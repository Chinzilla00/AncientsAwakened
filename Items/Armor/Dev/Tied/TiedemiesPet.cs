using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Tied
{
	public class TiedemiesPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tiedemies");
			Main.projFrames[projectile.type] = 10;
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.Wisp);
            aiType = ProjectileID.Wisp;
        }

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.wisp = false;
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.dead)
			{
				modPlayer.TiedHead = false;
			}
			if (modPlayer.TiedHead)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}