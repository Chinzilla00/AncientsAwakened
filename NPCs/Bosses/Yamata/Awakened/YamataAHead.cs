using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Infinity.Awakened
{
    [AutoloadBossHead]
    public class YamataAHead : YamataHead
    {
        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            DisplayName.SetDefault("Yamata Awakened");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
			isAwakened = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
    }
}
