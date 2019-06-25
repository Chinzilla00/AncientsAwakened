using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Yogan : BaseAAItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.SolarEruption);

            item.damage = 48;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 46;              //Sword width
            item.height = 66;             //Sword height

            item.knockBack = 7;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 4;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = false;
            item.shoot = mod.ProjectileType("Yogan");
			item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yogan");
			Tooltip.SetDefault(@"Inflicts Frostburn and ignites enemies on hit");
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TheMeatball);
			recipe.AddIngredient(mod.ItemType("GlacierBreaker"));
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BallOHurt);
			recipe.AddIngredient(mod.ItemType("GlacierBreaker"));
            recipe.AddIngredient(ItemID.BlueMoon);
			recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
