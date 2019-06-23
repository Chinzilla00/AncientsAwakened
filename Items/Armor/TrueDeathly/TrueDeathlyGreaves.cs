using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDeathly
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueDeathlyGreaves : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deathly Ghast Greaves");
            Tooltip.SetDefault("12% Increased ranged damage and movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 1000000;
            item.rare = 8;
            item.defense = 18;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.12f;
            player.moveSpeed *= 1.12f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Ectoplasm, 15);
                recipe.AddIngredient(ItemID.Bone, 45);
                recipe.AddIngredient(null, "DeathlyGreaves", 1);
                recipe.AddTile(null, "TruePaladinsSmeltery");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}