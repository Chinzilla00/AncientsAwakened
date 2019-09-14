using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class Miner : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MINE-er");
            Tooltip.SetDefault("Mines ores faster (Not Working yet)");
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 13;
            item.useAnimation = 20;
            item.pick = 205;
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
