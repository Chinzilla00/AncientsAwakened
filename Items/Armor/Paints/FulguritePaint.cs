using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguritePaint : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Face Paint");
			Tooltip.SetDefault(@"35% increased minion damage
+120 mana");

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
            player.minionDamage *= 1.35f;
            player.statManaMax2 += 120;
		}
        
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("FulguriteBreastplate") && legs.type == mod.ItemType("FulguritePants");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
+5 Minion slots";

            player.GetModPlayer<AAPlayer>(mod).fulgurite = true;
            player.maxMinions += 5;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(null, "Mortar_Tile");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}