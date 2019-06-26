namespace AAMod.Items.DevTools
{
    public class StormTest : BaseAAItem
	{
		public override void SetStaticDefaults()
		{	
			DisplayName.SetDefault("[DEV] Storm Test");
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Tests Shen Doragon's lightning breath" });					
		}			
		
        public override void SetDefaults()
        {
            item.damage = 10000;
            item.melee = true;
            item.width = 64;
            item.height = 70;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 0;
            item.rare = 11;
            item.autoReuse = true;
            item.useTurn = true;
            item.expert = true;
            item.shoot = mod.ProjectileType("ChaosLightning");
            item.shootSpeed = 9f;
        }
    }
}