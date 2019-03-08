
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
    [AutoloadBossHead]
    public class YamataAHeadF2 : YamataAHeadF1
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			leftHead = true;
        }
	}
}
