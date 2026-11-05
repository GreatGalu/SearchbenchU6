#nullable disable
namespace SearchBench.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpData = new System.Windows.Forms.GroupBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblSizeHint = new System.Windows.Forms.Label();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnExistingKey = new System.Windows.Forms.Button();
            this.btnMissingKey = new System.Windows.Forms.Button();
            this.lblAlgorithm = new System.Windows.Forms.Label();
            this.cmbAlgorithm = new System.Windows.Forms.ComboBox();
            this.btnSingleSearch = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblDataInfo = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblGridHint = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colRun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlgorithm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComparisons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIterations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMicroseconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMilliseconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComplexity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpData.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            //
            // grpData
            //
            this.grpData.Controls.Add(this.lblSize);
            this.grpData.Controls.Add(this.cmbSize);
            this.grpData.Controls.Add(this.btnGenerate);
            this.grpData.Controls.Add(this.lblSizeHint);
            this.grpData.Location = new System.Drawing.Point(12, 12);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(360, 122);
            this.grpData.TabIndex = 0;
            this.grpData.TabStop = false;
            this.grpData.Text = "1. Banco de datos";
            //
            // lblSize
            //
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(15, 33);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(66, 15);
            this.lblSize.TabIndex = 0;
            this.lblSize.Text = "Tamaño N:";
            //
            // cmbSize
            //
            this.cmbSize.Location = new System.Drawing.Point(100, 29);
            this.cmbSize.MaxLength = 9;
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(130, 23);
            this.cmbSize.TabIndex = 1;
            this.cmbSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSize_KeyDown);
            //
            // btnGenerate
            //
            this.btnGenerate.Location = new System.Drawing.Point(245, 26);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 30);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generar datos";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            //
            // lblSizeHint
            //
            this.lblSizeHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSizeHint.Location = new System.Drawing.Point(15, 70);
            this.lblSizeHint.Name = "lblSizeHint";
            this.lblSizeHint.Size = new System.Drawing.Size(330, 40);
            this.lblSizeHint.TabIndex = 3;
            this.lblSizeHint.Text = "Escribe un valor o elige uno de la lista.\r\nRango permitido: 1 a 1,000,000 de elementos.";
            //
            // grpSearch
            //
            this.grpSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSearch.Controls.Add(this.lblKey);
            this.grpSearch.Controls.Add(this.txtKey);
            this.grpSearch.Controls.Add(this.btnExistingKey);
            this.grpSearch.Controls.Add(this.btnMissingKey);
            this.grpSearch.Controls.Add(this.lblAlgorithm);
            this.grpSearch.Controls.Add(this.cmbAlgorithm);
            this.grpSearch.Controls.Add(this.btnSingleSearch);
            this.grpSearch.Controls.Add(this.btnCompare);
            this.grpSearch.Location = new System.Drawing.Point(384, 12);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(884, 122);
            this.grpSearch.TabIndex = 1;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "2. Búsqueda";
            //
            // lblKey
            //
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(15, 33);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(88, 15);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "Clave a buscar:";
            //
            // txtKey
            //
            this.txtKey.Location = new System.Drawing.Point(120, 29);
            this.txtKey.MaxLength = 11;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(130, 23);
            this.txtKey.TabIndex = 1;
            this.txtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyDown);
            //
            // btnExistingKey
            //
            this.btnExistingKey.Location = new System.Drawing.Point(265, 26);
            this.btnExistingKey.Name = "btnExistingKey";
            this.btnExistingKey.Size = new System.Drawing.Size(150, 30);
            this.btnExistingKey.TabIndex = 2;
            this.btnExistingKey.Text = "Clave existente";
            this.btnExistingKey.UseVisualStyleBackColor = true;
            this.btnExistingKey.Click += new System.EventHandler(this.btnExistingKey_Click);
            //
            // btnMissingKey
            //
            this.btnMissingKey.Location = new System.Drawing.Point(425, 26);
            this.btnMissingKey.Name = "btnMissingKey";
            this.btnMissingKey.Size = new System.Drawing.Size(150, 30);
            this.btnMissingKey.TabIndex = 3;
            this.btnMissingKey.Text = "Clave inexistente";
            this.btnMissingKey.UseVisualStyleBackColor = true;
            this.btnMissingKey.Click += new System.EventHandler(this.btnMissingKey_Click);
            //
            // lblAlgorithm
            //
            this.lblAlgorithm.AutoSize = true;
            this.lblAlgorithm.Location = new System.Drawing.Point(15, 82);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(60, 15);
            this.lblAlgorithm.TabIndex = 4;
            this.lblAlgorithm.Text = "Algoritmo:";
            //
            // cmbAlgorithm
            //
            this.cmbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlgorithm.Location = new System.Drawing.Point(120, 78);
            this.cmbAlgorithm.Name = "cmbAlgorithm";
            this.cmbAlgorithm.Size = new System.Drawing.Size(160, 23);
            this.cmbAlgorithm.TabIndex = 5;
            //
            // btnSingleSearch
            //
            this.btnSingleSearch.Location = new System.Drawing.Point(295, 74);
            this.btnSingleSearch.Name = "btnSingleSearch";
            this.btnSingleSearch.Size = new System.Drawing.Size(180, 34);
            this.btnSingleSearch.TabIndex = 6;
            this.btnSingleSearch.Text = "Búsqueda individual";
            this.btnSingleSearch.UseVisualStyleBackColor = true;
            this.btnSingleSearch.Click += new System.EventHandler(this.btnSingleSearch_Click);
            //
            // btnCompare
            //
            this.btnCompare.Location = new System.Drawing.Point(485, 74);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(290, 34);
            this.btnCompare.TabIndex = 7;
            this.btnCompare.Text = "Búsqueda comparativa simultánea";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            //
            // grpInfo
            //
            this.grpInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInfo.Controls.Add(this.lblDataInfo);
            this.grpInfo.Location = new System.Drawing.Point(12, 142);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(1256, 72);
            this.grpInfo.TabIndex = 2;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Estado del banco de datos";
            //
            // lblDataInfo
            //
            this.lblDataInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataInfo.Location = new System.Drawing.Point(12, 22);
            this.lblDataInfo.Name = "lblDataInfo";
            this.lblDataInfo.Size = new System.Drawing.Size(1232, 44);
            this.lblDataInfo.TabIndex = 0;
            this.lblDataInfo.Text = "Aún no hay datos. Elige el tamaño N y presiona \"Generar datos\" para comenzar.";
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(12, 222);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(160, 28);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Limpiar tabla";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // lblGridHint
            //
            this.lblGridHint.AutoSize = true;
            this.lblGridHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblGridHint.Location = new System.Drawing.Point(185, 229);
            this.lblGridHint.Name = "lblGridHint";
            this.lblGridHint.Size = new System.Drawing.Size(500, 15);
            this.lblGridHint.TabIndex = 4;
            this.lblGridHint.Text = "Panel comparativo: cada búsqueda comparativa agrega 3 filas (misma clave, mismo banco de N datos).";
            //
            // dgvResults
            //
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvResults.ColumnHeadersHeight = 32;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRun,
            this.colN,
            this.colKey,
            this.colAlgorithm,
            this.colStatus,
            this.colIndex,
            this.colComparisons,
            this.colIterations,
            this.colMicroseconds,
            this.colMilliseconds,
            this.colComplexity,
            this.colDetails});
            this.dgvResults.Location = new System.Drawing.Point(12, 258);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 26;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(1256, 430);
            this.dgvResults.TabIndex = 5;
            //
            // colRun
            //
            this.colRun.HeaderText = "Ejecución";
            this.colRun.Name = "colRun";
            this.colRun.ReadOnly = true;
            this.colRun.Width = 80;
            //
            // colN
            //
            this.colN.HeaderText = "N";
            this.colN.Name = "colN";
            this.colN.ReadOnly = true;
            this.colN.Width = 80;
            //
            // colKey
            //
            this.colKey.HeaderText = "Clave";
            this.colKey.Name = "colKey";
            this.colKey.ReadOnly = true;
            this.colKey.Width = 80;
            //
            // colAlgorithm
            //
            this.colAlgorithm.HeaderText = "Algoritmo";
            this.colAlgorithm.Name = "colAlgorithm";
            this.colAlgorithm.ReadOnly = true;
            this.colAlgorithm.Width = 140;
            //
            // colStatus
            //
            this.colStatus.HeaderText = "Estado";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 105;
            //
            // colIndex
            //
            this.colIndex.HeaderText = "Índice";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            this.colIndex.ToolTipText = "Secuencial: posición en el arreglo original. Binaria: posición en el arreglo ordenado. Hash: cubeta h(k).";
            this.colIndex.Width = 80;
            //
            // colComparisons
            //
            this.colComparisons.HeaderText = "Comparaciones";
            this.colComparisons.Name = "colComparisons";
            this.colComparisons.ReadOnly = true;
            this.colComparisons.Width = 105;
            //
            // colIterations
            //
            this.colIterations.HeaderText = "Iteraciones";
            this.colIterations.Name = "colIterations";
            this.colIterations.ReadOnly = true;
            this.colIterations.Width = 85;
            //
            // colMicroseconds
            //
            this.colMicroseconds.HeaderText = "Tiempo (µs)";
            this.colMicroseconds.Name = "colMicroseconds";
            this.colMicroseconds.ReadOnly = true;
            this.colMicroseconds.ToolTipText = "Mediana de varias ejecuciones medidas con Stopwatch.";
            this.colMicroseconds.Width = 95;
            //
            // colMilliseconds
            //
            this.colMilliseconds.HeaderText = "Tiempo (ms)";
            this.colMilliseconds.Name = "colMilliseconds";
            this.colMilliseconds.ReadOnly = true;
            this.colMilliseconds.Width = 95;
            //
            // colComplexity
            //
            this.colComplexity.HeaderText = "Complejidad";
            this.colComplexity.Name = "colComplexity";
            this.colComplexity.ReadOnly = true;
            this.colComplexity.Width = 105;
            //
            // colDetails
            //
            this.colDetails.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDetails.HeaderText = "Detalle";
            this.colDetails.MinimumWidth = 180;
            this.colDetails.Name = "colDetails";
            this.colDetails.ReadOnly = true;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 700);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lblGridHint);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.grpData);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Evaluación empírica de métodos de búsqueda - Estructuras de Datos, Unidad 6";
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblSizeHint;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnExistingKey;
        private System.Windows.Forms.Button btnMissingKey;
        private System.Windows.Forms.Label lblAlgorithm;
        private System.Windows.Forms.ComboBox cmbAlgorithm;
        private System.Windows.Forms.Button btnSingleSearch;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblDataInfo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblGridHint;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRun;
        private System.Windows.Forms.DataGridViewTextBoxColumn colN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlgorithm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComparisons;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIterations;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMicroseconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMilliseconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComplexity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetails;
    }
}
