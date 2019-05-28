using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Firebuster : ModItem
    {

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 40;
            item.useTime = 40;
            item.width = 54;
            item.height = 24;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item11;
            item.damage = 36;
            item.shootSpeed = 9f;
            item.noMelee = true;
            item.value = 100000;
            item.knockBack = 10f;
            item.rare = 1;
            item.ranged = true;
            item.crit = 10;
        }
    }
}
