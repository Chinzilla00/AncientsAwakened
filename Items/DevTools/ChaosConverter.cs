using AAMod.Misc;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.DevTools
{
    public class ChaosConverter : BaseAAItem
	{
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("[DEV] Chaos Converter");
            BaseUtility.AddTooltips(item, new string[] { "Converts a strand of Mire or Inferno down below you." });					
		}			
		
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.rare = ItemRarityID.Red;
            item.value = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 45;
            item.useTime = 45;		
        }

        public bool flag = false;

        public override bool UseItem(Player player)
        {
            if (flag)
            {
                ConversionHandler.ConvertDown((int)(player.Center.X / 16f), (int)(player.Bottom.Y / 16f) + 3, 40, ConversionType.INFERNO);
            }
            else
            {
                ConversionHandler.ConvertDown((int)(player.Center.X / 16f), (int)(player.Bottom.Y / 16f) + 3, 40, ConversionType.MIRE);
            }
            flag = false;
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            flag = true;
            return true;
        }
    }
}