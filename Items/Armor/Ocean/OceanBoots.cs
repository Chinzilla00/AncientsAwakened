using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Ocean
{
    [AutoloadEquip(EquipType.Legs)]
	public class OceanBoots : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ocean Greaves");
            Tooltip.SetDefault(@"Increases maximum mana by 20
2% increased magic damage");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = 3;
			item.defense = 5;
		}

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.magicDamage += 0.02f;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 4);
			recipe.AddIngredient(ItemID.Starfish);
			recipe.AddIngredient(ItemID.Seashell);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}