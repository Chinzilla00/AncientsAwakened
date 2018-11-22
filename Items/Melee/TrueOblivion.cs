using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class TrueOblivion : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Oblivion");
            Tooltip.SetDefault("Unleash the true power!");
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Melee/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
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
            item.glowMask = customGlowMask;
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
