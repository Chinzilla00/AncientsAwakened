using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class Etheral : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Etheral");
			Tooltip.SetDefault(@"Fires a beam that blasts through enemies");
        }

	    public override void SetDefaults()
	    {
	        item.damage = 150;
	        item.magic = true;
	        item.mana = 15;
	        item.width = 130;
	        item.height = 46;
	        item.useTime = 10;
	        item.useAnimation = 10;
	        item.reuseDelay = 5;
	        item.useStyle = 5;
	        item.UseSound = SoundID.Item13;
	        item.noMelee = true;
			item.channel = true;
	        item.knockBack = 0f;
	        item.value = Item.sellPrice(1, 0, 0, 0); ;
            item.channel = true;
            item.shoot = mod.ProjectileType("EtheralLazer");
	        item.shootSpeed = 30f;
            
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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
                SpriteEffects.None,
                0f
            );
        }

        public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.overrideColor = new Color(159, 207, 190);
	            }
	        }
	    }
    }
}