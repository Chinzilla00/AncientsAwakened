using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Head)]
	public class BlazingKabuto : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Kabuto");
			Tooltip.SetDefault(@"Increases melee speed/critical strike chance by 7%
The headgear glows with the intensity of a burning flame
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
			player.meleeSpeed += 0.07f;
			player.meleeCrit += 7;
            player.AddBuff(BuffID.Shine, 2);
			player.AddBuff(BuffID.Hunter, 2);
			player.AddBuff(BuffID.Dangersense, 2);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("BlazingDou") && legs.type == mod.ItemType("BlazingSuneate");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"10% increased damage resistance
you cannot be knocked back
Your Swung weapons set your enemies ablaze
Enemies are more likely to target you";
            player.endurance *= 1.1f;
            player.noKnockback = true;
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>(mod).kindledSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledKabuto"));
            recipe.AddIngredient(mod.ItemType("OceanHelm"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}