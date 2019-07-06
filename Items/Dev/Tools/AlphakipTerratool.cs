using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dev.Tools
{
    public class AlphakipTerratool : BaseAAItem
    {
        
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
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
            DisplayName.SetDefault("Amphibious Terratool");
            Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noUseGraphic = true;
                AAMod.instance.TerratoolKipState.ToggleUI(AAMod.instance.TerratoolKipInterface);
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                return false;
            }
            else
            {
                item.noUseGraphic = false;
                item.pick = UI.TerratoolKipUI.Pick;
                item.axe = UI.TerratoolKipUI.Axe;
                item.hammer = UI.TerratoolKipUI.Hammer;
            }
            return true;
        }
    }
}
