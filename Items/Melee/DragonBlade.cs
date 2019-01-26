using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class DragonBlade : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Blade");
            Tooltip.SetDefault("Shoots tiny swords!");
        }
        public override void SetDefaults()
        {

            item.damage = 54;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 60;              //Sword width
            item.height = 60;             //Sword height
            
            item.useTime = 30;          //how fast 
            item.useAnimation = 30;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 10;        
            item.rare = 5;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = false;
			item.shoot = mod.ProjectileType("DragonSP");
			item.shootSpeed = 14f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "DragonSpirit", 30);   //you need 1 DirtBlock
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
