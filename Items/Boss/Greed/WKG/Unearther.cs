using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class Unearther : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unearther");
            Tooltip.SetDefault("Mines ores even faster");
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.melee = true;
            item.width = 44;
            item.height = 44;
            item.useAnimation = 10;
            item.useTime = 5;
            item.pick = 230;
            item.tileBoost += 4;
            item.useStyle = 1;
            item.knockBack = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
            if (Main.tileValue[tile.type] > 0 && PickCheck(tile, item.pick))
            {
                player.PickTile(Player.tileTargetX, Player.tileTargetY, 5000);
            }
            return true;
        }
        public bool PickCheck(Tile tile, int pickPower)
        {
            ModTile tile2 = TileLoader.GetTile(tile.type);
            if (tile.type == 211 && pickPower < 200)
            {
                return false;
            }
            else if ((tile.type == 25 || tile.type == 203) && pickPower < 65)
            {
                return false;
            }
            else if (tile.type == 117 && pickPower < 65)
            {
                return false;
            }
            else if (tile.type == 37 && pickPower < 50)
            {
                return false;
            }
            else if (tile.type == 404 && pickPower < 65)
            {
                return false;
            }
            else if ((tile.type == 22 || tile.type == 204) && pickPower < 55)
            {
                return false;
            }
            else if (tile.type == 56 && pickPower < 65)
            {
                return false;
            }
            else if (tile.type == 58 && pickPower < 65)
            {
                return false;
            }
            else if ((tile.type == 226 || tile.type == 237) && pickPower < 210)
            {
                return false;
            }
            else if (tile.type == 107 && pickPower < 100)
            {
                return false;
            }
            else if (tile.type == 108 && pickPower < 110)
            {
                return false;
            }
            else if (tile.type == 111 && pickPower < 150)
            {
                return false;
            }
            else if (tile.type == 221 && pickPower < 100)
            {
                return false;
            }
            else if (tile.type == 222 && pickPower < 110)
            {
                return false;
            }
            else if (tile.type == 223 && pickPower < 150)
            {
                return false;
            }
            else if (tile2 != null && pickPower < tile2.minPick)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
