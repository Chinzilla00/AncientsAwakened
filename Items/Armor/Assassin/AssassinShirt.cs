using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Assassin
{
	[AutoloadEquip(EquipType.Body)]
	class AssassinShirt : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Assassin Shirt");
            Tooltip.SetDefault(@"14% increased melee/ranged damage and critical strike chance
20% decreased ammo consumption
A dark armor infused with the shadow of midnight");
        }

        public override void SetDefaults()
		{
			item.width = 14;
			item.height = 14;
            item.rare = 9;
            AARarity = 12;
            item.value = 300000;
            item.defense = 29;
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
            player.meleeCrit += 14;
            player.rangedCrit += 14;
            player.meleeDamage += .14f;
            player.rangedDamage += .14f;
            player.ammoCost80 = true;
        }

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
