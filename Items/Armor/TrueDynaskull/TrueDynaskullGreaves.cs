using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDynaskull
{
    [AutoloadEquip(EquipType.Legs)]
	public class TrueDynaskullGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primeval Dynaskull Greaves");
            Tooltip.SetDefault(@"20% Increased ranged critical chance
+12% movement speed");

        }

		public override void SetDefaults()
		{
            item.width = 30;
			item.height = 28;
			item.value = 100000;
			item.rare = 7;
			item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 20;
            player.moveSpeed *= 1.12f;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DynaskullGreaves", 1);
            recipe.AddIngredient(null, "DesertCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}