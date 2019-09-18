using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod.Items.Boss.Greed
{
    public class StoneScarf : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Scarf");
            Tooltip.SetDefault(@"Reduces damage taken by 20%");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 8;
            item.defense = 3;
            item.accessory = true;
            item.expertOnly = true;
            item.expert = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += .2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WormScarf, 1);
            recipe.AddIngredient(null, "StoneShell", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}