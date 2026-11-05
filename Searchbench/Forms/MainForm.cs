using System.Globalization;
using SearchBench.Models;
using SearchBench.Services;

namespace SearchBench.Forms
{
    /// <summary>
    /// Ventana principal. Actúa únicamente como puente visual: captura los eventos del usuario,
    /// valida la entrada y delega TODO el trabajo (generación de datos, ordenamiento, hash y búsquedas)
    /// a los servicios. No contiene ningún algoritmo de búsqueda.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly DataSetService _dataSetService;
        private readonly SearchComparisonService _comparisonService;

        /// <summary>Contador de ejecuciones; sirve para numerar y alternar el color de cada grupo de filas.</summary>
        private int _runCounter;

        public MainForm()
        {
            InitializeComponent();

            // Composición de dependencias: el formulario solo conoce a los servicios, no a los algoritmos.
            _dataSetService = new DataSetService(new DataGeneratorService(), new HashTableService());
            _comparisonService = new SearchComparisonService(new SequentialSearchService(), new BinarySearchService());

            InitializeUiState();
        }

        // ------------------------------------------------------------------
        // Inicialización de la interfaz
        // ------------------------------------------------------------------

        /// <summary>Prepara listas desplegables, tabla y estado inicial de los controles.</summary>
        private void InitializeUiState()
        {
            // N puede escribirse libremente o elegirse de la lista.
            cmbSize.Items.AddRange(new object[] { "1,000", "10,000", "100,000", "1,000,000" });
            cmbSize.Text = "1,000";

            cmbAlgorithm.Items.AddRange(new object[]
            {
                new AlgorithmOption(SearchAlgorithm.Sequential, "Búsqueda secuencial"),
                new AlgorithmOption(SearchAlgorithm.Binary, "Búsqueda binaria"),
                new AlgorithmOption(SearchAlgorithm.Hash, "Tabla hash")
            });
            cmbAlgorithm.SelectedIndex = 0;

            ConfigureGrid();

            // Sin datos no se puede buscar: se habilitan hasta generar el banco.
            SetSearchControlsEnabled(false);
        }

        /// <summary>Ajustes visuales de la tabla: alineación, formato numérico y sin ordenamiento por columna.</summary>
        private void ConfigureGrid()
        {
            foreach (DataGridViewColumn column in dgvResults.Columns)
            {
                // Ordenar por columna rompería el agrupamiento de filas por ejecución.
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DataGridViewColumn[] numericColumns =
            {
                colRun, colN, colKey, colIndex, colComparisons, colIterations, colMicroseconds, colMilliseconds
            };
            foreach (DataGridViewColumn column in numericColumns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            colN.DefaultCellStyle.Format = "N0";
            colComparisons.DefaultCellStyle.Format = "N0";
            colIterations.DefaultCellStyle.Format = "N0";
            colIndex.DefaultCellStyle.Format = "N0";
            colMicroseconds.DefaultCellStyle.Format = "N3";
            colMilliseconds.DefaultCellStyle.Format = "N6";
        }

        private void SetSearchControlsEnabled(bool enabled)
        {
            txtKey.Enabled = enabled;
            btnExistingKey.Enabled = enabled;
            btnMissingKey.Enabled = enabled;
            cmbAlgorithm.Enabled = enabled;
            btnSingleSearch.Enabled = enabled;
            btnCompare.Enabled = enabled;
        }

        // ------------------------------------------------------------------
        // Eventos: generación del banco de datos
        // ------------------------------------------------------------------

        private void btnGenerate_Click(object? sender, EventArgs e)
        {
            if (!TryReadSize(out int size))
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                _dataSetService.Generate(size);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            ShowDataSetInfo();
            SetSearchControlsEnabled(true);
            txtKey.Clear();
            txtKey.Focus();
        }

        private void cmbSize_KeyDown(object? sender, KeyEventArgs e)
        {
            // Enter en el campo N equivale a presionar "Generar datos".
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnGenerate.PerformClick();
            }
        }

        // ------------------------------------------------------------------
        // Eventos: claves de apoyo y búsquedas
        // ------------------------------------------------------------------

        private void btnExistingKey_Click(object? sender, EventArgs e)
        {
            if (!EnsureDataExists())
            {
                return;
            }

            txtKey.Text = _dataSetService.GetRandomExistingKey().ToString(CultureInfo.InvariantCulture);
        }

        private void btnMissingKey_Click(object? sender, EventArgs e)
        {
            if (!EnsureDataExists())
            {
                return;
            }

            txtKey.Text = _dataSetService.GetMissingKey().ToString(CultureInfo.InvariantCulture);
        }

        private void btnSingleSearch_Click(object? sender, EventArgs e)
        {
            if (!EnsureDataExists() || !TryReadKey(out int key))
            {
                return;
            }

            if (cmbAlgorithm.SelectedItem is not AlgorithmOption option)
            {
                ShowWarning("Selecciona el algoritmo que deseas ejecutar.");
                return;
            }

            SearchResult result = _comparisonService.RunSingle(option.Value, _dataSetService, key);
            AppendResults(new[] { result });
        }

        private void btnCompare_Click(object? sender, EventArgs e)
        {
            if (!EnsureDataExists() || !TryReadKey(out int key))
            {
                return;
            }

            // Las tres técnicas se ejecutan con la MISMA clave y sobre el MISMO banco de datos.
            IReadOnlyList<SearchResult> results = _comparisonService.RunAll(_dataSetService, key);
            AppendResults(results);
        }

        private void txtKey_KeyDown(object? sender, KeyEventArgs e)
        {
            // Enter en el campo de clave equivale a la búsqueda comparativa.
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCompare.PerformClick();
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            dgvResults.Rows.Clear();
            _runCounter = 0;
        }

        // ------------------------------------------------------------------
        // Validación de entradas (TryParse evita excepciones por texto inválido)
        // ------------------------------------------------------------------

        /// <summary>Lee y valida el tamaño N. Acepta separadores de miles (por ejemplo, "10,000").</summary>
        private bool TryReadSize(out int size)
        {
            string text = cmbSize.Text.Trim();

            if (!int.TryParse(text, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out size))
            {
                ShowWarning("El tamaño N debe ser un número entero válido (por ejemplo: 1000 o 10,000).");
                cmbSize.Focus();
                return false;
            }

            if (size < DataSetService.MinSize || size > DataSetService.MaxSize)
            {
                ShowWarning($"El tamaño N debe estar entre {DataSetService.MinSize:N0} y {DataSetService.MaxSize:N0}.");
                cmbSize.Focus();
                return false;
            }

            return true;
        }

        /// <summary>Lee y valida la clave a buscar (entero, con signo opcional).</summary>
        private bool TryReadKey(out int key)
        {
            string text = txtKey.Text.Trim();

            if (!int.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out key))
            {
                ShowWarning("La clave a buscar debe ser un número entero válido. " +
                            "Puedes usar los botones \"Clave existente\" o \"Clave inexistente\".");
                txtKey.Focus();
                txtKey.SelectAll();
                return false;
            }

            return true;
        }

        private bool EnsureDataExists()
        {
            if (_dataSetService.HasData)
            {
                return true;
            }

            ShowWarning("Primero genera el banco de datos con el botón \"Generar datos\".");
            return false;
        }

        // ------------------------------------------------------------------
        // Presentación de resultados
        // ------------------------------------------------------------------

        /// <summary>Muestra las estadísticas de preparación del banco de datos y de la tabla hash.</summary>
        private void ShowDataSetInfo()
        {
            DataSetStatistics? stats = _dataSetService.Statistics;
            if (stats is null)
            {
                return;
            }

            lblDataInfo.Text =
                $"N = {stats.Size:N0}   |   Rango de claves: 1 – {stats.KeyRangeMax:N0}   |   " +
                $"Generación: {stats.GenerationMilliseconds:N2} ms   |   " +
                $"Ordenamiento previo (para la binaria): {stats.SortMilliseconds:N2} ms   |   " +
                $"Construcción de la tabla hash: {stats.HashBuildMilliseconds:N2} ms" +
                Environment.NewLine +
                $"Tabla hash (encadenamiento, h(k) = k mod M): M = {stats.HashTableSize:N0} cubetas   |   " +
                $"Factor de carga: {stats.HashLoadFactor:N2}   |   " +
                $"Colisiones: {stats.HashCollisions:N0}   |   " +
                $"Cadena más larga: {stats.HashMaxChainLength}   |   " +
                $"Cubetas ocupadas: {stats.HashUsedBuckets:N0}";
        }

        /// <summary>Agrega al panel comparativo una fila por cada resultado de la ejecución actual.</summary>
        private void AppendResults(IReadOnlyList<SearchResult> results)
        {
            _runCounter++;
            Color bandColor = _runCounter % 2 == 0 ? Color.FromArgb(232, 240, 252) : Color.White;

            foreach (SearchResult result in results)
            {
                int rowIndex = dgvResults.Rows.Add(
                    _runCounter,
                    _dataSetService.Size,
                    result.Key,
                    result.AlgorithmName,
                    result.StatusText,
                    result.Found ? (object)result.Index : "—",
                    result.Comparisons,
                    result.Iterations,
                    result.ElapsedMicroseconds,
                    result.ElapsedMilliseconds,
                    result.Complexity,
                    result.Details);

                DataGridViewRow row = dgvResults.Rows[rowIndex];
                row.DefaultCellStyle.BackColor = bandColor;
                row.Cells[colStatus.Index].Style.ForeColor = result.Found ? Color.DarkGreen : Color.Firebrick;
            }

            // Se desplaza la tabla para que siempre se vea la última ejecución.
            if (dgvResults.Rows.Count > 0)
            {
                dgvResults.FirstDisplayedScrollingRowIndex = dgvResults.Rows.Count - 1;
            }

            dgvResults.ClearSelection();
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(this, message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>Elemento de la lista desplegable de algoritmos (texto visible + valor del enum).</summary>
        private sealed record AlgorithmOption(SearchAlgorithm Value, string Label)
        {
            public override string ToString() => Label;
        }
    }
}
