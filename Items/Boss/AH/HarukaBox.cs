using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HarukaBox : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.consumable = true;
            item.width = 16;
            item.height = 16;
            item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haruka's Lockbox");
            Tooltip.SetDefault(@"Right click to open
Contains a set of Midnight Assassin clothes");
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("AssassinHood"));
            player.QuickSpawnItem(mod.ItemType("AssassinShirt"));
            player.QuickSpawnItem(mod.ItemType("AssassinBoots"));
        }
    }
}
