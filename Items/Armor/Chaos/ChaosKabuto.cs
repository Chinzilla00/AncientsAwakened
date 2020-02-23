using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Kabuto");
			Tooltip.SetDefault(@"25% increased melee damage");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 100000;
            item.rare = 7;
            item.defense = 26;
		}

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.25f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ChaosDou") && legs.type == mod.ItemType("ChaosGreaves");
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChaosKabutoBonus");
            player.meleeSpeed += .1f;
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>().kindledSet = true;
            player.GetModPlayer<AAPlayer>().ChaosMe = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BlazingKabuto"));
            recipe.AddIngredient(mod.ItemType("ChaosCrystal"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RaiderHelm"));
            recipe.AddIngredient(mod.ItemType("ChaosCrystal"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}