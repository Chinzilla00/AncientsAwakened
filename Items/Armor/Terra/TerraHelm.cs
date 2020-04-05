using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Terra
{
	[AutoloadEquip(EquipType.Head)]
	public class TerraHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Helm");
			Tooltip.SetDefault(@"22% increased melee damage
9% increased melee speed");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.value = 90000;
			item.rare = 7;
			item.defense = 30;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.meleeDamage += .22f;
			player.meleeSpeed += 0.09f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("TerraPlate") && legs.type == mod.ItemType("TerraGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = @"Increased Life Regen
28% increased melee speed
Being struck by an enemy causes a terra sphere to home in on the enemy that attacked you";

			player.moveSpeed += 0.28f;
			player.GetModPlayer<AAPlayer>().TerraMe = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe;
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "NightsHelm", 1);
			recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FleshrendHelm", 1);
			recipe.AddIngredient(null, "TerraCrystal", 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}