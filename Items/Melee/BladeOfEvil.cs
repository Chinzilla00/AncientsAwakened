using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BladeOfEvil : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 31;
            item.melee = true;
            item.width = 52;
            item.height = 52;
            item.useTime = 30;
            item.useAnimation = 30;     
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 10000;        
            item.rare = 3;
			item.scale = 1.5f;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true; 
        }

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 14);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 117);
			}
		}
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Blade of Evil");
		  Tooltip.SetDefault("The perfect balance between Corruption and Crimson");
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
			recipe.AddIngredient(ItemID.TissueSample, 5);
			recipe.AddIngredient(ItemID.DemoniteBar, 8);
			recipe.AddIngredient(ItemID.ShadowScale, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
