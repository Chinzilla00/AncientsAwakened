using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Mech
{
    [AutoloadEquip(EquipType.Body)]
	public class MechBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Mechanical Breastplate");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(0, 4, 0, 0);
			item.rare = 5;
			item.defense = 20;
		}
	}
}