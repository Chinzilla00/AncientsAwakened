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
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = ItemRarityID.Lime;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Unity Pickaxe");
      Tooltip.SetDefault("Can mine mythril and orichalcum.");
    }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PixieDust, 12);   
			recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
