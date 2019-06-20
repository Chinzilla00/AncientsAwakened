using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDemon
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueDemonGarb : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devil Garb");
            Tooltip.SetDefault(@"13% Increased Minion damage
Increases your max number of minions");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 26;
            item.value = 700000;
            item.rare = 8;
            item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.13f;
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DemonGarb", 1);
                recipe.AddIngredient(null, "PureEvil", 2);
                recipe.AddIngredient(null, "DevilSilk", 8);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}