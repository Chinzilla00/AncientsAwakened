using System;
using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
	[AutoloadEquip(EquipType.Body)]
	public class InvokerBody : EquipTexture
	{
		public override bool DrawBody()
		{
			return false;
		}
	}
}