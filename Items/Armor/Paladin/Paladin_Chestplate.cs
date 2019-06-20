using Terraria.ModLoader;

namespace AAMod.Items.Armor.Paladin
{
    [AutoloadEquip(EquipType.Body)]
	public class Paladin_Chestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Paladin Chestplate");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.value = 10000;
			item.rare = 8;
			item.vanity = true;
		}
	}
}