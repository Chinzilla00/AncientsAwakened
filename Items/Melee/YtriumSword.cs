using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class YtriumSword : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 30;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 50;              //Sword width
            item.height = 58;             //Sword height
            item.useTime = 20;          //how fast 
            item.useAnimation = 20;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 20;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;
            item.value = BaseMod.BaseUtility.CalcValue(0, 5, 0, 0);

        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Yttrium Blade");
          Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "YtriumBar", 15);   //you need 1 DirtBlock
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
