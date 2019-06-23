using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fulgurite
{
    [AutoloadEquip(EquipType.Legs)]
	public class FulguritePants : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Pants");
            Tooltip.SetDefault(@"5% increased critical chance
16% increased movement speed");

        }

		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 18;
			item.value = 50000;
			item.rare = 5;
			item.defense = 11;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 5;
            player.meleeCrit += 5;
            player.magicCrit += 5;
            player.rangedCrit += 5;
            player.moveSpeed *= 1.16f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}