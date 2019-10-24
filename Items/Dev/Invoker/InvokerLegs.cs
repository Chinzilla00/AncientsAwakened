using Terraria.ModLoader;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.Legs)]
	public class InvokerLegs : EquipTexture
	{
		public override bool DrawLegs()
		{
			return false;
		}
	}
}