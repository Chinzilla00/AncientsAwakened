using Terraria.ModLoader;
namespace AAMod.NPCs.Bosses.Orthrus
{
    [AutoloadBossHead]
    public class OrthrusHead2 : OrthrusHead1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            leftHead = true;
        }
    }
}