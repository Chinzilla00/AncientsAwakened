using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Witch
{
	[AutoloadEquip(EquipType.Head)]
	public class WitchHood : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Fury Witch's Cowl");
			Tooltip.SetDefault(@"+120 Max Mana
Reduced mana consumption by 20%
+2 Max Minions
10% increased magic/minion damage 
10% increased magic critical strike chance
A hood enchanted with the firey spirit of a supreme dragon acolyte");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
            item.value = 300000;
            item.rare = 11;
			item.defense = 24;
		}



        public override void UpdateEquip(Player player)
        {
            player.manaCost *= .8f;
            player.magicCrit += 10;
            player.magicDamage += .1f;
            player.minionDamage += .1f;
            player.maxMinions += 2;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("WitchRobe") && legs.type == mod.ItemType("WitchBoots");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"Scorch and Burn.
20% Increased Magic and Minion damage
+4 max minions
A Fire spirit protects you
The Fire spirit becomes more powerful the less mana you have";
            player.magicDamage += .2f;
            player.minionDamage += .2f;
            player.maxMinions += 4;
        }
        
	}
}