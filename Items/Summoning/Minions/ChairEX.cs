using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
	public abstract class ChairEX : ModProjectile
	{
		public override void AI()
		{
			CheckActive();
		}

		public abstract void CheckActive();
	}
}