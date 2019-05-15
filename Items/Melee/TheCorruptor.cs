using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TheCorruptor : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 62;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 72;              //Sword width
            item.height = 72;             //Sword height
            item.useTime = 20;          //how fast 
            item.useAnimation = 20;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 20;        
            item.rare = 9;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
			item.shoot =  306;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Corruptor");
            Tooltip.SetDefault("Shoots corrupt eaters that break after hitting an enemy");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.RottenChunk, 8);			//you need 1 DirtBlock
            recipe.AddTile(TileID.DemonAltar);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
