using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dynaskull
{
    [AutoloadEquip(EquipType.Head)]
	public class Dynaskull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dynaskull");
			Tooltip.SetDefault("50% increased throwing velocity");

		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 90000;
			item.rare = 4;
			item.defense = 7;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.thrownVelocity *= 1.50f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DynaskullRibguard") && legs.type == mod.ItemType("DynaskullGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Your thrown projectiles have so much power behind them, they confuse the target due to concussive force
50% chance to not consume thrown weapons";

            player.thrownCost50 = true;
			player.GetModPlayer<AAPlayer>(mod).DynaskullSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilHelm, 1);
            recipe.AddIngredient(null, "DynaskullOre", 30);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}