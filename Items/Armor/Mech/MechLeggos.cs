using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Mech
{
    [AutoloadEquip(EquipType.Legs)]
	public class MechLeggos : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Greaves");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = Item.sellPrice(0, 3, 0, 0);
			item.rare = 5;
			item.defense = 14;
		}
	}
}