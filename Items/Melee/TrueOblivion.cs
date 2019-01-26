using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TrueOblivion : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Oblivion");
            Tooltip.SetDefault("Unleash the true power!");
        }
        public override void SetDefaults()
        {

            item.damage = 250;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 68;              //Sword width
            item.height = 68;             //Sword height
            item.useTime = 11;          //how fast 
            item.useAnimation = 11;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5;      //Sword knockback
            item.value = 2000000;        
            item.rare = 10;
            item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Oblivion", 1);
			recipe.AddIngredient(3467, 12);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);			//you need 1 DirtBlock
            recipe.AddTile(null, "QuantumFusionAccelerator");   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
