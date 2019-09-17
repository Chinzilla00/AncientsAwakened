using Terraria.ID;

namespace AAMod.Items.Ranged
{
    public class PurityString : BaseAAItem
    {

        public override void SetDefaults()
        {

            item.damage = 50;
            item.noMelee = true;
            item.ranged = true;
            item.width = 34;
            item.height = 60;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 5;
            item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            item.rare = 7;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 22f;

        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Crystal Bow");
          Tooltip.SetDefault("");
        }
    }
}
