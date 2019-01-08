using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Icepick : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icepick");
        }

        public override void SetDefaults()
        {

            item.damage = 10;
            item.melee = true;
            item.width = 46;
            item.height = 42;
            item.useTime = 9;
            item.useAnimation = 16;
            item.pick = 105;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
    }
}
