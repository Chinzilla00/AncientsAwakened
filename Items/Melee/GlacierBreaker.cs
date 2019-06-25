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

            item.damage = 18;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 46;             //Sword height

            item.knockBack = 6;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = false;
            item.shoot = mod.ProjectileType("GlacierBreaker");
			item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glacier Breaker");
			Tooltip.SetDefault(@"Hitting enemy causes some icicles to drop");
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
