using Terraria.ID;

namespace AAMod.Items.Boss.Equinox
{
    public class DBTrophy : BaseAAItem
	{

        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Daybringer Trophy");
            
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
			//item.rare = 1; duplicated fsr
			item.createTile = mod.TileType("DBTrophy");
            
		}
    }
}