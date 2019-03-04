using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Mech
{
    [AutoloadEquip(EquipType.Head)]
	public class MechHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Helmet");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 5;
			item.defense = 12;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MechBody") && legs.type == mod.ItemType("MechLeggos");
		}
		public override void UpdateArmorSet(Player player)
        {
            player.endurance += 0.10f;
            player.setBonus = "+10% Damage Resistance";
		}
	}
}