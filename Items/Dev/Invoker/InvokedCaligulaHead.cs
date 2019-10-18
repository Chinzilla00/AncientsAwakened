using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Head)]
	public class InvokedCaligulaHead : EquipTexture
	{
		public override bool DrawHead()
		{
			return false;
		}
	}
}