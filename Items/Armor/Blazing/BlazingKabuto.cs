using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Head)]
	public class BlazingKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Kabuto");
			Tooltip.SetDefault(@"2% increased Damage Resistance
Forged in the flames of the blazing sun");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 100000;
			item.rare = 4;
			item.defense = 8;
		}

        public override void UpdateEquip(Player player)
        {
            player.endurance += .02f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("BlazingDou") && legs.type == mod.ItemType("BlazingSuneate");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"5% increased damage resistance
Enemies are more likely to target you
Your Swung weapons set your enemies ablaze";
            player.endurance = .05f;
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>(mod).kindledSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledKabuto"));
            recipe.AddIngredient(mod.ItemType("OceanHelm"));
            recipe.AddIngredient(ItemID.FossilHelm);
            recipe.AddIngredient(mod.ItemType("DoomiteUHelm"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}