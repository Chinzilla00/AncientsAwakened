using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class JungleReaper : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 13;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 78;              //Sword width
            item.height = 60;             //Sword height
            item.useTime = 30;          //how fast 
            item.useAnimation = 30;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3;      //Sword knockback
            item.value = 10;        
            item.rare = 2;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = false;
            item.shoot = mod.ProjectileType("JungleReaperP");
            item.shootSpeed = 8f;                //projectile speed                 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Jungle Reaper");
      Tooltip.SetDefault("It's a scythe. Calm down Welox.");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddRecipeGroup("AAMod:Gold", 15);
            recipe.AddTile(TileID.LivingLoom);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
