using Monocle;
using FortRise;

namespace TFModFortRiseVariantSpeed
{
  public class MyLevel {

    internal static void Load()
    {
      On.TowerFall.Level.Update += Update_patch;
    }

    internal static void Unload()
    {
      On.TowerFall.Level.Update -= Update_patch;
    }
    public static void Update_patch(On.TowerFall.Level.orig_Update orig, global::TowerFall.Level self) {

      orig(self);
      if (VariantManager.GetCustomVariant("SpeedGamex1.1"))
      {
        Engine.TimeRate = 1.1f;
      }
      else if (VariantManager.GetCustomVariant("SpeedGamex1.2"))
      {
        Engine.TimeRate = 1.2f;
      }
      else if (VariantManager.GetCustomVariant("SpeedGamex1.3"))
      {
        Engine.TimeRate = 1.3f;
      }
      else if (VariantManager.GetCustomVariant("SpeedGamex1.4"))
      {
        Engine.TimeRate = 1.4f;
      }
      else if (VariantManager.GetCustomVariant("SpeedGamex1.5"))
      {
        Engine.TimeRate = 1.5f;
      }
    }
  }
}
