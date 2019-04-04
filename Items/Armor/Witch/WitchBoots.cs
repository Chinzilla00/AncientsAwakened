using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Witch
{
    [AutoloadEquip(EquipType.Legs)]
	public class WitchBoots : ModItem
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
            item.rare = 11;
		}

		public override void UpdateEquip(Player player)
		{
            player.magicDamage += .12f;
            player.minionDamage += .12f;
            player.moveSpeed += .1f;
            player.maxMinions += 2;
		}
        
    }
}