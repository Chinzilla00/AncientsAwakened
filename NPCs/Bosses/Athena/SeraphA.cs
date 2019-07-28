using AAMod.NPCs.Enemies.Sky;

namespace AAMod.NPCs.Bosses.Athena
{
	public class SeraphA : Seraph
	{
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}