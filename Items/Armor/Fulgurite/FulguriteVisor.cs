using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Fulgurite
{
    [AutoloadEquip(EquipType.Head)]
	public class FulguriteVisor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fulgurite Visor");
			Tooltip.SetDefault(@"17% increased ranged damage
5% increased ranged critical strike chance");

		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 50000;
			item.rare = 5;
			item.defense = 8;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.rangedDamage *= 1.17f;
            player.rangedCrit += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("FulguriteBreastplate") && legs.type == mod.ItemType("FulguritePants");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Being struck causes a burst of lightning to erupt from your body, knocking back enemies
25% chance to not consume ammo weapons";

            player.GetModPlayer<AAPlayer>(mod).fulgurite = true;
            player.ammoCost75 = true;
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