using Terraria;

namespace AAMod.Items.Boss.AH
{
    public class AsheSatchel : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.consumable = true;
            item.width = 16;
            item.height = 16;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe's Satchel");
            Tooltip.SetDefault(@"Right click to open
Contains a set of Fury Witch's robes");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("WitchHood"));
            player.QuickSpawnItem(mod.ItemType("WitchRobe"));
            player.QuickSpawnItem(mod.ItemType("WitchBoots"));
        }
    }
}
