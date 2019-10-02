using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Demon
{
    [AutoloadEquip(EquipType.Body)]
    public class DemonGarb : BaseAAItem
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
            item.value = 9000;
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
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddIngredient(ItemID.ShadowScale, 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpGarb", 1);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddIngredient(ItemID.TissueSample, 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}