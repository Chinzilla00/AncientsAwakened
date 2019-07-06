using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class DragonsBreath : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 10;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 24;
            item.height = 28;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;
            item.mana = 5;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = false;
            item.shoot = 664;  //this make the item shoot your projectile
            item.shootSpeed = 11f;    //projectile speed when shoot
        }   

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Dragon's Breath");
          Tooltip.SetDefault("");
        }

		public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(null, "IncineriteBar", 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
