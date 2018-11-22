using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Kindled
{
    [AutoloadEquip(EquipType.Head)]
	public class KindledKabuto : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kindled Kabuto");
			Tooltip.SetDefault(@"The headgear glows with the intensity of a burning flame
Forged in the flames of the blazing sun");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 10000;
			item.rare = 2;
			item.defense = 7;
		}

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(BuffID.Shine, 2);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("KindledDou") && legs.type == mod.ItemType("KindledSuneate");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"5% increased damage resistance
you cannot be knocked back
Your Swung weapons set your enemies ablaze
Enemies are more likely to target you";
            player.endurance *= 1.05f;
            player.noKnockback = true;
            player.aggro += 2;
            player.GetModPlayer<AAPlayer>(mod).kindledSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar", 15);
            recipe.AddIngredient(null, "BroodScale", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}