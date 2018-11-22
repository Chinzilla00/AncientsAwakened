using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Mech
{
	[AutoloadEquip(EquipType.Head)]
	public class MechHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mechanical Helmet");
			Tooltip.SetDefault("15% increased thrown damage");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(0, 5, 0, 0);
			item.rare = 5;
			item.defense = 8;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MechBody") && legs.type == mod.ItemType("MechLeggos");
		}
		
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.15f; //15% throwing damage
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 0.15f; //15% movement speed
			player.setBonus = "+15% movement speed";
		}
	}
}