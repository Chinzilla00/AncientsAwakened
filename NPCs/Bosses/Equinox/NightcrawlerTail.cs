using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.NPCs.Bosses.Equinox
{
    [AutoloadBossHead]
    public class NightcrawlerTail : NightcrawlerHead
	{	
		public override void SetDefaults()
		{
            base.SetDefaults();
            npc.dontCountMe = true;
			nightcrawler = true;
		}

        public override void PostAI()
        {
            if (!NPC.AnyNPCs(mod.NPCType<NightcrawlerHead>()))
            {
                npc.life = 0;
            }
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