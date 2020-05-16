using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class MadnessSword : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Saber");
        }
        public override void SetDefaults()
        {
            item.damage = 16;
            item.melee = true;
            item.width = 42;
            item.height = 46;
            item.useTime = 17;
            item.useAnimation = 17;     
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5;
            item.value = 3000;        
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "MadnessFragment", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
