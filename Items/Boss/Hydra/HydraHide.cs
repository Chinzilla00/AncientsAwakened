using Terraria.ModLoader;

namespace AAMod.Items.Boss.Hydra
{
    public class HydraHide : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 22;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 1;
			
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Hide");
            Tooltip.SetDefault("The skin of a formidable foe");
        }
    }
}
