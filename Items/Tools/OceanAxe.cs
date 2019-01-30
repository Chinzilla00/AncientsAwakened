using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class OceanAxe : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;
            item.melee = true;
            item.width = 44;
            item.height = 40;

            item.useTime = 12;
            item.useAnimation = 20;
            item.axe = 50;    //pickaxe power
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 10;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coral Axe");
            Tooltip.SetDefault("the axe made from the Ocean");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Coral, 6);   //you need 10 Wood
			recipe.AddIngredient(ItemID.IronAxe, 1);
            recipe.AddTile(TileID.WorkBenches);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
