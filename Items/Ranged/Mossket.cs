using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Ranged
{
    public class Mossket : BaseAAItem
    {

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 18;
            item.useTime = 18;
            item.width = 24;
            item.height = 28;
            item.shoot = ProjectileID.PurificationPowder;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item11;
            item.damage = 15;
            item.shootSpeed = 12f;
            item.noMelee = true;
            item.knockBack = .5f;
            item.value = 50000;
            item.scale = 1f;
            item.rare = ItemRarityID.Blue;
            item.ranged = true;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }
    }
}
