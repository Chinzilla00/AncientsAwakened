using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Raider
{
    [AutoloadEquip(EquipType.Body)]
	public class RaiderChest : BaseAAItem
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raider Chestplate");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = 4;
			item.defense = 14;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == mod.ItemType("RaiderHelm") && legs.type == mod.ItemType("RaiderLegs");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Lang.ArmorBonus("RaiderChestBonus");
            player.noKnockback = true;
            player.endurance += (1 - (player.statLife / player.statLifeMax)) * .1f;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("VikingPlate"));
            recipe.AddIngredient(mod.ItemType("DepthGi"));
            recipe.AddIngredient(mod.ItemType("OceanShirt"));
            recipe.AddIngredient(mod.ItemType("DoomiteUPlate"));
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}