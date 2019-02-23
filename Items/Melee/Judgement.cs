using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Judgement : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 55;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 64;              //Sword width
            item.height = 64;             //Sword height
            item.useTime = 19;          //how fast 
            item.useAnimation = 19;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 20;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
			item.shoot = 221;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Judgement");
            Tooltip.SetDefault("Would be more clever if it was a hammer");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.MythrilBar, 18);
                recipe.AddIngredient(ItemID.SoulofLight, 8);//you need 1 DirtBlock
                recipe.AddTile(TileID.MythrilAnvil);   //at work bench
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.OrichalcumBar, 18);
                recipe.AddIngredient(ItemID.SoulofLight, 8);//you need 1 DirtBlock
                recipe.AddTile(TileID.MythrilAnvil);   //at work bench
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
