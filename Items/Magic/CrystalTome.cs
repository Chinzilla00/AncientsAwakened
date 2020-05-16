using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic       
{
    public class CrystalTome : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 33;                   
            item.magic = true;   
            item.width = 24;
            item.height = 28;
            item.useTime = 14;     
            item.useAnimation = 14; 
            item.useStyle = ItemUseStyleID.HoldingOut;      
            item.noMelee = true;    
            item.knockBack = 1;  
            item.value = Item.sellPrice(0, 5, 0, 0); 
            item.rare = ItemRarityID.Lime;   
            item.mana = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true; 
            item.shoot = ProjectileID.CrystalBullet;    
            item.shootSpeed = 8f;    
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Tome");
			Tooltip.SetDefault("Casts crystals that shatter into pieces");
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
              int numberProjectiles = 1 + Main.rand.Next(3); 
              for (int i = 0; i < numberProjectiles; i++)
              {
                  Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20)); 
                  int p = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                  Main.projectile[p].magic = true;
              }
              return false;
        }  

		public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PixieDust, 18);   
			recipe.AddIngredient(ItemID.CrystalShard, 16);
            recipe.AddIngredient(ItemID.CrystalStorm, 1);
            recipe.AddTile(TileID.Bookcases);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
