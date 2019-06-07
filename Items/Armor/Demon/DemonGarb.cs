using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Demon
{
    [AutoloadEquip(EquipType.Body)]
    public class DemonGarb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Garb");
            Tooltip.SetDefault("9% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 22;
            item.value = 90000;
            item.rare = 4;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.09f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpGarb", 1);
                recipe.AddIngredient(ItemID.NecroBreastplate, 1);
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.CrimsonScalemail, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpGarb", 1);
                recipe.AddIngredient(ItemID.NecroBreastplate, 1);
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.ShadowScalemail, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}