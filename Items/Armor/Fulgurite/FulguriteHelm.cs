using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fulgurite
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Helm");
			Tooltip.SetDefault(@"14% increased magic damage & critical strike chance
+120 maximum Mana");

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
            player.magicDamage *= 1.14f;
            player.magicCrit += 14;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("FulguriteBreastplate") && legs.type == mod.ItemType("FulguritePants");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
-20% Mana Usage";

            player.GetModPlayer<AAPlayer>(mod).fulgurite = true;
            player.manaCost *= .8f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FulguriteBar", 12);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}