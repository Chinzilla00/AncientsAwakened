using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Bogwood
{
    [AutoloadEquip(EquipType.Body)]
    public class BogwoodChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Chestplate");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 2000;
            item.rare = ItemRarityID.White;
            item.defense = 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bogwood", 30);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}