using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class AshWall : ModWall
	{
        public override void SetDefaults()
        {
            drop = mod.ItemType("TorchashWall");
            AddMapEntry(new Color(16, 11, 26));
        }
    }
}