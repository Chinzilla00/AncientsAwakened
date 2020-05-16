using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class JungleReaper : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 13;            
            item.melee = true;            
            item.width = 78;              
            item.height = 60;             
            item.useTime = 30;          
            item.useAnimation = 30;     
            item.useStyle = ItemUseStyleID.SwingThrow;        
            item.knockBack = 3;      
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;       
            item.autoReuse = false;   
            item.useTurn = false;
            item.shoot = mod.ProjectileType("JungleReaperP");
            item.shootSpeed = 8f;                                 
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Jungle Reaper");
      Tooltip.SetDefault("It's a scythe. Calm down Welox.");
    }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddRecipeGroup("AAMod:Gold", 15);
            recipe.AddTile(TileID.LivingLoom);   
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
