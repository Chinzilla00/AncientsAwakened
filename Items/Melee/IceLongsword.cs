using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class IceLongsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 26;          
            item.melee = true;            
            item.width = 62;             
            item.height = 64;             
            item.useTime = 23;         
            item.useAnimation = 23;     
            item.useStyle = ItemUseStyleID.SwingThrow;        
            item.knockBack = 2;     
            item.value = 8000;        
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;      
            item.autoReuse = true;   
            item.useTurn = false;
            item.shoot = mod.ProjectileType("IceChunk");
            item.shootSpeed = 14f;                        
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ice Longsword");
      Tooltip.SetDefault("Chuck literal ice at your foes instead of that wimpy little snow bolt");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.IceBlade, 1);  
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddIngredient(ItemID.SnowBlock, 100);
            recipe.AddIngredient(null, "SnowMana", 3);
            recipe.AddTile(TileID.Anvils); 
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
