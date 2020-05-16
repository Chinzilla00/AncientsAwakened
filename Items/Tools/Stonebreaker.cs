using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Stonebreaker : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 15;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useAnimation = 30;
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
            DisplayName.SetDefault("Stonebreaker");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "DragonDigger");
            recipe.AddIngredient(mod, "OceanPick");
            recipe.AddIngredient(mod, "Excavator");
            recipe.AddIngredient(mod, "DoomiteMiningLaser");
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
