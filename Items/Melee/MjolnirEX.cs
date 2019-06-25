using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class MjolnirEX : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stormbreaker");
			Tooltip.SetDefault("Forged with star's heat, with the living wood handle"
			+"\nMjolnir EX");
        }

		public override void SetDefaults()
		{
			item.noMelee = true;
			item.useStyle = 1;
			item.shootSpeed = 16f;
			item.damage = 240;
			item.knockBack = 9f;
			item.width = 14;
			item.height = 28;
			item.UseSound = SoundID.Item1;
			item.useAnimation = 10;
			item.useTime = 10;
			item.noUseGraphic = true;
			item.rare = 9;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.melee = true;
			item.shoot = mod.ProjectileType("MjolnirEX");
			item.autoReuse = true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.ownedProjectileCounts[item.shoot] < 1)
			{
				return true;
			}
			return false;
		}
		
		public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Mjolnir"));
			recipe.AddIngredient(null, "EXSoul");
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}
