using System.Reflection;

namespace CleanArchitecture.Presentation;
public static class AssemblyReference
{
    // Uygulama assembly reference ile Presentation katmanının dll haline erişmiş olur.
    public static readonly Assembly Assembly = typeof(Assembly).Assembly;
}
