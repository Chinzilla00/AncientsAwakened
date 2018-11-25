using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.DyingStar
{
    [AutoloadEquip(EquipType.Legs)]
	public class NovaGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nova Greaves");
			Tooltip.SetDefault("7% increased critical strike chance"
				+ "\n+12% thrown velocity");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 10;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 7;
			player.thrownVelocity += 0.12f;
		}
	}
}