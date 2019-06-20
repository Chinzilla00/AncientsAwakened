using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Sagittarius
{
    public class Legg : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sagittarius' Leg");
            Tooltip.SetDefault("It's a piece of metal. You beat things with it. Pretty basic concept.");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useAnimation = 38;
            item.useTime = 38;
            item.knockBack = 8f;
            item.width = 50;
            item.height = 92;
            item.damage = 42;
            item.scale = 1.05f;
            item.UseSound = SoundID.Item1;
            item.rare = 4;
            item.value = 150000;
            item.melee = true;
            item.autoReuse = true;
        }
    }
}