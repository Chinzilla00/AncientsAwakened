using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class CrystalShortsword : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 22;
            item.melee = true;
            item.width = 42;
            item.height = 42;
            item.useTime = 24;
            item.useAnimation = 28;     
            item.useStyle = ItemUseStyleID.Stabbing;
            item.knockBack = 1;
            item.value = 1000;        
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Harmony Shortsword");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "TerraShard", 10);
            recipe.AddTile(TileID.Anvils);  
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
