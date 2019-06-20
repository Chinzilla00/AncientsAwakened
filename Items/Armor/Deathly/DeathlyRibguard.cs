using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Deathly
{
    [AutoloadEquip(EquipType.Body)]
    public class DeathlyRibguard : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Ribguard");
            Tooltip.SetDefault("9% Increased ranged damage");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.value = 90000;
            item.rare = 4;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.09f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.NecroBreastplate, 1);
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.CrimsonScalemail, 1);
                recipe.AddIngredient(null, "ImpGarb", 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.NecroBreastplate, 1);
                recipe.AddIngredient(ItemID.JungleShirt, 1);
                recipe.AddIngredient(ItemID.ShadowScalemail, 1);
                recipe.AddIngredient(null, "ImpGarb", 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}