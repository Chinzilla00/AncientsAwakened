using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class AsgardianWings : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Asgardian Wings");
            Tooltip.SetDefault("Allows flight and slow fall");
        }

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 30;
			item.value = Item.sellPrice(0, 1, 50, 0);
			item.rare = 1;
			item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 170;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.1f;
            ascentWhenRising = 0.5f;
            maxCanAscendMultiplier = 1.5f;
            maxAscentMultiplier = .5f;
            constantAscend = 0.1f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 7f;
            acceleration *= 1.33f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrozenWings, 1);
            recipe.AddIngredient(null, "RelicBar", 10);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(null, "SnowCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}