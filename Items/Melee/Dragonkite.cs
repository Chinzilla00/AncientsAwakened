using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class Dragonkite : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonkite");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {

            
            item.damage = 260;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 176;              //Sword width
            item.height = 176;             //Sword height
            item.useTime = 30;          //how fast 
            item.useAnimation = 30;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 4;      //Sword knockback
            item.value = 20;        
            item.rare = 9;
            item.UseSound = SoundID.Item20;       //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Dragonfire", 1);
            recipe.AddIngredient(null, "RadiantIncinerite", 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 15); //you need 1 DirtBlock
            recipe.AddTile(TileID.MythrilAnvil);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
