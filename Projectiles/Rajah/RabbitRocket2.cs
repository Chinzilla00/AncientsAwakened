namespace AAMod.Projectiles.Rajah

{
    public class RabbitRocket2 : RabbitRocket1
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rocket");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            RocketType = 2;
        }
    }
}
