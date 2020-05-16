using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Dynaskull
{
    [AutoloadEquip(EquipType.Head)]
	public class Dynaskull : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dynaskull");
			Tooltip.SetDefault("20% decreased ammo consumption");

		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 90000;
			item.rare = ItemRarityID.LightRed;
			item.defense = 7;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.ammoCost80 = true ;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DynaskullRibguard") && legs.type == mod.ItemType("DynaskullGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DynaskullBonus");
            
			player.GetModPlayer<AAPlayer>().DynaskullSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilHelm, 1);
            recipe.AddIngredient(null, "DynaskullOre", 15);
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(null, "BroodScale", 5);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}