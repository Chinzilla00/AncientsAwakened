using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class Unearther : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 44;
            item.height = 44;
            item.useAnimation = 10;
            item.useTime = 5;
            item.pick = 230;
            item.tileBoost += 4;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
    }
}
