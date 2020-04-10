using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class Poppy : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poppy");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {

            item.damage = 32;            
            item.melee = true;            
            item.width = 44;              
            item.height = 44;             
            item.useTime = 20;          
            item.useAnimation = 20;
            item.useStyle = 1;        
            item.knockBack = 3;      
            item.value = 5000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddIngredient(ItemID.Topaz, 1);
            recipe.AddIngredient(ItemID.Amber, 1);
            recipe.AddIngredient(ItemID.Diamond, 1);
            recipe.AddIngredient(ItemID.Amethyst, 1);
            recipe.AddIngredient(null, "Prism", 10);
            recipe.AddRecipeGroup("AAMod:Gold", 12);		
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
