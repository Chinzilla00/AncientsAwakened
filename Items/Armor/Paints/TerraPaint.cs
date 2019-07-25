using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
	public class TerraPaint : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Face Paint");
			Tooltip.SetDefault(@"42% increased minion damage
+120 mana");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 60000;
            item.rare = 8;
            item.defense = 6;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage *= 1.42f;
            player.statManaMax2 += 120;
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
            player.setBonus = Lang.ArmorBonus("TerraPaintBonus");
            player.maxMinions += 6;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HallowedPaint", 1);
            recipe.AddIngredient(null, "ChlorophytePaint", 1);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}