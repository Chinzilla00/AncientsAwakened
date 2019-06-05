using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Usable
{
    public class MireLifeFruit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life Fruit");
			Tooltip.SetDefault("Permanently increases maximum life by 5");
		}

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LifeFruit);
        }
    }
}
