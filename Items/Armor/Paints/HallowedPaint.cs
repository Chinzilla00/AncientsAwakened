using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
	public class HallowedPaint : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallowed Face Paint");
			Tooltip.SetDefault(@"32% increased minion damage
+100 mana");

		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 50000;
			item.rare = 5;
			item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage *= 1.32f;
            player.statManaMax2 += 100;
		}

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.HallowedPlateMail && legs.type == ItemID.HallowedGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = @"+6 Minion slots";
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
            player.maxMinions += 6;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}