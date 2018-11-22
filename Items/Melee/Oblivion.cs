using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Oblivion : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 186;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 64;              //Sword width
            item.height = 64;             //Sword height
            item.useTime = 21;          //how fast 
            item.useAnimation = 21;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 100000;        
            item.rare = 10;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = true;                //projectile speed                 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Oblivion");
      Tooltip.SetDefault("Unleash the power");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Amnesia", 1);   //you need 1 DirtBlock
			recipe.AddIngredient(ItemID.InfluxWaver, 1);
			recipe.AddIngredient(null, "Dragonkite", 1);
			recipe.AddIngredient(null, "GuardianNight", 1);
            recipe.AddTile(TileID.LunarCraftingStation);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
