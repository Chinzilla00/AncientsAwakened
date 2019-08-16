using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class AvesInABox : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Aves In A Box");
			AddMapEntry(new Color(100, 200, 100), name);
			dustType = DustID.t_LivingWood;
			disableSmartCursor = true;
		}

        public bool Quack = false;
        public int QuackTimer = 90;

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return true;
        }

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("AvesInABox"));
		}

		public override void RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
            if (Quack == false)
            {
                QuackTimer = 90;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/QUAK"));
            }
            Quack = true;
		}
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (QuackTimer <= 0)
            {
                frame = 0;
                Quack = false;
            }
            if (Quack)
            {
                frame = 1;
                QuackTimer--;
            }
        }
    }
}