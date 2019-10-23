using Microsoft.Xna.Framework;

namespace AAMod.NPCs.Bosses.Anubis
{
    public class UraeusBody : Uraeus
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Uraeus");
        }

		public override void SetDefaults()
		{
            base.SetDefaults();
            npc.dontCountMe = true;
		}

		public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}	
    }
}