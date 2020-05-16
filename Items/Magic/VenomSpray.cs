using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class VenomSpray : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;                        
            item.magic = true;                     
            item.width = 24;
            item.height = 28;
            item.useStyle = ItemUseStyleID.HoldingOut;        
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 0, 20, 0);
            item.rare = ItemRarityID.Blue;
            item.mana = 5;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.useTime = 12;
            item.useAnimation = 12;
            item.shoot = mod.ProjectileType("Venom");
            item.shootSpeed = 9f;    
        }   

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Venom Spray");
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
            recipe.AddIngredient(null, "AbyssiumBar", 10);
            recipe.AddTile(TileID.Bookcases);   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
