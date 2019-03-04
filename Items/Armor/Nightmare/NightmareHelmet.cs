using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Nightmare
{
    [AutoloadEquip(EquipType.Head)]
	public class NightmareHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Helmet");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 0, 84, 0);
			item.rare = 5;
			item.defense = 8;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("NightmareBreastplate") && legs.type == mod.ItemType("NightmareGreaves");
		}
		
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
		}
		
		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 0.1f; 
			player.setBonus = "+10% movement speed";
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Nightmare_Bar", 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}