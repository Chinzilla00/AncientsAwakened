using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class DragonStaff : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 70;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 60;
            item.height = 60;

            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;     //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 6;        
            item.value = 10000;
            item.rare = 5;
            item.mana = 5;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("DragonP");  //this make the item shoot your projectile
            item.shootSpeed = 13f;    //projectile speed when shoot
        }   

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Dragon Staff");
      Tooltip.SetDefault("Shoots dragon scales.");
            Item.staff[item.type] = true;
    }

		public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DragonSpirit", 20);
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
