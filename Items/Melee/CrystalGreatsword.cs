using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class CrystalGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.TrueNightsEdge);
            item.damage = 83;
            item.melee = true;
            item.width = 96;
            item.height = 96;
            item.useTime = 27;
            item.useAnimation = 27;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 50000;
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = false;
            item.shoot = mod.ProjectileType<Projectiles.CGP>();
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Order Greatsword");
          Tooltip.SetDefault("Its green");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "CrystalShortsword");   //you need 1 DirtBlock
			recipe.AddIngredient(null, "TerraCrystal");
            recipe.AddTile(TileID.MythrilAnvil);  
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
