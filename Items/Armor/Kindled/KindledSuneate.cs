using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Kindled
{
    [AutoloadEquip(EquipType.Legs)]
	public class KindledSuneate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kindled Suneate");
            Tooltip.SetDefault("Forged in the flames of the blazing sun");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 5000;
			item.rare = ItemRarityID.Green;
			item.defense = 7;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar", 20);
            recipe.AddIngredient(null, "BroodScale", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}