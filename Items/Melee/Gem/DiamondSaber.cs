using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class DiamondSaber : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 29;            
            item.melee = true;            
            item.width = 44;              
            item.height = 44;               //Item Description
            item.useTime = 17;          
            item.useAnimation = 17;
            item.useStyle = ItemUseStyleID.SwingThrow;        
            item.knockBack = 3;     
            item.value = 1000;        
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = false;   
            item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Diamond Saber");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Diamond, 5);   
            recipe.AddRecipeGroup("AAMod:Gold", 12);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
