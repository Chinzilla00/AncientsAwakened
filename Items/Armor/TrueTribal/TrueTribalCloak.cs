using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueTribal
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueTribalCloak : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Tribal Cloak");
            Tooltip.SetDefault(@"14% Increased magic damage
Increases Maximum Mana by 60");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.value = 500000;
            item.rare = 8;
            item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 60;
            player.magicDamage *= 1.14f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "TribalCloak", 1);
                recipe.AddIngredient(null, "PlanteraPetal", 8);
                recipe.AddIngredient(ItemID.JungleSpores, 16);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}