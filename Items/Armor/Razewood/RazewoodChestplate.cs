using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Razewood
{
    [AutoloadEquip(EquipType.Body)]
    public class RazewoodChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Chestplate");
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
            recipe.AddIngredient(null, "Razewood", 30);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}