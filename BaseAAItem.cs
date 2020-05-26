using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod
{
    public abstract class BaseAAItem : ModItem
    {
		public const int GLOWMASKTYPE_NONE = -1;	 //for shit like Daystorm which is a 'projectile' gun
		public const int  GLOWMASKTYPE_SWORD = 0; //for swords and swordlike items
		public const int GLOWMASKTYPE_GUN = 1; //for guns and gunlike items (bows too)
        public int AARarity = 0;

        //glowmask shenanigans
        public string glowmaskTexture = null;
		public int glowmaskDrawType = 0;
		public Color glowmaskDrawColor = Color.White;	//default white	

        //custom name color
        public Color? customNameColor = null;

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
			if(glowmaskTexture != null)
			{
	            Texture2D texture = mod.GetTexture(glowmaskTexture);
				spriteBatch.Draw
				(
					texture,
					new Vector2
					(
						item.position.X - Main.screenPosition.X + item.width * 0.5f,
						item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
					),
					new Rectangle(0, 0, texture.Width, texture.Height),
					Color.White,
					rotation,
					texture.Size() * 0.5f,
					scale,
					item.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
					0f
				);
			}
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (customNameColor != null)
            {
                foreach (TooltipLine line2 in list)
                {
                    if (line2.mod == "Terraria" && line2.Name == "ItemName")
                    {
                        line2.overrideColor = (Color)customNameColor;
                    }
                }
                return;
            }
	    
	    
            if (item.modItem is BaseAAItem AAitem && AAitem.AARarity != 0)
            {
                Color Rare;
                switch (AAitem.AARarity)
                {
                    default: Rare = Color.White; break;
                    case 12: Rare = AAColor.Rarity12; break; //Ashe and Haruka
                    case 13: Rare = AAColor.Rarity13; break; //Ancients
                    case 14: Rare = AAColor.Rarity14; break; //Super Ancients	
                    case 15: Rare = AAColor.Rarity15; break; //Hyper Ancients				
                }
                foreach (TooltipLine line2 in list)
                {
                    if (line2.mod == "Terraria" && line2.Name == "ItemName")
                    {
                        line2.overrideColor = Rare;
                    }
                }
            }
        }

		//DO NOT FUCK WITH THIS!! EDITING THIS COULD BREAK ITEMS BADLY!!!
		public override ModItem NewInstance(Item itemClone)
		{
			BaseAAItem newItem = (BaseAAItem)base.NewInstance(itemClone);
			newItem.glowmaskTexture = glowmaskTexture;
			newItem.glowmaskDrawType = glowmaskDrawType;
			newItem.glowmaskDrawColor = glowmaskDrawColor;
			newItem.customNameColor = customNameColor;
            return newItem;
		}
	}
}
