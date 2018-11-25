using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Dragonfire : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 30;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 52;              //Sword width
            item.height = 52;             //Sword height
            item.useTime = 26;          //how fast 
            item.useAnimation = 26;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 6;      //Sword knockback
            item.value = 20000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Dragonfire");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
			recipe.AddIngredient(ItemID.CrimtaneBar, 14);			//you need 1 DirtBlock
            recipe.AddIngredient(null, "FlamingFury", 1);
            recipe.AddIngredient(null, "IncineriteBar", 7);
            recipe.AddTile(TileID.DemonAltar);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
