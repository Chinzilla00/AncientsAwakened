using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Biomite
{
    [AutoloadEquip(EquipType.Head)]
	public class BiomiteHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biomite Helmet");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 7500;
			item.rare = 2;
			item.defense = 5;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("BiomePlate") && legs.type == mod.ItemType("BiomeBoots");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.KindledKabutoBonus");
            player.endurance += .02f;
            player.GetModPlayer<AAPlayer>().kindledSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PurityShard", 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}