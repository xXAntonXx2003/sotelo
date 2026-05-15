using GestionEstudiantes.Data;
using GestionEstudiantes.Models;
using GestionEstudiantes.Repositories;

namespace GestionEstudiantes.UI
{
    public partial class FormPrincipal : Form
    {
        private readonly EstudianteRepository _repository;
        private DataGridView dgvEstudiantes = null!;
        private TextBox txtBuscar = null!;
        private ComboBox cmbFiltroBusqueda = null!;
        private Button btnBuscar = null!;
        private Button btnLimpiar = null!;
        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnEliminar = null!;
        private Button btnRefrescar = null!;
        private Label lblTotal = null!;
        private Label lblEstadisticas = null!;
        private Panel panelSuperior = null!;
        private Panel panelInferior = null!;
        private Panel panelLateral = null!;

        private static readonly Color ColorFondo = Color.FromArgb(244, 247, 251);
        private static readonly Color ColorPrimario = Color.FromArgb(29, 78, 216);
        private static readonly Color ColorPrimarioOscuro = Color.FromArgb(30, 64, 175);
        private static readonly Color ColorTexto = Color.FromArgb(30, 41, 59);
        private static readonly Color ColorTextoSuave = Color.FromArgb(100, 116, 139);
        private static readonly Color ColorBorde = Color.FromArgb(226, 232, 240);
        private static readonly Color ColorExito = Color.FromArgb(22, 163, 74);
        private static readonly Color ColorPeligro = Color.FromArgb(220, 38, 38);
        private static readonly Color ColorNeutral = Color.FromArgb(71, 85, 105);

        public FormPrincipal()
        {
            var context = new EstudianteDbContext();
            _repository = new EstudianteRepository(context);
            InitializeComponent();
            CargarEstudiantes();
            ActualizarEstadisticas();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            Text = "Gestion de Estudiantes";
            Size = new Size(1180, 740);
            MinimumSize = new Size(1000, 640);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = ColorFondo;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            panelSuperior = CrearHeader();
            panelLateral = CrearPanelLateral();
            panelInferior = CrearBarraEstado();
            var panelContenedor = CrearContenido();

            Controls.Add(panelContenedor);
            Controls.Add(panelLateral);
            Controls.Add(panelInferior);
            Controls.Add(panelSuperior);

            ResumeLayout(false);
        }

        private Panel CrearHeader()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 86,
                BackColor = ColorPrimario,
                Padding = new Padding(24, 14, 24, 12)
            };

            var lblTitulo = new Label
            {
                Text = "Gestion de Estudiantes",
                Dock = DockStyle.Top,
                Height = 34,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblSubtitulo = new Label
            {
                Text = "Administra registros, contacto y estadisticas academicas",
                Dock = DockStyle.Top,
                Height = 24,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(219, 234, 254),
                TextAlign = ContentAlignment.MiddleLeft
            };

            panel.Controls.Add(lblSubtitulo);
            panel.Controls.Add(lblTitulo);

            return panel;
        }

        private Panel CrearPanelLateral()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 238,
                BackColor = Color.White,
                Padding = new Padding(18)
            };

            btnAgregar = CrearBotonMenu("Agregar estudiante", ColorExito, new Point(18, 22));
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = CrearBotonMenu("Editar seleccionado", ColorPrimario, new Point(18, 78));
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBotonMenu("Eliminar seleccionado", ColorPeligro, new Point(18, 134));
            btnEliminar.Click += BtnEliminar_Click;

            btnRefrescar = CrearBotonMenu("Actualizar lista", ColorNeutral, new Point(18, 190));
            btnRefrescar.Click += BtnRefrescar_Click;

            var panelEstadisticas = new Panel
            {
                Location = new Point(18, 270),
                Size = new Size(202, 210),
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(14),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblEstadisticasTitulo = new Label
            {
                Text = "Resumen",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = ColorTexto,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblEstadisticas = new Label
            {
                Text = "Cargando...",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = ColorTextoSuave,
                TextAlign = ContentAlignment.TopLeft
            };

            panelEstadisticas.Controls.Add(lblEstadisticas);
            panelEstadisticas.Controls.Add(lblEstadisticasTitulo);

            panel.Controls.AddRange(new Control[]
            {
                btnAgregar,
                btnEditar,
                btnEliminar,
                btnRefrescar,
                panelEstadisticas
            });

            return panel;
        }

        private Panel CrearBarraEstado()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 46,
                BackColor = Color.White,
                Padding = new Padding(22, 10, 22, 10)
            };

            lblTotal = new Label
            {
                Text = "Total: 0 estudiantes",
                Dock = DockStyle.Left,
                Width = 420,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = ColorTextoSuave,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblAyuda = new Label
            {
                Text = "Doble clic sobre una fila para editar",
                Dock = DockStyle.Right,
                Width = 280,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = ColorTextoSuave,
                TextAlign = ContentAlignment.MiddleRight
            };

            panel.Controls.Add(lblAyuda);
            panel.Controls.Add(lblTotal);

            return panel;
        }

        private Panel CrearContenido()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorFondo,
                Padding = new Padding(22)
            };

            var panelBusqueda = CrearPanelBusqueda();
            dgvEstudiantes = CrearGridEstudiantes();

            panel.Controls.Add(dgvEstudiantes);
            panel.Controls.Add(panelBusqueda);

            return panel;
        }

        private Panel CrearPanelBusqueda()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.White,
                Padding = new Padding(18, 12, 18, 12)
            };

            var lblBuscar = new Label
            {
                Text = "Buscar",
                Location = new Point(18, 13),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ColorTexto,
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtBuscar = new TextBox
            {
                Location = new Point(18, 39),
                Size = new Size(300, 28),
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Escribe el texto a buscar"
            };
            txtBuscar.TextChanged += TxtBuscar_TextChanged;
            txtBuscar.KeyDown += TxtBuscar_KeyDown;

            var lblFiltro = new Label
            {
                Text = "Filtro",
                Location = new Point(334, 13),
                Size = new Size(140, 20),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ColorTexto,
                TextAlign = ContentAlignment.MiddleLeft
            };

            cmbFiltroBusqueda = new ComboBox
            {
                Location = new Point(334, 38),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F),
                FlatStyle = FlatStyle.Flat
            };
            cmbFiltroBusqueda.Items.AddRange(new object[]
            {
                "Nombre",
                "Apellido",
                "Email",
                "Telefono",
                "Edad",
                "ID",
                "Todos"
            });
            cmbFiltroBusqueda.SelectedIndex = 0;
            cmbFiltroBusqueda.SelectedIndexChanged += CmbFiltroBusqueda_SelectedIndexChanged;

            btnBuscar = CrearBotonAccion("Buscar", ColorPrimario, new Point(500, 38), new Size(104, 30));
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBotonAccion("Limpiar", ColorNeutral, new Point(614, 38), new Size(104, 30));
            btnLimpiar.Click += BtnLimpiar_Click;

            panel.Controls.AddRange(new Control[] { lblBuscar, txtBuscar, lblFiltro, cmbFiltroBusqueda, btnBuscar, btnLimpiar });

            return panel;
        }

        private DataGridView CrearGridEstudiantes()
        {
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                GridColor = ColorBorde,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                Font = new Font("Segoe UI", 9F),
                RowTemplate = { Height = 42 }
            };

            grid.ColumnHeadersDefaultCellStyle.BackColor = ColorPrimarioOscuro;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 6, 10, 6);
            grid.ColumnHeadersHeight = 42;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grid.DefaultCellStyle.Padding = new Padding(10, 6, 10, 6);
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = ColorTexto;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = ColorPrimarioOscuro;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            grid.Columns.Add(CrearColumna("Id", "ID", 56, DataGridViewAutoSizeColumnMode.None));
            grid.Columns.Add(CrearColumna("Nombre", "Nombre", 120));
            grid.Columns.Add(CrearColumna("Apellido", "Apellido", 120));
            grid.Columns.Add(CrearColumna("Edad", "Edad", 70, DataGridViewAutoSizeColumnMode.None));
            grid.Columns.Add(CrearColumna("Email", "Email", 180));
            grid.Columns.Add(CrearColumna("Telefono", "Telefono", 130));
            grid.Columns.Add(CrearColumna("FechaRegistro", "Registro", 120, format: "dd/MM/yyyy"));

            grid.CellDoubleClick += DgvEstudiantes_CellDoubleClick;

            return grid;
        }

        private static DataGridViewTextBoxColumn CrearColumna(
            string propiedad,
            string encabezado,
            float ancho,
            DataGridViewAutoSizeColumnMode modo = DataGridViewAutoSizeColumnMode.Fill,
            string? format = null)
        {
            var columna = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propiedad,
                Name = propiedad,
                HeaderText = encabezado,
                AutoSizeMode = modo,
                FillWeight = ancho,
                MinimumWidth = modo == DataGridViewAutoSizeColumnMode.None ? (int)ancho : 80,
                Width = modo == DataGridViewAutoSizeColumnMode.None ? (int)ancho : 100
            };

            if (!string.IsNullOrEmpty(format))
            {
                columna.DefaultCellStyle.Format = format;
            }

            return columna;
        }

        private static Button CrearBotonMenu(string texto, Color color, Point ubicacion)
        {
            return PrepararBoton(new Button
            {
                Text = texto,
                Location = ubicacion,
                Size = new Size(202, 44)
            }, color);
        }

        private static Button CrearBotonAccion(string texto, Color color, Point ubicacion, Size tamano)
        {
            return PrepararBoton(new Button
            {
                Text = texto,
                Location = ubicacion,
                Size = tamano
            }, color);
        }

        private static Button PrepararBoton(Button boton, Color color)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(color, 0.15F);
            boton.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(color, 0.08F);
            boton.BackColor = color;
            boton.ForeColor = Color.White;
            boton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.TextAlign = ContentAlignment.MiddleCenter;

            return boton;
        }

        private void CargarEstudiantes()
        {
            var estudiantes = _repository.ObtenerTodos();
            MostrarEstudiantes(estudiantes, $"Total: {estudiantes.Count} estudiante(s) registrado(s)");
        }

        private void MostrarEstudiantes(List<Estudiante> estudiantes, string textoTotal)
        {
            dgvEstudiantes.DataSource = null;
            dgvEstudiantes.DataSource = estudiantes;
            lblTotal.Text = textoTotal;
        }

        private void ActualizarEstadisticas()
        {
            var estudiantes = _repository.ObtenerTodos();
            int total = estudiantes.Count;

            if (total > 0)
            {
                double edadPromedio = estudiantes.Average(e => e.Edad);
                int edadMaxima = estudiantes.Max(e => e.Edad);
                int edadMinima = estudiantes.Min(e => e.Edad);

                lblEstadisticas.Text =
                    $"Total: {total} estudiantes{Environment.NewLine}{Environment.NewLine}" +
                    $"Edad promedio: {edadPromedio:F1} anos{Environment.NewLine}{Environment.NewLine}" +
                    $"Edad maxima: {edadMaxima} anos{Environment.NewLine}{Environment.NewLine}" +
                    $"Edad minima: {edadMinima} anos";
            }
            else
            {
                lblEstadisticas.Text = "No hay estudiantes registrados.";
            }
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            using var form = new FormEstudiante();

            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (_repository.AgregarEstudiante(form.Estudiante))
            {
                txtBuscar.Clear();
                CargarEstudiantes();
                ActualizarEstadisticas();
                MessageBox.Show("Estudiante agregado exitosamente.", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Error al agregar el estudiante.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            var estudiante = ObtenerEstudianteSeleccionado();

            if (estudiante == null)
            {
                MessageBox.Show("Seleccione un estudiante para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new FormEstudiante(estudiante);

            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (_repository.ActualizarEstudiante(estudiante.Id, form.Estudiante))
            {
                RefrescarListadoActual();
                ActualizarEstadisticas();
                MessageBox.Show("Estudiante actualizado exitosamente.", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Error al actualizar el estudiante.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            var estudiante = ObtenerEstudianteSeleccionado();

            if (estudiante == null)
            {
                MessageBox.Show("Seleccione un estudiante para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Desea eliminar a {estudiante.Nombre} {estudiante.Apellido}?",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            if (_repository.EliminarEstudiante(estudiante.Id))
            {
                RefrescarListadoActual();
                ActualizarEstadisticas();
                MessageBox.Show("Estudiante eliminado exitosamente.", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Error al eliminar el estudiante.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnRefrescar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarEstudiantes();
            ActualizarEstadisticas();
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            Buscar();
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarEstudiantes();
        }

        private void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            Buscar();
        }

        private void CmbFiltroBusqueda_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Buscar();
        }

        private void TxtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buscar();
                e.SuppressKeyPress = true;
            }
        }

        private void Buscar()
        {
            string termino = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(termino))
            {
                CargarEstudiantes();
                return;
            }

            string filtro = cmbFiltroBusqueda.SelectedItem?.ToString() ?? "Nombre";
            var estudiantes = _repository.Buscar(termino, filtro);
            MostrarEstudiantes(estudiantes, $"Resultados: {estudiantes.Count} estudiante(s) encontrado(s)");
        }

        private void RefrescarListadoActual()
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarEstudiantes();
            }
            else
            {
                Buscar();
            }
        }

        private Estudiante? ObtenerEstudianteSeleccionado()
        {
            if (dgvEstudiantes.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvEstudiantes.SelectedRows[0].DataBoundItem as Estudiante;
        }

        private void DgvEstudiantes_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEditar_Click(sender, e);
            }
        }
    }
}
