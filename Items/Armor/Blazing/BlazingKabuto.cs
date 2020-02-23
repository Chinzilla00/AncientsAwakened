using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Blazing
{
    [AutoloadEquip(EquipType.Head)]
	public class BlazingKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Kabuto");
			Tooltip.SetDefault(@"1% increased Damage Resistance
3% increased Melee Damage
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
            player.endurance += .01f;
            player.meleeDamage += 0.03f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == mod.ItemType("BlazingDou") && legs.type == mod.ItemType("BlazingSuneate");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.BlazingBonus");
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>().kindledSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledKabuto"));
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ItemID.FossilOre, 5);
            recipe.AddIngredient(mod.ItemType("Doomite"), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}