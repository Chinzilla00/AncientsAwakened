using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Melee     //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class ThanosSwordEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thanoscopter Blades");
            
        }

        public override void SetDefaults()
        {
            item.damage = 170;  
            item.melee = true;
            item.noMelee = true;
            item.width = 88;    
            item.height = 100; 
            item.useTime = 6; 
            item.useAnimation = 6;
            item.channel = true;
            item.knockBack = 0f; 
            item.value = Item.buyPrice(1, 0, 0, 0); 
            item.rare = 11;
            item.shoot = mod.ProjectileType("ThanosSwordEXT"); 
            item.noUseGraphic = true;
        }
        
        public override bool UseItemFrame(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ThanosSword", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
