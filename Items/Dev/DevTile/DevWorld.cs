using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace AAMod.Items.Dev.DevTile
{
    public class DevWorld : ModWorld
	{
        public override void Initialize()
		{
            InvokerBookSetOK = true;
            CCBoxSetOK = true;
        }
        public override TagCompound Save()
		{
			List<string> list = new List<string>();
			if (InvokerBookSetOK) list.Add("InvokerBookSetOK");
            if (CCBoxSetOK) list.Add("CCBoxSetOK");
            TagCompound tagCompound = new TagCompound();
			tagCompound.Add("DevTileSet", list);
			return tagCompound;
        }

        public override void Load(TagCompound tag)
		{
            IList<string> list = tag.GetList<string>("DevTileSet");
            InvokerBookSetOK = list.Contains("InvokerBookSetOK");
            CCBoxSetOK = list.Contains("CCBoxSetOK");
        }
        public static bool InvokerBookSetOK;
        public static bool CCBoxSetOK;
    }
}