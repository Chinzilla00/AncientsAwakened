using Terraria;
using Terraria.ID;

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
            item.rare = ItemRarityID.Blue;
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 30;
        }

    }
}