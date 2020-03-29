using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Razewood
{
    [AutoloadEquip(EquipType.Legs)]
	public class RazewoodBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Boots");
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
            recipe.AddIngredient(null, "Razewood", 25);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}