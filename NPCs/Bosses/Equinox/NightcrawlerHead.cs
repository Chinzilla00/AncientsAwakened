using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Equinox
{
    [AutoloadBossHead]		
	public class NightcrawlerHead : DaybringerHead
	{
		public override bool CloneNewInstances => (ModSupport.GetMod("AAModEXAI") != null ? true : false);

        public override ModNPC Clone()
		{
            if(ModSupport.GetMod("AAModEXAI") != null)
            {
                return ModSupport.GetModNPC("AAModEXAI", "NightcrawlerHead");
            }
			return (ModNPC)MemberwiseClone();
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightcrawler");
            Main.npcFrameCount[npc.type] = 1;			
		}		
		
		public override void SetDefaults()
		{
            base.SetDefaults();
			nightcrawler = true;
			bossBag = mod.ItemType("EquinoxBag");
		}
    }
}