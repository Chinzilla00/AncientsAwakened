using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem   //where is located
{
    public class PrismaticGreatsword : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 42;            
            item.melee = true;            
            item.width = 58;              
            item.height = 60;             
            item.useTime = 20;          
            item.useAnimation = 20;     
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
			recipe.AddIngredient(null, "Poppy", 1);
            recipe.AddIngredient(null, "AmethystGreatsword", 1);
            recipe.AddIngredient(null, "EmeraldGreatsword", 1);
            recipe.AddIngredient(null, "RubyGreatsword", 1);
            recipe.AddIngredient(null, "SapphireGreatsword", 1);
            recipe.AddIngredient(null, "TopazGreatsword", 1);
            recipe.AddIngredient(null, "AmberGreatsword", 1);
            recipe.AddIngredient(null, "DiamondGreatsword", 1);
            recipe.AddIngredient(ItemID.BeamSword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
