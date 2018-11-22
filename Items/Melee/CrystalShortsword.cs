using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class CrystalShortsword : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 85;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 60;              //Sword width
            item.height = 60;             //Sword height  //Item Description
            item.useTime = 16;          //how fast 
            item.useAnimation = 16;     
            item.useStyle = 3;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3;      //Sword knockback
            item.value = 1000;        
            item.rare = 7;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;    
			item.shoot = 493;
			item.shootSpeed = 8f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Crystal Shortsword");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.PixieDust, 15);   //you need 1 DirtBlock
			recipe.AddIngredient(ItemID.CrystalShard, 16);
            recipe.AddTile(TileID.WorkBenches);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
