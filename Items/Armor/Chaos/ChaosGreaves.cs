using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Legs)]
	public class ChaosGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Greaves");
            Tooltip.SetDefault(@"10% increased movement speed
7% increased damage");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 16;
            item.defense = 20;
            item.rare = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.allDamage += .07f;
            player.moveSpeed += .1f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:ChaosBoots");
            recipe.AddIngredient(null, "ChaosCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}