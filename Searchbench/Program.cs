using SearchBench.Forms;

namespace SearchBench;

/// <summary>
/// Punto de entrada de la aplicación. Solo inicializa Windows Forms
/// y abre la ventana principal; no contiene lógica de negocio.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        // Configuración estándar de .NET 8 (DPI, fuente predeterminada, estilos visuales).
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
