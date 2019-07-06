using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class PrismaticGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 37;            
            item.melee = true;            
            item.width = 58;              
            item.height = 60;             
            item.useTime = 16;          
            item.useAnimation = 16;     
            item.useStyle = 1;        
            item.knockBack = 5;      
            item.value = 20000;        
            item.rare = 4;
            item.UseSound = new LegacySoundStyle(2, 8, Terraria.Audio.SoundType.Sound);
            item.autoReuse = true;   
            item.useTurn = true; 
			item.shoot = mod.ProjectileType("PrismBolt");
			item.shootSpeed = 13f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Greatsword");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(null, "Poppy", 1);
            recipe.AddIngredient(null, "AmethystGreatsword", 1);
            recipe.AddIngredient(null, "EmeraldGreatsword", 1);
            recipe.AddIngredient(null, "RubyGreatsword", 1);
            recipe.AddIngredient(null, "SapphireGreatsword", 1);
            recipe.AddIngredient(null, "TopazGreatsword", 1);
            recipe.AddIngredient(null, "AmberGreatsword", 1);
            recipe.AddIngredient(null, "DiamondGreatsword", 1); ;
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
