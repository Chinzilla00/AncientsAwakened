using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Tools
{
    public class Excavator : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 5;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.useTime = 19;
            item.useAnimation = 22;
            item.pick = 100;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DesertFossil, 15);
            recipe.AddIngredient(ItemID.Sandstone, 20);
            recipe.AddIngredient(null, "DesertMana", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
