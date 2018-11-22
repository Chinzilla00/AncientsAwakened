using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class SpookySword : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 140;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 48;              //Sword width
            item.height = 48;             //Sword height
            item.useTime = 18;          //how fast 
            item.useAnimation = 18;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 21;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
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
            recipe.AddIngredient(ItemID.SpookyWood, 100);   //you need 1 DirtBlock
            recipe.AddTile(TileID.Sawmill);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
