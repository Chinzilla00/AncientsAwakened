using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Olympian
{
    [AutoloadEquip(EquipType.Head)]
	public class OlympianHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Olympian Helmet");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 6;
            item.defense = 8;
        }
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("OlympianPlate") && legs.type == mod.ItemType("OlympianBoots");
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"'They will fear your precision, yet you will fear the slightest gust'
60% increased critical strike chance";

			player.meleeCrit += 60;
			player.rangedCrit += 60;
			player.magicCrit += 60;
			player.thrownCrit += 60;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GladiatorHelmet);
            recipe.AddIngredient(null, "GoddessFeather", 6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}