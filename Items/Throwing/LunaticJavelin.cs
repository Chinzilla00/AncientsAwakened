using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Throwing
{
    public class LunaticJavelin : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 120;           
            item.melee = true;             
            item.noMelee = true;
            item.width = 20;
            item.height = 20;
            item.useTime = 8;       
            item.useAnimation = 8;   
            item.useStyle = 1;      
            item.knockBack = 6;
            item.value = 1;
            item.rare = 8;
            item.reuseDelay = 0;    
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;       
            item.shoot = mod.ProjectileType("LJavelinP");  
            item.shootSpeed = 18f;     
            item.useTurn = true;
            item.maxStack = 999;       
            item.consumable = true;  
            item.noUseGraphic = true;
            item.noMelee = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Lunatic Javelin");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 250);
            recipe.AddRecipe();
        }
    }
}
