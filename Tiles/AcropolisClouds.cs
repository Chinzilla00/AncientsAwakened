using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class AcropolisClouds : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = DustID.BlueCrystalShard;
            drop = mod.ItemType("Acropolis Clouds");   
            AddMapEntry(new Color(30, 89, 125));
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseMod.BaseUtility.ColorMult(AAColor.Sky, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (tile != null && tile.active() && tile.type == this.Type)
            {
                BaseMod.BaseDrawing.DrawTileTexture(sb, Main.tileTexture[Type], x, y, true, false, false, null, AAGlobalTile.GetSkyColorBright);
            }
        }
        public override void RandomUpdate(int i, int j)
        {
            if (!NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Athena.Athena>()))
            {
                WorldGen.KillTile(i, j, false, false, true);
            }
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}