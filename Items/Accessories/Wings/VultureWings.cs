using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class VultureWings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Vulture Wings");
            Tooltip.SetDefault("Allows slow fall");
        }

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 30;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Blue;
			item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 30;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 4f;
            acceleration *= 1f;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "vulture_feather", 15);
            recipe.AddIngredient(null, "DesertMana", 5);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}