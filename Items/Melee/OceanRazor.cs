using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class OceanRazor : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ocean Razor");
			Tooltip.SetDefault("Salty");
		}
		public override void SetDefaults()
		{
			item.damage = 18;
			item.melee = true;
			item.width = 38;
			item.height = 44;
			item.useTime = 13;
			item.useAnimation = 13;
			item.useStyle = 3;
			item.knockBack =6;
			item.value = 2000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 15);
			recipe.AddIngredient(ItemID.Starfish, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Wet, 300);
        }
	}
}
