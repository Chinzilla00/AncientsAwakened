using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class AmethystSaber : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 17;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 44;              //Sword width
            item.height = 44;             //Sword height  //Item Description
            item.useTime = 17;          //how fast 
            item.useAnimation = 17;
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3;      //Sword knockback
            item.value = 1000;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Amethyst Saber");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Amethyst, 5);   //you need 1 DirtBlock
            recipe.AddRecipeGroup("AAMod:Copper", 12);
            recipe.AddTile(TileID.WorkBenches);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
