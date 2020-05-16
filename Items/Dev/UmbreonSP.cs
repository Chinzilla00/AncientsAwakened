using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Dev
{
    public class UmbreonSP : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blade of Night");
			Tooltip.SetDefault("A dark sword from a dark creature.");
		}
		
		public override void SetDefaults()
		{
			item.damage = 200;
			item.melee = true;
			item.width = 84;
			item.height = 84;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
			item.value = Item.sellPrice(0, 1, 50, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item71;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("UmbreonSPProjectile");
			item.shootSpeed = 20f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }
    }
}
