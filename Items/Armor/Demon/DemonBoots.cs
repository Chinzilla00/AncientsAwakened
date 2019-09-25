using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Demon
{
    [AutoloadEquip(EquipType.Legs)]
	public class DemonBoots : BaseAAItem
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
                recipe.AddIngredient(null, "ImpBoots", 1);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.ShadowScale, 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "ImpBoots", 1);
                recipe.AddIngredient(ItemID.Bone, 6);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.TissueSample, 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}