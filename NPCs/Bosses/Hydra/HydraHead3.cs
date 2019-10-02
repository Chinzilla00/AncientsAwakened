using Terraria.ModLoader;
namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class HydraHead3 : HydraHead1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            leftHead = true;
			middleHead = false;	
			distFromBodyX = 80;			
			distFromBodyY = 90;			
        }
    }
}