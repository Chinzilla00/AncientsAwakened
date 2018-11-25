namespace AAMod.NPCs.Bosses.Yamata.Awakened
{
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
        }
    }
}
