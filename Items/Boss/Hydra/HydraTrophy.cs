using Terraria.ID;

namespace AAMod.Items.Boss.Hydra
{
    public class HydraTrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Trophy");
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
            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 2000;
            item.rare = 2;
            item.expert = true; item.expertOnly = true;
			item.createTile = mod.TileType("HydraTrophy");
		}
	}
}