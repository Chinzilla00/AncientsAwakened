using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Legs)]
	public class InvokedCaligulaLegs : EquipTexture
	{
		public override bool DrawLegs()
		{
			return false;
		}
	}
}