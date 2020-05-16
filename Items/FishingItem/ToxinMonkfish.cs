using Terraria.ID;

namespace AAMod.Items.FishingItem
{
    public class ToxinMonkfish : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxin Monkfish");
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
            item.rare = ItemRarityID.LightRed;
            AARarity = 6;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 128000;
            item.createTile = mod.TileType("ToxinMonkfishTile");
        }
    }
}
