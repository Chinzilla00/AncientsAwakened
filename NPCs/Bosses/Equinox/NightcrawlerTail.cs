using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.NPCs.Bosses.Equinox
{
    //[AutoloadBossHead]
    public class NightcrawlerTail : NightcrawlerHead
	{	
		public override void SetDefaults()
		{
            base.SetDefaults();
            npc.dontCountMe = true;
			nightcrawler = true;
		}

        public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>()))
            {
                return false;
            }
            return true;
        }
    }
}