using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Mech
{
	[AutoloadEquip(EquipType.Legs)]
	public class MechLeggos : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Greaves");
			Tooltip.SetDefault("7% increased thrown damage");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 5;
			item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.07f;
		}
	}
}