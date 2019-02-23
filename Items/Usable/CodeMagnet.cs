using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class CodeMagnet : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Binary Code Magnet");
			Tooltip.SetDefault("'Pulls items to you by moving its code closer to you'");
		}


        public override void SetDefaults()
        {
            item.width = item.height = 16;
            item.rare = 6;
            item.maxStack = 1;
			item.value = 800000;
        }
    }
}
