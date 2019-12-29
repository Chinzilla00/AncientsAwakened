using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofRagnarok : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SpellBook of Ragnarok");
            Tooltip.SetDefault(@"Increase 20% spellbook damage
After a shooting turn, put the spellbook back in your inventory automatically
According to the number of spellbook used in this turn, you get the following effect:
5 spellbooks: Increase 30% your spellbook damage
10 spellbooks: Summon a rune on your head");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().SpellBookofRagnarok = true;
            player.GetModPlayer<AAPlayer>().spellbookDamage += .2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FragmentNebula, 30);
            recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }

    }
}