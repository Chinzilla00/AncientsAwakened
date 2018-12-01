using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
	public class GroxNote : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Note");
            Tooltip.SetDefault(@"'Want my dev set? Go play GRealm!'
-Grox The Great");
		}

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 22;
            item.maxStack = 999;
            item.value = 0;
            item.rare = 0;
        }
	}
}
