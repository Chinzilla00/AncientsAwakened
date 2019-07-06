using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem
{
    public class AmberGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 42;            
            item.melee = true;            
            item.width = 58;              
            item.height = 60;             
            item.useTime = 20;          
            item.useAnimation = 20;     
            item.useStyle = 1;        
            item.knockBack = 5;      
            item.value = 3000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = false;   
            item.useTurn = true; 
			item.shoot = mod.ProjectileType<Projectiles.GemShot.AmberShot>();
			item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber Greatsword");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "AmberSaber", 1);
            recipe.AddIngredient(ItemID.LargeAmber, 1);		
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
