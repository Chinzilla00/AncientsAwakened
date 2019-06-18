using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class VenomSpray : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 24;
            item.height = 28;
            item.useStyle = 5;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = 1;
            item.mana = 5;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.useTime = 6;
            item.useAnimation = 18;
            item.shoot = mod.ProjectileType("Venom");
            item.shootSpeed = 10f;   //projectile speed when shoot
        }   

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Venom Spray");
      Tooltip.SetDefault("");
    }

		public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(null, "AbyssiumBar", 10);
            recipe.AddTile(TileID.Bookcases);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
