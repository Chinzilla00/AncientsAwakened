using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using BaseMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod.Items.BossSummons
{
    public class InfinityOverloader : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Overloader");
            Tooltip.SetDefault(@"Calls the Infinity Slayer");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 500;
            item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(158, 3, 32);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "UnstableSingularity", 15);
            recipe.AddIngredient(null, "ApocalyptitePlate", 20);
            recipe.AddIngredient(null, "OroborosWood", 10);
            recipe.AddIngredient(null, "ZeroTesseract", 1);
            recipe.AddTile(null, "BinaryReassembler");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);
            Texture2D texture = mod.GetTexture("Items/BossSummons/" + GetType().Name + "_Glow");
            if (!AAWorld.downedZero)
            {
                spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                color1,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
            }
        }

        public override bool UseItem(Player player)
		{
            Main.NewText("...Target Identified.", new Color(175, 75, 255));
            SpawnBoss(player, "IZSpawn1", "Infinity Zero");
			Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
		}

		public override bool CanUseItem(Player player)
		{
            if (!AAWorld.downedZero)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Overloader lies dormant; Inactive...", Color.Indigo.R, Color.Indigo.G, Color.Indigo.B, false);
                return false;
            }
			return true;
		}

		public void SpawnBoss(Player player, string name, string displayName)
		{
			if (Main.netMode != 1)
			{
				int bossType = mod.NPCType(name);
				if(NPC.AnyNPCs(bossType)){ return; } //don't spawn if there's already a boss!
				int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
				Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 100f);
				Main.npc[npcID].netUpdate2 = true;
			}
		}	

		public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
		public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }		
	}
}