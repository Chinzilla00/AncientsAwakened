using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Oroboros
{
    [AutoloadEquip(EquipType.Body)]
    public class OroborosChestplate : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oroboros Wood Chestplate");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 2000;
            item.rare = ItemRarityID.Orange;
            item.defense = 4;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "OroborosWood", 30);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}