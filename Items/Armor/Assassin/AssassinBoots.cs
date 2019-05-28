using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Assassin
{
    [AutoloadEquip(EquipType.Legs)]
	public class AssassinBoots : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Midnight Assassin's Boots");
			Tooltip.SetDefault(@"15% increased ranged/melee damage
15% increased movement speed
14% increased melee speed
Dark boots infused with the shadow of midnight");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 300000;
			item.defense = 20;
            item.rare = 13;
		}

		public override void UpdateEquip(Player player)
		{
            player.meleeDamage += .15f;
            player.rangedDamage += .15f;
            player.moveSpeed += .15f;
            player.meleeSpeed += .14f;
		}
    }
}