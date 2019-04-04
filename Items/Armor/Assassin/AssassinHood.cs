using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Assassin
{
	[AutoloadEquip(EquipType.Head)]
	public class AssassinHood : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Midnight Assassin Hood");
			Tooltip.SetDefault(@"13% increased melee/ranged damage and critical strike chance
A dark hood infused with the shadow of midnight");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
            item.value = 300000;
            item.rare = 11;
			item.defense = 25;
		}



        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 13;
            player.rangedCrit += 13;
            player.meleeDamage += .13f;
            player.rangedDamage += .13f;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("AssassinShirt") && legs.type == mod.ItemType("AssassinBoots");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"Slash and Stalk.
18% increased Melee and Ranged damage
25% decreased ammo consumption
Double tap down to go into stealth mode
Movement is not impeded while in stealth mode
Melee and Ranged damage increased while in stealth";
            player.rangedDamage += .2f;
            player.meleeDamage += .2f;
            player.GetModPlayer<AAPlayer>(mod).Assassin = true;
        }
	}
}