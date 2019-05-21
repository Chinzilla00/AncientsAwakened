using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class ReforgeHammerM : ModItem
    {
        public int Durability = 100;

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Martian Improver");
            Tooltip.SetDefault(@"Reforges Items
Only gives the highest-level modifiers");
        }
    }
}
