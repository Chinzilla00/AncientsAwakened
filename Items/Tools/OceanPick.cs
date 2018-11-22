using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class OceanPick : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 7;
            item.melee = true;
            item.width = 40;
            item.height = 40;

            item.useTime = 12;
            item.useAnimation = 20;
            item.pick = 40;    //pickaxe power
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10;
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coral Pickaxe");
            Tooltip.SetDefault("Because Blue Pickaxe was a boring name");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Coral, 6);   //you need 10 Wood
			recipe.AddIngredient(ItemID.IronPickaxe, 1);
            recipe.AddTile(TileID.WorkBenches);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
