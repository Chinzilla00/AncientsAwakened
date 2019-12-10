using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class FurnitureDynamo : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Furniture Dynamo");
            Tooltip.SetDefault(@"Combines all funiture-crafting stations into one block
Now you don't have to clutter your base with 12 crafting stations!");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 9;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 1000000;
            item.createTile = mod.TileType("FurnitureDynamo");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sawmill, 1);
            recipe.AddIngredient(ItemID.BoneWelder, 1);
            recipe.AddIngredient(ItemID.BlendOMatic, 1);
            recipe.AddIngredient(ItemID.GlassKiln, 1);
            recipe.AddIngredient(ItemID.HeavyWorkBench, 1);
            recipe.AddIngredient(ItemID.HoneyDispenser, 1);
            recipe.AddIngredient(ItemID.IceMachine, 1);
            recipe.AddIngredient(TileID.LivingLoom, 1);
            recipe.AddIngredient(ItemID.MeatGrinder, 1);
            recipe.AddIngredient(ItemID.SkyMill, 1);
            recipe.AddIngredient(ItemID.Solidifier, 1);
            recipe.AddIngredient(ItemID.SteampunkBoiler, 1);
            recipe.AddIngredient(ItemID.FleshCloningVaat, 1);
            recipe.AddIngredient(ItemID.LihzahrdFurnace, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
