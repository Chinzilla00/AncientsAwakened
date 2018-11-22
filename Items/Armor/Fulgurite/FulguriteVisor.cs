using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fulgurite
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteVisor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Visor");
			Tooltip.SetDefault("70% increased throwing velocity");

		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 90000;
			item.rare = 5;
			item.defense = 10;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.thrownVelocity *= 1.70f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("FulguriteBreastplate") && legs.type == mod.ItemType("FulguritePants");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Your Thrown weapons fly as fast as a bolt of lightning
50% chance to not consume thrown weapons";

            player.thrownCost50 = true;
            player.thrownVelocity *= 2;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}