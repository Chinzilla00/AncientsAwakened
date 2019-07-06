using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class SpookySword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 140;            
            item.melee = true;            
            item.width = 48;              
            item.height = 48;             
            item.useTime = 18;          
            item.useAnimation = 18;     
            item.useStyle = 1;        
            item.knockBack = 5;
            item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = true;   
            item.useTurn = true;               
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Sword");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.SpookyWood, 100);   
            recipe.AddTile(TileID.Sawmill);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
