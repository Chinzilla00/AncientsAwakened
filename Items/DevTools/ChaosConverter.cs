
using Terraria;
using Terraria.ModLoader;
using AAMod.Worldgen;

namespace AAMod.Items.DevTools
{
    public class ChaosConverter : BaseAAItem
	{
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("[DEV] Chaos Converter");
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Converts a strand of Mire or Inferno down below you." });					
		}			
		
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.rare = 10;
            item.value = 0;
			item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;		
        }

        public bool flag = false;

        public override bool UseItem(Player player)
        {
            if (flag)
            {
                ConversionHandler.ConvertDown((int)(player.Center.X / 16f), (int)(player.Bottom.Y / 16f) + 3, 40, ConversionHandler.CONVERTID_INFERNO);
            }
            else
            {
                ConversionHandler.ConvertDown((int)(player.Center.X / 16f), (int)(player.Bottom.Y / 16f) + 3, 40, ConversionHandler.CONVERTID_MIRE);
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