using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Scalpel : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 15;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useAnimation = 25;
            item.useTime = 10;
            item.pick = 110;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = Terraria.Item.sellPrice(0, 1, 8, 0);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scalpel");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DeathbringerPickaxe);
            recipe.AddIngredient(mod, "Grasscutter");
            recipe.AddIngredient(mod, "Toothpick");
            recipe.AddIngredient(ItemID.MoltenPickaxe);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
