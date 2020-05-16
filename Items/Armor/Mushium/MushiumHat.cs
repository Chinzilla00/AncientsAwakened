using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Mushium
{
    [AutoloadEquip(EquipType.Head)]
	public class MushiumHat : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushium Hat");
			Tooltip.SetDefault("1% Increased life regeneration");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 90;
			item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(0, 0, 25, 0);
            item.defense = 3;
		}
		
		public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 1;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("MushiumShirt") && legs.type == mod.ItemType("MushiumPants");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.MushiumHatBonus");
            player.pStone = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}