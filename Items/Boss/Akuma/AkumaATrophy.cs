using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class AkumaATrophy : ModItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened Trophy");
		}

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
            item.rare = 1;
            item.useStyle = 1;
			item.consumable = true;
			item.value = 2000;
			item.rare = 1;
            item.expert = true;
			item.createTile = mod.TileType("AkumaATrophy");
		}
	}
}