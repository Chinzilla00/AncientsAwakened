using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.AncientGold
{
	[AutoloadEquip(EquipType.Body)]
	public class AncientGoldBody : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Gold Chainmail");
            Tooltip.SetDefault(@"You have chance to get gold coins in stoneblocks");
        }

        public override void SetDefaults()
		{
			item.width = 18;
            item.height = 18;
            item.defense = 4;
            item.value = 10000;
            item.expert = true;
            item.expertOnly = true;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().AncientGoldBody = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == ItemID.AncientGoldHelmet && legs.type == mod.ItemType("AncientGoldLeg");
        }

        public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.AncientGoldSetBonus");
            player.GetModPlayer<AAPlayer>().AncientGoldSet = true;
        }
	}
}
