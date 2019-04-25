namespace AAMod.NPCs.Bosses.Shen
{
    public class ChaosBlastBlue : ChaosBlast
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blade of Blazing Fury");
		}

        public override void SetDefaults()
        {
           base.SetDefaults();
		   swordType = 2;
		   offsetLeft = true;
		}	
    }
}