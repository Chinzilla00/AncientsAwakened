using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueFleshrendClaymore : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Fleshrend Claymore");
			Tooltip.SetDefault(@"Inflics Ichor on your target
Despite the name, it's not actually made of flesh");
        }
		public override void SetDefaults()
		{
            
			item.damage = 150;
			item.melee = true;
			item.width = 75;
			item.height = 71;
			item.useTime = 29;
			item.useAnimation = 29;
			item.useStyle = 1;
			item.knockBack = 8;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("TrueFleshClaymoreShot");
            item.shootSpeed = 12f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()
		{
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "FleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "FleshrendClaymore", 1);
                recipe.AddIngredient(null, "CrimsonCrystal", 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }

			
		}


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
	       player.HealEffect(damage / 20);
        }
    }
}
