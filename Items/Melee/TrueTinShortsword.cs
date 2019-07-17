using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueTinShortsword : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Tin Shortsword");
			Tooltip.SetDefault("The true blade of shadows");
        }
		public override void SetDefaults()
		{
            
			item.damage = 70;
			item.melee = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = 3;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 9;
			item.expert = true; item.expertOnly = true;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
            item.shootSpeed = 20f;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinShortsword, 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
