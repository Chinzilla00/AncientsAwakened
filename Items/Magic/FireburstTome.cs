using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class FireburstTome : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 36;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 24;
            item.height = 28;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 6;
            item.mana = 11;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = 664;  //this make the item shoot your projectile
            item.shootSpeed = 11f;    //projectile speed when shoot
        }   

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Fireburst Tome");
      Tooltip.SetDefault("");
    }

		public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book, 1);   //you need 10 Wood
			recipe.AddIngredient(ItemID.LivingFireBlock, 60);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddTile(TileID.WorkBenches);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
