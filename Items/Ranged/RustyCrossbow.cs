using Terraria.ID;

namespace AAMod.Items.Ranged
{
    public class RustyCrossbow : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusty Crossbow");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.width = 32;
            item.height = 20;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 25;
            item.shootSpeed = 10f;
            item.knockBack = 2f;
            item.rare = 3;
            item.noMelee = true;
            item.value = 10000;
            item.ranged = true;
        }
    }
}