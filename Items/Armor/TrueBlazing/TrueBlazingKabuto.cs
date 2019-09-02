using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueBlazing
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueBlazingKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Perfect Blazing Kabuto");
			Tooltip.SetDefault(@"5% increased Damage Resistance
                                15% increased melee damage");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 100000;
			item.rare = 4;
			item.defense = 18;
		}

        public override void UpdateEquip(Player player)
        {
            player.endurance += .05f;
            player.meleeDamage *= 1.15f;

        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("TrueBlazingDou") && legs.type == mod.ItemType("TrueBlazingSuneate");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"5% increased damage resistance
Enemies are more likely to target you
Enemies that strike you are set ablaze
Your Swung weapons set your enemies ablaze";
            player.endurance = .05f;
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>(mod).kindledSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BlazingKabuto"));
            recipe.AddIngredient(mod.ItemType("InfernoCrystal"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}