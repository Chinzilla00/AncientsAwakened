using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Mushium
{
    [AutoloadEquip(EquipType.Head)]
	public class MushiumHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom Hat");
			Tooltip.SetDefault("10% increased throwing velocity");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 90;
			item.rare = 1;
			item.defense = 3;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.thrownVelocity *= 1.10f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MushiumShirt") && legs.type == mod.ItemType("MushiumPants");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Potion Sickness time is cut by 25%";
            
            player.pStone = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}