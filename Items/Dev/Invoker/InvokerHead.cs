using System;
using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
	[AutoloadEquip(EquipType.Legs)]
	public class InvokerHead : EquipTexture
	{
		public override bool DrawHead()
		{
			return false;
		}
	}
}