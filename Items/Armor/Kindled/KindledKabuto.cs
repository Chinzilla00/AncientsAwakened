using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Kindled
{
    [AutoloadEquip(EquipType.Head)]
	public class KindledKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kindled Kabuto");
			Tooltip.SetDefault(@"Forged in the flames of the blazing sun");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 10000;
			item.rare = 2;
			item.defense = 7;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("KindledDou") && legs.type == mod.ItemType("KindledSuneate");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.KindledKabutoBonus");
            player.endurance += .05f;
            player.GetModPlayer<AAPlayer>().kindledSet = true;
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