using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class EnderMemory : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Memory");
            Tooltip.SetDefault(@"An immense statue made to commemorate somebody
A somber engraving is etched into the base.");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 38;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 9;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 0;
            item.createTile = mod.TileType("EnderMemory");
        }
    }
}
