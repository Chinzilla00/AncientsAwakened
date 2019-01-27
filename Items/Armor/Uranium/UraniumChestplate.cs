using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Uranium
{
    [AutoloadEquip(EquipType.Body)]
    public class UraniumChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium Chestplate");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = 90000;
            item.rare = 4;
            item.defense = 16;
        }
        

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "UraniumBar", 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}