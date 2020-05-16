using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Ranged
{
    public class Laser_Rifle : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laser Carbine");
			Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.ranged = true;
			item.width = 46;
			item.height = 22;
			item.useAnimation = 14;
			item.useTime = 14;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 4, 72, 0);
			item.rare = ItemRarityID.Pink;
			item.UseSound = SoundID.Item12;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 22f;
			item.useAmmo = mod.ItemType("Energy_Cell");			
			item.crit = 5;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 2);
		}
	}
}
