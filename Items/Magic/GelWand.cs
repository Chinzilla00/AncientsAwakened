using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class GelWand : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 9;                        
            item.magic = true;                     
            item.width = 26;
            item.height = 38;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;        
            item.noMelee = true;
            item.knockBack = 2;        
            item.value = 1000;
            item.rare = 2;
            item.mana = 5;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("TFGWP");  
            item.shootSpeed = 7f;     
        }   

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Flaming Gel Wand");
      Tooltip.SetDefault("It shoots flaming gel");
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
