using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Demon
{
    [AutoloadEquip(EquipType.Legs)]
	public class DemonBoots : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon Hoofs");
            Tooltip.SetDefault("9% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
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
                recipe.AddIngredient(null, "ImpBoots", 1);
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(ItemID.JunglePants, 1);
                recipe.AddIngredient(ItemID.CrimsonGreaves, 1); ;
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpBoots", 1);
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(ItemID.JunglePants, 1);
                recipe.AddIngredient(ItemID.ShadowGreaves, 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}