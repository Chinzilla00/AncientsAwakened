using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class DiversDoom : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 147;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 42;              //Sword width
            item.height = 50;             //Sword height
            item.useTime = 23;          //how fast 
            item.useAnimation = 23;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item1;                  //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;               
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Diver's Doom");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "DeepAbyssium", 15);   //you need 1 DirtBlock
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
