using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Akuma
{
    public class AkumaTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 4;
            item.useAnimation = 20;
            item.tileBoost += 20;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 100;
            item.pick = 300;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Terratool");
            Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active
+20 tile reach");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && Main.mouseRight && Main.mouseRightRelease)
            {
                item.autoReuse = false;
                item.noUseGraphic = true;
                AAMod.instance.TerratoolAState.ToggleUI(AAMod.instance.TerratoolInterface);
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                item.damage = 0;
                return false;
            }
            else if(player.altFunctionUse != 2)
            {
                item.autoReuse = true;
                item.noUseGraphic = false;
                item.pick = UI.TerratoolAUI.Pick;
                item.axe = UI.TerratoolAUI.Axe;
                item.hammer = UI.TerratoolAUI.Hammer;
                item.damage = 100;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
