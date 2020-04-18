using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Head)]
	public class InvokerHead : EquipTexture
	{
		public override bool DrawHead()
		{
			return true;
		}
	}
}