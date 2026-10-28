namespace Analizador_Léxico_PQ_DJ_EM
{
    partial class FrmAnalizador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvResultado = new System.Windows.Forms.DataGridView();
            this.dgvError = new System.Windows.Forms.DataGridView();
            this.dgvSimbolos = new System.Windows.Forms.DataGridView();
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.rtbTexto = new System.Windows.Forms.RichTextBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.lbltokens = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.cmsExportar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuExportarTodo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportarTokens = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportarErrores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportarSimbolos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportarEstadisticas = new System.Windows.Forms.ToolStripMenuItem();
            this.chkResaltado = new System.Windows.Forms.CheckBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.grpEstadisticas = new System.Windows.Forms.GroupBox();
            this.lblStatLineas = new System.Windows.Forms.Label();
            this.lblStatTokens = new System.Windows.Forms.Label();
            this.lblStatErrores = new System.Windows.Forms.Label();
            this.lblStatTiempo = new System.Windows.Forms.Label();
            this.lblStatCategorias = new System.Windows.Forms.Label();
            this.dgvEstadisticas = new System.Windows.Forms.DataGridView();
            this.tabResultados = new System.Windows.Forms.TabControl();
            this.tabPaginaTokens = new System.Windows.Forms.TabPage();
            this.tabPaginaErrores = new System.Windows.Forms.TabPage();
            this.tabPaginaSimbolos = new System.Windows.Forms.TabPage();
            this.timerResaltado = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimbolos)).BeginInit();
            this.cmsExportar.SuspendLayout();
            this.grpEstadisticas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadisticas)).BeginInit();
            this.tabResultados.SuspendLayout();
            this.tabPaginaTokens.SuspendLayout();
            this.tabPaginaErrores.SuspendLayout();
            this.tabPaginaSimbolos.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResultado
            // 
            this.dgvResultado.AllowUserToAddRows = false;
            this.dgvResultado.AllowUserToDeleteRows = false;
            this.dgvResultado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultado.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dgvResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultado.Location = new System.Drawing.Point(3, 3);
            this.dgvResultado.Name = "dgvResultado";
            this.dgvResultado.ReadOnly = true;
            this.dgvResultado.RowHeadersWidth = 51;
            this.dgvResultado.RowTemplate.Height = 24;
            this.dgvResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultado.Size = new System.Drawing.Size(949, 265);
            this.dgvResultado.TabIndex = 0;
            // 
            // dgvError
            // 
            this.dgvError.AllowUserToAddRows = false;
            this.dgvError.AllowUserToDeleteRows = false;
            this.dgvError.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvError.BackgroundColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvError.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Crimson;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvError.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvError.EnableHeadersVisualStyles = false;
            this.dgvError.Location = new System.Drawing.Point(3, 3);
            this.dgvError.Name = "dgvError";
            this.dgvError.ReadOnly = true;
            this.dgvError.RowHeadersWidth = 51;
            this.dgvError.RowTemplate.Height = 24;
            this.dgvError.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvError.Size = new System.Drawing.Size(949, 265);
            this.dgvError.TabIndex = 1;
            // 
            // dgvSimbolos
            // 
            this.dgvSimbolos.AllowUserToAddRows = false;
            this.dgvSimbolos.AllowUserToDeleteRows = false;
            this.dgvSimbolos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSimbolos.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dgvSimbolos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimbolos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSimbolos.Location = new System.Drawing.Point(3, 3);
            this.dgvSimbolos.Name = "dgvSimbolos";
            this.dgvSimbolos.ReadOnly = true;
            this.dgvSimbolos.RowHeadersWidth = 51;
            this.dgvSimbolos.RowTemplate.Height = 24;
            this.dgvSimbolos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSimbolos.Size = new System.Drawing.Size(949, 265);
            this.dgvSimbolos.TabIndex = 0;
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnAnalizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnalizar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizar.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnAnalizar.Location = new System.Drawing.Point(180, 50);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(120, 38);
            this.btnAnalizar.TabIndex = 1;
            this.btnAnalizar.Text = "▶ ANALIZAR";
            this.btnAnalizar.UseVisualStyleBackColor = false;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnLimpiar.Location = new System.Drawing.Point(310, 50);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 38);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // rtbTexto
            // 
            this.rtbTexto.AcceptsTab = true;
            this.rtbTexto.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTexto.Location = new System.Drawing.Point(30, 118);
            this.rtbTexto.Name = "rtbTexto";
            this.rtbTexto.Size = new System.Drawing.Size(550, 240);
            this.rtbTexto.TabIndex = 5;
            this.rtbTexto.Text = "";
            this.rtbTexto.TextChanged += new System.EventHandler(this.rtbTexto_TextChanged);
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.Color.White;
            this.btnCargar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnCargar.Location = new System.Drawing.Point(30, 50);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(140, 38);
            this.btnCargar.TabIndex = 0;
            this.btnCargar.Text = "📂 CARGAR ARCHIVO";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // lbltokens
            // 
            this.lbltokens.AutoSize = true;
            this.lbltokens.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbltokens.Font = new System.Drawing.Font("Century Gothic", 8.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltokens.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbltokens.Location = new System.Drawing.Point(885, 20);
            this.lbltokens.Name = "lbltokens";
            this.lbltokens.Size = new System.Drawing.Size(120, 21);
            this.lbltokens.TabIndex = 8;
            this.lbltokens.Text = "Ir a Tokens →";
            this.lbltokens.Click += new System.EventHandler(this.lbltokens_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExportar.ContextMenuStrip = this.cmsExportar;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.btnExportar.Location = new System.Drawing.Point(420, 50);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(175, 38);
            this.btnExportar.TabIndex = 3;
            this.btnExportar.Text = "💾 EXPORTAR RESULTADOS ▾";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // cmsExportar
            // 
            this.cmsExportar.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.cmsExportar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsExportar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExportarTodo,
            this.menuExportarTokens,
            this.menuExportarErrores,
            this.menuExportarSimbolos,
            this.menuExportarEstadisticas});
            this.cmsExportar.Name = "cmsExportar";
            this.cmsExportar.Size = new System.Drawing.Size(463, 144);
            // 
            // menuExportarTodo
            // 
            this.menuExportarTodo.Name = "menuExportarTodo";
            this.menuExportarTodo.Size = new System.Drawing.Size(462, 28);
            this.menuExportarTodo.Text = "Exportar Todo (Completo: CSV / TXT)";
            this.menuExportarTodo.Click += new System.EventHandler(this.menuExportarTodo_Click);
            // 
            // menuExportarTokens
            // 
            this.menuExportarTokens.Name = "menuExportarTokens";
            this.menuExportarTokens.Size = new System.Drawing.Size(462, 28);
            this.menuExportarTokens.Text = "Exportar solo Tokens (CSV / TXT)";
            this.menuExportarTokens.Click += new System.EventHandler(this.menuExportarTokens_Click);
            // 
            // menuExportarErrores
            // 
            this.menuExportarErrores.Name = "menuExportarErrores";
            this.menuExportarErrores.Size = new System.Drawing.Size(462, 28);
            this.menuExportarErrores.Text = "Exportar solo Errores Léxicos (CSV / TXT)";
            this.menuExportarErrores.Click += new System.EventHandler(this.menuExportarErrores_Click);
            // 
            // menuExportarSimbolos
            // 
            this.menuExportarSimbolos.Name = "menuExportarSimbolos";
            this.menuExportarSimbolos.Size = new System.Drawing.Size(462, 28);
            this.menuExportarSimbolos.Text = "Exportar solo Tabla de Símbolos (CSV / TXT)";
            this.menuExportarSimbolos.Click += new System.EventHandler(this.menuExportarSimbolos_Click);
            // 
            // menuExportarEstadisticas
            // 
            this.menuExportarEstadisticas.Name = "menuExportarEstadisticas";
            this.menuExportarEstadisticas.Size = new System.Drawing.Size(462, 28);
            this.menuExportarEstadisticas.Text = "Exportar solo Panel de Estadísticas (CSV / TXT)";
            this.menuExportarEstadisticas.Click += new System.EventHandler(this.menuExportarEstadisticas_Click);
            // 
            // chkResaltado
            // 
            this.chkResaltado.AutoSize = true;
            this.chkResaltado.Checked = true;
            this.chkResaltado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResaltado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkResaltado.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResaltado.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkResaltado.Location = new System.Drawing.Point(605, 59);
            this.chkResaltado.Name = "chkResaltado";
            this.chkResaltado.Size = new System.Drawing.Size(159, 25);
            this.chkResaltado.TabIndex = 4;
            this.chkResaltado.Text = "Resaltar Sintaxis";
            this.chkResaltado.UseVisualStyleBackColor = true;
            this.chkResaltado.CheckedChanged += new System.EventHandler(this.chkResaltado_CheckedChanged);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTitulo.Location = new System.Drawing.Point(26, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(301, 34);
            this.lblTitulo.TabIndex = 7;
            this.lblTitulo.Text = "ANALIZADOR LÉXICO";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.Location = new System.Drawing.Point(28, 97);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(205, 19);
            this.lblCodigo.TabIndex = 6;
            this.lblCodigo.Text = "Código fuente a analizar:";
            // 
            // grpEstadisticas
            // 
            this.grpEstadisticas.BackColor = System.Drawing.Color.Transparent;
            this.grpEstadisticas.Controls.Add(this.lblStatLineas);
            this.grpEstadisticas.Controls.Add(this.lblStatTokens);
            this.grpEstadisticas.Controls.Add(this.lblStatErrores);
            this.grpEstadisticas.Controls.Add(this.lblStatTiempo);
            this.grpEstadisticas.Controls.Add(this.lblStatCategorias);
            this.grpEstadisticas.Controls.Add(this.dgvEstadisticas);
            this.grpEstadisticas.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEstadisticas.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.grpEstadisticas.Location = new System.Drawing.Point(595, 100);
            this.grpEstadisticas.Name = "grpEstadisticas";
            this.grpEstadisticas.Size = new System.Drawing.Size(390, 258);
            this.grpEstadisticas.TabIndex = 6;
            this.grpEstadisticas.TabStop = false;
            this.grpEstadisticas.Text = "📊 Panel de Estadísticas del Análisis";
            // 
            // lblStatLineas
            // 
            this.lblStatLineas.AutoSize = true;
            this.lblStatLineas.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatLineas.Location = new System.Drawing.Point(14, 25);
            this.lblStatLineas.Name = "lblStatLineas";
            this.lblStatLineas.Size = new System.Drawing.Size(138, 21);
            this.lblStatLineas.TabIndex = 0;
            this.lblStatLineas.Text = "Total de líneas: 0";
            // 
            // lblStatTokens
            // 
            this.lblStatTokens.AutoSize = true;
            this.lblStatTokens.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatTokens.Location = new System.Drawing.Point(14, 48);
            this.lblStatTokens.Name = "lblStatTokens";
            this.lblStatTokens.Size = new System.Drawing.Size(146, 21);
            this.lblStatTokens.TabIndex = 1;
            this.lblStatTokens.Text = "Total de tokens: 0";
            // 
            // lblStatErrores
            // 
            this.lblStatErrores.AutoSize = true;
            this.lblStatErrores.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatErrores.Location = new System.Drawing.Point(195, 25);
            this.lblStatErrores.Name = "lblStatErrores";
            this.lblStatErrores.Size = new System.Drawing.Size(146, 21);
            this.lblStatErrores.TabIndex = 2;
            this.lblStatErrores.Text = "Total de errores: 0";
            // 
            // lblStatTiempo
            // 
            this.lblStatTiempo.AutoSize = true;
            this.lblStatTiempo.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatTiempo.Location = new System.Drawing.Point(195, 48);
            this.lblStatTiempo.Name = "lblStatTiempo";
            this.lblStatTiempo.Size = new System.Drawing.Size(180, 21);
            this.lblStatTiempo.TabIndex = 3;
            this.lblStatTiempo.Text = "Tiempo aprox: 0.00 ms";
            // 
            // lblStatCategorias
            // 
            this.lblStatCategorias.AutoSize = true;
            this.lblStatCategorias.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatCategorias.Location = new System.Drawing.Point(14, 73);
            this.lblStatCategorias.Name = "lblStatCategorias";
            this.lblStatCategorias.Size = new System.Drawing.Size(175, 19);
            this.lblStatCategorias.TabIndex = 4;
            this.lblStatCategorias.Text = "Tokens por categoría:";
            // 
            // dgvEstadisticas
            // 
            this.dgvEstadisticas.AllowUserToAddRows = false;
            this.dgvEstadisticas.AllowUserToDeleteRows = false;
            this.dgvEstadisticas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstadisticas.BackgroundColor = System.Drawing.Color.LightBlue;
            this.dgvEstadisticas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEstadisticas.Font = new System.Drawing.Font("Century Gothic", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvEstadisticas.Location = new System.Drawing.Point(14, 93);
            this.dgvEstadisticas.Name = "dgvEstadisticas";
            this.dgvEstadisticas.ReadOnly = true;
            this.dgvEstadisticas.RowHeadersVisible = false;
            this.dgvEstadisticas.RowHeadersWidth = 51;
            this.dgvEstadisticas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstadisticas.Size = new System.Drawing.Size(360, 150);
            this.dgvEstadisticas.TabIndex = 5;
            // 
            // tabResultados
            // 
            this.tabResultados.Controls.Add(this.tabPaginaTokens);
            this.tabResultados.Controls.Add(this.tabPaginaErrores);
            this.tabResultados.Controls.Add(this.tabPaginaSimbolos);
            this.tabResultados.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabResultados.Location = new System.Drawing.Point(30, 368);
            this.tabResultados.Name = "tabResultados";
            this.tabResultados.SelectedIndex = 0;
            this.tabResultados.Size = new System.Drawing.Size(963, 305);
            this.tabResultados.TabIndex = 7;
            // 
            // tabPaginaTokens
            // 
            this.tabPaginaTokens.Controls.Add(this.dgvResultado);
            this.tabPaginaTokens.Location = new System.Drawing.Point(4, 30);
            this.tabPaginaTokens.Name = "tabPaginaTokens";
            this.tabPaginaTokens.Padding = new System.Windows.Forms.Padding(3);
            this.tabPaginaTokens.Size = new System.Drawing.Size(955, 271);
            this.tabPaginaTokens.TabIndex = 0;
            this.tabPaginaTokens.Text = "Tokens Encontrados";
            this.tabPaginaTokens.UseVisualStyleBackColor = true;
            // 
            // tabPaginaErrores
            // 
            this.tabPaginaErrores.Controls.Add(this.dgvError);
            this.tabPaginaErrores.Location = new System.Drawing.Point(4, 30);
            this.tabPaginaErrores.Name = "tabPaginaErrores";
            this.tabPaginaErrores.Padding = new System.Windows.Forms.Padding(3);
            this.tabPaginaErrores.Size = new System.Drawing.Size(955, 271);
            this.tabPaginaErrores.TabIndex = 1;
            this.tabPaginaErrores.Text = "Errores Léxicos";
            this.tabPaginaErrores.UseVisualStyleBackColor = true;
            // 
            // tabPaginaSimbolos
            // 
            this.tabPaginaSimbolos.Controls.Add(this.dgvSimbolos);
            this.tabPaginaSimbolos.Location = new System.Drawing.Point(4, 30);
            this.tabPaginaSimbolos.Name = "tabPaginaSimbolos";
            this.tabPaginaSimbolos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPaginaSimbolos.Size = new System.Drawing.Size(955, 271);
            this.tabPaginaSimbolos.TabIndex = 2;
            this.tabPaginaSimbolos.Text = "Tabla de Símbolos";
            this.tabPaginaSimbolos.UseVisualStyleBackColor = true;
            // 
            // timerResaltado
            // 
            this.timerResaltado.Interval = 350;
            this.timerResaltado.Tick += new System.EventHandler(this.timerResaltado_Tick);
            // 
            // FrmAnalizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1018, 688);
            this.Controls.Add(this.tabResultados);
            this.Controls.Add(this.grpEstadisticas);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.chkResaltado);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.lbltokens);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.rtbTexto);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAnalizar);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmAnalizador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analizador Léxico - Windows Forms";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimbolos)).EndInit();
            this.cmsExportar.ResumeLayout(false);
            this.grpEstadisticas.ResumeLayout(false);
            this.grpEstadisticas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadisticas)).EndInit();
            this.tabResultados.ResumeLayout(false);
            this.tabPaginaTokens.ResumeLayout(false);
            this.tabPaginaErrores.ResumeLayout(false);
            this.tabPaginaSimbolos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResultado;
        private System.Windows.Forms.DataGridView dgvError;
        private System.Windows.Forms.DataGridView dgvSimbolos;
        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.RichTextBox rtbTexto;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Label lbltokens;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.ContextMenuStrip cmsExportar;
        private System.Windows.Forms.ToolStripMenuItem menuExportarTodo;
        private System.Windows.Forms.ToolStripMenuItem menuExportarTokens;
        private System.Windows.Forms.ToolStripMenuItem menuExportarErrores;
        private System.Windows.Forms.ToolStripMenuItem menuExportarSimbolos;
        private System.Windows.Forms.ToolStripMenuItem menuExportarEstadisticas;
        private System.Windows.Forms.CheckBox chkResaltado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.GroupBox grpEstadisticas;
        private System.Windows.Forms.Label lblStatLineas;
        private System.Windows.Forms.Label lblStatTokens;
        private System.Windows.Forms.Label lblStatErrores;
        private System.Windows.Forms.Label lblStatTiempo;
        private System.Windows.Forms.Label lblStatCategorias;
        private System.Windows.Forms.DataGridView dgvEstadisticas;
        private System.Windows.Forms.TabControl tabResultados;
        private System.Windows.Forms.TabPage tabPaginaTokens;
        private System.Windows.Forms.TabPage tabPaginaErrores;
        private System.Windows.Forms.TabPage tabPaginaSimbolos;
        private System.Windows.Forms.Timer timerResaltado;
    }
}