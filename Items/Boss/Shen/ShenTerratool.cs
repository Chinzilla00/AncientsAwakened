using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class ShenTerratool : BaseAAItem
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
            DisplayName.SetDefault("Discordian Terratool");
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
                AAMod.instance.TerratoolSState.ToggleUI(AAMod.instance.TerratoolSInterface);
                item.pick = UI.TerratoolSUI.Pick;
                item.axe = UI.TerratoolSUI.Axe;
                item.hammer = UI.TerratoolSUI.Hammer;
                return true;
            }
            else
            {
                // do stuff
            }

            return false;
        }
    }
}
