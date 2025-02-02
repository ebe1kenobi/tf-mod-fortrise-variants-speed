using FortRise;
using Monocle;

namespace TFModFortRiseVariantSpeed
{
  [Fort("com.ebe1.kenobi.tfmodfortrisevariantspeed", "TFModFortRiseVariantSpeed")]
  public class TFModFortRiseVariantSpeedModule : FortModule
  {
    public static TFModFortRiseVariantSpeedModule Instance;
    public static Atlas SpeedAtlas;

    public TFModFortRiseVariantSpeedModule()
    {
      Instance = this;
      //Logger.Init("VariantSpeedLOG");
    }

    public override void LoadContent()
    {
      SpeedAtlas = Content.LoadAtlas("Atlas/atlas.xml", "Atlas/atlas.png");
    }

    public override void Load()
    {
      MyLevel.Load();
    }

    public override void Unload()
    {
      MyLevel.Unload();
    }

    public override void OnVariantsRegister(VariantManager manager, bool noPerPlayer = false)
    {
      var info1x1 = new CustomVariantInfo(
          "SpeedGamex1.1", VariantManager.GetVariantIconFromName("SpeedGamex1.1", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x2 = new CustomVariantInfo(
          "SpeedGamex1.2", VariantManager.GetVariantIconFromName("SpeedGamex1.2", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x3 = new CustomVariantInfo(
          "SpeedGamex1.3", VariantManager.GetVariantIconFromName("SpeedGamex1.3", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x4 = new CustomVariantInfo(
          "SpeedGamex1.4", VariantManager.GetVariantIconFromName("SpeedGamex1.4", SpeedAtlas),
          CustomVariantFlags.None
          );
      var info1x5 = new CustomVariantInfo(
          "SpeedGamex1.5", VariantManager.GetVariantIconFromName("SpeedGamex1.5", SpeedAtlas),
          CustomVariantFlags.None
          );

      manager.AddVariant(info1x1);
      manager.AddVariant(info1x2);
      manager.AddVariant(info1x3);
      manager.AddVariant(info1x4);
      manager.AddVariant(info1x5);
    }
  }
}
