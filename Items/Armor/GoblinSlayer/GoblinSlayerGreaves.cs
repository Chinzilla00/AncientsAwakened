using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.GoblinSlayer
{
    [AutoloadEquip(EquipType.Legs)]
	public class GoblinSlayerGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Slayer's Greaves");
            Tooltip.SetDefault(@"An immense hatred of Goblinkind haunts these greaves");

        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = ItemRarityID.Orange;
			item.defense = 7;
		}
        
	}
}