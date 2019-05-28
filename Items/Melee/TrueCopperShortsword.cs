using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueCopperShortsword : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Copper Shortsword");
			Tooltip.SetDefault("Literally just did it for the memes");
        }
		public override void SetDefaults()
		{
            
			item.damage = 500;
			item.melee = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 3;
			item.knockBack =20;
			item.value = 10000000;
			item.rare = 9;
			item.expert = true;
			item.UseSound = SoundID.Item37;
			item.autoReuse = false;
            item.shoot = mod.ProjectileType("TrueCopperShot");
            item.shootSpeed = 20f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperShortsword, 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
