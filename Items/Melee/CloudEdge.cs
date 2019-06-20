using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class CloudEdge : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 20;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 20;          //how fast 
            item.useAnimation = 20;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 1;      //Sword knockback
            item.value = 5000;        
            item.rare = 2;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;
            item.shoot = mod.ProjectileType("CloudEdgeP");
            item.shootSpeed = 12f;                //projectile speed                 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cloud Edge");
      Tooltip.SetDefault("Shoots cloud projectiles");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.FallenStar, 5);   //you need 1 DirtBlock
			recipe.AddIngredient(ItemID.Cloud, 200);
            recipe.AddTile(TileID.WorkBenches);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
