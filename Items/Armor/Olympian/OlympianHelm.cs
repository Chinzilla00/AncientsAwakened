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
            Tooltip.SetDefault(@"Decreases mana usage by 25%");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
            item.defense = 16;
        }
		
		public override void UpdateEquip(Player player)
		{
			player.manaCost -= 0.25f;
		}
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("OlympianPlate") && legs.type == mod.ItemType("OlympianBoots");
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"NYI set bonus";
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