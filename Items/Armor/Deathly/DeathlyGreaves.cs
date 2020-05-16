using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Deathly
{
    [AutoloadEquip(EquipType.Legs)]
	public class DeathlyGreaves : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Greaves");
            Tooltip.SetDefault("9% Increased ranged damage");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 90000;
            item.rare = ItemRarityID.LightRed;
            item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.09f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.ShadowScale, 6);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 6);
                recipe.AddIngredient(ItemID.TissueSample, 6);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}