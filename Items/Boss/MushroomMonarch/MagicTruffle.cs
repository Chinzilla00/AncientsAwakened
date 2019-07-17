using Terraria;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class MagicTruffle : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Truffle");
            Tooltip.SetDefault(
@"+30 Mana
Don't lick it.");
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

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 30;
        }

    }
}