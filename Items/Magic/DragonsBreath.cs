using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class DragonsBreath : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 25;                        
            item.magic = true;                     
            item.width = 24;
            item.height = 28;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;        
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;
            item.mana = 5;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.shoot = 664;  //this make the item shoot your projectile
            item.shootSpeed = 11f;     
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
