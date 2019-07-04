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
            item.useStyle = 1;
            item.useTime = 4;
            item.useAnimation = 20;
            item.tileBoost += 20;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 9;
            AARarity = 13;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 100;
            item.pick = 300;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Terratool");
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
                AAMod.instance.TerratoolAState.ToggleUI(AAMod.instance.TerratoolAInterface);
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
            }
            else
            {
                item.pick = UI.TerratoolAUI.Pick;
                item.axe = UI.TerratoolAUI.Axe;
                item.hammer = UI.TerratoolAUI.Hammer;
            }
            return true;
        }
    }
}
