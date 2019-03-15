using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Legs)]
    public class DoomiteGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Greaves");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 3;
            item.defense = 7;
            item.value = 9000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}