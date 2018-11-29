using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class PrismaticGreatsword : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 45;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 58;              //Sword width
            item.height = 60;             //Sword height
            item.useTime = 26;          //how fast 
            item.useAnimation = 26;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 3000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
			item.shoot = mod.ProjectileType("PrismBolt");
			item.shootSpeed = 15f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Emerald Greatsword");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(null, "Poppy", 1);
            recipe.AddIngredient(null, "EmeraldGreatsword", 1);
            recipe.AddIngredient(null, "RubyGreatsword", 1);
            recipe.AddIngredient(null, "SapphireGreatsword", 1);
            recipe.AddIngredient(null, "TopazGreatsword", 1);
            recipe.AddIngredient(null, "EmeraldGreatsword", 1);
            recipe.AddIngredient(null, "DiamondGreatsword", 1);			//you need 1 DirtBlock
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
