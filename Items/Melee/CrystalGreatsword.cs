using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class CrystalGreatsword : ModItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.TrueNightsEdge);

            item.damage = 83;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 96;              //Sword width
            item.height = 96;             //Sword height

            item.useTime = 30;          //how fast 
            item.useAnimation = 30;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 6;      //Sword knockback
            item.value = 10;        
            item.rare = 7;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = false;
            item.shoot = mod.ProjectileType<Projectiles.CGP>();
            item.shootSpeed = 6f;                //projectile speed                 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Crystal Greatsword");
      Tooltip.SetDefault("Its pink");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.PixieDust, 15);   //you need 1 DirtBlock
			recipe.AddIngredient(ItemID.CrystalShard, 20);
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
