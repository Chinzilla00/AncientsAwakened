using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class CrystalPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;
            item.melee = true;
            item.width = 42;
            item.height = 42;

            item.useTime = 10;
            item.useAnimation = 14;
            item.pick = 110;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Unity Pickaxe");
      Tooltip.SetDefault("Can mine mythril and orichalcum.");
    }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PixieDust, 12);   //you need 1 DirtBlock
			recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
