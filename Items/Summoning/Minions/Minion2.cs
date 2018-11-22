using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
	public abstract class Minion2 : ModProjectile
	{
		public override void AI()
		{
			CheckActive();
		}

		public abstract void CheckActive();
	}
}