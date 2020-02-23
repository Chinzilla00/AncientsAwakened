using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Mushium
{
    [AutoloadEquip(EquipType.Legs)]
	public class MushiumPants : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushium Pants");
            Tooltip.SetDefault("1% Increased life regeneration");

        }

		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 18;
			item.value = 50;
			item.rare = 1;
			item.defense = 3;
            item.value = Item.sellPrice(0, 0, 25, 0);
        }

		public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 1;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}