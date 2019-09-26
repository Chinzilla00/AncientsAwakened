using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Usable
{
    public class RiftMirror : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rift Mirror");
            Tooltip.SetDefault(@"Pressing the rift hotkey returns you home
Pressing the rift return hotkey brings you back to your most recent rift location");
        }    

		public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.MagicMirror);
            item.maxStack = 1;
			item.useAnimation = 15;
            item.useTime = 15;
            item.consumable = false;
        }

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.IceMirror);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
