using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class Gong : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 35;
            item.height = 54;
            item.maxStack = 1;
            item.value = 10000;
            item.rare = 3;
			item.damage = 20;                        
            item.magic = true;
			item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 4;
			item.mana = 8;             //mana use
            item.UseSound = SoundID.Item13;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = 122;
			item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gong");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("AAMod:Gold", 15);
            recipe.AddIngredient(ItemID.WhiteString);
            recipe.AddIngredient(ItemID.ManaCrystal);
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
