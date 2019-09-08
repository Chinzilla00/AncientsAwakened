using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class DevilWings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Archdemon Wings");
            Tooltip.SetDefault("Allows flight and slow fall");
        }

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 30;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 8;
			item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 170;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
                   ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2.1f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 8f;
            acceleration *= 2f;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FlameWings, 1);
            recipe.AddIngredient(null, "DevilSilk", 10);
            recipe.AddIngredient(null, "PureEvil", 3);
            recipe.AddIngredient(null, "HellCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}