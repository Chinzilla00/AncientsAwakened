using AAMod.Items.Materials;
using AAMod.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    public class Lantern : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 3;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            if (item.accessory)
            {
                player.GetModPlayer<AAPlayer>().FogRemover = true;
            }
            else
            {
                player.GetModPlayer<AAPlayer>().FogRemover = false;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lantern");
            Tooltip.SetDefault(@"Permanent accessory to partially remove The Fog");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<DragonClaw>(), 15);
            recipe.AddTile(mod.TileType<ChaosAltar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}