using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class SkyBook : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 27;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 24;
            item.height = 28;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 6;
            item.mana = 2;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = false;
            item.shoot = mod.ProjectileType ("CrystalP");  //this make the item shoot your projectile
            item.shootSpeed = 15f;    //projectile speed when shoot
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Tome");
            Tooltip.SetDefault("Casts crystal shards towards your cursor");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StarcloudBar", 15);   //you need 10 Wood
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.Bookcases);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
