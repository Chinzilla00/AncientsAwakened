using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Witch
{
	[AutoloadEquip(EquipType.Body)]
	class WitchRobe : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Witch's Robe");
            Tooltip.SetDefault(@"10% increased magic/minion damage 
10% increased critical strike chance
+2 Max Minions
+30 Max Life
A robe enchanted with the firey spirit of a supreme dragon acolyte");
        }

        public override void SetDefaults()
		{
			item.width = 14;
			item.height = 14;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            item.value = 300000;
            item.defense = 26;
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
            player.magicCrit += 10;
            player.magicDamage += .1f;
            player.minionDamage += .1f;
            player.maxMinions += 2;
            player.statLifeMax2 += 30;

        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
