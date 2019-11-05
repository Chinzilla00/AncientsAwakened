using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Assassin
{
    [AutoloadEquip(EquipType.Legs)]
	public class AssassinBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Midnight Assassin's Boots");
			Tooltip.SetDefault(@"15% increased ranged/melee damage
15% increased movement speed
8% increased melee speed
Dark boots infused with the shadow of midnight");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 300000;
			item.defense = 20;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
            player.meleeDamage += .15f;
            player.rangedDamage += .15f;
            player.moveSpeed += .15f;
            player.meleeSpeed += .08f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.15f;
		}
    }
}