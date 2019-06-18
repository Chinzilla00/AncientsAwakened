using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class Icepick : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 5;
            item.melee = true;
            item.width = 32;
            item.height = 32;

            item.useTime = 19;
            item.useAnimation = 20;
            item.pick = 100;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icepick");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWood, 10);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(null, "SnowMana", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
