using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class GaleOfWings : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 50;                        
            item.magic = true;                     
            item.width = 24;
            item.height = 28;
            item.useStyle = 5;        
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 7;
            item.mana = 8;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.shoot = mod.ProjectileType("Gale");
            item.shootSpeed = 9f;    
        }   

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Gale of Wings");
          Tooltip.SetDefault("");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY), item.shoot, item.damage, item.knockBack, Main.myPlayer);
            return false;
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(null, "GoddessFeather", 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
