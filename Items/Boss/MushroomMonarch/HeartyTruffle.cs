using Terraria;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class HeartyTruffle : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hearty Truffle");
            Tooltip.SetDefault(
@"+50 Health
Don't eat it");
        }


        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
                player.statLifeMax2 += 50;
        }

    }
}