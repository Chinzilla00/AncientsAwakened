using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Yogan : BaseAAItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.Sunfury);

            item.damage = 48; 
            item.melee = true; 
            item.width = 46; 
            item.height = 66;    
            item.knockBack = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.LightRed;
            item.autoReuse = false;
            item.useTurn = false;
            item.shoot = mod.ProjectileType("Yogan");
			item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yogan");
			Tooltip.SetDefault(@"Ignites enemies on hit");
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Pyrosphere"));
            recipe.AddIngredient(mod.ItemType("GlacierBreaker"));
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
			recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Pyrosphere"));
            recipe.AddIngredient(mod.ItemType("GlacierBreaker"));
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
