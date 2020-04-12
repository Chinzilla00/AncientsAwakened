using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class GoldDigger : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 40;                        
            item.magic = true;                     
            item.width = 46;
            item.height = 46;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;     
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;
            item.mana = 5;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Dig");
            item.shootSpeed = 13f;     
        }   

    public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Digger");
            Tooltip.SetDefault(@"Fires a projectile that, upon collision with a tile, creates a fountain of coins
Only 1 fountain may be active at once");
            Item.staff[item.type] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldOre, 30);
            recipe.AddIngredient(null, "StoneShell", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumOre, 30);
            recipe.AddIngredient(null, "StoneShell", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
