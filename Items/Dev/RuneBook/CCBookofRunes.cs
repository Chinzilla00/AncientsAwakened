using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dev.RuneBook
{
    public class CCBookofRunes : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("The Book of Runes");
            Tooltip.SetDefault(@"Summons runes according to how many minion slots you have left
When player has 1 minion slot it summons terra rune.
When player has 2 minion slots it summons terra and chaos rune.
When player has 3 minion slots it summons terra, chaos and void rune.");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 100000;
            item.rare = ItemRarityID.Purple;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.CCBookEX = true;
            if(hideVisual)
            {
                modPlayer.CCBookEX = false;
                player.ClearBuff(ModContent.BuffType<Buffs.CCRune>());
            }
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "CCRuneBookPage", 1);
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(mod, "DreadScale", 15);
            recipe.AddIngredient(mod, "EXSoul", 1);
			recipe.AddTile(mod, "ACS");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}