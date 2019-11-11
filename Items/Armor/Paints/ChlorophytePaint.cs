using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
	public class ChlorophytePaint : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chlorophyte Face Paint");
			Tooltip.SetDefault(@"38% increased minion damage
+80 mana");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 60000;
			item.rare = 7;
			item.defense = 5;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage *= 1.38f;
            player.statManaMax2 += 80;
		}


        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChlorophytePaintBonus");
            player.AddBuff(BuffID.LeafCrystal, 2);
            player.maxMinions += 6;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}