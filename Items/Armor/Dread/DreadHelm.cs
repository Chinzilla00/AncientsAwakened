using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dread
{
    [AutoloadEquip(EquipType.Head)]
	public class DreadHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dread Moon Fukumen");
			Tooltip.SetDefault(@"24% increased ranged critical chance
20% increased movement speed
The abyssal wrath of the Mire rests in this armor");

		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.value = 3000000;
			item.defense = 34;
            item.rare = 9;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 24;
            player.moveSpeed *= 1.2f;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DreadPlate") && legs.type == mod.ItemType("DreadBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Lang.ArmorBonus("DreadHelmBonus");

            player.buffImmune[24] = true;
            player.buffImmune[39] = true;
            player.buffImmune[44] = true;
            player.buffImmune[67] = true;
            player.AddBuff(BuffID.Shine, 2);
            player.GetModPlayer<AAPlayer>(mod).dreadSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 15);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DepthFukumen", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}