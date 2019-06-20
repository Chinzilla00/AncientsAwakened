using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDemon
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueDemonBoots : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devil Hoofs");
            Tooltip.SetDefault(@"13% Increased Minion damage
Increases your max number of minions
10% Increased Movement Speed");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.value = 700000;
            item.rare = 8;
            item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.13f;
            player.moveSpeed *= 1.1f;
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DemonBoots", 1);
                recipe.AddIngredient(null, "PureEvil", 2);
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}