using AAMod.NPCs.Bosses.Yamata.Awakened;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using BaseMod;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class ScreenFog
    {
		public int fogOffsetX = 0;
		public float fadeOpacity = 0f;
		public float dayTimeOpacity = 0f;
		public bool backgroundFog = false;
        public static bool Shadow = false;

		public ScreenFog(bool bg)
		{
			backgroundFog = bg;
		}

        public void Update(Texture2D texture)
        {
			if(Main.netMode == NetmodeID.Server || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

			Player player = Main.LocalPlayer;
			bool inMire = Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire;
			if(!backgroundFog && (BasePlayer.HasAccessory(player, AAMod.instance.ItemType("Lantern"), true, false) || AAWorld.downedYamata)) inMire = false;
            Shadow = inMire;
			
			fogOffsetX += 1;
			if(fogOffsetX >= texture.Width) fogOffsetX = 0;
			if(inMire)
			{
				fadeOpacity += 0.05f;
				if(fadeOpacity > 1f) fadeOpacity = 1f;
			}else
			{
				fadeOpacity -= 0.05f;
				if(fadeOpacity < 0f) fadeOpacity = 0f;
			}
			if(backgroundFog)
			{
				dayTimeOpacity = Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, 0.5f, 1f, 1f, 1f, 1f, 1f, 0.5f) : 0.5f;		
				dayTimeOpacity *= 0.7f; //make it fadier as it's in the background
			}else
			{
				dayTimeOpacity = Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, 1f, 1f, 1f, 1f, 1f, 1f, 1f) : 0.3f;
                dayTimeOpacity *= Main.dayTime ? 3f : 1f;
			}
        }

        public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
        {
			if(fadeOpacity == 0f) return; //don't draw if no fog
            if(setSB) Main.spriteBatch.Begin();
            Mod mod = AAMod.instance;
            Player player = Main.LocalPlayer;

            Color DefaultFog = new Color(62, 68, 100);
            Color YamataFog = new Color(100, 38, 62);

            bool YamataA = NPC.AnyNPCs(ModContent.NPCType<YamataA>());

			Color fogColor = GetAlpha(YamataA ? YamataFog : DefaultFog, 0.4f * fadeOpacity * dayTimeOpacity);

            //ensure we cover the whole screen first
            // Main.spriteBatch.Draw(texture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), null, bgColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);	

            //overlap a little so you cannot see edges

            int minX = -texture.Width;
			int minY = -texture.Height;
			int maxX = Main.screenWidth + texture.Width;
			int maxY = Main.screenHeight + texture.Height;


            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
                    if (player.position.Y < Main.worldSurface * 16.0)
                    {
                        Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? -fogOffsetX : fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                    }
                }
            }
            if(setSB) Main.spriteBatch.End();
        }

		public Color GetAlpha(Color newColor, float alph)
		{
			int alpha = 255 - (int)(255 * alph);
			float alphaDiff = (255 - alpha) / 255f;
			int newR = (int)(newColor.R * alphaDiff);
			int newG = (int)(newColor.G * alphaDiff);
			int newB = (int)(newColor.B * alphaDiff);
			int newA = newColor.A - alpha;
			if (newA < 0) newA = 0;
			if (newA > 255) newA = 255;		
			return new Color(newR, newG, newB, newA);
		}		
    }


    public class SilhouetteWorld : ModWorld
    {
        public static bool SilouetteMode = !Main.gameMenu && !Main.dayTime && Main.LocalPlayer.GetModPlayer<AAPlayer>().ZoneMire && (!BasePlayer.HasAccessory(Main.LocalPlayer, AAMod.instance.ItemType("Lantern"), true, false) || !AAWorld.downedYamata);

        public override void PreUpdate()
        {
            if (SilouetteMode)
            {
                foreach (Dust dust in Main.dust)
                {
                    dust.shader = GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye);
                }
            }
        }
    }
    public class SilhouetteProjectile : GlobalProjectile
    {
        public override void PostDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            }
        }
        public override bool PreDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye).Apply(null);
            }
            return true;
        }
    }
    public class SilhouetteNPC : GlobalNPC
    {
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color lightColor)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            }
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color lightColor)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye).Apply(null);
            }
            return true;
        }
    }
    public class SilhouetteItem : GlobalItem
    {
        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            }
        }
        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye).Apply(null);

            }
            return true;
        }
        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            }
        }
        public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (SilhouetteWorld.SilouetteMode)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye).Apply(null);

            }
            return true;
        }
    }
    public class SilhouetteTile : GlobalTile
    {
        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (i * 16 > Main.screenPosition.X - 16 && i * 16 < Main.screenPosition.X + Main.screenWidth + 16 && j * 16 > Main.screenPosition.Y - 16 && j * 16 < Main.screenPosition.Y + Main.screenHeight + 16)
            {
                if (SilhouetteWorld.SilouetteMode)
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
                }
            }

        }
        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (i * 16 > Main.screenPosition.X - 16 && i * 16 < Main.screenPosition.X + Main.screenWidth + 16 && j * 16 > Main.screenPosition.Y - 16 && j * 16 < Main.screenPosition.Y + Main.screenHeight + 16)
            {
                if (SilhouetteWorld.SilouetteMode)
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                    GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye).Apply(null);

                }
            }
            return true;
        }
    }
    public class SilhouetteWall : GlobalWall
    {
        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (i * 16 > Main.screenPosition.X - 16 && i * 16 < Main.screenPosition.X + Main.screenWidth + 16 && j * 16 > Main.screenPosition.Y - 16 && j * 16 < Main.screenPosition.Y + Main.screenHeight + 16)
            {
                if (SilhouetteWorld.SilouetteMode)
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
                }
            }
        }
        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (i * 16 > Main.screenPosition.X - 16 && i * 16 < Main.screenPosition.X + Main.screenWidth + 16 && j * 16 > Main.screenPosition.Y - 16 && j * 16 < Main.screenPosition.Y + Main.screenHeight + 16)
            {
                if (SilhouetteWorld.SilouetteMode)
                {
                    Main.spriteBatch.End();
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                    GameShaders.Armor.GetShaderFromItemId(ItemID.ShadowDye).Apply(null);

                }
            }
            return true;
        }
    }
}
