using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class FogWaterSplash : ModDust
	{
		public override void SetDefaults()
		{
			updateType = 33;
		}

		public override void OnSpawn(Dust dust)
		{
			dust.alpha = 255;
			dust.velocity *= 0.5f;
			dust.velocity.Y += 1f;
		}
	}
}