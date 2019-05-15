using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class RazorLeaf : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 6;   //The damage stat for the Weapon.                      
            item.magic = true;   //This defines if it does magic damage and if its effected by magic increasing Armor/Accessories.
            item.width = 24;      //The size of the width of the hitbox in pixels.
            item.height = 28;      //The size of the height of the hitbox in pixels.

            item.useTime = 14;     //How fast the Weapon is used.
            item.useAnimation = 14;    //How long the Weapon is used for.
            item.useStyle = 5;         //The way your Weapon will be used, 5 is the Holding Out Used for: Guns, Spellbooks, Drills, Chainsaws, Flails, Spears for example
            item.noMelee = true;     //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.knockBack = 1;
            item.value = Item.buyPrice(0, 1, 0, 0); // How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 10gold)
            item.rare = 2;   //The color the title of your Weapon when hovering over it ingame
            item.mana = 4;//How many mana this weapon use
            item.UseSound = SoundID.Item1; //item.UseSound = SoundID.Item1;   //The sound played when using your Weapon
            item.autoReuse = true; //Weather your Weapon will be used again after use while holding down, if false you will need to click again after use to use it again.
            item.shoot = 206;  //This defines what type of projectile this weapon will shoot  
            item.shootSpeed = 22f;    //This defines the projectile speed when shoot
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Razor Leaf");
			Tooltip.SetDefault("Casts a flurry of leaves.");
		}

     
        //--------------------------------------------------Shotgun style: Multiple Projectiles, Random spread ---------------------------------------------------------
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
              int numberProjectiles = 3; //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
              for (int i = 0; i < numberProjectiles; i++)
              {
                  Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // This defines the projectiles random spread . 30 degree spread.
                  Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
              }
              return false;
        }  
		public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Everleaf", 6);
			recipe.AddIngredient(ItemID.Book);
            recipe.AddTile(TileID.Bookcases);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
