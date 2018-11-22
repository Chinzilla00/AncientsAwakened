using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Imp
{
    [AutoloadEquip(EquipType.Legs)]
	public class ImpBoots : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Imp Boots");
            Tooltip.SetDefault("7% Increased Minion damage");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 7000;
            item.rare = 2;
            item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.07f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DevilSilk", 5);
                recipe.AddTile(null, "HellstoneAnvil");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}