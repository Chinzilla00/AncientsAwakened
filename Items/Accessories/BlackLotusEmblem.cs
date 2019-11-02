using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Accessories
{
    public class BlackLotusEmblem : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Lotus Emblem");
            Tooltip.SetDefault(
@"Increases pickup range for mana stars
Automatically use mana potions when needed
Greatly reduce manasick time
Your magic attacks inflicts moonraze
15% increased movement speed
12% reduced mana usage
15% increased magic damage");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 50, 0, 0);
            item.rare = 8;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().BlackLotusEmblem = true;
            player.manaMagnet = true;
            player.manaRegenDelay = 0;
			player.manaCost -= 0.12f;
			player.magicDamage += 0.15f;
            player.moveSpeed += 0.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaFlower, 1);
            recipe.AddIngredient(ItemID.CelestialEmblem, 1);
            recipe.AddIngredient(null, "BlackLotus", 1);
            recipe.AddIngredient(null, "ShadowBand", 1);
            recipe.AddIngredient(null, "SoulOfSpite", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}