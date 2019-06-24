using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Mushium
{
    [AutoloadEquip(EquipType.Body)]
	public class MushiumShirt : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushium Shirt");
            Tooltip.SetDefault("2% Increased life regeneration");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 50;
			item.rare = 1;
			item.defense = 4;
            item.value = Item.sellPrice(0, 0, 25, 0);
        }

		public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 2;
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