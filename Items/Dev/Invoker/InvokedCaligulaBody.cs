using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Body)]
	public class InvokedCaligulaBody : EquipTexture
	{
		public override bool DrawBody()
		{
			return false;
		}
	}
}