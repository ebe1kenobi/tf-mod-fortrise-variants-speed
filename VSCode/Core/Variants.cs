using FortRise;

namespace TFModFortRiseVariantSpeed
{
  public class Variants : IRegisterable
  {
    public static IVariantEntry Speedx1 = null!;
    public static IVariantEntry Speedx2 = null!;
    public static IVariantEntry Speedx3 = null!;
    public static IVariantEntry Speedx4 = null!;
    public static IVariantEntry Speedx5 = null!;

    public static void Register(IModContent content, IModRegistry registry)
    {
      Speedx1 = registry.Variants.RegisterVariant("Speedx1", new()
      {
        Title = "Speedx1",
        Flags = CustomVariantFlags.None,
        Icon = TextureRegistry.Speedx1
      });
      Speedx2 = registry.Variants.RegisterVariant("Speedx2", new()
      {
        Title = "Speedx2",
        Flags = CustomVariantFlags.None,
        Icon = TextureRegistry.Speedx2
      });

      Speedx3 = registry.Variants.RegisterVariant("Speedx3", new()
      {
        Title = "Speedx3",
        Flags = CustomVariantFlags.None,
        Icon = TextureRegistry.Speedx3
      });

      Speedx4 = registry.Variants.RegisterVariant("Speedx4", new()
      {
        Title = "Speedx4",
        Flags = CustomVariantFlags.None,
        Icon = TextureRegistry.Speedx4
      });

      Speedx5 = registry.Variants.RegisterVariant("Speedx5", new()
      {
        Title = "Speedx5",
        Flags = CustomVariantFlags.None,
        Icon = TextureRegistry.Speedx5
      });


    }
  }
}
