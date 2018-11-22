using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee  //where is located
{
    public class CthulhusBlade : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetDefaults()
        {

            item.damage = 23;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 48;              //Sword width
            item.height = 52;             //Sword height
            item.useTime = 22;          //how fast 
            item.useAnimation = 22;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 7;      //Sword knockback
            item.value = 19000;        
            item.rare = 3;
            item.UseSound = SoundID.Item1;                  //1 is the sound of the sword
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;   
            item.glowMask = customGlowMask;            
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Cthulhu's Blade");
      Tooltip.SetDefault("");
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

    }
}
