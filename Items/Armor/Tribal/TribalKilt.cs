using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Tribal
{
    [AutoloadEquip(EquipType.Legs)]
	public class TribalKilt : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tribal Kilt");
            Tooltip.SetDefault(@"8% Increased magic critical chance
Increases Maximum Mana by 20");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 90000;
            item.rare = 4;
            item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.magicCrit += 8;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JunglePants, 1);
                recipe.AddIngredient(ItemID.CrimsonGreaves, 1);
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(null, "ImpBoots", 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JunglePants, 1);
                recipe.AddIngredient(ItemID.ShadowGreaves, 1);
                recipe.AddIngredient(ItemID.NecroGreaves, 1);
                recipe.AddIngredient(null, "ImpBoots", 1);
                recipe.AddTile(TileID.DemonAltar);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}