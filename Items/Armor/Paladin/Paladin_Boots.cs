using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paladin
{
    [AutoloadEquip(EquipType.Legs)]
	public class Paladin_Boots : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Paladin Greaves");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 10000;
			item.rare = 8;
			item.vanity = true;
		}
	}
}