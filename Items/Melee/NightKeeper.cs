using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class NightKeeper : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 44;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 54;              //Sword width
            item.height = 54;             //Sword height
            item.useTime = 22;          //how fast 
            item.useAnimation = 22;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 20000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("The Night Keeper");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.LightsBane, 1);
			recipe.AddIngredient(ItemID.DemoniteBar, 7);			//you need 1 DirtBlock
            recipe.AddIngredient(null, "ExilesKatana", 1);
            recipe.AddIngredient(null, "AbyssiumBar", 7);
            recipe.AddTile(TileID.DemonAltar);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
