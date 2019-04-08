using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueBlazing
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueBlazingKabuto : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Blazing Kabuto");
			Tooltip.SetDefault(@"Increases melee speed/critical strike chance by 12%
The headgear glows with the intensity of a burning flame
Forged in the flames of the blazing sun");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 100000;
			item.rare = 7;
			item.defense = 18;
		}

        public override void UpdateEquip(Player player)
        {
			player.meleeSpeed += 0.12f;
			player.meleeCrit += 12;
            player.AddBuff(BuffID.Shine, 2);
			player.AddBuff(BuffID.Hunter, 2);
			player.AddBuff(BuffID.Dangersense, 2);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("TrueBlazingDou") && legs.type == mod.ItemType("TrueBlazingSuneate");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"15% increased damage resistance
you cannot be knocked back
Your Swung weapons set your enemies ablaze
Enemies are more likely to target you";
            player.endurance *= 1.15f;
            player.noKnockback = true;
            player.aggro += 5;
            player.GetModPlayer<AAPlayer>(mod).kindledSet = true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("BlazingKabuto"));
			recipe.AddIngredient(null, "InfernoCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}