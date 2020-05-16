using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dev.RuneBook
{
    public class CCRuneBookPage : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("A Page of RuneBook");
            Tooltip.SetDefault(@"Summons runes according to your minion slots
When player has 1 minion slot, it summons bunny rune.
When player has 2 minion slots, it summons bunny and discord rune.
When player has 3 minion slots, it summons bunny, discord and energy rune.");
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
            modPlayer.CCBook = true;
            if(hideVisual)
            {
                modPlayer.CCBook = false;
                player.ClearBuff(ModContent.BuffType<Buffs.CCRune>());
            }
        }
    }
}