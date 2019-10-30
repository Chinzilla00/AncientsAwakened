using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dev.Tools
{
    public class LizTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 56;
            item.height = 56;
            item.useStyle = 1;
            item.useTime = 4;
            item.useAnimation = 16;
            item.tileBoost += 25;
            item.knockBack = 3;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 120;
            item.pick = 320;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galaxium Terratool");
            Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active");
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
                AAMod.instance.TerratoolLizState.ToggleUI(AAMod.instance.TerratoolInterface);
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
                item.pick = UI.TerratoolLizUI.Pick;
                item.axe = UI.TerratoolLizUI.Axe;
                item.hammer = UI.TerratoolLizUI.Hammer;
                item.damage = 120;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
