using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.GoblinSlayer
{
    [AutoloadEquip(EquipType.Body)]
	public class GoblinSlayerChest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Goblin Slayer's Chestplate");
            Tooltip.SetDefault(@"An immense hatred of Goblinkind haunts this chestplate");
        }

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 20;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 7;
        }
	}
}