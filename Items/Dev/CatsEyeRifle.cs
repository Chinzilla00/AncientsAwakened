using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace AAMod.Items.Dev
{
    public class CatsEyeRifle : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cat's Eye Rifle");
            Tooltip.SetDefault(@"Fires Shadow bolts
Doesn't require ammo
'QUICK HIDE THE LOLI STASH'
-Liz");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void SetDefaults()
        {
            item.damage = 430;
            item.noMelee = true;
            item.ranged = true; 
            item.width = 72; 
            item.height = 22;
            item.useTime = 30; 
            item.useAnimation = 30; 
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("CatsEye");
            item.knockBack = 12;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9; 
            item.UseSound = new LegacySoundStyle(2, 40, Terraria.Audio.SoundType.Sound);
            item.autoReuse = false; 
            item.shootSpeed = 20f;
            item.crit = 0;

			glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
			glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun
			glowmaskDrawColor = Color.White; //glowmask draw color			
			customNameColor = new Color(121, 21, 214); //custom name color				
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}