using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Nights
{
    [AutoloadEquip(EquipType.Head)]
	public class NightsHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night's Helm");
			Tooltip.SetDefault("9% increased melee speed");

		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.value = 90000;
			item.rare = 4;
			item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.meleeSpeed += 0.09f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("NightsPlate") && legs.type == mod.ItemType("NightsGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Lang.ArmorBonus("NightsHelmBonus");
            player.moveSpeed += 0.22f;
            player.panic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ShadowHelmet, 1);
			recipe.AddIngredient(ItemID.JungleHat, 1);
            recipe.AddIngredient(ItemID.NecroHelmet, 1);
            recipe.AddIngredient(null, "ImpHood", 1);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}