using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Witch
{
	[AutoloadEquip(EquipType.Body)]
	class WitchRobe : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Witch's Robe");
            Tooltip.SetDefault(@"10% increased magic/minion damage 
10% increased critical strike chance
+2 Max Minions
A robe enchanted with the firey spirit of a supreme dragon acolyte");
        }

        public override void SetDefaults()
		{
			item.width = 14;
			item.height = 14;
            item.rare = 2;
            item.value = 300000;
            item.defense = 26;
		}
        
        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 10;
            player.magicDamage += .1f;
            player.minionDamage += .1f;
            player.maxMinions += 2;
        }

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
