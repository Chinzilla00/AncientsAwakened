using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
	public class WingsofChaos : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Wings of Chaos");
            Tooltip.SetDefault("The Wings of an ancient Dragon god, sealed away by the hero of legend");
		}

		public override void SetDefaults()
		{
			item.width = 56;
			item.height = 28;
			item.value = 50000000;
			item.rare = 9;
			item.accessory = true;
            item.expert = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 700;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 16f;
			acceleration *= 4.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SoulOfSpite", 6);
            recipe.AddIngredient(null, "SoulOfSmite", 6);
            recipe.AddIngredient(null, "Discordium", 15);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}