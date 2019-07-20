using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class AmethystGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 24;            
            item.melee = true;            
            item.width = 58;              
            item.height = 60;             
            item.useTime = 30;          
            item.useAnimation = 30;     
            item.useStyle = 1;        
            item.knockBack = 5;      
            item.value = 3000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = false;   
            item.useTurn = true;
            item.shoot = mod.ProjectileType<Projectiles.GemShot.AmethystShot>();
            item.shootSpeed = 7f;
        }

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Amethyst Greatsword");
          Tooltip.SetDefault("");
        }

        static int shoot = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
        {
            shoot++;
            if (shoot % 2 != 0) return false;

            shoot = 0;
            return true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "AmethystSaber", 1);
            recipe.AddIngredient(ItemID.LargeAmethyst, 1);		
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
