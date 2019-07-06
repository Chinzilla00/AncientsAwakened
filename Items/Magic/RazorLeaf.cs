using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class RazorLeaf : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 6;                   
            item.magic = true;   
            item.width = 24;      
            item.height = 28;      //The size of the height of the hitbox in pixels.

            item.useTime = 14;     
            item.useAnimation = 14;    //How long the Weapon is used for.
            item.useStyle = 5;         //The way your Weapon will be used, 5 is the Holding Out Used for: Guns, Spellbooks, Drills, Chainsaws, Flails, Spears for example
            item.noMelee = true;     //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 1, 0, 0); 
            item.rare = 2;   
            item.mana = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; 
            item.shoot = 206;    
            item.shootSpeed = 14f;    
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Razor Leaf");
			Tooltip.SetDefault("Casts a flurry of leaves.");
		}

     
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
              int numberProjectiles = 3; 
              for (int i = 0; i < numberProjectiles; i++)
              {
                  Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)); 
                  Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
              }
              return false;
        }  
		public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Everleaf", 6);
			recipe.AddIngredient(ItemID.Book);
            recipe.AddTile(TileID.Bookcases);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
