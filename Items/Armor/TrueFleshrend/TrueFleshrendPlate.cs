using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueFleshrend
{
    [AutoloadEquip(EquipType.Body)]
	public class TrueFleshrendPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Fleshrend Plate");
			Tooltip.SetDefault("13% Increased melee damage");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.value = 100000;
			item.rare = 7;
			item.defense = 19;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.13f;
		}



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FleshrendPlate", 1);
            recipe.AddIngredient(null, "CrimsonCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}