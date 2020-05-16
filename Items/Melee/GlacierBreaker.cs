using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class GlacierBreaker : BaseAAItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.SolarEruption);

            item.damage = 18;            
            item.melee = true;            
            item.width = 32;              
            item.height = 46;             

            item.knockBack = 6;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.autoReuse = true;   
            item.useTurn = false;
            item.shoot = mod.ProjectileType("GlacierBreaker");
			item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glacier Breaker");
			Tooltip.SetDefault(@"Drops Icicles while the flail travels");
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.BorealWood, 20);
			recipe.AddIngredient(ItemID.IceBlock, 40);
			recipe.AddIngredient(mod.ItemType("SnowMana"), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
