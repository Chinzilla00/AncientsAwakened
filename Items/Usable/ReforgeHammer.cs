using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class ReforgeHammer : ModItem
	{
        public int Durability = 100;

		public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
        }

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(@"Reforges Items");
		}
    }
}
