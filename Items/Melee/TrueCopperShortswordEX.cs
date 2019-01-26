using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueCopperShortswordEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultima Shortsword");
			Tooltip.SetDefault("Copper Shortsword EX");
        }
		public override void SetDefaults()
		{
            
			item.damage = 1000;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 3;
			item.knockBack =20;
			item.value = 10000000;
			item.rare = 9;
			item.expert = true;
			item.UseSound = SoundID.Item37;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("TrueCopperShot");
            item.shootSpeed = 20f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TrueCopperShortsword", 1);
			recipe.AddIngredient(null, "EXSoul", 1);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
