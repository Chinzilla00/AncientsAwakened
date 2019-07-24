using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Items.Dev
{
    public class ThunderLord : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Storm Rifle");
            Tooltip.SetDefault(@"Fires off static shots
'NUM'
-BlazenBreaker");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType<Projectiles.ThunderSpark>(), damage, knockBack, Main.myPlayer, 0, 0);
            return false;
        }

        public override void SetDefaults()
        {
            item.damage = 115;
            item.noMelee = true;
            item.ranged = true; 
            item.width = 70; 
            item.height = 24;
            item.useTime = 20; 
            item.useAnimation = 20; 
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("ThunderSpark");
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item92;
            item.autoReuse = true; 
            item.shootSpeed = 9f;
            item.useAmmo = AmmoID.Bullet;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
			glowmaskDrawType = GLOWMASKTYPE_GUN;
			glowmaskDrawColor = AAColor.COLOR_WHITEFADE1;
			customNameColor = new Color(0, 0, 255);			
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}