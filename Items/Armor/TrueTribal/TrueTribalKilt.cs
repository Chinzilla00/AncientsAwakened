using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueTribal
{
    [AutoloadEquip(EquipType.Legs)]
    public class TrueTribalKilt : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Druid Kilt");
            Tooltip.SetDefault("12% Increased magic damage \n" + "Increases Maximum Mana by 60 \n" + "15% Increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 18;
            item.value = 400000;
            item.rare = 8;
            item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 60;
            player.magicDamage *= 1.12f;
            player.moveSpeed *= 1.15f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "TribalKilt", 1);
                recipe.AddIngredient(null, "PlanteraPetal", 10);
                recipe.AddIngredient(ItemID.JungleSpores, 14);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}