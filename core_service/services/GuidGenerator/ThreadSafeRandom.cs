using System.Runtime.CompilerServices;

namespace core_service.services.GuidGenerator;

public static class ThreadSafeRandom
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Random ObtainThreadStaticRandom() => ObtainRandom();

    private static Random ObtainRandom() => Random.Shared;
}