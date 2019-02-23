using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using System.Diagnostics;
using System.Collections.Generic;
using Terraria.Enums;
using System;
using BaseMod;
using Terraria.ID;

namespace AAMod.Tiles
{
    class DracoAltar : ModTile
	{
        Texture2D SunTexture = null;

		public override void SetDefaults()
		{
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            ModTranslation name = CreateMapEntryName();
            TileObjectData.addTile(Type);
            minPick = 200;
            mineResist = 3f;
            disableSmartCursor = true;
			name.SetDefault("Draconian Sun Pedestal");
            dustType = mod.DustType("AkumaDust");
            AddMapEntry(new Color(200, 50, 0), name);
		}

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            for (int num66 = 0; num66 < 58; num66++)
            {
                if (!AAWorld.downedYamata && player.inventory[num66].type == mod.ItemType("DracoSigil") && player.inventory[num66].stack > 0)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("You NEED to use that sigil on the altar at the center of the mire! Trust me, nothing bad will happen!", new Color(180, 41, 32), false);
                    return;
                }
                if (!Main.dayTime && player.inventory[num66].type == mod.ItemType("DracoSigil") && player.inventory[num66].stack > 0)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Geez, kid. Can't a dragon get a little shut-eye? Come back in the morning.", new Color(180, 41, 32), false);
                    return;
                }
                if (NPC.AnyNPCs(mod.NPCType("Akuma")) && player.inventory[num66].type == mod.ItemType("DracoSigil") && player.inventory[num66].stack > 0)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey kid, that Sigil only works once, ya know", new Color(180, 41, 32), false);
                    return;
                }
                if (NPC.AnyNPCs(mod.NPCType("AkumaA")) && player.inventory[num66].type == mod.ItemType("DracoSigil") && player.inventory[num66].stack > 0)
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("Hey kid, that Sigil only works once, ya know", new Color(146, 30, 68), false);
                    return;
                }
                for (int m = 0; m < Main.maxProjectiles; m++)
                {
                    Projectile p = Main.projectile[m];
                    if (p != null && p.active && p.type == mod.ProjectileType("AkumaTransition") && player.inventory[num66].type == mod.ItemType("DracoSigil") && player.inventory[num66].stack > 0)
                    {
                        return;
                    }
                    if (p != null && p.active && p.type == mod.ProjectileType("ShenTransition") && player.inventory[num66].type == mod.ItemType("ChaosSigil") && player.inventory[num66].stack > 0)
                    {
                        return;
                    }
                    if (p != null && p.active && p.type == mod.ProjectileType("ShenSpawn") && player.inventory[num66].type == mod.ItemType("ChaosSigil") && player.inventory[num66].stack > 0)
                    {
                        return;
                    }
                }
                if (player.inventory[num66].type == mod.ItemType("DracoSigil") && player.inventory[num66].stack > 0)
                {
                    SpawnBoss(player, "Akuma", "Akuma");
                    Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                    if (!AAWorld.downedAkuma)
                    {
                        Main.NewText("Heh, I hope you’re ready to feel the fury of the blazing sun kid.", new Color(180, 41, 32));
                        return;
                    }
                    if (AAWorld.downedAkuma)
                    {
                        Main.NewText("Back for more, kid? Don’t you have better things to do? You already beat me once.  Alright, but I won’t go easy on you.", new Color(180, 41, 32));
                        return;
                    }
                }
                if ((NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Shen.ShenDoragon>()) || NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Shen.ShenA>()) && player.inventory[num66].type == mod.ItemType("ChaosSigil") && player.inventory[num66].stack > 0))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("HAH! I WISH there were two of me to smash you into the ground!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                    return;
                }
                if (player.inventory[num66].type == mod.ItemType("Chaosigil") && player.inventory[num66].stack > 0)
                {
                    if (AAWorld.ShenSummoned)
                    {
                        Main.NewText(AAWorld.downedShen ? "Big mistake, child..." : "Hmpf...Again..? Alright, let's just get this done and overwith.", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

                        SpawnBoss(player, "ShenDoragon", "Shen Doragon; Draconian Doomsayer");
                    }
                    if (!AAWorld.ShenSummoned)
                    {
                        SpawnBoss(player, "ShenSpawn", "Shen Doragon; Draconian Doomsayer");
                        AAWorld.ShenSummoned = true;
                        Main.PlaySound(SoundID.Roar, player.position, 0);
                    }
                    Main.PlaySound(SoundID.Roar, player.position, 0);
                    return;

                }
            }
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 400f);
                Main.npc[npcID].netUpdate2 = true;

            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("DracoAltar"));
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            if (SunTexture == null)
            {
                if (!AAWorld.downedAllAncients)
                {
                    SunTexture = mod.GetTexture("Tiles/DracoAltarSun");
                }
                else
                {
                    SunTexture = mod.GetTexture("Tiles/DiscordianEclipse");
                }
            }
            Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int num3 = 0;
            int num8 = 16;
            for (int num301 = 0; num301 < num3; num301++)
            {
                int num302 = Main.specX[num301];
                int num303 = Main.specY[num301];
                Tile tile6 = Main.tile[num302, num303];
                ushort type4 = tile6.type;
                short frameX = tile6.frameX;
                Vector2 SunVector1 = new Vector2((num302 * 16) - (int)Main.screenPosition.X + (num8 / 2f), (num303 * 16) - (int)Main.screenPosition.Y - 36) + zero;
                Rectangle source = new Rectangle(0, 0, SunTexture.Width, SunTexture.Height);
                Color color = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, 0);
                Vector2 origin = new Vector2((float)(SunTexture.Width / 2), (float)(SunTexture.Height / 2));
                if (NPC.downedMoonlord && !AAWorld.downedAllAncients)
                {
                    Main.spriteBatch.Draw(SunTexture, SunVector1, new Rectangle?(source), color, Main.sunCircle, origin, 1f, SpriteEffects.None, 0f);
                }
                if (NPC.downedMoonlord && AAWorld.downedAllAncients)
                {
                    Main.spriteBatch.Draw(SunTexture, SunVector1, new Rectangle?(source), color, Main.sunCircle, origin, 1f, SpriteEffects.None, 0f);
                }
            }
        }

        public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.showItemIcon = true;
			player.showItemIcon2 = mod.ItemType("DracoAltar");
		}
	}
}
