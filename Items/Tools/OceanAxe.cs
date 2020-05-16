using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class OceanAxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;
            item.melee = true;
            item.width = 44;
            item.height = 40;

            item.useTime = 12;
            item.useAnimation = 20;
            item.axe = 10;    //pickaxe power
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.value = 10;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coral Axe");
            Tooltip.SetDefault("the axe made from the Ocean");
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
