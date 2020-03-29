using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dread
{
    [AutoloadEquip(EquipType.Legs)]
	public class DreadBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dread Moon Hakama");
			Tooltip.SetDefault(@"50% increased movement speed
25% decreased ammo consumption
The abyssal wrath of the Mire rests in this armor");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 3000000;
			item.defense = 34;
            item.rare = 9;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.moveSpeed += .5f;
            player.ammoCost75 = true;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .5f;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EventideAbyssium", 18);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DepthHakama", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}