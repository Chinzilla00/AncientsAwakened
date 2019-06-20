using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class CrystalTome : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 33;   //The damage stat for the Weapon.                      
            item.magic = true;   //This defines if it does magic damage and if its effected by magic increasing Armor/Accessories.
            item.width = 24;      //The size of the width of the hitbox in pixels.
            item.height = 28;      //The size of the height of the hitbox in pixels.
            item.useTime = 14;     //How fast the Weapon is used.
            item.useAnimation = 14;    //How long the Weapon is used for.
            item.useStyle = 5;         //The way your Weapon will be used, 5 is the Holding Out Used for: Guns, Spellbooks, Drills, Chainsaws, Flails, Spears for example
            item.noMelee = true;     //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.knockBack = 1;  //The knockback stat of your Weapon.      
            item.value = Item.sellPrice(0, 5, 0, 0); // How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 10gold)
            item.rare = 7;   //The color the title of your Weapon when hovering over it ingame
            item.mana = 9;//How many mana this weapon use
            item.UseSound = SoundID.Item1; //item.UseSound = SoundID.Item1;   //The sound played when using your Weapon
            item.autoReuse = true; //Weather your Weapon will be used again after use while holding down, if false you will need to click again after use to use it again.
            item.shoot = 89;  //This defines what type of projectile this weapon will shoot  
            item.shootSpeed = 8f;    //This defines the projectile speed when shoot
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Tome");
			Tooltip.SetDefault("Casts crystals that shatter into pieces");
		}

     
        //--------------------------------------------------Shotgun style: Multiple Projectiles, Random spread ---------------------------------------------------------
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
              int numberProjectiles = 1 + Main.rand.Next(3); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
              for (int i = 0; i < numberProjectiles; i++)
              {
                  Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20)); // This defines the projectiles random spread . 30 degree spread.
                  Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
              }
              return false;
        }  
		public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PixieDust, 18);   //you need 1 DirtBlock
			recipe.AddIngredient(ItemID.CrystalShard, 16);
            recipe.AddIngredient(ItemID.CrystalStorm, 1);
            recipe.AddTile(TileID.WorkBenches);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
