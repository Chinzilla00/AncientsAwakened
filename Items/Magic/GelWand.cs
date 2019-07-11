using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class GelWand : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 10;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 26;
            item.height = 38;
            item.useTime = 23;
            item.useAnimation = 23;
            item.useStyle = 1;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 2;        
            item.value = 1000;
            item.rare = 2;
            item.mana = 2;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("TFGWP");  //this make the item shoot your projectile
            item.shootSpeed = 7f;     
        }   

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Flaming Gel Wand");
      Tooltip.SetDefault("It shoots flaming gel.");
            Item.staff[item.type] = true;
        }

		public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WandofSparking, 1);   //you need 10 Wood
			recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
