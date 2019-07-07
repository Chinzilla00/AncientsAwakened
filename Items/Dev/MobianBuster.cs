using Terraria;

namespace AAMod.Items.Dev
{
    public class MobianBuster : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mobian Buster");
            Tooltip.SetDefault("A standard issue Mobian blaster.\n" +
                "Hold the use button to charge, and then release a powerful Charged Shot!\n" +
                "\"Remember, the charged shot fires when you RELEASE the trigger, not the other way around.\" \n" +
                "- Tails\n");
        }

        public override void SetDefaults()
        {
            item.width = 74;
            item.height = 34;
            item.ranged = true;
            item.damage = 100;
            item.shoot = mod.ProjectileType("MobianBuster");
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.channel = true;
            Item.sellPrice(0, 30, 0, 0);
            item.noMelee = true;
			item.rare = 10;
			item.shootSpeed = 12f;
			item.noUseGraphic = true;
        }
    }
}
