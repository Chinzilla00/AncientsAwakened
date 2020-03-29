using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Fleshrend
{
    [AutoloadEquip(EquipType.Head)]
	public class FleshrendHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fleshrend Helm");
			Tooltip.SetDefault("7% increased melee damage");

		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 26;
			item.value = 90000;
			item.rare = 4;
			item.defense = 7;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.meleeDamage += .07f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("FleshrendPlate") && legs.type == mod.ItemType("FleshrendGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.FleshrendHelmBonus");

            player.crimsonRegen = true;
			player.GetModPlayer<AAPlayer>().fleshrendSet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimsonHelmet, 1);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddIngredient(ItemID.Bone, 5);
            recipe.AddIngredient(null, "DevilSilk", 5);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}