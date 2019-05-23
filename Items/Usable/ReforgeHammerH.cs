using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class ReforgeHammerH : ModItem
    {
        public int Durability = 100;

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Reforge Hammer");
            Tooltip.SetDefault(@"Reforges Items
Cannot give negative modifiers");
        }
    }
}
