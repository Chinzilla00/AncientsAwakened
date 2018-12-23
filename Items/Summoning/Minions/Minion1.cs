using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public abstract class Minion1 : ModProjectile
	{
		public override void AI()
		{
			CheckActive();
		}

        public abstract void CheckActive();

        public abstract void Behavior();

        public abstract void SelectFrame();
    }
}