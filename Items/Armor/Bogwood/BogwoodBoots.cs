using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Bogwood
{
    [AutoloadEquip(EquipType.Legs)]
	public class BogwoodBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Boots");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.value = 100;
            item.rare = 0;
            item.defense = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bogwood", 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}