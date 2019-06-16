namespace AAMod.NPCs.Bosses.Shen
{
    public class ChaosBlastRed : ChaosBlast
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Wrath");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            swordType = 1;
            offsetLeft = false;
        }
    }
}