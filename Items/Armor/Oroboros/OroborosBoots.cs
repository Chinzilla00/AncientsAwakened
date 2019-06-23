using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Oroboros
{
    [AutoloadEquip(EquipType.Legs)]
	public class OroborosBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oroboros Wood Boots");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.value = 100;
            item.rare = 3;
            item.defense = 4;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "OroborosWood", 25);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}