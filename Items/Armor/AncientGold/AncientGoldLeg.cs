using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.AncientGold
{
    [AutoloadEquip(EquipType.Legs)]
	public class AncientGoldLeg : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Gold Greaves");
			Tooltip.SetDefault(@"You have more chance to meet with vanilla gold critters.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
            item.height = 18;
            item.defense = 4;
            item.value = 15000;
			item.expert = true;
			item.expertOnly = true;
        }

        public override void UpdateEquip(Player player)
		{
            player.GetModPlayer<AAPlayer>().AncientGoldLeg = true;
		}
    }
}