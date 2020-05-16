using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class OceanPick : BaseAAItem
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
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = 10;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coral Pickaxe");
            Tooltip.SetDefault("Because Blue Pickaxe was a boring name");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Coral, 15);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
