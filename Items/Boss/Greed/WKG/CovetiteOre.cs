using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class CovetiteOre : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(0, 1, 8, 0);
            item.consumable = true;
            item.createTile = mod.TileType("CovetiteOre");
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Covetite Ore");
            Tooltip.SetDefault("Only a fool would want this. Makes sense why greed has it.");
        }

    }
}
