using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Witch
{
    [AutoloadEquip(EquipType.Legs)]
	public class WitchBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fury Witch's Boots");
			Tooltip.SetDefault(@"12% increased magic/minion damage
12% increased movement speed
+2 max minions
Boots enchanted with the firey spirit of a supreme dragon acolyte");
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
            player.magicDamage += .12f;
            player.minionDamage += .12f;
            player.moveSpeed += .1f;
            player.maxMinions += 2;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .12f;
		}
        
    }
}